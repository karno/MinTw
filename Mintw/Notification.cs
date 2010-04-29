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
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            statusDigestCount.Value = Kernel.Config.StatusDigestCount;
            textLength.Value = Kernel.Config.TextLength;
            digestFormat.Text = Kernel.Config.DigestFormat;
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            Kernel.Config.StatusDigestCount = (int)statusDigestCount.Value;
            Kernel.Config.TextLength = (int)textLength.Value;
            Kernel.Config.DigestFormat = digestFormat.Text;
        }
    }
}
