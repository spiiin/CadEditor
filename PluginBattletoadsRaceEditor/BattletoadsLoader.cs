using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MiscUtil.IO;
using MiscUtil.Conversion;

namespace BtRaceEditor
{
    class BattletoadsLoader : IBaseLoader
    {
        public GameObjectList load(byte[] romdata)
        {
            var objects = new List<GameObject>();
                using (var br = new EndianBinaryReader(EndianBitConverter.Big, new MemoryStream(romdata)))
                {
                    br.BaseStream.Seek(BtConfig.startAddress, SeekOrigin.Begin);
                    for (int i = 0; i < BtConfig.objCount; i++)
                    {
                        byte type = br.ReadByte();
                        UInt16 x = br.ReadUInt16();
                        UInt16 x1 = br.ReadUInt16();
                        UInt16 y = br.ReadUInt16();
                        UInt16 z = br.ReadUInt16();
                        byte blinkTime = br.ReadByte();
                        byte jumpPower = br.ReadByte();
                        var obj = new BattletoadsGameObject(type, x, x1, y, z, blinkTime, jumpPower);
                        objects.Add(obj);
                    }
                }
            return new GameObjectList(objects);
        }

        public void save(byte[] romdata, GameObjectList objects)
        {

            using (var bw = new EndianBinaryWriter(EndianBitConverter.Big, new MemoryStream(romdata)))
            {
                bw.BaseStream.Seek(BtConfig.startAddress, SeekOrigin.Begin);
                for (int i = 0; i < BtConfig.objCount; i++)
                {
                    var obj = (BattletoadsGameObject)objects.GetList()[i];
                    bw.Write((byte)obj.type);
                    bw.Write((UInt16)obj.x);
                    bw.Write((UInt16)obj.x1);
                    bw.Write((UInt16)obj.y);
                    bw.Write((UInt16)obj.z);
                    bw.Write((byte)obj.blinkTime);
                    bw.Write((byte)obj.jumpPower);
                }
            }
        }

        Dictionary<int, Color> colorDict = new Dictionary<int, Color>()
            {
                { 0x26, Color.Red },
                { 0x27, Color.Red },
                { 0x19, Color.Red },
                { 0x49, Color.Red },
                { 0x22, Color.Red },

                { 0x23, Color.FromArgb(128, 128, 110) },
                { 0x24, Color.FromArgb(128, 196, 150) },
                { 0x25, Color.FromArgb(128, 128, 190) },

                { 0x2A, Color.FromArgb(160, 128, 190) },

                { 0x28, Color.FromArgb(255, 255, 170) },
                { 0x2B, Color.FromArgb(255, 255, 130) },
            };

        public void cellFormatting(DataGridView dgvGameObjects, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                long value = 0;
                if (long.TryParse(e.Value.ToString(), out value))
                {
                    e.Value = "0x" + value.ToString("X");
                    e.FormattingApplied = true;
                }
            }

            //coloring
            if (e.ColumnIndex == 0)
            {
                var row = dgvGameObjects.Rows[e.RowIndex];
                int cell0 = Convert.ToInt32(row.Cells[0].Value);
                row.DefaultCellStyle.BackColor = colorDict.ContainsKey(cell0) ? colorDict[cell0] : Color.White;

                if (row.Cells.Count == 8)
                {
                    var fname = String.Format("bt_objects/{0:X}.png", cell0);

                    if (File.Exists(fname))
                    {
                        row.Cells[1].Value = new Bitmap(Image.FromFile(fname), 16, 16);
                    }
                    else
                    {
                        row.Cells[1].Value = new Bitmap(16, 16);
                    }
                }
            }
        }

        public void cellParsing(DataGridView dgvGameObjects, DataGridViewCellParsingEventArgs e)
        {
            if (e != null && e.Value != null && e.DesiredType.Equals(typeof(int)))
            {
                var s = e.Value.ToString();
                if (s.StartsWith("0x") || s.StartsWith("0X"))
                {
                    try
                    {
                        int hex = (int)Convert.ToInt32(s.ToString(), 16);
                        e.Value = hex;
                        e.ParsingApplied = true;
                    }
                    catch
                    {
                        // Not a Valid Hexadecimal
                    }
                }
            }
        }

        public void setFormText(FormHexTableEditor frmMain)
        {
            frmMain.Text = "Battletoads Turbo Tunnel Editor";
        }

        public void initDataSource(FormHexTableEditor frmMain)
        {
            frmMain.setDataSource<BattletoadsGameObject>();

            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            iconColumn.Name = "Icon";
            iconColumn.HeaderText = "Icon";
            frmMain.getDataGrid().Columns.Insert(1, iconColumn);
        }
    }

    public class BattletoadsGameObject : GameObject
    {
        public BattletoadsGameObject(int type, int x, int x1, int y, int z, int blinkTime, int jumpPower)
        {
            this.type = type;
            this.x = x;
            this.x1 = x1;
            this.y = y;
            this.z = z;
            this.blinkTime = blinkTime;
            this.jumpPower = jumpPower;
        }
        public int type { get; set; }
        public int x { get; set; }
        public int x1 { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int blinkTime { get; set; }
        public int jumpPower { get; set; }
    }
}
