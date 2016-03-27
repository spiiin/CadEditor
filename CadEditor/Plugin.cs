using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Drawing;
using CSScriptLibrary;

namespace CadEditor
{

  public static class PluginLoader
  {
    public static T loadPlugin<T>(string path)
    {
      string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Assembly currentAssembly = Assembly.LoadFile(Path.Combine(appPath, path));
      foreach (Type type in currentAssembly.GetTypes())
      {
        if (type.GetInterfaces().Contains(typeof(T)))
          return (T)Activator.CreateInstance(type); 
      }
      return default(T);
    }
  }

  //--------------------------------------------------------------------------------------------------------------
  public interface IPlugin
  {
    void addSubeditorButton(FormMain formMain);
    void addToolButton(FormMain formMain);
    void loadFromConfig(object asm, object data); //asm is CSScriptLibrary.AsmHelper
    string getName();
  }

  public interface IVideoPluginNes
  {
      Image[] makeBigBlocks(int videoNo, int bigBlockNo, int blockNo, int palleteNo, MapViewType smallObjectsViewType = MapViewType.Tiles,
          float smallBlockScaleFactor = 2.0f, int blockWidth = 32, int blockHeight = 32, float curButtonScale = 2, MapViewType curViewType = MapViewType.Tiles, bool showAxis = false);
      Image[] makeBigBlocks(int videoNo, int bigBlockNo, BigBlock[] bigBlockData, int palleteNo, MapViewType smallObjectsViewType = MapViewType.Tiles,
          float smallBlockScaleFactor = 2.0f, int blockWidth = 32, int blockHeight = 32, float curButtonScale = 2, MapViewType curViewType = MapViewType.Tiles, bool showAxis = false);
      void updateColorsFromConfig();
      Bitmap makeImageStrip(byte[] videoChunk, byte[] pallete, int subPalIndex, float scale, bool scaleAccurate = true, bool withAlpha = false);
      Bitmap makeImageRectangle(byte[] videoChunk, byte[] pallete, int subPalIndex, float scale, bool scaleAccurate = true, bool withAlpha = false);
      Bitmap makeObjectsStrip(byte videoPageId, byte tilesId, byte palId, float scale, MapViewType drawType, int constantSubpal = -1);
      Bitmap makeObjectsRectangle(byte videoPageId, byte tilesId, byte palId, float scale, MapViewType drawType, int constantSubpal = -1);
      Bitmap makeScreen(int scrNo, int levelNo, int videoNo, int bigBlockNo, int blockNo, int palleteNo, float scale = 2.0f, bool withBorders = true);

      Bitmap makeBigBlock(int i, int width, int height, BigBlock[] bigBlocks, System.Windows.Forms.ImageList smallBlocks);
      Bitmap makeBigBlock3E(int i, int width, int height, BigBlock[] bigBlocks, System.Windows.Forms.ImageList smallBlocks);
      Bitmap makeBigBlockTT(int i, int width, int height, BigBlock[] bigBlocks, System.Windows.Forms.ImageList[] smallBlocksAll, byte[] smallBlocksColorBytes);

      Color[] NesColors { get; set; }

      string getName();
  }

  public interface IVideoPluginSega
  {
      Image[] makeBigBlocks(byte[] mapping, byte[] tiles, byte[] palette, int count, float zoom, MapViewType curViewType = MapViewType.Tiles, bool showAxis = false);
      Color[] GetPalette(byte[] pal);
      Bitmap GetTileFromArray(byte[] Tiles, ref int Position, Color[] Palette, byte PalIndex);
      Bitmap GetTileFrom2ColorArray(byte[] Tiles, ref int Position);
      byte[] GetArrayFrom2ColorTile(Bitmap tile);
      byte[] GetArrayFrom2ColorBlock(Bitmap block);
      Bitmap GetZoomBlockFrom2ColorArray(byte[] Tiles, int Width, float Zoom);
      //Bitmap GetTile(byte[] Tiles, ushort Word, Color[] Palette, byte PalIndex, bool HF, bool VF);
      Bitmap GetZoomTile(byte[] tiles, ushort Word, Color[] palette, byte palIndex, bool HF, bool VF, float zoom);
     // Bitmap GetBlock(ushort[] mapping, byte[] tiles, Color[] palette, byte Index);
      Bitmap GetZoomBlock(ushort[] mapping, byte[] tiles, Color[] palette, int Index, float zoom);
      Bitmap GetZoomBlock4x4(ushort[] mapping, byte[] tiles, Color[] palette, int Index, float zoom);

      string getName();
  }
}
