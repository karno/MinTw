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
    public partial class AtSuggest : Form
    {
        public string GetNearestCandidate()
        {
            if (usersList.SelectedIndex < 0)
                return null;
            else
                return (string)usersList.Items[usersList.SelectedIndex];
        }

        TextBox parent;
        public AtSuggest(TextBox owner, Point location)
        {
            InitializeComponent();
            parent = owner;
            location.Y += 20;
            this.Location = location;
        }

        private void AtSuggest_Load(object sender, EventArgs e)
        {
            var names = from u in Kernel.Followings
                        select u.ScreenName;
            usersList.Items.AddRange(names.ToArray());
            usersList.Sorted = true;
        }

        public void UpdateSearchTargets(string inputted)
        {
            int selected = -1;
            for (int i = 0; i < usersList.Items.Count; i++)
            {
                if (((string)usersList.Items[i]).StartsWith(inputted, StringComparison.CurrentCultureIgnoreCase))
                {
                    selected = i;
                    break;
                }
            }
            usersList.SelectedIndex = selected;
        }

        private void AtSuggest_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            parent.Focus();
        }

        private void AtSuggest_KeyDown(object sender, KeyEventArgs e)
        {
            parent.Focus();
        }

        bool notClose = true;
        public void SuggestClose()
        {
            notClose = false;
            this.Close();
        }

        private void AtSuggest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (notClose)
            {
                e.Cancel = true;
                parent.Focus();
            }
        }

        public void MoveSelectionIndex(bool up)
        {
            if (usersList.SelectedIndex >= 0)
            {
                if (up && usersList.SelectedIndex > 0)
                    usersList.SelectedIndex--;
                else if (!up && usersList.SelectedIndex < usersList.Items.Count - 1)
                    usersList.SelectedIndex++;
            }
        }
    }
}
