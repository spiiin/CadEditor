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
            UtilsGui.setCbItemsCount(cbWidth, 64, 1);
            UtilsGui.setCbItemsCount(cbHeight, 64, 1);
            cbWidth.SelectedIndex = structWidth - 1;
            cbHeight.SelectedIndex = structHeight -1;
            cbName.Text = structName;
            result = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = true;
            structName = cbName.Text;
            structWidth = cbWidth.SelectedIndex + 1;
            structHeight = cbHeight.SelectedIndex + 1;
            Close();
        }

        public static string structName;
        public static int structWidth;
        public static int structHeight;
        public static bool result;
    }
}
