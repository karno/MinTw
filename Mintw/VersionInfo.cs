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
    public partial class VersionInfo : Form
    {
        public VersionInfo()
        {
            InitializeComponent();
        }

        private void VersionInfo_Load(object sender, EventArgs e)
        {
            versionString.Text = Define.Version;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://starwing.net");
            }
            catch { }
        }

        private void VersionInfo_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }
    }
}
