using Native.Sdk.Cqp;
using Native.Sdk.Cqp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Native.Core.App.DataHelper;

namespace Native.Core.App
{
    public static class HImage
    {
        public static void SendHimages(this string command, bool fAllowR18, Action RecordLimit, Action RecordCD, Func<object, QQMessage> SendMessage, Action<QQMessage> RevokeHimage)
        {
            string strGetHttpResponse;

            if (RobotInfo.UserHImageCmd.Contains(command))
            {
                strGetHttpResponse = $@"https://api.lolicon.app/setu/?{ (RobotInfo.Size1200 ? "size1200=true" : "") }";
            }
            else
            {
                //分割请求接口所需的参数
                long lImgCount = 1;
                string keyword = "";
                string size1200 = "";
                string strR18 = "0";

                #region -- R18 --
                Regex rxR18 = new Regex(RobotInfo.HImageR18Cmd);
                foreach (Match mchR18 in rxR18.Matches(command))
                {
                    strR18 = "1";
                    command = command.Replace(mchR18.Value, "");  //无论是否允许R18都现将命令中的R18移除, 避免和数量混淆
                }
                if (!fAllowR18)  //如果不允许R18
                {
                    strR18 = "0";
                }
                #endregion -- R18 --

                #region -- 色图数量 -- ;
                string strCount = command.GetRege(RobotInfo.HImageBeginCmd, RobotInfo.HImageCountCmd, RobotInfo.HImageUnitCmd);

                if (!long.TryParse(strCount, out lImgCount) && !string.IsNullOrEmpty(strCount))
                {
                    lImgCount = strCount.Chinese2Num();
                }

                if (string.IsNullOrEmpty(strCount))
                {
                    lImgCount = 1;
                }

                if (lImgCount == 0)
                {
                    return;
                }
                if (lImgCount > 10)
                {
                    lImgCount = 10;
                }

                #endregion -- 色图数量 -- 

                #region -- 关键词 --
                string strKeyword = command.GetRege(RobotInfo.HImageUnitCmd, RobotInfo.HImageKeywordCmd, RobotInfo.HImageEndCmd);

                if (!string.IsNullOrWhiteSpace(strKeyword))
                {
                    if (strKeyword.EndsWith("的"))
                    {
                        strKeyword = strKeyword.Substring(0, strKeyword.Length - 1);
                    }
                    keyword = "&keyword=" + strKeyword;
                }
                #endregion -- 关键词 --

                if (RobotInfo.Size1200)
                {
                    size1200 = "&size1200=true";
                }

                strGetHttpResponse = $@"https://api.lolicon.app/setu/?num={lImgCount}&r18={strR18}{keyword}{size1200}";
            }

            string resultValue = DataHelper.GetHttpResponse(strGetHttpResponse);

            JObject jo = (JObject)JsonConvert.DeserializeObject(resultValue);
            JToken jt = jo["data"];

            if (jo["code"].ToString() == "1")
            {
                SendMessage( RobotInfo.NoResult );
                return;
            }

            IEnumerable<ImageUrl> enumImg = jt.Select(i => new ImageUrl(i["p"].ToString(), i["pid"].ToString(), RobotInfo.Accelerate ? RobotInfo.AccelerateUrl + i["url"].ToString() : i["url"].ToString()));

            if (enumImg == null)
            {
                return;
            }

            StringBuilder sbAddress = new StringBuilder();
            foreach (var item in enumImg)
            {
                sbAddress.AppendLine(@"https://www.pixiv.net/artworks/" + item.ID + $" (p{item.P})");
            }

            if (string.IsNullOrEmpty(sbAddress.ToString()))
            {
                return;
            }

            QQMessage msgUrl = SendMessage(sbAddress.ToString());

            if (msgUrl.IsSuccess && RobotInfo.LimitType == "Frequency")
            {
                RecordLimit?.Invoke();
            }

            RecordCD?.Invoke();

            foreach (var pair in enumImg)
            {
                string imgPath = pair.ID + "_" + pair.P + ".png";
                if (!File.Exists(imgPath) || new FileInfo(imgPath).Length == 0)
                {
                    string strImageSafeName = Path.Combine(Application.StartupPath, "data", "image", imgPath);
                    Image img = Image.FromStream(WebRequest.Create(pair.URL).GetResponse().GetResponseStream());
                    img.Save(strImageSafeName);
                    img.Dispose();
                    RuntimeHelper.DownloadedImagesName.Add(strImageSafeName);
                }

                QQMessage msgImg;
                if (RobotInfo.AntiShielding)
                {
                    Bitmap temp = new Bitmap(Path.Combine(Application.StartupPath, "data", "image", imgPath));
                    Bitmap bmp = new Bitmap(temp);
                    temp.Dispose();

                    bmp.AntiShielding();

                    string antiPath = pair.ID + "_" + pair.P + "_AntiShielding" + ".png";
                    string strImageSafeName = Path.Combine(Application.StartupPath, "data", "image", antiPath);
                    bmp.Save(strImageSafeName);

                    msgImg = SendMessage(CQApi.CQCode_Image(antiPath));

                    if (msgImg.IsSuccess && RobotInfo.LimitType == "Count")
                    {
                        RecordLimit?.Invoke();
                    }

                    if (RobotInfo.LimitType == "Frequency")
                    {
                        RecordLimit?.Invoke();
                    }

                    try
                    {
                        File.Delete(strImageSafeName);
                    }
                    catch
                    {
                        RuntimeHelper.DownloadedImagesName.Add(strImageSafeName);
                    }
                }
                else
                {
                    msgImg = SendMessage(CQApi.CQCode_Image(imgPath));
                }

                RevokeHimage?.Invoke(msgImg);
            }
        }

        public static void RevokeHImage(this QQMessage message, int delay)
        {
            if (message.IsSuccess)
            {
                if (delay > 0)
                {
                    new Task(() =>
                    {
                        try
                        {
                            Thread.Sleep(delay * 1000);
                            message.RemoveMessage();
                        }
                        catch (Exception ex)
                        {
                        }
                    }).Start();
                }
            }
        }
    }
}
