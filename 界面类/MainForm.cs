using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTTP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Height = 325;
            this.Width = 666;
        }

        private void 选择_云包系统_CheckedChanged(object sender, EventArgs e)
        {
            if (选择_云包系统.Checked )
            {
                选择_抓包系统.Checked = false;
                this.Height = 610;
                this.Width = 666;
            }
            else
            {
                this.Height = 325;
                this.Width = 666;
            }
        }

        private void 选择_抓包系统_CheckedChanged(object sender, EventArgs e)
        {

            if (选择_抓包系统.Checked)
            {
                选择_云包系统.Checked = false;
                this.Height = 325;
                this.Width = 905;
            }
            else
            {
                this.Height = 325;
                this.Width = 666;
            }
        }

    }

}
