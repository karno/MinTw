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
    public partial class ScreenCapture : Form
    {
        public ScreenCapture()
        {
            InitializeComponent();
        }

        Point initPosition = new Point();
        Point initOffset = new Point();
        private void moveBtn_MouseDown(object sender, MouseEventArgs e)
        {
            initOffset = e.Location;
            initPosition = this.PointToScreen(initOffset);
        }

        private void moveBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                var newLoc = this.PointToScreen(e.Location);
                this.Location = new Point(
                    newLoc.X - initOffset.X,
                    newLoc.Y - initOffset.Y);
            }
        }

        public Rectangle ClipedBounds { get; private set; }
        private void captureBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ClipedBounds = this.Bounds;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
