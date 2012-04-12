namespace CadEditor
{
    partial class EnemyEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.btLeft = new System.Windows.Forms.Button();
            this.btRight = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.cbScreenNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.objPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lvObjects = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.objectSprites = new System.Windows.Forms.ImageList(this.components);
            this.cbCoordY = new System.Windows.Forms.ComboBox();
            this.cbCoordX = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btClearObjs = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.activeBlock = new System.Windows.Forms.PictureBox();
            this.btSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLevel
            // 
            this.cbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "Level 0",
            "Level A",
            "Level B",
            "Level C",
            "Level D",
            "Level E",
            "Level F",
            "Level G",
            "Level H",
            "Level I",
            "Level J"});
            this.cbLevel.Location = new System.Drawing.Point(83, 17);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(104, 21);
            this.cbLevel.TabIndex = 29;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Select level:";
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(193, 17);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(512, 512);
            this.mapScreen.TabIndex = 30;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // btLeft
            // 
            this.btLeft.Location = new System.Drawing.Point(24, 75);
            this.btLeft.Name = "btLeft";
            this.btLeft.Size = new System.Drawing.Size(53, 22);
            this.btLeft.TabIndex = 31;
            this.btLeft.Text = "<";
            this.btLeft.UseVisualStyleBackColor = true;
            this.btLeft.Click += new System.EventHandler(this.btLeft_Click);
            // 
            // btRight
            // 
            this.btRight.Location = new System.Drawing.Point(134, 75);
            this.btRight.Name = "btRight";
            this.btRight.Size = new System.Drawing.Size(53, 22);
            this.btRight.TabIndex = 32;
            this.btRight.Text = ">";
            this.btRight.UseVisualStyleBackColor = true;
            this.btRight.Click += new System.EventHandler(this.btRight_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(79, 54);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(53, 22);
            this.btUp.TabIndex = 33;
            this.btUp.Text = "^";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(79, 104);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(53, 22);
            this.btDown.TabIndex = 34;
            this.btDown.Text = "V";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // cbScreenNo
            // 
            this.cbScreenNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenNo.FormattingEnabled = true;
            this.cbScreenNo.Location = new System.Drawing.Point(79, 77);
            this.cbScreenNo.Name = "cbScreenNo";
            this.cbScreenNo.Size = new System.Drawing.Size(53, 21);
            this.cbScreenNo.TabIndex = 35;
            this.cbScreenNo.SelectedIndexChanged += new System.EventHandler(this.cbScreenNo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Screen No:";
            // 
            // objPanel
            // 
            this.objPanel.AutoScroll = true;
            this.objPanel.Location = new System.Drawing.Point(15, 132);
            this.objPanel.Name = "objPanel";
            this.objPanel.Size = new System.Drawing.Size(172, 177);
            this.objPanel.TabIndex = 37;
            // 
            // lvObjects
            // 
            this.lvObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjects.Location = new System.Drawing.Point(711, 17);
            this.lvObjects.Name = "lvObjects";
            this.lvObjects.Size = new System.Drawing.Size(192, 448);
            this.lvObjects.SmallImageList = this.objectSprites;
            this.lvObjects.TabIndex = 38;
            this.lvObjects.UseCompatibleStateImageBehavior = false;
            this.lvObjects.View = System.Windows.Forms.View.Details;
            this.lvObjects.SelectedIndexChanged += new System.EventHandler(this.lvObjects_SelectedIndexChanged);
            this.lvObjects.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbObjects_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 170;
            // 
            // objectSprites
            // 
            this.objectSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.objectSprites.ImageSize = new System.Drawing.Size(16, 16);
            this.objectSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbCoordY
            // 
            this.cbCoordY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoordY.Enabled = false;
            this.cbCoordY.FormattingEnabled = true;
            this.cbCoordY.Items.AddRange(new object[] {
            "Tiles",
            "Obj types"});
            this.cbCoordY.Location = new System.Drawing.Point(850, 465);
            this.cbCoordY.Name = "cbCoordY";
            this.cbCoordY.Size = new System.Drawing.Size(53, 21);
            this.cbCoordY.TabIndex = 42;
            this.cbCoordY.SelectedIndexChanged += new System.EventHandler(this.cbCoordY_SelectedIndexChanged);
            // 
            // cbCoordX
            // 
            this.cbCoordX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoordX.Enabled = false;
            this.cbCoordX.FormattingEnabled = true;
            this.cbCoordX.Items.AddRange(new object[] {
            "Tiles",
            "Obj types"});
            this.cbCoordX.Location = new System.Drawing.Point(793, 465);
            this.cbCoordX.Name = "cbCoordX";
            this.cbCoordX.Size = new System.Drawing.Size(53, 21);
            this.cbCoordX.TabIndex = 41;
            this.cbCoordX.SelectedIndexChanged += new System.EventHandler(this.cbCoordX_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(711, 468);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Object coords:";
            // 
            // btClearObjs
            // 
            this.btClearObjs.Location = new System.Drawing.Point(711, 495);
            this.btClearObjs.Name = "btClearObjs";
            this.btClearObjs.Size = new System.Drawing.Size(192, 36);
            this.btClearObjs.TabIndex = 39;
            this.btClearObjs.Text = "clear all objects on screen";
            this.btClearObjs.UseVisualStyleBackColor = true;
            this.btClearObjs.Click += new System.EventHandler(this.btClearObjs_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Active:";
            // 
            // activeBlock
            // 
            this.activeBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activeBlock.Location = new System.Drawing.Point(154, 315);
            this.activeBlock.Name = "activeBlock";
            this.activeBlock.Size = new System.Drawing.Size(32, 32);
            this.activeBlock.TabIndex = 43;
            this.activeBlock.TabStop = false;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(15, 325);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(64, 22);
            this.btSave.TabIndex = 45;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // EnemyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 535);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activeBlock);
            this.Controls.Add(this.cbCoordY);
            this.Controls.Add(this.cbCoordX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btClearObjs);
            this.Controls.Add(this.lvObjects);
            this.Controls.Add(this.objPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbScreenNo);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btRight);
            this.Controls.Add(this.btLeft);
            this.Controls.Add(this.mapScreen);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EnemyEditor";
            this.Text = "Enemy Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnemyEditor_FormClosing);
            this.Load += new System.EventHandler(this.EnemyEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.Button btLeft;
        private System.Windows.Forms.Button btRight;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.ComboBox cbScreenNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel objPanel;
        private System.Windows.Forms.ListView lvObjects;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox cbCoordY;
        private System.Windows.Forms.ComboBox cbCoordX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btClearObjs;
        private System.Windows.Forms.ImageList objectSprites;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox activeBlock;
        private System.Windows.Forms.Button btSave;
    }
}