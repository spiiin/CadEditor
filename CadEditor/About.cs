using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = (LinkLabel)sender;
            link.LinkVisited = true;
            System.Diagnostics.Process.Start(link.Text);
        }

        private void lbMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lbMail.LinkVisited = true;
            System.Diagnostics.Process.Start("mailto:sanya.boyko@gmail.com");
        }
    }
}
