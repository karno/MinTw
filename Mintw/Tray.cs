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
    public partial class Tray : Form
    {
        private Icon defaultIcon = Properties.Resources.MinTw;

        private Icon[] animations = new[]{
            Properties.Resources.loading_1,
            Properties.Resources.loading_2,
            Properties.Resources.loading_3,
            Properties.Resources.loading_4,
            Properties.Resources.loading_5,
            Properties.Resources.loading_6,
            Properties.Resources.loading_7,
            Properties.Resources.loading_8
        };

        private int iconCtor = 0;

        private System.Threading.Timer animator = null;

        public Tray()
        {
            InitializeComponent();

            if (Kernel.Config.UserId == null || Kernel.Config.UserToken == null || Kernel.Config.UserSecret == null)
            {
                menuSetting_Click(null, null);
            }
            else
            {
                Accessor_OnAccessStateChanged(true);
                Kernel.Accessor.UpdateFollowingsList();
                Accessor_OnAccessStateChanged(false);
            }


            Kernel.Accessor.OnAccessStateChanged += new Action<bool>(Accessor_OnAccessStateChanged);
            Kernel.Accessor.OnReceivedNew += new Action<IEnumerable<Std.Tweak.TwitterStatusBase>, bool>(Accessor_OnReceivedNew);
            Kernel.Accessor.OnThrownException += new Action<Exception>(Accessor_OnThrownException);
            Kernel.Accessor.Init();
        }

        void Accessor_OnAccessStateChanged(bool obj)
        {
            if (obj && animator == null)
            {
                iconCtor = 0;
                this.animator = new System.Threading.Timer(
                    UpdateAnimation,
                    null,
                    200,
                    200);
                this.notifier.Icon = animations[0];
            }
            else if (!obj && animator != null)
            {
                animator.Dispose();
                animator = null;
                this.notifier.Icon = this.defaultIcon;
            }
        }

        void UpdateAnimation(object obj)
        {
            iconCtor = ++iconCtor % animations.Length;
            this.notifier.Icon = animations[iconCtor];
            Application.DoEvents();
        }

        void Accessor_OnThrownException(Exception obj)
        {
            this.notifier.ShowBalloonTip(
                0,
                Lang.i18n.OnHandledError,
                obj.Message,
                ToolTipIcon.Error);
        }

        void Accessor_OnReceivedNew(IEnumerable<Std.Tweak.TwitterStatusBase> arg1, bool arg2)
        {
            StringBuilder trimed = new StringBuilder();
            foreach (var s in arg1.Take(Kernel.Config.StatusDigestCount))
            {
                try
                {
                    var trimedText = s.Text.Length > Kernel.Config.TextLength ?
                        s.Text.Substring(0, Kernel.Config.TextLength - 3) + "..." : s.Text;
                    trimed.AppendFormat(Kernel.Config.DigestFormat + Environment.NewLine,
                        s.User.ScreenName, s.User.Name, trimedText,
                        s.CreatedAt.ToString("G"));
                }
                catch (Exception e)
                {
                    this.Accessor_OnThrownException(e);
                }
            }
            if (trimed.Length == 0)
                return;
            this.notifier.ShowBalloonTip(
                0,
                String.Format(arg2 ? Lang.i18n.DMReceived : Lang.i18n.Received, arg1.Count()),
                trimed.ToString(),
                ToolTipIcon.Info);
        }

        private void menuPost_Click(object sender, EventArgs e)
        {
            Kernel.TweetWindow.Show();
            Kernel.TweetWindow.Visible = true;
        }

        private void menuCheck_Click(object sender, EventArgs e)
        {
            Kernel.Accessor.Check(true);
        }

        private void menuOpenTwitter_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://twitter.com/");
            }
            catch { }
        }

        private void menuSetting_Click(object sender, EventArgs e)
        {
            this.notifier.Visible = false;
            using (var set = new Setup())
            {
                set.ShowDialog();
                if (set.CredentialInformationUpdated)
                {
                    Kernel.Accessor.UpdateFollowingsList();
                    Kernel.Config.PrevReceivedDmId = 0;
                    Kernel.Config.PrevReceivedMentionId = 0;
                }
            }
            this.notifier.Visible = true;
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.notifier.Visible = false;
            Application.Exit();
        }

        private void notifier_BalloonTipClicked(object sender, EventArgs e)
        {
            menuOpenTwitter_Click(null, null);
        }

        private void notifier_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                menuPost_Click(null, null);

        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            using (var about = new VersionInfo())
            {
                about.ShowDialog();
            }
        }

        private void menuApiInfo_DropDownOpening(object sender, EventArgs e)
        {
            menuRateLimitInfo.Text = Kernel.Accessor.api.RateLimitRemaining.ToString() + "/" + Kernel.Accessor.api.RateLimitMax;
            menuApiRefreshDate.Text = "Reset: " + Kernel.Accessor.api.RateLimitReset.ToString("MM/dd HH:mm:ss");
        }
    }
}
