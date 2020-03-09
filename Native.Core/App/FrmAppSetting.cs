using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace Native.Core.App
{
    public partial class FrmAppSetting : Form
    {
        public FrmAppSetting()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txbRobotName.Text = RobotInfo.RobotName;
            chkImageCleaner.Checked = RobotInfo.ImageClean;
            txbClearTime.Text = RobotInfo.ImageClearTime.ToString();
            if (RobotInfo.ImageClearMode == rdoClearAllImage.Tag.ToString())
            {
                rdoClearAllImage.Checked = true;
            }
            else
            {
                rdoClearThisImage.Checked = true;
            }

            foreach (var item in RobotInfo.AdminQQ)
            {
                lstAdmins.Items.Add(item.ToString());
            }
            foreach (var item in RobotInfo.BannedGroup)
            {
                lstBannedGroup.Items.Add(item.ToString());
            }
            foreach (var item in RobotInfo.BannedUser)
            {
                lstBannedUser.Items.Add(item.ToString());
            }

            txbHImageCmd.Text = @RobotInfo.HImageCmd;
            txbHImageBegin.Text = @RobotInfo.HImageBeginCmd;
            txbHImageCount.Text = @RobotInfo.HImageCountCmd;
            txbHImageUnit.Text = @RobotInfo.HImageUnitCmd;
            txbHImageR18.Text = @RobotInfo.HImageR18Cmd;
            txbHImageKeyword.Text = @RobotInfo.HImageKeywordCmd;
            txbHImageEnd.Text = @RobotInfo.HImageEndCmd;

            chkHImageBeginNull.Checked = RobotInfo.HImageBeginCmdNull;
            chkHImageCountNull.Checked = RobotInfo.HImageCountCmdNull;
            chkHImageUnitNull.Checked = RobotInfo.HImageUnitCmdNull;
            chkHImageR18Null.Checked = RobotInfo.HImageR18CmdNull;
            chkHImageKeywordNull.Checked = RobotInfo.HImageKeywordCmdNull;
            chkHImageEndNull.Checked = RobotInfo.HImageEndCmdNull;

            foreach (var item in RobotInfo.UserHImageCmd)
            {
                lstUserCmd.Items.Add(item.ToString());
            }

            foreach (var item in RobotInfo.WhiteGroup)
            {
                lstWhiteGroup.Items.Add(item.ToString());
            }

            chkWhiteOnly.Checked = RobotInfo.WhiteOnly;
            chkR18.Checked = RobotInfo.R18;
            chkR18WhiteOnly.Checked = RobotInfo.R18WhiteOnly;
            chkPM.Checked = RobotInfo.AllowPM;
            chkAntiShielding.Checked = RobotInfo.AntiShielding;
            chkSize1200.Checked = RobotInfo.Size1200;

            chkDownloadAccelerate.Checked = RobotInfo.Accelerate;
            txbDownloadAccelerateUrl.Text = RobotInfo.AccelerateUrl;

            txbLimit.Text = RobotInfo.Limit.ToString();
            chkPMNoLimit.Checked = RobotInfo.PMNoLimit;
            chkAdminNoLimit.Checked = RobotInfo.AdminNoLimit;
            chkManageNoLimit.Checked = RobotInfo.ManageNoLimit;
            chkWhiteNoLimit.Checked = RobotInfo.WhiteNoLimit;

            txbCD.Text = RobotInfo.CD.ToString();
            txbRevoke.Text = RobotInfo.Revoke.ToString();
            txbWhiteCD.Text = RobotInfo.WhiteCD.ToString();
            txbWhiteRevoke.Text = RobotInfo.WhiteRevoke.ToString();
            txbPMCD.Text = RobotInfo.PMCD.ToString();
            txbPMRevoke.Text = RobotInfo.PMRevoke.ToString();

            txbCDUnreadyReply.Text = RobotInfo.CDUnreadyReply;
            txbOutOfLimitReply.Text = RobotInfo.OutOfLimitReply;
            txbErrorReply.Text = RobotInfo.ErrorReply;
            txbNoResult.Text = RobotInfo.NoResult;

            if (RobotInfo.LimitType == "Count")
            {
                rodLimitCount.Checked = true;
            }
            else
            {
                rdoLimitFrequency.Checked = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            RobotInfo.RobotName = txbRobotName.Text.Trim();
            RobotInfo.ImageClean = chkImageCleaner.Checked;
            int ClearTime = string.IsNullOrEmpty(txbClearTime.Text) ? 1440 : Convert.ToInt32(txbClearTime.Text);
            RobotInfo.ImageClearTime = ClearTime == 0 ? 1 : ClearTime;
            RobotInfo.ImageClearMode = (rdoClearThisImage.Checked ? rdoClearThisImage : rdoClearAllImage).Tag.ToString();

            if ( RobotInfo.ImageClean )
            {
                RuntimeHelper.ImageCleaner.Interval = RobotInfo.ImageClearTime * 1000 * 60;
                if (!RuntimeHelper.ImageCleaner.Enabled)
                {
                    RuntimeHelper.ImageCleaner.Start();
                }
            }
            else
            {
                RuntimeHelper.ImageCleaner.Stop();
            }

            List<long> tempAdminQQ = new List<long>();
            foreach (ListViewItem item in lstAdmins.Items)
            {
                tempAdminQQ.Add(Convert.ToInt64(item.SubItems[0].Text));
            }
            RobotInfo.AdminQQ = tempAdminQQ;

            List<long> tempBannedGroup = new List<long>();
            foreach (ListViewItem item in lstBannedGroup.Items)
            {
                tempBannedGroup.Add(Convert.ToInt64(item.SubItems[0].Text));
            }
            RobotInfo.BannedGroup = tempBannedGroup;

            List<long> tempBannedUser = new List<long>();
            foreach (ListViewItem item in lstBannedUser.Items)
            {
                tempBannedUser.Add(Convert.ToInt64(item.SubItems[0].Text));
            }
            RobotInfo.BannedUser = tempBannedUser;

            RobotInfo.HImageCmd = @txbHImageCmd.Text;

            RobotInfo.HImageBeginCmd = @txbHImageBegin.Text;
            RobotInfo.HImageCountCmd = @txbHImageCount.Text;
            RobotInfo.HImageUnitCmd = @txbHImageUnit.Text;
            RobotInfo.HImageR18Cmd = @txbHImageR18.Text;
            RobotInfo.HImageKeywordCmd = @txbHImageKeyword.Text;
            RobotInfo.HImageEndCmd = @txbHImageEnd.Text;

            RobotInfo.HImageBeginCmdNull = chkHImageBeginNull.Checked;
            RobotInfo.HImageCountCmdNull = chkHImageCountNull.Checked;
            RobotInfo.HImageUnitCmdNull = chkHImageUnitNull.Checked;
            RobotInfo.HImageR18CmdNull = chkHImageR18Null.Checked;
            RobotInfo.HImageKeywordCmdNull = chkHImageKeywordNull.Checked;
            RobotInfo.HImageEndCmdNull = chkHImageEndNull.Checked;

            List<string> tempUserHImageCmd = new List<string>();
            foreach (ListViewItem item in lstUserCmd.Items)
            {
                tempUserHImageCmd.Add(item.SubItems[0].Text);
            }
            RobotInfo.UserHImageCmd = tempUserHImageCmd;

            List<long> tempWhiteGroup = new List<long>();
            foreach (ListViewItem item in lstWhiteGroup.Items)
            {
                tempWhiteGroup.Add(Convert.ToInt64(item.SubItems[0].Text));
            }
            RobotInfo.WhiteGroup = tempWhiteGroup;

            RobotInfo.WhiteOnly = chkWhiteOnly.Checked;
            RobotInfo.R18 = chkR18.Checked;
            RobotInfo.R18WhiteOnly = chkR18WhiteOnly.Checked;
            RobotInfo.AllowPM = chkPM.Checked;
            RobotInfo.AntiShielding = chkAntiShielding.Checked;
            RobotInfo.Size1200 = chkSize1200.Checked;

            RobotInfo.Accelerate = chkDownloadAccelerate.Checked;
            RobotInfo.AccelerateUrl = txbDownloadAccelerateUrl.Text;

            RobotInfo.Limit = string.IsNullOrEmpty(txbLimit.Text) ? 0 : Convert.ToInt32(txbLimit.Text);
            RobotInfo.PMNoLimit = chkPMNoLimit.Checked;
            RobotInfo.AdminNoLimit = chkAdminNoLimit.Checked;
            RobotInfo.ManageNoLimit = chkManageNoLimit.Checked;

            RobotInfo.CD = string.IsNullOrEmpty(txbCD.Text) ? 0 : Convert.ToInt32(txbCD.Text);
            RobotInfo.Revoke = string.IsNullOrEmpty(txbRevoke.Text) ? 0 : Convert.ToInt32(txbRevoke.Text);
            RobotInfo.WhiteCD = string.IsNullOrEmpty(txbWhiteCD.Text) ? 0 : Convert.ToInt32(txbWhiteCD.Text);
            RobotInfo.WhiteRevoke = string.IsNullOrEmpty(txbWhiteRevoke.Text) ? 0 : Convert.ToInt32(txbWhiteRevoke.Text);
            RobotInfo.PMCD = string.IsNullOrEmpty(txbPMCD.Text) ? 0 : Convert.ToInt32(txbPMCD.Text);
            RobotInfo.PMRevoke = string.IsNullOrEmpty(txbPMRevoke.Text) ? 0 : Convert.ToInt32(txbPMRevoke.Text);

            RobotInfo.CDUnreadyReply = txbCDUnreadyReply.Text;
            RobotInfo.OutOfLimitReply = txbOutOfLimitReply.Text;
            RobotInfo.ErrorReply = txbErrorReply.Text;
            RobotInfo.NoResult = txbNoResult.Text;

            RobotInfo.LimitType = (rdoLimitFrequency.Checked ? rdoLimitFrequency : rdoLimitFrequency).Tag.ToString();
            RobotInfo.WhiteNoLimit = chkWhiteNoLimit.Checked;

            this.Close();
        }

        private void txbHImageEnd_TextChanged(object sender, EventArgs e)
        {
            AddStringToCmd();
        }

        private void AddStringToCmd()
        {
            string Begin, Count, Unit, R18, Keyword, End;

            Begin = txbHImageBegin.Text;
            if (chkHImageBeginNull.Checked)
            {
                Begin = @$"({Begin})?";
            }
            Count = txbHImageCount.Text;
            if (chkHImageCountNull.Checked)
            {
                Count = @$"({Count})?";
            }
            Unit = txbHImageUnit.Text;
            if (chkHImageUnitNull.Checked)
            {
                Unit = @$"({Unit})?";
            }
            R18 = txbHImageR18.Text;
            if (chkHImageR18Null.Checked)
            {
                R18 = @$"({R18})?";
            }
            Keyword = @txbHImageKeyword.Text;
            if (chkHImageKeywordNull.Checked)
            {
                Keyword = @$"({Keyword})?";
            }
            End = txbHImageEnd.Text;
            if (chkHImageEndNull.Checked)
            {
                End = @$"({End})?";
            }

            txbHImageCmd.Text = @$"^{Begin}{Count}{Unit}{R18}{Keyword}{R18}{End}$";
        }

        private void chkEnableHImage_CheckedChanged(object sender, EventArgs e)
        {
            pnlEnabelHImage.Enabled = chkEnableHImage.Checked;
        }

        private void btnAddUserHImageCmd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserHImageCmd.Text))
            {
                foreach (ListViewItem item in lstUserCmd.Items)
                {
                    foreach (ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text == txbAddAdmin.Text)
                        {
                            return;
                        }
                    }
                }
                lstUserCmd.Items.Add(txbUserHImageCmd.Text);
            }
        }

        private void btnAddToWhiteGroup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAddToWhiteGroup.Text))
            {
                foreach (ListViewItem item in lstWhiteGroup.Items)
                {
                    foreach (ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text == txbAddAdmin.Text)
                        {
                            return;
                        }
                    }
                }
                lstWhiteGroup.Items.Add(txbAddToWhiteGroup.Text);
            }
        }

        private void txbUserHImageCmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void lnkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chkHImageBeginNull.Checked = false;
            chkHImageCountNull.Checked = true;
            chkHImageUnitNull.Checked = false;
            chkHImageR18Null.Checked = true;
            chkHImageKeywordNull.Checked = true;
            chkHImageEndNull.Checked = false;

            txbHImageBegin.Text = @"[我再]?[要来來发發给給]";
            txbHImageCount.Text = @"[0-9零一壹二两贰兩三叁四肆五伍六陆陸七柒八捌九玖十拾百佰千仟万萬亿]+?";
            txbHImageUnit.Text = @"[张張个個幅份]";
            txbHImageR18.Text = @"[Rr]-?18的?";
            txbHImageKeyword.Text = @"[\u4e00-\u9fa5_a-zA-Z0-9]+?";
            txbHImageEnd.Text = @"的?[色瑟][图圖]";
            txbHImageCmd.Text = @"^[我再]?[要来來发發给給]([0-9零一壹二两贰兩三叁四肆五伍六陆陸七柒八捌九玖十拾百佰千仟万萬亿]+?)?[张張个個幅份]([Rr]-?18的?)?([\u4e00-\u9fa5_a-zA-Z0-9]+?)?([Rr]-?18的?)?的?[色瑟][图圖]$";
        }

        private void chkDownloadAccelerate_CheckedChanged(object sender, EventArgs e)
        {
            txbDownloadAccelerateUrl.Enabled = chkDownloadAccelerate.Checked;
        }

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAddAdmin.Text))
            {
                foreach (ListViewItem item in lstAdmins.Items)
                {
                    foreach (ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text == txbAddAdmin.Text)
                        {
                            return;
                        }
                    }
                }
                lstAdmins.Items.Add(txbAddAdmin.Text);
            }
        }

        private void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            if (lstAdmins.SelectedItems.Count > 0)
            {
                lstAdmins.Items.Remove(lstAdmins.SelectedItems[0]);
            }
        }

        private void btnRemoveUserHImageCmd_Click(object sender, EventArgs e)
        {
            if (lstUserCmd.SelectedItems.Count > 0)
            {
                lstUserCmd.Items.Remove(lstUserCmd.SelectedItems[0]);
            }
        }

        private void btnRemoveWhiteGroup_Click(object sender, EventArgs e)
        {
            if (lstWhiteGroup.SelectedItems.Count > 0)
            {
                lstWhiteGroup.Items.Remove(lstWhiteGroup.SelectedItems[0]);
            }
        }

        private void chkHImageBeginNull_CheckedChanged(object sender, EventArgs e)
        {
            AddStringToCmd();
        }

        private void chkImageCleaner_CheckedChanged(object sender, EventArgs e)
        {
            pnlImageCleaner.Enabled = chkImageCleaner.Checked;
        }

        private void btnAddBanGroup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbBanGroup.Text))
            {
                foreach (ListViewItem item in lstBannedGroup.Items)
                {
                    foreach (ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text == txbBanGroup.Text)
                        {
                            return;
                        }
                    }
                }
                lstBannedGroup.Items.Add(txbBanGroup.Text);
            }
        }

        private void btnRemoveBanGroup_Click(object sender, EventArgs e)
        {
            if (lstBannedGroup.SelectedItems.Count > 0)
            {
                lstBannedGroup.Items.Remove(lstBannedGroup.SelectedItems[0]);
            }
        }

        private void btnAddBanUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbBanUser.Text))
            {
                foreach (ListViewItem item in lstBannedUser.Items)
                {
                    foreach (ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text == txbBanUser.Text)
                        {
                            return;
                        }
                    }
                }
                lstBannedUser.Items.Add(txbBanUser.Text);
            }
        }

        private void btnRemoveBanUser_Click(object sender, EventArgs e)
        {
            if (lstBannedUser.SelectedItems.Count > 0)
            {
                lstBannedUser.Items.Remove(lstBannedUser.SelectedItems[0]);
            }
        }
    }
}
