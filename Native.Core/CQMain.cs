using Native.Core.App;
using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace Native.Core
{
	/// <summary>
	/// 酷Q应用主入口类
	/// </summary>
	public class CQMain
	{
		/// <summary>
		/// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
		/// </summary>
		/// <param name="container">用于注册的 IOC 容器 </param>
		public static void Register(IUnityContainer unityContainer)
		{
			// 在 Json 中, 群消息的 name 字段是: 群消息处理, 因此这里注册的第一个参数也是这样填写
			unityContainer.RegisterType<IPrivateMessage, Event_PrivateMessage>("私聊消息处理");
			unityContainer.RegisterType<IGroupMessage, Event_GroupMessage>("群消息处理");
			unityContainer.RegisterType<IDiscussMessage, Event_DiscussMessage>("讨论组消息处理");
			unityContainer.RegisterType<IMenuCall, Menu_OpenWindow>("应用设置");

			RuntimeHelper.SetTaskAtFixedTime();

			#region -- 读取配置 --
			try
			{
				string strRobotName = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "Name");
				if (!string.IsNullOrWhiteSpace(strRobotName))
				{
					RobotInfo.RobotName = strRobotName;
				}

				string strAdmin = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "Admin");
				if (!string.IsNullOrEmpty(strAdmin))
				{
					RobotInfo.AdminQQ = strAdmin.Split(';').Select(long.Parse).ToList();
				}

				string strBannedGroup = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "BannedGroup");
				if (!string.IsNullOrEmpty(strBannedGroup))
				{
					RobotInfo.BannedGroup = strBannedGroup.Split(';').Select(long.Parse).ToList();
				}

				string strBannedUser = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "BannedUser");
				if (!string.IsNullOrEmpty(strBannedUser))
				{
					RobotInfo.BannedUser = strBannedUser.Split(';').Select(long.Parse).ToList();
				}

				string strHImageCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "Cmd");
				if (!string.IsNullOrEmpty(strHImageCmd))
				{
					RobotInfo.HImageCmd = @strHImageCmd;
				}

				string strBeginCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "BeginCmd");
				if (!string.IsNullOrEmpty(strBeginCmd))
				{
					RobotInfo.HImageBeginCmd = @strBeginCmd;
				}

				string strHImageCountCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "CountCmd");
				if (!string.IsNullOrEmpty(strHImageCountCmd))
				{
					RobotInfo.HImageCountCmd = @strHImageCountCmd;
				}

				string strHImageUnitCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "UnitCmd");
				if (!string.IsNullOrEmpty(strHImageUnitCmd))
				{
					RobotInfo.HImageUnitCmd = @strHImageUnitCmd;
				}

				string strHImageR18Cmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "R18Cmd");
				if (!string.IsNullOrEmpty(strHImageR18Cmd))
				{
					RobotInfo.HImageR18Cmd = @strHImageR18Cmd;
				}

				string strHImageKeywordCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "KeywordCmd");
				if (!string.IsNullOrEmpty(strHImageKeywordCmd))
				{
					RobotInfo.HImageKeywordCmd = @strHImageKeywordCmd;
				}

				string strHImageEndCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "EndCmd");
				if (!string.IsNullOrEmpty(strHImageEndCmd))
				{
					RobotInfo.HImageEndCmd = @strHImageEndCmd;
				}

				string strBeginCmdNull = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "BeginCmdNull");
				if (!string.IsNullOrEmpty(strBeginCmdNull))
				{
					RobotInfo.HImageBeginCmdNull = Convert.ToBoolean(strBeginCmdNull);
				}

				string strCountCmdNull = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "CountCmdNull");
				if (!string.IsNullOrEmpty(strCountCmdNull))
				{
					RobotInfo.HImageCountCmdNull = Convert.ToBoolean(strCountCmdNull);
				}

				string strUnitCmdNull = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "UnitCmdNull");
				if (!string.IsNullOrEmpty(strUnitCmdNull))
				{
					RobotInfo.HImageUnitCmdNull = Convert.ToBoolean(strUnitCmdNull);
				}

				string strR18CmdNull = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "R18CmdNull");
				if (!string.IsNullOrEmpty(strR18CmdNull))
				{
					RobotInfo.HImageR18CmdNull = Convert.ToBoolean(strR18CmdNull);
				}

				string strKeywordCmdNull = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "KeywordCmdNull");
				if (!string.IsNullOrEmpty(strKeywordCmdNull))
				{
					RobotInfo.HImageKeywordCmdNull = Convert.ToBoolean(strKeywordCmdNull);
				}

				string strEndCmdNull = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "EndCmdNull");
				if (!string.IsNullOrEmpty(strEndCmdNull))
				{
					RobotInfo.HImageEndCmdNull = Convert.ToBoolean(strEndCmdNull);
				}

				string strUserHImageCmd = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "UserCmd");
				if (!string.IsNullOrEmpty(strUserHImageCmd))
				{
					RobotInfo.UserHImageCmd = strUserHImageCmd.Split(';').ToList();
				}

				string strWhiteGroup = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteGroup");
				if (!string.IsNullOrEmpty(strWhiteGroup))
				{
					RobotInfo.WhiteGroup = strWhiteGroup.Split(';').Select(long.Parse).ToList();
				}

				string strWhiteOnly = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteOnly");
				if (!string.IsNullOrEmpty(strWhiteOnly))
				{
					RobotInfo.WhiteOnly = Convert.ToBoolean(strWhiteOnly);
				}

				string strR18 = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "R18");
				if (!string.IsNullOrEmpty(strR18))
				{
					RobotInfo.R18 = Convert.ToBoolean(strR18);
				}

				string strR18WhiteOnly = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "R18WhiteOnly");
				if (!string.IsNullOrEmpty(strR18WhiteOnly))
				{
					RobotInfo.R18WhiteOnly = Convert.ToBoolean(strR18WhiteOnly);
				}

				string strAllowPM = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "AllowPM");
				if (!string.IsNullOrEmpty(strAllowPM))
				{
					RobotInfo.AllowPM = Convert.ToBoolean(strAllowPM);
				}

				string strAntiShielding = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "AntiShielding");
				if (!string.IsNullOrEmpty(strAntiShielding))
				{
					RobotInfo.AntiShielding = Convert.ToBoolean(strAntiShielding);
				}

				string strSize1200 = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "Size1200");
				if (!string.IsNullOrEmpty(strSize1200))
				{
					RobotInfo.Size1200 = Convert.ToBoolean(strSize1200);
				}

				string strAccelerate = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "Accelerate");
				if (!string.IsNullOrEmpty(strAccelerate))
				{
					RobotInfo.Accelerate = Convert.ToBoolean(strAccelerate);
				}

				string strAccelerateUrl = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "AccelerateUrl");
				if (!string.IsNullOrEmpty(strAccelerateUrl))
				{
					RobotInfo.AccelerateUrl = strAccelerateUrl;
				}

				string strCD = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "CD");
				if (!string.IsNullOrEmpty(strCD))
				{
					RobotInfo.CD = Convert.ToInt32(strCD);
				}

				string strRevoke = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "Revoke");
				if (!string.IsNullOrEmpty(strRevoke))
				{
					RobotInfo.Revoke = Convert.ToInt32(strRevoke);
				}

				string strWhiteCD = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteCD");
				if (!string.IsNullOrEmpty(strWhiteCD))
				{
					RobotInfo.WhiteCD = Convert.ToInt32(strWhiteCD);
				}

				string strWhiteRevoke = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteRevoke");
				if (!string.IsNullOrEmpty(strWhiteRevoke))
				{
					RobotInfo.WhiteRevoke = Convert.ToInt32(strWhiteRevoke);
				}

				string strPMCD = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "PMCD");
				if (!string.IsNullOrEmpty(strPMCD))
				{
					RobotInfo.PMCD = Convert.ToInt32(strPMCD);
				}

				string strPMRevoke = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "PMRevoke");
				if (!string.IsNullOrEmpty(strPMRevoke))
				{
					RobotInfo.PMRevoke = Convert.ToInt32(strPMRevoke);
				}

				string strPMNoLimit = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "PMNoLimit");
				if (!string.IsNullOrEmpty(strPMNoLimit))
				{
					RobotInfo.PMNoLimit = Convert.ToBoolean(strPMNoLimit);
				}


				string strAdminNoLimit = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "AdminNoLimit");
				if (!string.IsNullOrEmpty(strAdminNoLimit))
				{
					RobotInfo.AdminNoLimit = Convert.ToBoolean(strAdminNoLimit);
				}


				string strManageNoLimit = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "ManageNoLimit");
				if (!string.IsNullOrEmpty(strManageNoLimit))
				{
					RobotInfo.ManageNoLimit = Convert.ToBoolean(strManageNoLimit);
				}

				string strLimit = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "Limit");
				if (!string.IsNullOrEmpty(strLimit))
				{
					RobotInfo.Limit = Convert.ToInt32(strLimit);
				}

				string strCDUnreadyReply = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "CDUnreadyReply");
				if (!string.IsNullOrEmpty(strCDUnreadyReply))
				{
					RobotInfo.CDUnreadyReply = strCDUnreadyReply;
				}

				string strOutOfLimitReply = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "OutOfLimitReply");
				if (!string.IsNullOrEmpty(strOutOfLimitReply))
				{
					RobotInfo.OutOfLimitReply = strOutOfLimitReply;
				}

				string strErrorReply = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "ErrorReply");
				if (!string.IsNullOrEmpty(strErrorReply))
				{
					RobotInfo.ErrorReply = strErrorReply;
				}

				string strNoResult = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "NoResult");
				if (!string.IsNullOrEmpty(strNoResult))
				{
					RobotInfo.NoResult = strNoResult;
				}

				string strLimitType = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "LimitType");
				if (!string.IsNullOrEmpty(strLimitType))
				{
					RobotInfo.LimitType = strLimitType;
				}

				string strWhiteNoLimit = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteNoLimit");
				if (!string.IsNullOrEmpty(strWhiteNoLimit))
				{
					RobotInfo.WhiteNoLimit = Convert.ToBoolean(strWhiteNoLimit);
				}

				string strImageClean = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "ImageClean");
				if (!string.IsNullOrWhiteSpace(strImageClean))
				{
					RobotInfo.ImageClean = Convert.ToBoolean(strImageClean);
				}

				string strImageClearTime = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "ImageClearTime");
				if (!string.IsNullOrWhiteSpace(strImageClearTime))
				{
					RobotInfo.ImageClearTime = Convert.ToInt32(strImageClearTime);
				}

				string strImageClearMode = INIHelper.GetValue(Application.StartupPath + @"\config.ini", "Robot", "ImageClearMode");
				if (!string.IsNullOrWhiteSpace(strImageClearMode))
				{
					RobotInfo.ImageClearMode = strImageClearMode;
				}

				if (RobotInfo.ImageClean)
				{
					RuntimeHelper.ImageCleaner.Interval = RobotInfo.ImageClearTime * 1000 * 60;
					RuntimeHelper.ImageCleaner.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("读取配置发生异常，请删除酷Q目录下的config.ini文件后重载应用。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			#endregion -- 读取配置 --
		}
    }
}
