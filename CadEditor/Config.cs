using CadEditor;
using System.Collections.Generic;
using System.Drawing;

public class Config
{
  public string getFileName()      { return ""; }
  public string getDumpName()      { return ""; }
  public string getConfigName()    { return ""; }
  public bool showDumpFileField()  { return false;  }
  public Color[] getNesColors()
  {
    var NesColors = new Color[0x40];
    NesColors[0] = Color.FromArgb( 124,124,124);
    NesColors[1] = Color.FromArgb(0,0,252);
    NesColors[2] = Color.FromArgb(0,0,188);
    NesColors[3] = Color.FromArgb(68,40,188);
    NesColors[4] = Color.FromArgb(148,0,132);
    NesColors[5] = Color.FromArgb(168,0,32);
    NesColors[6] = Color.FromArgb(168,16,0);
    NesColors[7] = Color.FromArgb(136,20,0);
    NesColors[8] = Color.FromArgb(80,48,0);
    NesColors[9] = Color.FromArgb(0,120,0);
    NesColors[0xA] = Color.FromArgb(0,104,0);
    NesColors[0xB] = Color.FromArgb(0,88,0);
    NesColors[0xC] = Color.FromArgb(0,64,88);
    NesColors[0xD] = Color.FromArgb(0, 0, 0);
    NesColors[0xE] = Color.FromArgb(0, 0, 0);
    NesColors[0xF] = Color.FromArgb(0, 0, 0);

    NesColors[0x10] = Color.FromArgb(188,188,188);
    NesColors[0x11] = Color.FromArgb(0,120,248);
    NesColors[0x12] = Color.FromArgb(0, 88, 248);
    NesColors[0x13] = Color.FromArgb(104, 68, 252);
    NesColors[0x14] = Color.FromArgb(216, 0, 204);
    NesColors[0x15] = Color.FromArgb(228, 0, 88);
    NesColors[0x16] = Color.FromArgb(248, 56, 0);
    NesColors[0x17] = Color.FromArgb(228, 92, 16);
    NesColors[0x18] = Color.FromArgb(172, 124, 0);
    NesColors[0x19] = Color.FromArgb(0, 184, 0);
    NesColors[0x1A] = Color.FromArgb(0, 168, 0);
    NesColors[0x1B] = Color.FromArgb(0, 168, 68);
    NesColors[0x1C] = Color.FromArgb(0, 136, 136);
    NesColors[0x1D] = Color.FromArgb(0, 0, 0);
    NesColors[0x1E] = Color.FromArgb(0, 0, 0);
    NesColors[0x1F] = Color.FromArgb(0, 0, 0);

    NesColors[0x20] = Color.FromArgb(248,248,248);
    NesColors[0x21] = Color.FromArgb(60, 188, 252);
    NesColors[0x22] = Color.FromArgb(104, 136, 252);
    NesColors[0x23] = Color.FromArgb(152, 120, 248);
    NesColors[0x24] = Color.FromArgb(248, 120, 248);
    NesColors[0x25] = Color.FromArgb(248, 88, 152);
    NesColors[0x26] = Color.FromArgb(248, 120, 88);
    NesColors[0x27] = Color.FromArgb(252, 160, 68);
    NesColors[0x28] = Color.FromArgb(248, 184, 0);
    NesColors[0x29] = Color.FromArgb(184, 248, 24);
    NesColors[0x2A] = Color.FromArgb(88, 216, 84);
    NesColors[0x2B] = Color.FromArgb(88,248,152);
    NesColors[0x2C] = Color.FromArgb(0, 232, 216);
    NesColors[0x2D] = Color.FromArgb(120, 120, 120);
    NesColors[0x2E] = Color.FromArgb(0, 0, 0);
    NesColors[0x2F] = Color.FromArgb(0, 0, 0);

    NesColors[0x30] = Color.FromArgb(252,252,252);
    NesColors[0x31] = Color.FromArgb(164, 228, 252);
    NesColors[0x32] = Color.FromArgb(184, 184, 248);
    NesColors[0x33] = Color.FromArgb(216,184,248);
    NesColors[0x34] = Color.FromArgb(248, 184, 248);
    NesColors[0x35] = Color.FromArgb(248, 164, 192);
    NesColors[0x36] = Color.FromArgb(240,208,176);
    NesColors[0x37] = Color.FromArgb(252, 224, 168);
    NesColors[0x38] = Color.FromArgb(248, 216, 120);
    NesColors[0x39] = Color.FromArgb(216, 248, 120);
    NesColors[0x3A] = Color.FromArgb(184,248,184);
    NesColors[0x3B] = Color.FromArgb(184, 248, 216);
    NesColors[0x3C] = Color.FromArgb(0, 252, 252);
    NesColors[0x3D] = Color.FromArgb(248, 216, 248);
    NesColors[0x3E] = Color.FromArgb(0, 0, 0);
    NesColors[0x3F] = Color.FromArgb(0, 0, 0);
    
    return NesColors;
  }
}