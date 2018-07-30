using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BtRaceEditor
{
    interface IBaseLoader
    {
        GameObjectList load(byte[] romdata);
        void save(byte[] romdata, GameObjectList objects);

        void setFormText(FormHexTableEditor frmMain);
        void initDataSource(FormHexTableEditor frmMain);
        void cellFormatting(DataGridView dgvGameObjects, DataGridViewCellFormattingEventArgs e);
        void cellParsing(DataGridView dgvGameObjects, DataGridViewCellParsingEventArgs e);
    }

    public class GameObjectList
    {
        public GameObjectList()
        {
        }

        public GameObjectList(List<GameObject> list)
        {
            objects = list;
        }

        public static implicit operator List<GameObject>(GameObjectList gol)
        {
            return gol.GetList();
        }

        public List<GameObject> GetList()
        {
            return objects;
        }

        List<GameObject> objects = new List<GameObject>();
    }

    public class GameObject
    {
        public GameObject()
        {
        }
    }
}
