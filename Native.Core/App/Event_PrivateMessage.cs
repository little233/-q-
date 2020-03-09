using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Native.Core.App
{
    public class Event_PrivateMessage : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            if (!RobotInfo.AllowPM)
            {
                return;
            }

            if (RobotInfo.BannedUser.Contains(e.FromQQ.Id))
            {
                return;
            }

            if (e.Message.Text.StartsWith(RobotInfo.RobotName) || RobotInfo.UserHImageCmd.Contains(e.Message.Text))
            {
                string strHimageCommand = e.Message.Text.Substring(e.Message.Text.IndexOf(RobotInfo.RobotName) + RobotInfo.RobotName.Length);
                if (new Regex(RobotInfo.HImageCmd).IsMatch(strHimageCommand) || RobotInfo.UserHImageCmd.Contains(e.Message.Text))
                {
                    #region -- 次数限制 --

                    if (RobotInfo.Limit > 0 && !RobotInfo.PMNoLimit)
                    {
                        if (!RobotInfo.AdminQQ.Contains(e.FromQQ.Id) || !RobotInfo.AdminNoLimit)
                        {
                            if (RuntimeHelper.LimitDic.ContainsKey(e.FromQQ.Id))
                            {
                                if (RuntimeHelper.LimitDic[e.FromQQ.Id] >= RobotInfo.Limit)
                                {
                                    e.FromQQ.SendPrivateMessage(RobotInfo.OutOfLimitReply);
                                    return;
                                }
                            }
                        }
                    }

                    #endregion -- 次数限制 --

                    #region -- 冷却时间 --

                    if (RobotInfo.PMCD > 0 && !RobotInfo.PMNoLimit)
                    {
                        if (!RobotInfo.AdminQQ.Contains(e.FromQQ.Id) || !RobotInfo.AdminNoLimit)
                        {
                            if (RuntimeHelper.WhiteCDDic.ContainsKey(e.FromQQ.Id))
                            {
                                if (RuntimeHelper.WhiteCDDic[e.FromQQ.Id] >= DateTime.Now)
                                {
                                    e.FromQQ.SendPrivateMessage(RobotInfo.CDUnreadyReply);
                                    return;
                                }
                            }
                        }
                    }

                    #endregion -- 冷却时间 --

                    try
                    {
                        Func<object, QQMessage> SendMessage = msg => e.FromQQ.SendPrivateMessage(msg);

                        Action RecordLimit = () => RuntimeHelper.RecordLimit(e.FromQQ.Id);

                        Action RecordCD = () => RuntimeHelper.RecordCD(e.FromQQ.Id, e.FromQQ.Id);

                        Action<QQMessage> RevokeHimage = msg => msg?.RevokeHImage(RobotInfo.PMRevoke);

                        e.Message.Text.SendHimages(RobotInfo.R18, RecordLimit, RecordCD, SendMessage, RevokeHimage);

                        // 设置该属性, 表示阻塞本条消息, 该属性会在方法结束后传递给酷Q
                        e.Handler = true;
                    }
                    catch (Exception ex)
                    {
                        e.FromQQ.SendPrivateMessage(RobotInfo.ErrorReply);
                        INIHelper.WriteLog(Application.StartupPath + @"\HImageRobotError.log", ex.Message);
                    }
                }
            }
        }
    }
}