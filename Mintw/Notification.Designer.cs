namespace Mintw
{
    partial class Notification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notification));
            this.label1 = new System.Windows.Forms.Label();
            this.statusDigestCount = new System.Windows.Forms.NumericUpDown();
            this.textLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.digestFormat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.statusDigestCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLength)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // statusDigestCount
            // 
            this.statusDigestCount.AccessibleDescription = null;
            this.statusDigestCount.AccessibleName = null;
            resources.ApplyResources(this.statusDigestCount, "statusDigestCount");
            this.statusDigestCount.Font = null;
            this.statusDigestCount.Name = "statusDigestCount";
            this.statusDigestCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textLength
            // 
            this.textLength.AccessibleDescription = null;
            this.textLength.AccessibleName = null;
            resources.ApplyResources(this.textLength, "textLength");
            this.textLength.Font = null;
            this.textLength.Maximum = new decimal(new int[] {
            140,
            0,
            0,
            0});
            this.textLength.Name = "textLength";
            this.textLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // digestFormat
            // 
            this.digestFormat.AccessibleDescription = null;
            this.digestFormat.AccessibleName = null;
            resources.ApplyResources(this.digestFormat, "digestFormat");
            this.digestFormat.BackgroundImage = null;
            this.digestFormat.Font = null;
            this.digestFormat.Name = "digestFormat";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Name = "label4";
            // 
            // okBtn
            // 
            this.okBtn.AccessibleDescription = null;
            this.okBtn.AccessibleName = null;
            resources.ApplyResources(this.okBtn, "okBtn");
            this.okBtn.BackgroundImage = null;
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Font = null;
            this.okBtn.Name = "okBtn";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // Notification
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.ControlBox = false;
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.digestFormat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusDigestCount);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Notification";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Notification_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notification_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.statusDigestCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown statusDigestCount;
        private System.Windows.Forms.NumericUpDown textLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox digestFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okBtn;
    }
}