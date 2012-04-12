namespace CadEditor
{
    partial class EditLevelData
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbObjGfx = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBackGfx = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbPalleteNo = new System.Windows.Forms.ComboBox();
            this.cbPallete2No = new System.Windows.Forms.ComboBox();
            this.cbPalBlinkByte = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStartLoc = new System.Windows.Forms.ComboBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.cbLayoutWidth = new System.Windows.Forms.ComboBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbd1 = new System.Windows.Forms.Label();
            this.cbLayoutHeight = new System.Windows.Forms.ComboBox();
            this.cbScrX = new System.Windows.Forms.ComboBox();
            this.lbd2 = new System.Windows.Forms.Label();
            this.cbScrY = new System.Windows.Forms.ComboBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.cbLayoutAddr = new System.Windows.Forms.ComboBox();
            this.cbScrollAddr = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbLayoutAddress = new System.Windows.Forms.Label();
            this.lbScrollAddr = new System.Windows.Forms.Label();
            this.lbd3 = new System.Windows.Forms.Label();
            this.lbd4 = new System.Windows.Forms.Label();
            this.cbPlayerX = new System.Windows.Forms.ComboBox();
            this.cbPlayerY = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.cbBigBlocks = new System.Windows.Forms.ComboBox();
            this.cbMusicNo = new System.Windows.Forms.ComboBox();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.cbDirsAddr = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lbDirsAddr = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select level:";
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
            "Level J",
            "Door 1",
            "Door 2",
            "Door 3",
            "Door 4",
            "Door 5",
            "Door 6",
            "Door 7",
            "Door 8",
            "Door 9",
            "Door A",
            "Door B",
            "Door C",
            "Door D",
            "Door E",
            "Door F",
            "Door 10",
            "Door 11",
            "Door 12",
            "Door 13",
            "Door 14",
            "Door 15",
            "Door 16",
            "Door 17",
            "Door 18"});
            this.cbLevel.Location = new System.Drawing.Point(101, 6);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(121, 21);
            this.cbLevel.TabIndex = 1;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Objects Graphic Bank No:";
            // 
            // cbObjGfx
            // 
            this.cbObjGfx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjGfx.FormattingEnabled = true;
            this.cbObjGfx.Location = new System.Drawing.Point(167, 76);
            this.cbObjGfx.Name = "cbObjGfx";
            this.cbObjGfx.Size = new System.Drawing.Size(55, 21);
            this.cbObjGfx.TabIndex = 3;
            this.cbObjGfx.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Back Graphic Bank No:";
            // 
            // cbBackGfx
            // 
            this.cbBackGfx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackGfx.FormattingEnabled = true;
            this.cbBackGfx.Location = new System.Drawing.Point(167, 53);
            this.cbBackGfx.Name = "cbBackGfx";
            this.cbBackGfx.Size = new System.Drawing.Size(55, 21);
            this.cbBackGfx.TabIndex = 5;
            this.cbBackGfx.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pallete No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Pallete2 No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Palletes Blink byte";
            // 
            // cbPalleteNo
            // 
            this.cbPalleteNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalleteNo.FormattingEnabled = true;
            this.cbPalleteNo.Location = new System.Drawing.Point(167, 102);
            this.cbPalleteNo.Name = "cbPalleteNo";
            this.cbPalleteNo.Size = new System.Drawing.Size(55, 21);
            this.cbPalleteNo.TabIndex = 9;
            this.cbPalleteNo.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // cbPallete2No
            // 
            this.cbPallete2No.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPallete2No.FormattingEnabled = true;
            this.cbPallete2No.Location = new System.Drawing.Point(167, 125);
            this.cbPallete2No.Name = "cbPallete2No";
            this.cbPallete2No.Size = new System.Drawing.Size(55, 21);
            this.cbPallete2No.TabIndex = 10;
            this.cbPallete2No.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // cbPalBlinkByte
            // 
            this.cbPalBlinkByte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalBlinkByte.FormattingEnabled = true;
            this.cbPalBlinkByte.Location = new System.Drawing.Point(167, 148);
            this.cbPalBlinkByte.Name = "cbPalBlinkByte";
            this.cbPalBlinkByte.Size = new System.Drawing.Size(55, 21);
            this.cbPalBlinkByte.TabIndex = 11;
            this.cbPalBlinkByte.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Start screen index (in layout):";
            // 
            // cbStartLoc
            // 
            this.cbStartLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStartLoc.FormattingEnabled = true;
            this.cbStartLoc.Location = new System.Drawing.Point(167, 175);
            this.cbStartLoc.Name = "cbStartLoc";
            this.cbStartLoc.Size = new System.Drawing.Size(55, 21);
            this.cbStartLoc.TabIndex = 13;
            this.cbStartLoc.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(12, 227);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(67, 13);
            this.lbl1.TabIndex = 14;
            this.lbl1.Text = "Layout width";
            // 
            // cbLayoutWidth
            // 
            this.cbLayoutWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayoutWidth.FormattingEnabled = true;
            this.cbLayoutWidth.Location = new System.Drawing.Point(167, 224);
            this.cbLayoutWidth.Name = "cbLayoutWidth";
            this.cbLayoutWidth.Size = new System.Drawing.Size(55, 21);
            this.cbLayoutWidth.TabIndex = 15;
            this.cbLayoutWidth.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(12, 248);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(71, 13);
            this.lbl2.TabIndex = 16;
            this.lbl2.Text = "Layout height";
            // 
            // lbd1
            // 
            this.lbd1.AutoSize = true;
            this.lbd1.Location = new System.Drawing.Point(12, 406);
            this.lbd1.Name = "lbd1";
            this.lbd1.Size = new System.Drawing.Size(76, 13);
            this.lbd1.TabIndex = 17;
            this.lbd1.Text = "Start Screen X";
            // 
            // cbLayoutHeight
            // 
            this.cbLayoutHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayoutHeight.FormattingEnabled = true;
            this.cbLayoutHeight.Location = new System.Drawing.Point(167, 245);
            this.cbLayoutHeight.Name = "cbLayoutHeight";
            this.cbLayoutHeight.Size = new System.Drawing.Size(55, 21);
            this.cbLayoutHeight.TabIndex = 18;
            this.cbLayoutHeight.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // cbScrX
            // 
            this.cbScrX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScrX.FormattingEnabled = true;
            this.cbScrX.Location = new System.Drawing.Point(167, 403);
            this.cbScrX.Name = "cbScrX";
            this.cbScrX.Size = new System.Drawing.Size(55, 21);
            this.cbScrX.TabIndex = 19;
            this.cbScrX.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // lbd2
            // 
            this.lbd2.AutoSize = true;
            this.lbd2.Location = new System.Drawing.Point(12, 427);
            this.lbd2.Name = "lbd2";
            this.lbd2.Size = new System.Drawing.Size(76, 13);
            this.lbd2.TabIndex = 20;
            this.lbd2.Text = "Start Screen Y";
            // 
            // cbScrY
            // 
            this.cbScrY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScrY.FormattingEnabled = true;
            this.cbScrY.Location = new System.Drawing.Point(167, 424);
            this.cbScrY.Name = "cbScrY";
            this.cbScrY.Size = new System.Drawing.Size(55, 21);
            this.cbScrY.TabIndex = 21;
            this.cbScrY.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(12, 271);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(79, 13);
            this.lbl3.TabIndex = 22;
            this.lbl3.Text = "Layout address";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(12, 293);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(112, 13);
            this.lbl4.TabIndex = 23;
            this.lbl4.Text = "Scroll / Doors address";
            // 
            // cbLayoutAddr
            // 
            this.cbLayoutAddr.Enabled = false;
            this.cbLayoutAddr.FormattingEnabled = true;
            this.cbLayoutAddr.Location = new System.Drawing.Point(167, 268);
            this.cbLayoutAddr.Name = "cbLayoutAddr";
            this.cbLayoutAddr.Size = new System.Drawing.Size(71, 21);
            this.cbLayoutAddr.TabIndex = 24;
            this.cbLayoutAddr.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // cbScrollAddr
            // 
            this.cbScrollAddr.Enabled = false;
            this.cbScrollAddr.FormattingEnabled = true;
            this.cbScrollAddr.Location = new System.Drawing.Point(167, 290);
            this.cbScrollAddr.Name = "cbScrollAddr";
            this.cbScrollAddr.Size = new System.Drawing.Size(71, 21);
            this.cbScrollAddr.TabIndex = 25;
            this.cbScrollAddr.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(244, 271);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "addr in ROM (+10010):";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 293);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 13);
            this.label15.TabIndex = 27;
            this.label15.Text = "addr in ROM (+10010):";
            // 
            // lbLayoutAddress
            // 
            this.lbLayoutAddress.AutoSize = true;
            this.lbLayoutAddress.Location = new System.Drawing.Point(356, 271);
            this.lbLayoutAddress.Name = "lbLayoutAddress";
            this.lbLayoutAddress.Size = new System.Drawing.Size(13, 13);
            this.lbLayoutAddress.TabIndex = 28;
            this.lbLayoutAddress.Text = "0";
            // 
            // lbScrollAddr
            // 
            this.lbScrollAddr.AutoSize = true;
            this.lbScrollAddr.Location = new System.Drawing.Point(356, 293);
            this.lbScrollAddr.Name = "lbScrollAddr";
            this.lbScrollAddr.Size = new System.Drawing.Size(13, 13);
            this.lbScrollAddr.TabIndex = 29;
            this.lbScrollAddr.Text = "0";
            // 
            // lbd3
            // 
            this.lbd3.AutoSize = true;
            this.lbd3.Location = new System.Drawing.Point(12, 449);
            this.lbd3.Name = "lbd3";
            this.lbd3.Size = new System.Drawing.Size(46, 13);
            this.lbd3.TabIndex = 30;
            this.lbd3.Text = "Player X";
            // 
            // lbd4
            // 
            this.lbd4.AutoSize = true;
            this.lbd4.Location = new System.Drawing.Point(12, 471);
            this.lbd4.Name = "lbd4";
            this.lbd4.Size = new System.Drawing.Size(46, 13);
            this.lbd4.TabIndex = 31;
            this.lbd4.Text = "Player Y";
            // 
            // cbPlayerX
            // 
            this.cbPlayerX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayerX.FormattingEnabled = true;
            this.cbPlayerX.Location = new System.Drawing.Point(167, 446);
            this.cbPlayerX.Name = "cbPlayerX";
            this.cbPlayerX.Size = new System.Drawing.Size(55, 21);
            this.cbPlayerX.TabIndex = 32;
            this.cbPlayerX.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // cbPlayerY
            // 
            this.cbPlayerY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayerY.FormattingEnabled = true;
            this.cbPlayerY.Location = new System.Drawing.Point(167, 468);
            this.cbPlayerY.Name = "cbPlayerY";
            this.cbPlayerY.Size = new System.Drawing.Size(55, 21);
            this.cbPlayerY.TabIndex = 33;
            this.cbPlayerY.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(228, 406);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(175, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "* Must be correct caclulated coords";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(34, 194);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(204, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "* For level it must be LEFT-DOWN screen";
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(12, 340);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(70, 13);
            this.lbl6.TabIndex = 36;
            this.lbl6.Text = "Big blocks id:";
            // 
            // cbBigBlocks
            // 
            this.cbBigBlocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBigBlocks.FormattingEnabled = true;
            this.cbBigBlocks.Location = new System.Drawing.Point(167, 337);
            this.cbBigBlocks.Name = "cbBigBlocks";
            this.cbBigBlocks.Size = new System.Drawing.Size(55, 21);
            this.cbBigBlocks.TabIndex = 37;
            this.cbBigBlocks.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // cbMusicNo
            // 
            this.cbMusicNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMusicNo.FormattingEnabled = true;
            this.cbMusicNo.Location = new System.Drawing.Point(167, 358);
            this.cbMusicNo.Name = "cbMusicNo";
            this.cbMusicNo.Size = new System.Drawing.Size(55, 21);
            this.cbMusicNo.TabIndex = 39;
            this.cbMusicNo.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Location = new System.Drawing.Point(12, 361);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(55, 13);
            this.lbl7.TabIndex = 38;
            this.lbl7.Text = "Music No:";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(12, 315);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(123, 13);
            this.lbl5.TabIndex = 40;
            this.lbl5.Text = "Stage directions address";
            // 
            // cbDirsAddr
            // 
            this.cbDirsAddr.Enabled = false;
            this.cbDirsAddr.FormattingEnabled = true;
            this.cbDirsAddr.Location = new System.Drawing.Point(167, 312);
            this.cbDirsAddr.Name = "cbDirsAddr";
            this.cbDirsAddr.Size = new System.Drawing.Size(71, 21);
            this.cbDirsAddr.TabIndex = 41;
            this.cbDirsAddr.SelectedIndexChanged += new System.EventHandler(this.dirty_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(244, 315);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 13);
            this.label23.TabIndex = 42;
            this.label23.Text = "addr in ROM (+8010):";
            // 
            // lbDirsAddr
            // 
            this.lbDirsAddr.AutoSize = true;
            this.lbDirsAddr.Location = new System.Drawing.Point(356, 315);
            this.lbDirsAddr.Name = "lbDirsAddr";
            this.lbDirsAddr.Size = new System.Drawing.Size(13, 13);
            this.lbDirsAddr.TabIndex = 43;
            this.lbDirsAddr.Text = "0";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(15, 496);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(207, 23);
            this.btSave.TabIndex = 44;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // EditLevelData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 531);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.lbDirsAddr);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cbDirsAddr);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.cbMusicNo);
            this.Controls.Add(this.lbl7);
            this.Controls.Add(this.cbBigBlocks);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cbPlayerY);
            this.Controls.Add(this.cbPlayerX);
            this.Controls.Add(this.lbd4);
            this.Controls.Add(this.lbd3);
            this.Controls.Add(this.lbScrollAddr);
            this.Controls.Add(this.lbLayoutAddress);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cbScrollAddr);
            this.Controls.Add(this.cbLayoutAddr);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.cbScrY);
            this.Controls.Add(this.lbd2);
            this.Controls.Add(this.cbScrX);
            this.Controls.Add(this.cbLayoutHeight);
            this.Controls.Add(this.lbd1);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.cbLayoutWidth);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.cbStartLoc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbPalBlinkByte);
            this.Controls.Add(this.cbPallete2No);
            this.Controls.Add(this.cbPalleteNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbBackGfx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbObjGfx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditLevelData";
            this.Text = "Level/Doors Params Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditLevelData_FormClosing);
            this.Load += new System.EventHandler(this.EditLevelData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbObjGfx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBackGfx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbPalleteNo;
        private System.Windows.Forms.ComboBox cbPallete2No;
        private System.Windows.Forms.ComboBox cbPalBlinkByte;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbStartLoc;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cbLayoutWidth;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbd1;
        private System.Windows.Forms.ComboBox cbLayoutHeight;
        private System.Windows.Forms.ComboBox cbScrX;
        private System.Windows.Forms.Label lbd2;
        private System.Windows.Forms.ComboBox cbScrY;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.ComboBox cbLayoutAddr;
        private System.Windows.Forms.ComboBox cbScrollAddr;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbLayoutAddress;
        private System.Windows.Forms.Label lbScrollAddr;
        private System.Windows.Forms.Label lbd3;
        private System.Windows.Forms.Label lbd4;
        private System.Windows.Forms.ComboBox cbPlayerX;
        private System.Windows.Forms.ComboBox cbPlayerY;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.ComboBox cbBigBlocks;
        private System.Windows.Forms.ComboBox cbMusicNo;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.ComboBox cbDirsAddr;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lbDirsAddr;
        private System.Windows.Forms.Button btSave;
    }
}