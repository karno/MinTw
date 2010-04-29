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
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
            CredentialInformationUpdated = false;
        }

        public bool CredentialInformationUpdated { get; private set; }

        private void Setup_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Kernel.Config.UserId))
                authorizedAsLabel.Text = "[None]";
            else
                authorizedAsLabel.Text = Kernel.Config.UserId;

            mentionEnabled.Checked = Kernel.Config.MentionEnabled;
            mentionInterval.Value = Kernel.Config.MentionInverval;

            dmEnabled.Checked = Kernel.Config.DMEnabled;
            dmInterval.Value = Kernel.Config.DMInterval;

            urlCompress.Checked = Kernel.Config.UrlCompress;
            jmpId.Text = Kernel.Config.JmpId;
            jmpPass.Text = Kernel.Config.JmpPass;

            pictureUpload.Checked = Kernel.Config.PictureUpload;
            twitpicId.Text = Kernel.Config.TwitpicId;
            twitpicPass.Text = Kernel.Config.TwitpicPass;

            userIdSuggest.Checked = Kernel.Config.UserIdSuggest;
        }

        private void Setup_Shown(object sender, EventArgs e)
        {
            if (Kernel.Config.UserId == null || Kernel.Config.UserToken == null || Kernel.Config.UserSecret == null)
            {
                reauthenticateBtn_Click(this, e);
            }
        }

        private void reauthenticateBtn_Click(object sender, EventArgs e)
        {
            using (var a = new AccountSetup())
            {
                if (a.ShowDialog() == DialogResult.OK)
                {
                    Kernel.Config.UserId = a.UserName;
                    Kernel.Config.UserSecret = a.UserSecret;
                    Kernel.Config.UserToken = a.UserToken;

                    CredentialInformationUpdated = true;

                    if (String.IsNullOrEmpty(Kernel.Config.UserId))
                        authorizedAsLabel.Text = "[None]";
                    else
                        authorizedAsLabel.Text = Kernel.Config.UserId;
                }
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Setup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Kernel.Config.MentionEnabled = mentionEnabled.Checked;
            Kernel.Config.MentionInverval = (int)mentionInterval.Value;

            Kernel.Config.DMEnabled = dmEnabled.Checked;
            Kernel.Config.DMInterval = (int)dmInterval.Value;

            Kernel.Config.UrlCompress = urlCompress.Checked;
            Kernel.Config.JmpId = jmpId.Text;
            Kernel.Config.JmpPass = jmpPass.Text;

            Kernel.Config.PictureUpload = pictureUpload.Checked;
            Kernel.Config.TwitpicId = twitpicId.Text;
            Kernel.Config.TwitpicPass = twitpicPass.Text;

            Kernel.Config.UserIdSuggest = userIdSuggest.Checked;
        }

        private void notificationSetup_Click(object sender, EventArgs e)
        {
            using (var n = new Notification())
            {
                n.ShowDialog();
            }
        }
    }
}
