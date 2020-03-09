using System.Collections.Generic;
using System.Windows.Forms;

namespace Native.Core.App
{
    public static class RobotInfo
    {
        private static string _RobotName = "竹竹";
        private static List<long> _AdminQQ = new List<long>();
        private static List<long> _BannedGroup = new List<long>();
        private static List<long> _BannedUser = new List<long>();
        private static string _HImageCmd = @$"^[我再]?[要来來发發给給]([0-9零一壹二两贰兩三叁四肆五伍六陆陸七柒八捌九玖十拾百佰千仟万萬亿]+?)?[张張个個幅份]([Rr]-?18的?)?([\u4e00-\u9fa5_a-zA-Z0-9]+?)?([Rr]-?18的?)?的?[色瑟][图圖]$";
        private static string _HImageBeginCmd = @$"[我再]?[要来來发發给給]";
        private static string _HImageCountCmd = @$"[0-9零一壹二两贰兩三叁四肆五伍六陆陸七柒八捌九玖十拾百佰千仟万萬亿]+?";
        private static string _HImageUnitCmd = @$"[张張个個幅份]";
        private static string _HImageR18Cmd = @$"[Rr]-?18的?";
        private static string _HImageKeywordCmd = @$"[\u4e00-\u9fa5_a-zA-Z0-9]+?";
        private static string _HImageEndCmd = @$"的?[色瑟][图圖]";

        private static bool _HImageBeginCmdNull = false;
        private static bool _HImageCountCmdNull = true;
        private static bool _HImageUnitCmdNull = false;
        private static bool _HImageR18CmdNull = true;
        private static bool _HImageKeywordCmdNull = true;
        private static bool _HImageEndCmdNull = false;

        private static List<string> _UserHImageCmd = new List<string>();
        private static List<long> _WhiteGroup = new List<long>();

        private static bool _WhiteOnly = false;
        private static bool _R18 = false;
        private static bool _R18WhiteOnly = true;
        private static bool _AllowPM = true;
        private static bool _AntiShielding = true;
        private static bool _Size1200 = false;

        private static bool _Accelerate = false;
        private static string _AccelerateUrl = @"https://search.pstatic.net/common?type=origin&src=";

        private static int _CD = 0;
        private static int _Revoke = 0;
        private static int _Limit = 0;
        private static int _WhiteCD = 0;
        private static int _WhiteRevoke = 0;
        private static int _PMCD = 0;
        private static int _PMRevoke = 0;

        private static bool _AdminNoLimit = true;
        private static bool _ManageNoLimit = false;
        private static bool _PMNoLimit = true;
        private static bool _WhiteNoLimit = false;

        private static string _CDUnreadyReply = @"乖，要懂得节制哦，休息一会再冲吧 →_→";
        private static string _OutOfLimitReply = @"今天不要再冲了啦(>_<)";
        private static string _ErrorReply = @"色图服务器爆炸惹_(:3」∠)_";
        private static string _NoResult = @"没有找到符合条件的色图ㄟ( ▔, ▔ )ㄏ  ";

        private static string _LimitType = "Frequency";

        private static bool _ImageClean = false;
        private static int _ImageClearTime = 0;
        private static string _ImageClearMode = "This";

        public static string RobotName
        {
            get => _RobotName;
            set
            {
                _RobotName = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "Name", value);
            }
        }

