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


        public Form1()
        {
            InitializeComponent();

            mediaFinder = new MediaFinder();
            string[] strArr = new string[] { @"E:\HemLaddning\Download" };
            mediaFinder.FindMediaOnComputer(strArr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in mediaFinder.lstWithFiles)
            {
                this.lstMediaFiles.Items.Add(s);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            mediaFinder = new MediaFinder();
            string[] strArr = new string[] { @"E:\HemLaddning\Download" };
            mediaFinder.FindMediaOnComputer(strArr);
            foreach (string s in mediaFinder.lstWithFiles)
            {
                this.lstMediaFiles.Items.Add(s);
            }
        }


    }
}
