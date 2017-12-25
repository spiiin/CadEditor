using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

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
        public ObjRec(int w, int h, int type, int[] indexes, int[] palBytes)
        {
            //getSize() == indexes.Length == (if square) palBytes.Lenghth/4
            this.w = w;
            this.h = h;
            this.type = type;
            this.indexes = indexes;
            this.palBytes = palBytes;
        }

        public ObjRec(int c1, int c2, int c3, int c4, int type, int pal)
        {
            this.indexes = new int[4];
            this.palBytes = new int[1];

            this.indexes[0] = c1;
            this.indexes[1] = c2;
            this.indexes[2] = c3;
            this.indexes[3] = c4;
            this.palBytes[0] = pal;
            this.type = type;
        }

        public ObjRec(ObjRec other)
        {
            this.w = other.w;
            this.h = other.h;
            this.type = other.type;
            this.indexes = new int[other.indexes.Length];
            this.palBytes = new int[other.palBytes.Length];
            Array.Copy(other.indexes, this.indexes, this.indexes.Length);
            Array.Copy(other.palBytes, this.palBytes, this.palBytes.Length);
        }

        public int[] indexes;
        public int[] palBytes;
        public int type;
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
            return palBytes[0] & 0x3;
        }

        public virtual int getType()
        {
            return type;
        }

        bool IEquatable<ObjRec>.Equals(ObjRec other)
        {
            if ((w != other.w) || (h!=other.h))
            {
                return false;
            }
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] != other.indexes[i])
                    return false;
            }
            for (int p = 0; p < palBytes.Length; p++)
            {
                if (palBytes[p] != other.palBytes[p])
                    return false;
            }
            if (type != other.type)
            {
                return false;
            }
            return true;
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
            int hash = 0;
            foreach (var i in indexes)
            {
                hash += i.GetHashCode();
            }
            foreach(var p in palBytes)
            {
                hash += p.GetHashCode();
            }
            hash += type.GetHashCode();
            return hash;
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
        public LevelLayerData(int width, int height, int[] layer, int[] scroll, int[] dirs)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = scroll;
            this.dirs = dirs;
        }

        public LevelLayerData(int width, int height, int[] layer)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = null;
            this.dirs = null;
        }

        public int getDirForIndex(int index)
        {
            int line = index / width;
            return dirs[line];
        }
        public int width;
        public int height;
        public int[] layer;
        public int[] scroll;
        public int[] dirs;
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

    public class ObjectRec : IEquatable<ObjectRec>
    {
        [JsonConstructor]
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

        public ObjectRec(ObjectRec other)
        {
            this.type = other.type;
            this.sx = other.sx;
            this.sy = other.sy;
            this.x = other.x;
            this.y = other.y;
            this.additionalData = new Dictionary<String,int>(other.additionalData);
        }
        public int type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int sx { get; set; }
        public int sy { get; set; }
        public Dictionary<String, int> additionalData;

        public static int FIELD_COUNT = 6;

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
            return addDataEq;
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

    public class ObjectList : IEquatable<ObjectList>
    {
        public ObjectList()
        {
            objects = new List<ObjectRec>();
            name = "Objects";
        }
        public List<ObjectRec> objects;
        public string name;

        bool IEquatable<ObjectList>.Equals(ObjectList other)
        {
            if (name != other.name)
                return false;
            return objects.SequenceEqual(other.objects);
        }
    }

    public enum ToolType
    {
        Create,
        Select,
        Delete
    }


    public class BlockLayer
    {
        public int[][] screens;
        public bool showLayer;
        public int blockWidth;
        public int blockHeight;

        public BlockLayer()
        {
            screens = null;
            showLayer = true;
            blockWidth = 32;
            blockHeight = 32;
        }
    }
}