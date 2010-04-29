using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mintw
{
    public partial class AccountSetup : Form
    {
        public string UserToken { get; private set; }
        public string UserSecret { get; private set; }
        public string UserName { get; private set; }

        private string token;

        MintwOAuth oauth;
        public AccountSetup()
        {
            InitializeComponent();
            token = null;
            oauth = new MintwOAuth();
        }

        public void MoveStep(int stepCode)
        {
            Step1Panel.Visible = stepCode == 0;
            Step2Panel.Visible = stepCode == 1;
            Step3Panel.Visible = stepCode == 2;
            SuccessPanel.Visible = stepCode == 3;
            FailurePanel.Visible = stepCode == -1;
            switch (stepCode)
            {
                case 2:
                    closeBtn.Enabled = false;
                    break;
                case 3:
                    closeBtn.Enabled = true;
                    closeBtn.DialogResult = DialogResult.OK;
                    closeBtn.Text = "OK";
                    break;
                default:
                    closeBtn.Enabled = true;
                    closeBtn.DialogResult = DialogResult.Cancel;
                    closeBtn.Text = "Cancel";
                    break;
            }
            Refresh();
        }

        private void AccountSetup_Load(object sender, EventArgs e)
        {
            MoveStep(0);
        }

        private void openBrowserBtn_Click(object sender, EventArgs e)
        {
            var url = oauth.GetProviderAuthUrl(out token);
            try
            {
                System.Diagnostics.Process.Start(url.OriginalString);
            }
            catch { }
            MoveStep(1);
        }

        private void pinCheckBtn_Click(object sender, EventArgs e)
        {
            MoveStep(2);
            //Pin check
            Application.DoEvents();
            string un;
            try
            {
                if (oauth.GetAccessToken(token, pinCodeText.Text.Trim(), out un))
                {
                    UserName = un;
                    UserToken = oauth.Token;
                    UserSecret = oauth.Secret;
                    id.Text = un;
                    MoveStep(3);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "PIN code error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MoveStep(-1);
        }

        private void BackToFirstStepLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoveStep(0);
        }

    }
}
