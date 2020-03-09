using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Native.Core.App
{
    public static class RuntimeHelper
    {
        public static Dictionary<long, DateTime> CDDic = new Dictionary<long, DateTime>();
        public static Dictionary<long, DateTime> WhiteCDDic = new Dictionary<long, DateTime>();
        public static Dictionary<long, DateTime> PMCDDic = new Dictionary<long, DateTime>();
        public static Dictionary<long, int> LimitDic = new Dictionary<long, int>();
        public static List<string> DownloadedImagesName = new List<string>();
        public static Timer ImageCleaner = new Timer();

        static RuntimeHelper()
        {
            ImageCleaner.Tick += ImageCleaner_Tick;
        }

        private static void ImageCleaner_Tick(object sender, EventArgs e)
        {
            if (RobotInfo.ImageClearMode == "All")
            {
                string[] images = Directory.GetFiles(Path.Combine(Application.StartupPath, "data", "image"));
                foreach (var item in images)
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                for (int i = DownloadedImagesName.Count - 1; i >= 0; i--)
                {
                    if (File.Exists(DownloadedImagesName[i]))
                    {
                        try
                        {
                            File.Delete(DownloadedImagesName[i]);
                        }
                        catch
                        {
                            goto IL_01;
                        }
                    }
                    DownloadedImagesName.RemoveAt(i);
                    IL_01:;
                }
            }
        }

        public static void SetTaskAtFixedTime()
        {
            DateTime now = DateTime.Now;
            DateTime oneOClock = DateTime.Today.AddHours(0);
            if (now > oneOClock)
            {
                oneOClock = oneOClock.AddDays(1.0);
            }
            int msUntilFour = (int)((oneOClock - now).TotalMilliseconds);

            var t = new System.Threading.Timer(DoAt);
            t.Change(msUntilFour, Timeout.Infinite);
        }

        private static void DoAt(object state)
        {
            LimitDic.Clear();
            SetTaskAtFixedTime();
        }


        public static void RecordLimit(long qqId)
        {
            if (LimitDic.ContainsKey(qqId))
            {
                LimitDic[qqId] += 1;
            }
            else
            {
                LimitDic.Add(qqId, 1);
            }
        }

        public static void RecordCD(long qqId, long groupId)
        {

            if (RobotInfo.WhiteGroup.Contains(groupId))
            {
                if (RobotInfo.WhiteCD > 0)
                {
                    if (WhiteCDDic.ContainsKey(qqId))
                    {
                        WhiteCDDic[qqId] = DateTime.Now.AddSeconds(RobotInfo.WhiteCD);
                    }
                    else
                    {
                        WhiteCDDic.Add(qqId, DateTime.Now.AddSeconds(RobotInfo.WhiteCD));
                    }
                }
            }
            else
            {
                if (RobotInfo.CD > 0)
                {
                    if (CDDic.ContainsKey(qqId))
                    {
                        CDDic[qqId] = DateTime.Now.AddSeconds(RobotInfo.CD);
                    }
                    else
                    {
                        CDDic.Add(qqId, DateTime.Now.AddSeconds(RobotInfo.CD));
                    }
                }
            }
        }
    }
}
