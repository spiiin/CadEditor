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
    public partial class FormStructuresName : Form
    {
        public FormStructuresName()
        {
            InitializeComponent();
        }

        private void FormStructuresName_Load(object sender, EventArgs e)
        {
            Utils.setCbItemsCount(cbWidth, 8, 1);
            Utils.setCbItemsCount(cbHeight, 8, 1);
            cbWidth.SelectedIndex = StructWidth - 1;
            cbHeight.SelectedIndex = StructHeight -1;
            cbName.Text = StructName;
            Result = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = true;
            StructName = cbName.Text;
            StructWidth = cbWidth.SelectedIndex + 1;
            StructHeight = cbHeight.SelectedIndex + 1;
            Close();
        }

        public static string StructName;
        public static int StructWidth;
        public static int StructHeight;
        public static bool Result;
    }
}
