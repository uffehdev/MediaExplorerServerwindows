using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaExplorerServer
{
    public partial class Form1 : Form
    {
        MediaFinder mediaFinder;

        delegate void SetTextCallback(string text);
        delegate void SetStatusTextCallback(string txt);

        public Form1()
        {
            InitializeComponent();

            mediaFinder = new MediaFinder();
            mediaFinder.adder = AddFileToList;
            mediaFinder.statusUpt = UpdateStatusText;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //lstMediaFiles.Columns[0].Width = lstMediaFiles.Width;
        }

        public void AddFileToList(string name) {
            if (this.lstMediaFiles.InvokeRequired) {
                SetTextCallback d = new SetTextCallback(setText);
                this.Invoke(d, new object[] { name });
            } else {
                this.lstMediaFiles.Text = name;
            }
        }

        void setText(string txt) {
            this.lstMediaFiles.Items.Add(txt);
        }

        public void UpdateStatusText(string txt) {
            if (this.lblStatus.InvokeRequired) {
                SetStatusTextCallback d = new SetStatusTextCallback(updateStatus);
                this.Invoke(d, new object[] { txt });
            } else {
                this.lblStatus.Text = txt;
            }
        }

        void updateStatus(string txt) {
            this.lblStatus.Text = txt;
        }

        private void searchTB_Click(object sender, EventArgs e)
        {
            this.lstMediaFiles.Items.Clear();
            this.stopTB.Enabled = true;
            mediaFinder.StartSearch();
        }

        private void stopTB_Click(object sender, EventArgs e)
        {
            mediaFinder.EndSearch();
            this.stopTB.Enabled = false;
            lblStatus.Text = "Search stopped";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mediaFinder.EndSearch();
        }

        private void lstMediaFiles_Click(object sender, EventArgs e)
        {
            string qwe = this.lstMediaFiles.Items[lstMediaFiles.SelectedIndices[0]].Text;
            System.Diagnostics.Process.Start(qwe);
        }

       


    }
}
