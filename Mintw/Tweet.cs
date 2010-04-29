using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Mintw
{
    public partial class Tweet : Form
    {
        public Tweet()
        {
            InitializeComponent();
        }

        private void menuUploadSS_Click(object sender, EventArgs e)
        {
            using (var sc = new ScreenCapture())
            {
                if (sc.ShowDialog() == DialogResult.OK)
                {
                    Rectangle desktopBound = sc.ClipedBounds;
                    using (var img = new Bitmap(desktopBound.Width, desktopBound.Height))
                    {
                        using (var g = Graphics.FromImage(img))
                        {
                            g.CopyFromScreen(desktopBound.Location, new Point(0, 0), desktopBound.Size);
                        }
                        var tmpfn = Path.GetTempFileName();
                        try
                        {
                            img.Save(tmpfn, System.Drawing.Imaging.ImageFormat.Png);
                            using (var upload = new ImageUploader(tmpfn))
                            {
                                upload.ShowDialog();
                                tweetText.SelectedText = upload.UrlString;
                            }
                        }
                        finally
                        {
                            File.Delete(tmpfn);
                        }
                    }
                }
            }
        }

        private void Tweet_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Size = Kernel.Config.PostWindowSize;
                autoShortenURLCheck.Checked = Kernel.Config.AutoShorten;
                UpdateCommandState();
            }
            else
            {
                Kernel.Config.PostWindowSize = this.Size;
                Kernel.Config.AutoShorten = autoShortenURLCheck.Checked;
                tweetText.Clear();
            }
        }

        private void Tweet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void tweetText_TextChanged(object sender, EventArgs e)
        {
            tweetButton.Enabled = tweetText.Text.Length > 0;
            Process(tweetText.Text, tweetText.SelectionStart);
        }

        private void tweetText_KeyChange(object sender, KeyEventArgs e)
        {
            UpdateCommandState();
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                tweetButton_Click(null, null);
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                if (autoShortenURLCheck.Checked && Kernel.Config.UrlCompress)
                    CompressAllSuggestions();
            }
        }

        private void tweetText_MouseChange(object sender, MouseEventArgs e)
        {
            UpdateCommandState();
        }

        private void UpdateCommandState()
        {
            if (Kernel.Config.UrlCompress)
            {
                shotenSelectedUrlButton.Visible = tweetText.SelectionLength > 0 && !autoShortenURLCheck.Checked;
                shotenSelectedUrlButton.Enabled = shotenSelectedUrlButton.Visible;
                autoShortenURLCheck.Visible = !shotenSelectedUrlButton.Visible;
                autoShortenURLCheck.Enabled = autoShortenURLCheck.Visible;
            }
            else
            {
                shotenSelectedUrlButton.Visible = false;
                autoShortenURLCheck.Visible = false;
            }
            attachImage.Enabled = Kernel.Config.PictureUpload;
            attachOption.Enabled = Kernel.Config.PictureUpload;
        }

        private void tweetButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                if (Kernel.Config.UrlCompress && autoShortenURLCheck.Checked)
                {
                    tweetText.Text = Define.URLRegex.Replace(tweetText.Text, URLReplacer);
                }
                Kernel.Accessor.Tweet(tweetText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    Lang.i18n.UpdateError + Environment.NewLine +
                    ex.Message, Lang.i18n.UpdateErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.Enabled = true;
            }
            this.Hide();
        }
    
        private void shotenSelectedUrlButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tweetText.SelectedText))
            {
                string target = tweetText.SelectedText;
                if (!Define.URLRegex.IsMatch(target))
                    if (MessageBox.Show(String.Format(Lang.i18n.URLCheckNotify, target), Lang.i18n.URLCheckNotifyTitle,
                         MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        return;
                try
                {
                    tweetText.Enabled = false;
                    string repl;
                    if (Kernel.JmpUrlCompressor.TryCompress(target, out repl))
                        tweetText.SelectedText = repl;
                }
                finally
                {
                    tweetText.Enabled = true;
                    tweetText.Focus();
                }
            }
        }

        private void CompressAllSuggestions()
        {
            try
            {
                tweetText.Enabled = false;
                if (Kernel.Config.UrlCompress && autoShortenURLCheck.Checked)
                {
                    int ss = tweetText.SelectionStart;
                    tweetText.Text = Define.URLRegex.Replace(tweetText.Text, URLReplacer);
                    tweetText.SelectionStart = ss;
                }
            }
            finally
            {
                tweetText.Enabled = true;
                tweetText.Focus();
            }
        }

        private string URLReplacer(Match m)
        {
            if (!Kernel.JmpUrlCompressor.IsCompressed(m.Value))
            {
                string compressed;
                try
                {
                    System.Diagnostics.Debug.WriteLine("replace:" + m.Value);
                    if (Kernel.JmpUrlCompressor.TryCompress(m.Value, out compressed))
                        return compressed;
                }
                catch { }
            }
            return m.Value;
        }

        private void attachOption_Click(object sender, EventArgs e)
        {
            menuAttachOption.Show(attachOption, 0, attachOption.Height);
        }

        private void attachImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Supported|*.gif;*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK &&
                    File.Exists(ofd.FileName))
                {
                    using (var upload = new ImageUploader(ofd.FileName))
                    {
                        upload.ShowDialog();
                        tweetText.AppendText(" " + upload.UrlString);
                    }
                }
            }
        }

        private void tweetText_KeyDown(object sender, KeyEventArgs e)
        {
            OverridedKeyAction(sender, e);
            tweetText_KeyChange(sender, e);
        }

        private void tweetText_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OverridedPreKeyAction(sender, e);
        }

        #region Integrated suggestion

        AtSuggest asg = null;
        int targetStart = -1;
        private void Process(string text, int selectedStart)
        {
            if (asg == null)
            {
                if (selectedStart > text.Length || selectedStart < 1)
                    return;
                if (text[selectedStart - 1] == '@')
                {
                    targetStart = selectedStart;
                    //Start suggest
                    asg = new AtSuggest(this.tweetText, this.tweetText.PointToScreen(this.tweetText.GetPositionFromCharIndex(selectedStart)));
                    this.AddOwnedForm(asg);
                    asg.Show();
                    this.tweetText.Focus();
                }
            }
            else
            {
                if (text.Length < targetStart || text[targetStart - 1] != '@')
                {
                    asg.SuggestClose();
                    asg = null;
                }
                else if (selectedStart < targetStart)
                {
                    asg.SuggestClose();
                    asg = null;
                }
                else
                {
                    var substr = text.Substring(targetStart);
                    int cutidx = 0;
                    while ((cutidx = substr.IndexOf(' ')) > -1)
                        substr = substr.Substring(0, cutidx);
                    asg.UpdateSearchTargets(substr);
                }
            }
        }

        private void OverridedPreKeyAction(object sender, PreviewKeyDownEventArgs e)
        {
            if (asg == null) return;
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void OverridedKeyAction(object sender, KeyEventArgs e)
        {
            if (asg == null) return;
            string near = null;
            if (e.KeyCode == Keys.Up)
            {
                asg.MoveSelectionIndex(true);
            }
            else if (e.KeyCode == Keys.Down)
            {
                asg.MoveSelectionIndex(false);
            }
            else if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space) && (near = asg.GetNearestCandidate()) != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }

                var substr = tweetText.Text.Substring(targetStart);
                int cutidx = 0;
                while ((cutidx = substr.IndexOf(' ')) > -1)
                    substr = substr.Substring(0, cutidx);
                tweetText.SelectionStart = targetStart;
                tweetText.SelectionLength = substr.Length;
                tweetText.SelectedText = near;
                tweetText.SelectionStart = targetStart + near.Length;

                asg.SuggestClose();
                asg = null;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (e.KeyCode != Keys.Space)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }

                asg.SuggestClose();
                asg = null;
            }
        }

        #endregion
    }
}
