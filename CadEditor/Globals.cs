using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;

using System.Drawing;

namespace CadEditor
{
    public static class Globals
    {
        static Globals()
        {
        }

        public static void loadData(string Filename, string Dumpfile, string ConfigFilename)
        {
            try
            {
                int size = (int)new FileInfo(Filename).Length;
                using (FileStream f = File.OpenRead(Filename))
                {
                    romdata = new byte[size];
                    f.Read(romdata, 0, size);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                if (Dumpfile != "")
                {
                    int size = (int)new FileInfo(Dumpfile).Length;
                    using (FileStream f = File.OpenRead(Dumpfile))
                    {
                        dumpdata = new byte[size];
                        f.Read(dumpdata, 0, size);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            try
            {
                ConfigScript.LoadFromFile(ConfigFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool flushToFile()
        {
            if (OpenFile.DumpName != "")
            {
                try
                {
                    using (FileStream f = File.OpenWrite(OpenFile.DumpName))
                    {
                        f.Write(Globals.dumpdata, 0, Globals.dumpdata.Length);
                        f.Seek(0, SeekOrigin.Begin);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            try
            {
                using (FileStream f = File.OpenWrite(OpenFile.FileName))
                {
                    f.Write(Globals.romdata, 0, Globals.romdata.Length);
                    f.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public static int[] getScreen(OffsetRec screenOffset,  int screenIndex)
        {
            var result = new int[Math.Max(64, screenOffset.recSize)];
            var arrayWithData = Globals.dumpdata != null ? Globals.dumpdata : Globals.romdata;
            int dataStride = ConfigScript.getScreenDataStride();
            int wordLen = ConfigScript.getWordLen();
            bool littleEndian = ConfigScript.isLittleEndian();
            int beginAddr = screenOffset.beginAddr + screenIndex * screenOffset.recSize * dataStride * wordLen;
            if (wordLen == 1)
            {
                for (int i = 0; i < screenOffset.recSize; i++)
                    result[i] = ConfigScript.convertScreenTile(arrayWithData[beginAddr + i * dataStride]);
                for (int i = screenOffset.recSize; i < 64; i++)
                    result[i] = ConfigScript.convertScreenTile(0); //need this?
            }
            else if (wordLen == 2)
            {
                if (littleEndian)
                {
                    for (int i = 0; i < screenOffset.recSize; i++)
                        result[i] = ConfigScript.convertScreenTile(Utils.readWordLE(arrayWithData, beginAddr + i * (dataStride * wordLen)));
                }
                else
                {
                    for (int i = 0; i < screenOffset.recSize; i++)
                        result[i] = ConfigScript.convertScreenTile(Utils.readWord(arrayWithData, beginAddr + i * (dataStride * wordLen)));
                }
            }
            return result;
        }

        public static byte[] romdata;
        public static byte[] dumpdata;
        public static int CHUNKS_COUNT = 256;
        public static int VIDEO_PAGE_SIZE = 4096;
        public static int PAL_LEN = 16;
        public static int SEGA_PAL_LEN = 128;
        public static int MAX_SCREEN_LIST_LEN = 64;
    }
}