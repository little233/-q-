using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

namespace Native.Core.App
{
    public class Menu_OpenWindow : IMenuCall
    {
        FrmAppSetting frmAppSetting = null;
        public void MenuCall(object sender, CQMenuCallEventArgs e)
        {
            if (this.frmAppSetting == null)
            {
                this.frmAppSetting = new FrmAppSetting();
                this.frmAppSetting.Closing += FrmAppSetting_Closing;
                this.frmAppSetting.Show();	// 显示窗体
            }
            else
            {
                this.frmAppSetting.Activate();	// 将窗体调制到前台激活
            }
        }

        private void FrmAppSetting_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 对变量置 null, 因为被关闭的窗口无法重复显示
            this.frmAppSetting = null;
        }
    }
}
