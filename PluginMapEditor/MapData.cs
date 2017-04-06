using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginMapEditor
{
    public class MapData
    {
        public MapData(int width, int height)
        {
            this.width = width;
            this.height = height;
            mapData = new int[width * height];
            attrData = new int[mapData.Length / 16];
        }
        public MapData(int[] mapData, int[] attrData, int width)
        {
            this.mapData = mapData;
            this.attrData = attrData;
            this.width = width;
            this.height = mapData.Length / width;
        }

        public int[] getFullArray()
        {
            int[] result = new int[mapData.Length + attrData.Length];
            Array.Copy(mapData, result, mapData.Length);
            Array.Copy(attrData, 0, result, mapData.Length, attrData.Length);
            return result;
        }
        public int[] mapData;
        public int[] attrData;
        public int width;
        public int height;
    }
}
