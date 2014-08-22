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
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        int scrOffset = 0;
        int scrWidth = 0;
        int scrHeight = 0;
        private FormMain formMain;

        public delegate void OnApply();
        public event OnApply onApply;

        private void btOk_Click(object sender, EventArgs e)
        {
            scrOffset = Utils.parseInt(tbScrOffset.Text, -1);
            if (scrOffset == -1)
              tbScrOffset.Text = scrOffset.ToString();
            scrWidth = Utils.parseInt(tbScrWidth.Text, -1);
            if (scrWidth == -1)
                tbScrWidth.Text = scrWidth.ToString();
            scrHeight = Utils.parseInt(tbScrHeight.Text, -1);
            if (scrHeight == -1)
                tbScrHeight.Text = scrHeight.ToString();
            ConfigScript.screensOffset[formMain.LevelNoForScreens].beginAddr = scrOffset;
            ConfigScript.screensOffset[formMain.LevelNoForScreens].width = scrWidth;
            ConfigScript.screensOffset[formMain.LevelNoForScreens].height = scrHeight;

            //callback
            onApply();

        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            var of = ConfigScript.screensOffset[formMain.LevelNoForScreens];
            scrOffset = of.beginAddr;
            scrWidth = of.width;
            scrHeight = of.height;
            tbScrOffset.Text = String.Format("0x{0:X}", scrOffset);
            tbScrWidth.Text = scrWidth.ToString();
            tbScrHeight.Text = scrHeight.ToString();
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }
    }
}
