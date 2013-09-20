using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace CadEditor
{
    public partial class EditHexEditor : Form
    {
        public EditHexEditor()
        {
            InitializeComponent();
            hexBox = new HexBox();
            hexBox.UseFixedBytesPerLine = true;
            hexBox.LineInfoVisible = true;
            hexBox.VScrollBarVisible = true;
            hexBox.Size = this.ClientSize;
            /*hexBox.GroupSeparatorVisible = true;
            hexBox.GroupSize = 4;*/
            hexBox.Paint += new PaintEventHandler(hexBox_Paint);
            this.Controls.Add(hexBox);
            hexBox.ByteProvider = new DynamicByteProvider(Globals.romdata);
        }

        private void highlightLine(Graphics g, int lineNo, int firstChar, int length, bool withSep)
        {
            int firstCharOfs = (byteSize.X + sepSize.X) * firstChar;
            int yOfs = (byteSize.Y + sepSize.Y) * (lineNo + 1);
            g.FillRectangle(highlightBrush, new Rectangle(firstCoord.X + firstCharOfs, firstCoord.Y + yOfs, byteSize.X * length + sepSize.X * (length - 1), byteSize.Y + (withSep ? sepSize.Y : 0)));
        }

        private void highlightZone(Graphics g, int firstByte, int byteCount)
        {
            if (byteCount <= 0)
                return;
            int screenOffset = -(int)hexBox.FirstLineOnScreen;
            int firstLineIndex = firstByte / bytesInLine;
            int lastLineIndex = (firstByte + byteCount) / 16;
            int linesCount = lastLineIndex - firstLineIndex + 1;
            int firstByteIndex = firstByte % 16;
            int lastByteIndex = (firstByte + byteCount) % 16;
            if (linesCount == 1)
            {
                highlightLine(g, firstLineIndex + screenOffset, firstByteIndex, byteCount, false);
                return;
            }
            //firstLine
            highlightLine(g, firstLineIndex + screenOffset, firstByteIndex, 16 - firstByteIndex, true);
            //full lines
            for (int i = firstLineIndex + 1; i < lastLineIndex; i++)
                highlightLine(g, screenOffset + i, 0, 16, true);
            //lastLine
            highlightLine(g, screenOffset + lastLineIndex, 0, lastByteIndex, true);
        }

        private void hexBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            highlightZone(g, highlightFirst, highlightLength);
        }

        private void EditHexEditor_Load(object sender, EventArgs e)
        {
            int vertOfs = hexBox.VerticalByteCount * hexBox.HorizontalByteCount;
            hexBox.ScrollByteIntoView(highlightFirst + vertOfs);
        }

        private void EditHexEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to save changes?", "Save", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (dr == DialogResult.Yes)
            {
                DynamicByteProvider bp = hexBox.ByteProvider as DynamicByteProvider;
                bp.ApplyChanges();
                bp.Bytes.CopyTo(Globals.romdata);
            }
        }

        public void setHighlightZone(int first, int length)
        {
            highlightFirst = first;
            highlightLength = length;
        }

        private HexBox hexBox;
        Point firstCoord = new Point(85, 4);
        Point byteSize = new Point(18, 12);
        Point sepSize = new Point(6, 2);
        Brush highlightBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
        int bytesInLine = 16;

        private int highlightFirst;
        private int highlightLength;
    }
}
