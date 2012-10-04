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

        }

        

        public void AddFileToList(string name) {

            if (this.lstMediaFiles.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setText);
                this.Invoke(d, new object[] { name });
            }
            else
            {
                this.lstMediaFiles.Text = name;
            }
            
        }

        void setText(string txt) {
            this.lstMediaFiles.Items.Add(txt);
        }

        public void UpdateStatusText(string txt)
        {
            if (this.lblStatus.InvokeRequired) {
                SetStatusTextCallback d = new SetStatusTextCallback(updateStatus);
                this.Invoke(d, new object[] { txt });
            }
            else
            {
                this.lblStatus.Text = txt;
            }

        }
        void updateStatus(string txt) {
            this.lblStatus.Text = txt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.lstMediaFiles.Items.Clear();
            this.btnStop.Enabled = true;
            string[] strArr = new string[] { @"E:\HemLaddning\Download" };
            mediaFinder.StartSearch();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mediaFinder.EndSearch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mediaFinder.EndSearch();
            this.btnStop.Enabled = false;
            lblStatus.Text = "Search stopped";
        }


    }
}
