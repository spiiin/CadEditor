using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

using System.Drawing;

namespace CadEditor
{
    public struct OffsetRec
    {
        public OffsetRec(int beginAddr, int recCount, int recSize)
        {
            this.beginAddr = beginAddr;
            this.recCount = recCount;
            this.recSize = recSize;
            this.width = 0;
            this.height = 0;
        }

        public OffsetRec(int beginAddr, int recCount, int recSize, int width = 0, int height = 0)
        {
            this.beginAddr = beginAddr;
            this.recCount = recCount;
            this.recSize = recSize;
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return String.Format("Start address:0x{0:X}. Records count:{1}, Record Size:{2}", beginAddr, recCount, recSize);
        }

        public int beginAddr;
        public int recCount;
        public int recSize;
        public int width;
        public int height;
    }

    public struct LevelObjRec
    {
        public LevelObjRec(string name, int addrOfObjects, int addrOfPallete, int addrOfVideo)
        {
            this.name = name;
            this.addrOfObjects = addrOfObjects;
            this.addrOfPallete = addrOfPallete;
            this.addrOfVideo = addrOfVideo;
        }
        public string name;
        public int addrOfObjects;
        public int addrOfPallete;
        public int addrOfVideo;
    }

    public class ObjRec : IEquatable<ObjRec>
    {
        public ObjRec(int w, int h, int[] indexes, int[] palBytes)
        {
            //assert(getSize() == indexes.Length == palBytes.Lenghth/4)
            this.w = w;
            this.h = h;
            this.indexes = indexes;
            this.palBytes = palBytes;
        }

        public ObjRec(byte c1, byte c2, byte c3, byte c4, byte typeColor)
        {
            this.indexes = new int[4];
            this.palBytes = new int[indexes.Length / 4];

            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
            this.c4 = c4;
            this.typeColor = typeColor;
        }

        public ObjRec(ObjRec other)
        {
            this.indexes = new int[4];
            this.palBytes = new int[indexes.Length / 4];
            this.c1 = other.c1;
            this.c2 = other.c2;
            this.c3 = other.c3;
            this.c4 = other.c4;
            this.typeColor = other.typeColor;
        }

        //TODO: remove. FOR REFACTORING PURPOSES ONLY. properties only mask access to public field indexes and palBytes

        public int c1
        {
            get
            {
                return indexes[0];
            }
            set
            {
                indexes[0] = value;
            }
        }
        public int c2
        {
            get
            {
                return indexes[1];
            }
            set
            {
                indexes[1] = value;
            }
        }
        public int c3
        {
            get
            {
                return indexes[2];
            }
            set
            {
                indexes[2] = value;
            }
        }
        public int c4
        {
            get
            {
                return indexes[3];
            }
            set
            {
                indexes[3] = value;
            }
        }
        public int typeColor
        {
            get
            {
                return palBytes[0];
            }
            set
            {
                palBytes[0] = value;
            }
        }

        public int[] indexes;
        public int[] palBytes;
        public int w = 2, h = 2;

        public int getSize()
        {
            return w * h;
        }

        public virtual int getSubpallete(int i)
        {
            return palBytes[i] & 0x3;
        }

        public virtual int getSubpallete()
        {
            return typeColor & 0x3;
        }

        public virtual int getType()
        {
            return (typeColor & 0xF0) >> 4;
        }

        bool IEquatable<ObjRec>.Equals(ObjRec other)
        {
            return (c1 == other.c1) && (c2 == other.c2) && (c3 == other.c3) && (c4 == other.c4) && (typeColor == other.typeColor);
        }

        public override bool Equals(Object obj)
        {
            ObjRec other = obj as ObjRec;
            if (other == null)
                return false;
            else
                return ((IEquatable<ObjRec>)this).Equals(other);
        }

        public override int GetHashCode()
        {
            return c1.GetHashCode() + c2.GetHashCode() + c3.GetHashCode() + c4.GetHashCode() + typeColor.GetHashCode();
        }
    }

