using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Native.Core.App
{
    public class Event_DiscussMessage : IDiscussMessage
    {
        public void DiscussMessage(object sender, CQDiscussMessageEventArgs e)
        {
            if (RobotInfo.BannedGroup.Contains(e.FromDiscuss.Id))
            {
                return;
            }

            if (RobotInfo.BannedUser.Contains(e.FromQQ.Id))
            {
                return;
            }

            if (RobotInfo.WhiteOnly && !RobotInfo.WhiteGroup.Contains(e.FromDiscuss.Id))
            {
                return;
            }

            // 获取 At 某人对象
            CQCode cqat = e.FromQQ.CQCode_At();

            if (e.Message.Text.StartsWith(RobotInfo.RobotName) || RobotInfo.UserHImageCmd.Contains(e.Message.Text))
            {
                string strHimageCommand = e.Message.Text.Substring(e.Message.Text.IndexOf(RobotInfo.RobotName) + RobotInfo.RobotName.Length);
                if (new Regex(RobotInfo.HImageCmd).IsMatch(strHimageCommand) || RobotInfo.UserHImageCmd.Contains(e.Message.Text))
                {
                    #region -- 次数限制 --

                    if (RobotInfo.Limit > 0 && (!RobotInfo.WhiteGroup.Contains(e.FromDiscuss.Id) || !RobotInfo.WhiteNoLimit))
                    {
                        if (!RobotInfo.AdminQQ.Contains(e.FromQQ.Id) || !RobotInfo.AdminNoLimit)
                        {
                            if (e.FromQQ.GetGroupMemberInfo(e.FromDiscuss.Id).MemberType == Sdk.Cqp.Enum.QQGroupMemberType.Member || !RobotInfo.ManageNoLimit)
                            {
                                if (RuntimeHelper.LimitDic.ContainsKey(e.FromQQ.Id))
                                {
                                    if (RuntimeHelper.LimitDic[e.FromQQ.Id] >= RobotInfo.Limit)
                                    {
                                        e.FromDiscuss.SendDiscussMessage(cqat, RobotInfo.OutOfLimitReply);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    #endregion -- 次数限制 --

                    #region -- 冷却时间 --

                    if (!RobotInfo.AdminQQ.Contains(e.FromQQ.Id) || !RobotInfo.AdminNoLimit)
                    {
                        if (e.FromQQ.GetGroupMemberInfo(e.FromDiscuss.Id).MemberType == Sdk.Cqp.Enum.QQGroupMemberType.Member || !RobotInfo.ManageNoLimit)
                        {
                            if (RobotInfo.WhiteGroup.Contains(e.FromDiscuss.Id))
                            {
                                if (RobotInfo.WhiteCD > 0)
                                {
                                    if (RuntimeHelper.WhiteCDDic.ContainsKey(e.FromQQ.Id))
                                    {
                                        if (RuntimeHelper.WhiteCDDic[e.FromQQ.Id] >= DateTime.Now)
                                        {
                                            e.FromDiscuss.SendDiscussMessage(cqat, RobotInfo.CDUnreadyReply);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (RobotInfo.CD > 0)
                                {
                                    if (RuntimeHelper.CDDic.ContainsKey(e.FromQQ.Id))
                                    {
                                        if (RuntimeHelper.CDDic[e.FromQQ.Id] >= DateTime.Now)
                                        {
                                            e.FromDiscuss.SendDiscussMessage(cqat, RobotInfo.CDUnreadyReply);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion -- 冷却时间 --

                    try
                    {
                        Func<object, QQMessage> SendMessage = msg => e.FromDiscuss.SendDiscussMessage(msg);

                        Action RecordLimit = () => RuntimeHelper.RecordLimit(e.FromQQ.Id);

                        Action RecordCD = () => RuntimeHelper.RecordCD(e.FromQQ.Id, e.FromDiscuss.Id);

                        Action<QQMessage> RevokeHimage = msg => msg?.RevokeHImage(RobotInfo.WhiteGroup.Contains(e.FromDiscuss.Id) ? RobotInfo.WhiteRevoke : RobotInfo.Revoke);

                        strHimageCommand.SendHimages(RobotInfo.R18 && (!RobotInfo.R18WhiteOnly || RobotInfo.WhiteGroup.Contains(e.FromDiscuss.Id)), RecordLimit, RecordCD, SendMessage, RevokeHimage);

                        // 设置该属性, 表示阻塞本条消息, 该属性会在方法结束后传递给酷Q
                        e.Handler = true;
                    }
                    catch (Exception ex)
                    {
                        e.FromDiscuss.SendDiscussMessage(cqat, RobotInfo.ErrorReply);
                        INIHelper.WriteLog(Application.StartupPath + @"\HImageRobotError.log", ex.Message);
                    }
                }
            }
        }
    }
}
