namespace Mintw
{
    partial class Tray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tray));
            this.notifier = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuOpenTwitter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPost = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApiInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRateLimitInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApiRefreshDate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.trayContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifier
            // 
            resources.ApplyResources(this.notifier, "notifier");
            this.notifier.ContextMenuStrip = this.trayContext;
            this.notifier.BalloonTipClicked += new System.EventHandler(this.notifier_BalloonTipClicked);
            this.notifier.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifier_MouseClick);
            // 
            // trayContext
            // 
            this.trayContext.AccessibleDescription = null;
            this.trayContext.AccessibleName = null;
            resources.ApplyResources(this.trayContext, "trayContext");
            this.trayContext.BackgroundImage = null;
            this.trayContext.Font = null;
            this.trayContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenTwitter,
            this.menuPost,
            this.toolStripMenuItem2,
            this.menuCheck,
            this.menuSetting,
            this.menuApiInfo,
            this.toolStripMenuItem1,
            this.menuAbout,
            this.menuExit});
            this.trayContext.Name = "trayContext";
            // 
            // menuOpenTwitter
            // 
            this.menuOpenTwitter.AccessibleDescription = null;
            this.menuOpenTwitter.AccessibleName = null;
            resources.ApplyResources(this.menuOpenTwitter, "menuOpenTwitter");
            this.menuOpenTwitter.BackgroundImage = null;
            this.menuOpenTwitter.Name = "menuOpenTwitter";
            this.menuOpenTwitter.ShortcutKeyDisplayString = null;
            this.menuOpenTwitter.Click += new System.EventHandler(this.menuOpenTwitter_Click);
            // 
            // menuPost
            // 
            this.menuPost.AccessibleDescription = null;
            this.menuPost.AccessibleName = null;
            resources.ApplyResources(this.menuPost, "menuPost");
            this.menuPost.BackgroundImage = null;
            this.menuPost.Name = "menuPost";
            this.menuPost.ShortcutKeyDisplayString = null;
            this.menuPost.Click += new System.EventHandler(this.menuPost_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.AccessibleDescription = null;
            this.toolStripMenuItem2.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // menuCheck
            // 
            this.menuCheck.AccessibleDescription = null;
            this.menuCheck.AccessibleName = null;
            resources.ApplyResources(this.menuCheck, "menuCheck");
            this.menuCheck.BackgroundImage = null;
            this.menuCheck.Name = "menuCheck";
            this.menuCheck.ShortcutKeyDisplayString = null;
            this.menuCheck.Click += new System.EventHandler(this.menuCheck_Click);
            // 
            // menuSetting
            // 
            this.menuSetting.AccessibleDescription = null;
            this.menuSetting.AccessibleName = null;
            resources.ApplyResources(this.menuSetting, "menuSetting");
            this.menuSetting.BackgroundImage = null;
            this.menuSetting.Name = "menuSetting";
            this.menuSetting.ShortcutKeyDisplayString = null;
            this.menuSetting.Click += new System.EventHandler(this.menuSetting_Click);
            // 
            // menuApiInfo
            // 
            this.menuApiInfo.AccessibleDescription = null;
            this.menuApiInfo.AccessibleName = null;
            resources.ApplyResources(this.menuApiInfo, "menuApiInfo");
            this.menuApiInfo.BackgroundImage = null;
            this.menuApiInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRateLimitInfo,
            this.menuApiRefreshDate});
            this.menuApiInfo.Name = "menuApiInfo";
            this.menuApiInfo.ShortcutKeyDisplayString = null;
            this.menuApiInfo.DropDownOpening += new System.EventHandler(this.menuApiInfo_DropDownOpening);
            // 
            // menuRateLimitInfo
            // 
            this.menuRateLimitInfo.AccessibleDescription = null;
            this.menuRateLimitInfo.AccessibleName = null;
            resources.ApplyResources(this.menuRateLimitInfo, "menuRateLimitInfo");
            this.menuRateLimitInfo.BackgroundImage = null;
            this.menuRateLimitInfo.Name = "menuRateLimitInfo";
            this.menuRateLimitInfo.ShortcutKeyDisplayString = null;
            // 
            // menuApiRefreshDate
            // 
            this.menuApiRefreshDate.AccessibleDescription = null;
            this.menuApiRefreshDate.AccessibleName = null;
            resources.ApplyResources(this.menuApiRefreshDate, "menuApiRefreshDate");
            this.menuApiRefreshDate.BackgroundImage = null;
            this.menuApiRefreshDate.Name = "menuApiRefreshDate";
            this.menuApiRefreshDate.ShortcutKeyDisplayString = null;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = null;
            this.toolStripMenuItem1.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // menuAbout
            // 
            this.menuAbout.AccessibleDescription = null;
            this.menuAbout.AccessibleName = null;
            resources.ApplyResources(this.menuAbout, "menuAbout");
            this.menuAbout.BackgroundImage = null;
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.ShortcutKeyDisplayString = null;
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // menuExit
            // 
            this.menuExit.AccessibleDescription = null;
            this.menuExit.AccessibleName = null;
            resources.ApplyResources(this.menuExit, "menuExit");
            this.menuExit.BackgroundImage = null;
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeyDisplayString = null;
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // Tray
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Font = null;
            this.Icon = null;
            this.Name = "Tray";
            this.trayContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifier;
        private System.Windows.Forms.ContextMenuStrip trayContext;
        private System.Windows.Forms.ToolStripMenuItem menuPost;
        private System.Windows.Forms.ToolStripMenuItem menuCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuSetting;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuOpenTwitter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStripMenuItem menuApiInfo;
        private System.Windows.Forms.ToolStripMenuItem menuRateLimitInfo;
        private System.Windows.Forms.ToolStripMenuItem menuApiRefreshDate;
    }
}