    public struct LevelRec
    {
        public LevelRec(int objectBeginAddr, int objCount)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectBeginAddr;
            this.width = 0;
            this.height = 0;
            this.layoutAddr = 0;
            this.name = "";
            this.levelNo = 0;
        }

        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
            this.name = "";
            this.levelNo = 0;
        }

        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0, string name = "")
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
            this.name = name;
            this.levelNo = 0;
        }

        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0, string name = "", int levelNo = 0)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
            this.name = name;
            this.levelNo = levelNo;
        }
        public int objCount;
        public int objectsBeginAddr;
        public int width;
        public int height;
        public int layoutAddr;
        public int levelNo;
        public string name;
    }

    public struct ScreenRec
    {
        public ScreenRec(int no, byte sx, byte sy, int door, bool backSort = false, bool upSort = false)
        {
            this.no = no;
            this.sx = sx;
            this.sy = sy;
            this.backSort = backSort;
            this.upsort = upSort;
            this.door = door;
        }
        public int no;
        public byte sx;
        public byte sy;
        public bool backSort;
        public bool upsort;
        public int door;
    }

    public struct LevelLayerData
    {
        public LevelLayerData(int width, int height, byte[] layer, byte[] scroll, byte[] dirs)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = scroll;
            this.dirs = dirs;
        }

        public LevelLayerData(int width, int height, byte[] layer)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = null;
            this.dirs = null;
        }

        public byte getDirForIndex(int index)
        {
            int line = index / width;
            return dirs[line];
        }
        public int width;
        public int height;
        public byte[] layer;
        public byte[] scroll;
        public byte[] dirs;
    }

    public struct GroupRec
    {
        public string name;
        public int videoNo;
        public int bigBlockNo;
        public int blockNo;
        public int palNo;
        public int firstScreen;

        public GroupRec(string name, int videoNo, int bigBlockNo, int blockNo, int palNo, int firstScreen)
        {
            this.name = name;
            this.videoNo = videoNo;
            this.bigBlockNo = bigBlockNo;
            this.blockNo = blockNo;
            this.palNo = palNo;
            this.firstScreen = firstScreen;
        }
    }

    public struct ObjectRec : IEquatable<ObjectRec>
    {
        public ObjectRec(int type, int sx, int sy, int x, int y, Dictionary<String, int> additionalData)
            : this(type, sx, sy, x, y)
        {
            this.additionalData = additionalData;
        }

        public ObjectRec(int type, int sx, int sy, int x, int y)
        {
            this.type = type;
            this.sx = sx;
            this.sy = sy;
            this.x = x;
            this.y = y;
            this.additionalData = null;
        }
        public int type;
        public int x;
        public int y;
        public int sx;
        public int sy;
        public Dictionary<String, int> additionalData;

        public override String ToString()
        {
            String formatStr = (type > 15) ? "{0:X} : ({1:X}:{2:X})" : "0{0:X} : ({1:X}:{2:X})";
            return String.Format(formatStr, type, sx << 8 | x, sy << 8 | y);
        }

        bool IEquatable<ObjectRec>.Equals(ObjectRec other)
        {
            bool fieldsEq = (type == other.type) && (x == other.x) && (y == other.y) && (sx == other.sx) && (sy == other.sy);
            if (!fieldsEq)
            {
                return false;
            }
            if (additionalData == null)
            {
                return other.additionalData == null;
            }

            //compare all values in dictionary
            bool addDataEq = additionalData.SequenceEqual(other.additionalData);
            return true;
        }
    }

    public enum MapViewType
    {
        Tiles,
        ObjType,
        ObjNumbers,
        SmallObjNumbers,
    };

    public class BigBlockWithPal : BigBlock
    {
        public BigBlockWithPal(int w, int h)
            : base(w, h)
        {
            palBytes = new int[indexes.Length];
        }

        public override int getPalBytes(int index)
        {
            return palBytes[index];
        }

        public override bool smallBlocksWithPal()
        {
            return false;
        }

        public override Bitmap makeBigBlock(Image[][] smallBlocksAll)
        {
            //calc size
            var smallBlocks = smallBlocksAll[0];
            int bWidth = smallBlocks[0].Width;
            int bHeight = smallBlocks[0].Height;
            var b = new Bitmap(bWidth * this.width, bHeight * this.height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawImage(smallBlocksAll[this.getPalBytes(0)][this.indexes[0]], new Rectangle(0, 0, bWidth, bHeight));
                g.DrawImage(smallBlocksAll[this.getPalBytes(1)][this.indexes[1]], new Rectangle(bWidth, 0, bWidth, bHeight));
                g.DrawImage(smallBlocksAll[this.getPalBytes(2)][this.indexes[2]], new Rectangle(0, bHeight, bWidth, bHeight));
                g.DrawImage(smallBlocksAll[this.getPalBytes(3)][this.indexes[3]], new Rectangle(bWidth, bHeight, bWidth, bHeight));
            }
            return b;
        }

        public int[] palBytes;
    }

    public class BigBlock : IEquatable<BigBlock>
    {
        public BigBlock(int w, int h)
        {
            width = w;
            height = h;
            indexes = new int[getSize()];
        }

        public int getSize() { return width * height; }

        public virtual Bitmap makeBigBlock(Image[][] smallBlockss)
        {
            var smallBlocks = smallBlockss[0];
            int bWidth = smallBlocks[0].Width;
            int bHeight = smallBlocks[0].Height;
            var b = new Bitmap(bWidth * this.width, bHeight * this.height);
            using (Graphics g = Graphics.FromImage(b))
            {
                for (int h = 0; h < this.height; h++)
                {
                    for (int w = 0; w < this.width; w++)
                    {
                        int sbX = w * bWidth;
                        int sbY = h * bHeight;
                        int idx = h * this.width + w;
                        var r = new Rectangle(sbX, sbY, bWidth, bHeight);
                        g.DrawImage(smallBlocks[this.indexes[idx]], r);
                    }
                }
            }
            return b;
        }

        public virtual int getPalBytes(int index)
        {
            return 0;
        }

        public virtual bool smallBlocksWithPal()
        {
            return true;
        }

        bool IEquatable<BigBlock>.Equals(BigBlock other)
        {
            return (width == other.width) && (height == other.height) && (indexes.SequenceEqual(other.indexes));
        }

        public override bool Equals(Object obj)
        {
            BigBlock other = obj as BigBlock;
            if (other == null)
                return false;
            else
                return ((IEquatable<BigBlock>)this).Equals(other);
        }

        public override int GetHashCode()
        {
            int hash = width.GetHashCode() + height.GetHashCode();
            foreach (var i in indexes)
            {
                hash += i.GetHashCode();
            }
            return hash;
        }

        public int[] indexes;
        public int width;
        public int height;
    }
}