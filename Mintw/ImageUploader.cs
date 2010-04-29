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
    public partial class ImageUploader : Form
    {
        public string UrlString { get; private set; }

        private string targetPath;

        public ImageUploader(string targetPath)
        {
            InitializeComponent();
            this.targetPath = targetPath;
        }

        bool invoking = true;
        private void ImageUploader_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            var inv = new Action(Upload);
            inv.BeginInvoke(
                (iar) =>
                {
                    ((Action)iar.AsyncState).EndInvoke(iar);
                    invoking = false;
                    this.Invoke(new Action(this.Close));
                }, inv);
        }

        private void Upload()
        {
            UrlString = Kernel.TwitPic.UploadImage(targetPath, Kernel.Config.TwitpicId, Kernel.Config.TwitpicPass);
        }

        private void ImageUploader_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (invoking && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
