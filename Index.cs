using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace learnWinForms
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();

            //args.Text = username;
            args.Text = GlobalFunc.Instance.account;
        }

        private void Index_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
