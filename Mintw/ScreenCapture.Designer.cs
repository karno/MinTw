namespace Mintw
{
    partial class ScreenCapture
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
            this.captureBtn = new System.Windows.Forms.Button();
            this.moveBtn = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // captureBtn
            // 
            this.captureBtn.Location = new System.Drawing.Point(50, 10);
            this.captureBtn.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.captureBtn.Name = "captureBtn";
            this.captureBtn.Size = new System.Drawing.Size(75, 23);
            this.captureBtn.TabIndex = 0;
            this.captureBtn.Text = "Capture";
            this.captureBtn.UseVisualStyleBackColor = true;
            this.captureBtn.Click += new System.EventHandler(this.captureBtn_Click);
            // 
            // moveBtn
            // 
            this.moveBtn.AutoSize = true;
            this.moveBtn.BackColor = System.Drawing.SystemColors.Control;
            this.moveBtn.Location = new System.Drawing.Point(12, 12);
            this.moveBtn.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Padding = new System.Windows.Forms.Padding(3);
            this.moveBtn.Size = new System.Drawing.Size(38, 18);
            this.moveBtn.TabIndex = 1;
            this.moveBtn.Text = "Move";
            this.moveBtn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveBtn_MouseMove);
            this.moveBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveBtn_MouseDown);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(125, 10);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ScreenCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(525, 275);
            this.ControlBox = false;
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.moveBtn);
            this.Controls.Add(this.captureBtn);
            this.Name = "ScreenCapture";
            this.ShowInTaskbar = false;
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button captureBtn;
        private System.Windows.Forms.Label moveBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}