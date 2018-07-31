using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CadEditor;

namespace BtRaceEditor
{
    public partial class FormHexTableEditor : Form
    {
        readonly IBaseLoader loader = new BattletoadsLoader();
        private bool dirty;

        public FormHexTableEditor()
        {
            InitializeComponent();
        }

        public void setDataSource<T>()
            where T : GameObject
        {
            var gameObjects = loader.load(Globals.romdata);
            dgvGameObjects.DataSource = gameObjects.GetList().ConvertAll(x => x as T);
        }

        private void FormHexTableEditor_Load(object sender, EventArgs e)
        {
            loader.setFormText(this);
            dgvGameObjects.AutoGenerateColumns = true;
            loader.initDataSource(this);
            for (int i = 0; i < dgvGameObjects.Columns.Count; i++)
            {
                dgvGameObjects.Columns[i].Width = 55;
            }

            dirty = false; updateSaveVisibility();
        }

        private bool saveToFile()
        {
            dirty = !Globals.flushToFile(); updateSaveVisibility();
            return !dirty;
        }

        private void updateSaveVisibility()
        {
            tbSave.Enabled = dirty;
        }

        private void dgvGameObjects_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            loader.cellFormatting(dgvGameObjects, e);
        }

        private void dgvGameObjects_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            loader.cellParsing(dgvGameObjects, e);
        }

        private void tbSave_Click(object sender, EventArgs e)
        {
            var lo = (List<BattletoadsGameObject>)dgvGameObjects.DataSource;
            loader.save(Globals.romdata, new GameObjectList(lo.ConvertAll(x=>x as GameObject)));
            saveToFile();
        }

        private void dgvGameObjects_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dirty = true; updateSaveVisibility();
        }

        private void dgvGameObjects_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("Invalid format: " + e.Exception.Message);
            }
            e.ThrowException = false;
        }

        public DataGridView getDataGrid()
        {
            return dgvGameObjects;
        }

        private void FormHexTableEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UtilsGui.askToSave(ref dirty, saveToFile, () => { }))
            {
                updateSaveVisibility();
                e.Cancel = true;
            }
        }
    }
}
