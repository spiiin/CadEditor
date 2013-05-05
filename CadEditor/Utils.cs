using System;
using System.Windows.Forms;
using System.Globalization;

namespace CadEditor
{
    public static class Utils
    {
        public static void setCbItemsCount(ComboBox cb, int count, int first = 0)
        {
            cb.Items.Clear();
            for (int i = 0; i < count; i++)
                cb.Items.Add(first + i);
        }

        public static void setCbIndexWithoutUpdateLevel(ComboBox cb, EventHandler ev, int index = 0)
        {
            cb.SelectedIndexChanged -= ev;
            cb.SelectedIndex = index;
            cb.SelectedIndexChanged += ev;
        }

        public delegate bool SaveFunction();
        public delegate void ReturnComboBoxIndexFunction();
        public static bool askToSave(ref bool dirty, SaveFunction saveToFile, ReturnComboBoxIndexFunction returnCbLevelIndex)
        {
            if (!dirty)
                return true;
            DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Cancel)
            {
                returnCbLevelIndex();
                return false;
            }
            else if (dr == DialogResult.Yes)
            {
                if (!saveToFile())
                {
                    returnCbLevelIndex();
                    return false;
                }
                return true;
            }
            else
            {
                dirty = false;
                return true;
            }
        }

        public static int parseInt(string value)
        {
            int ans = 0;
            //try hex parsing
            if ((value.Length > 2) && (value[0] == '0') && ((value[1] == 'x') || (value[1] == 'X')))
            {
                var newStr = value.Substring(2);
                int.TryParse(newStr, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out ans);
                return ans;
            }
            int.TryParse(value, out ans);
            return ans;

        }
    }
}
