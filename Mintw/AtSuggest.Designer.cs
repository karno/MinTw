namespace Mintw
{
    partial class AtSuggest
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
            this.usersList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // usersList
            // 
            this.usersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersList.FormattingEnabled = true;
            this.usersList.IntegralHeight = false;
            this.usersList.ItemHeight = 12;
            this.usersList.Location = new System.Drawing.Point(0, 0);
            this.usersList.Name = "usersList";
            this.usersList.Size = new System.Drawing.Size(144, 184);
            this.usersList.TabIndex = 0;
            // 
            // AtSuggest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 184);
            this.ControlBox = false;
            this.Controls.Add(this.usersList);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AtSuggest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AtSuggest_FormClosing);
            this.Load += new System.EventHandler(this.AtSuggest_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AtSuggest_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.AtSuggest_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox usersList;
    }
}