        public static List<long> AdminQQ
        {
            get => _AdminQQ;
            set
            {
                _AdminQQ = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "Admin", string.Join(";", value));
            }
        }

        public static string HImageCmd
        {
            get => _HImageCmd;
            set
            {
                _HImageCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "Cmd", value);
            }
        }

        public static string HImageBeginCmd
        {
            get => _HImageBeginCmd;
            set
            {
                _HImageBeginCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "BeginCmd", value);
            }
        }

        public static string HImageCountCmd
        {
            get => _HImageCountCmd;
            set
            {
                _HImageCountCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "CountCmd", value);
            }
        }

        public static string HImageUnitCmd
        {
            get => _HImageUnitCmd;
            set
            {
                _HImageUnitCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "UnitCmd", value);
            }
        }

        public static string HImageR18Cmd
        {
            get => _HImageR18Cmd;
            set
            {
                _HImageR18Cmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "R18Cmd", value);
            }
        }

        public static string HImageKeywordCmd
        {
            get => _HImageKeywordCmd;
            set
            {
                _HImageKeywordCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "KeywordCmd", value);
            }
        }

        public static string HImageEndCmd
        {
            get => _HImageEndCmd;
            set
            {
                _HImageEndCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "EndCmd", value);
            }
        }


        public static bool HImageBeginCmdNull
        {
            get => _HImageBeginCmdNull;
            set
            {
                _HImageBeginCmdNull = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "BeginCmdNull", value.ToString());
            }
        }

        public static bool HImageCountCmdNull
        {
            get => _HImageCountCmdNull;
            set
            {
                _HImageCountCmdNull = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "CountCmdNull", value.ToString());
            }
        }

        public static bool HImageUnitCmdNull
        {
            get => _HImageUnitCmdNull;
            set
            {
                _HImageUnitCmdNull = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "UnitCmdNull", value.ToString());
            }
        }

        public static bool HImageR18CmdNull
        {
            get => _HImageR18CmdNull;
            set
            {
                _HImageR18CmdNull = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "R18CmdNull", value.ToString());
            }
        }

        public static bool HImageKeywordCmdNull
        {
            get => _HImageKeywordCmdNull;
            set
            {
                _HImageKeywordCmdNull = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "KeywordCmdNull", value.ToString());
            }
        }

        public static bool HImageEndCmdNull
        {
            get => _HImageEndCmdNull;
            set
            {
                _HImageEndCmdNull = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "EndCmdNull", value.ToString());
            }
        }

        public static List<string> UserHImageCmd
        {
            get => _UserHImageCmd;
            set
            {
                _UserHImageCmd = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "UserCmd", string.Join(";", value));
            }
        }

        public static List<long> WhiteGroup
        {
            get => _WhiteGroup;
            set
            {
                _WhiteGroup = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteGroup", string.Join(";", value));
            }
        }

        public static bool WhiteOnly
        {
            get => _WhiteOnly;
            set
            {
                _WhiteOnly = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteOnly", value.ToString());
            }
        }

        public static bool R18
        {
            get => _R18;
            set
            {
                _R18 = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "R18", value.ToString());
            }
        }

        public static bool R18WhiteOnly
        {
            get => _R18WhiteOnly;
            set
            {
                _R18WhiteOnly = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "R18WhiteOnly", value.ToString());
            }
        }

        public static bool AllowPM
        {
            get => _AllowPM;
            set
            {
                _AllowPM = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "AllowPM", value.ToString());
            }
        }

        public static bool AntiShielding
        {
            get => _AntiShielding;
            set
            {
                _AntiShielding = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "AntiShielding", value.ToString());
            }
        }

        public static bool Size1200
        {
            get => _Size1200;
            set
            {
                _Size1200 = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "Size1200", value.ToString());
            }
        }

        public static bool Accelerate
        {
            get => _Accelerate;
            set
            {
                _Accelerate = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "Accelerate", value.ToString());
            }
        }

        public static string AccelerateUrl
        {
            get => _AccelerateUrl;
            set
            {
                _AccelerateUrl = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "AccelerateUrl", value);
            }
        }

        public static int CD
        {
            get => _CD;
            set
            {
                _CD = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "CD", value.ToString());
            }
        }

        public static int Limit
        {
            get => _Limit;
            set
            {
                _Limit = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "Limit", value.ToString());
            }
        }

        public static int Revoke
        {
            get => _Revoke;
            set
            {
                _Revoke = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "Revoke", value.ToString());
            }
        }

        public static int WhiteCD
        {
            get => _WhiteCD;
            set
            {
                _WhiteCD = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteCD", value.ToString());
            }
        }

        public static int WhiteRevoke
        {
            get => _WhiteRevoke;
            set
            {
                _WhiteRevoke = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteRevoke", value.ToString());
            }
        }

        public static int PMCD
        {
            get => _PMCD;
            set
            {
                _PMCD = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "PMCD", value.ToString());
            }
        }

        public static int PMRevoke
        {
            get => _PMRevoke;
            set
            {
                _PMRevoke = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "PMRevoke", value.ToString());
            }
        }

        public static bool AdminNoLimit
        {
            get => _AdminNoLimit;
            set
            {
                _AdminNoLimit = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "AdminNoLimit", value.ToString());
            }
        }

        public static bool ManageNoLimit
        {
            get => _ManageNoLimit;
            set
            {
                _ManageNoLimit = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "ManageNoLimit", value.ToString());
            }
        }

        public static bool PMNoLimit
        {
            get => _PMNoLimit;
            set
            {
                _PMNoLimit = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "PMNoLimit", value.ToString());
            }
        }

        public static string CDUnreadyReply
        {
            get => _CDUnreadyReply;
            set
            {
                _CDUnreadyReply = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "CDUnreadyReply", value);
            }
        }

        public static string OutOfLimitReply
        {
            get => _OutOfLimitReply;
            set
            {
                _OutOfLimitReply = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "OutOfLimitReply", value);
            }
        }

        public static string ErrorReply
        {
            get => _ErrorReply;
            set
            {
                _ErrorReply = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "ErrorReply", value);
            }
        }
        
        public static string NoResult
        {
            get => _NoResult;
            set
            {
                _NoResult = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "NoResult", value);
            }
        }

        public static string LimitType 
        { 
            get => _LimitType;
            set
            {
                _LimitType = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "LimitType", value);
            }
        }

        public static bool WhiteNoLimit 
        {
            get => _WhiteNoLimit;
            set 
            {
                _WhiteNoLimit = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "HImage", "WhiteNoLimit", value.ToString());
            }
        }

        public static bool ImageClean
        { 
            get => _ImageClean;
            set 
            {
                _ImageClean = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "ImageClean", value.ToString());
            }
        }

        public static int ImageClearTime
        { 
            get => _ImageClearTime;
            set 
            { 
                _ImageClearTime = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "ImageClearTime", value.ToString());
            }
        }

        public static string ImageClearMode 
        {
            get => _ImageClearMode;
            set
            { 
                _ImageClearMode = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "ImageClearMode", value);
            }
        }

        public static List<long> BannedGroup
        {
            get => _BannedGroup;
            set
            {
                _BannedGroup = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "BannedGroup", string.Join(";", value));
            }
        }

        public static List<long> BannedUser
        {
            get => _BannedUser;
            set
            {
                _BannedUser = value;
                INIHelper.SetValue(Application.StartupPath + @"\config.ini", "Robot", "BannedUser", string.Join(";", value));
            }
        }
    }
}