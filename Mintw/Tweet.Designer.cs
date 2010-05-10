namespace Mintw
{
    partial class Tweet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tweet));
            this.tweetText = new System.Windows.Forms.TextBox();
            this.menuAttachOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuUploadSS = new System.Windows.Forms.ToolStripMenuItem();
            this.attachImage = new System.Windows.Forms.Button();
            this.tweetButton = new System.Windows.Forms.Button();
            this.attachOption = new System.Windows.Forms.Button();
            this.autoShortenURLCheck = new System.Windows.Forms.CheckBox();
            this.shotenSelectedUrlButton = new System.Windows.Forms.Button();
            this.menuAttachOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // tweetText
            // 
            resources.ApplyResources(this.tweetText, "tweetText");
            this.tweetText.HideSelection = false;
            this.tweetText.Name = "tweetText";
            this.tweetText.TextChanged += new System.EventHandler(this.tweetText_TextChanged);
            this.tweetText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tweetText_KeyDown);
            this.tweetText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tweetText_KeyChange);
            this.tweetText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tweetText_MouseChange);
            this.tweetText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tweetText_MouseChange);
            this.tweetText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tweetText_MouseChange);
            this.tweetText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tweetText_PreviewKeyDown);
            // 
            // menuAttachOption
            // 
            this.menuAttachOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUploadSS});
            this.menuAttachOption.Name = "menuAttachOption";
            resources.ApplyResources(this.menuAttachOption, "menuAttachOption");
            // 
            // menuUploadSS
            // 
            this.menuUploadSS.Name = "menuUploadSS";
            resources.ApplyResources(this.menuUploadSS, "menuUploadSS");
            this.menuUploadSS.Click += new System.EventHandler(this.menuUploadSS_Click);
            // 
            // attachImage
            // 
            resources.ApplyResources(this.attachImage, "attachImage");
            this.attachImage.Name = "attachImage";
            this.attachImage.UseVisualStyleBackColor = true;
            this.attachImage.Click += new System.EventHandler(this.attachImage_Click);
            // 
            // tweetButton
            // 
            resources.ApplyResources(this.tweetButton, "tweetButton");
            this.tweetButton.Name = "tweetButton";
            this.tweetButton.UseVisualStyleBackColor = true;
            this.tweetButton.Click += new System.EventHandler(this.tweetButton_Click);
            // 
            // attachOption
            // 
            resources.ApplyResources(this.attachOption, "attachOption");
            this.attachOption.Image = global::Mintw.Properties.Resources.bullet_down;
            this.attachOption.Name = "attachOption";
            this.attachOption.UseVisualStyleBackColor = true;
            this.attachOption.Click += new System.EventHandler(this.attachOption_Click);
            // 
            // autoShortenURLCheck
            // 
            resources.ApplyResources(this.autoShortenURLCheck, "autoShortenURLCheck");
            this.autoShortenURLCheck.Name = "autoShortenURLCheck";
            this.autoShortenURLCheck.UseVisualStyleBackColor = true;
            // 
            // shotenSelectedUrlButton
            // 
            resources.ApplyResources(this.shotenSelectedUrlButton, "shotenSelectedUrlButton");
            this.shotenSelectedUrlButton.Name = "shotenSelectedUrlButton";
            this.shotenSelectedUrlButton.UseVisualStyleBackColor = true;
            this.shotenSelectedUrlButton.Click += new System.EventHandler(this.shotenSelectedUrlButton_Click);
            // 
            // Tweet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.attachImage);
            this.Controls.Add(this.autoShortenURLCheck);
            this.Controls.Add(this.shotenSelectedUrlButton);
            this.Controls.Add(this.tweetButton);
            this.Controls.Add(this.attachOption);
            this.Controls.Add(this.tweetText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tweet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tweet_FormClosing);
            this.Shown += new System.EventHandler(this.Tweet_Shown);
            this.VisibleChanged += new System.EventHandler(this.Tweet_VisibleChanged);
            this.menuAttachOption.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tweetText;
        private System.Windows.Forms.ContextMenuStrip menuAttachOption;
        private System.Windows.Forms.ToolStripMenuItem menuUploadSS;
        private System.Windows.Forms.Button attachOption;
        private System.Windows.Forms.Button attachImage;
        private System.Windows.Forms.Button tweetButton;
        private System.Windows.Forms.CheckBox autoShortenURLCheck;
        private System.Windows.Forms.Button shotenSelectedUrlButton;
    }
}