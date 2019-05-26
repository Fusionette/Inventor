namespace Inventor
{
	partial class Form1
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSet = new System.Windows.Forms.TabPage();
            this.saveJson = new System.Windows.Forms.Button();
            this.loadJson = new System.Windows.Forms.Button();
            this.setBoostsAllowed = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.setIconName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.bonusList = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.boostList = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.createDataFiles = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.slotMinLevel = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.setMaxLevel = new System.Windows.Forms.NumericUpDown();
            this.setMinLevel = new System.Windows.Forms.NumericUpDown();
            this.conversionGroups = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.setGroupName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.setName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.setDisplayName = new System.Windows.Forms.TextBox();
            this.tabBoosts = new System.Windows.Forms.TabPage();
            this.boostSlotRequires = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.boostShortHelp = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.boostDescription = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.boostSave = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.boostAspectList = new System.Windows.Forms.ListBox();
            this.boostAddAspect = new System.Windows.Forms.Button();
            this.boostTypeList = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.boostName = new System.Windows.Forms.TextBox();
            this.boostNameLabel = new System.Windows.Forms.Label();
            this.boostDisplayName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.boostLetter = new System.Windows.Forms.TextBox();
            this.boostLetterLabel = new System.Windows.Forms.Label();
            this.tabRecipes = new System.Windows.Forms.TabPage();
            this.salvageAdd = new System.Windows.Forms.Button();
            this.salvageList = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.salvageTree = new System.Windows.Forms.TreeView();
            this.label24 = new System.Windows.Forms.Label();
            this.recipeRarity = new System.Windows.Forms.ComboBox();
            this.recipeSku = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.recipeTabName = new System.Windows.Forms.TextBox();
            this.tabBonus = new System.Windows.Forms.TabPage();
            this.bonusAddNew = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.bonusRequires = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.bonusAutoPowers = new System.Windows.Forms.TextBox();
            this.bonusMaxBoosts = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.bonusMinBoosts = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.tabSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slotMinLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setMaxLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setMinLevel)).BeginInit();
            this.tabBoosts.SuspendLayout();
            this.tabRecipes.SuspendLayout();
            this.tabBonus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bonusMaxBoosts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonusMinBoosts)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSet);
            this.tabs.Controls.Add(this.tabBoosts);
            this.tabs.Controls.Add(this.tabRecipes);
            this.tabs.Controls.Add(this.tabBonus);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(462, 452);
            this.tabs.TabIndex = 104;
            this.tabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabs_Selected);
            // 
            // tabSet
            // 
            this.tabSet.Controls.Add(this.saveJson);
            this.tabSet.Controls.Add(this.loadJson);
            this.tabSet.Controls.Add(this.setBoostsAllowed);
            this.tabSet.Controls.Add(this.label21);
            this.tabSet.Controls.Add(this.setIconName);
            this.tabSet.Controls.Add(this.label20);
            this.tabSet.Controls.Add(this.bonusList);
            this.tabSet.Controls.Add(this.label9);
            this.tabSet.Controls.Add(this.boostList);
            this.tabSet.Controls.Add(this.label8);
            this.tabSet.Controls.Add(this.createDataFiles);
            this.tabSet.Controls.Add(this.label7);
            this.tabSet.Controls.Add(this.slotMinLevel);
            this.tabSet.Controls.Add(this.label6);
            this.tabSet.Controls.Add(this.label5);
            this.tabSet.Controls.Add(this.setMaxLevel);
            this.tabSet.Controls.Add(this.setMinLevel);
            this.tabSet.Controls.Add(this.conversionGroups);
            this.tabSet.Controls.Add(this.label4);
            this.tabSet.Controls.Add(this.setGroupName);
            this.tabSet.Controls.Add(this.label3);
            this.tabSet.Controls.Add(this.setName);
            this.tabSet.Controls.Add(this.label2);
            this.tabSet.Controls.Add(this.label1);
            this.tabSet.Controls.Add(this.setDisplayName);
            this.tabSet.Location = new System.Drawing.Point(4, 22);
            this.tabSet.Name = "tabSet";
            this.tabSet.Padding = new System.Windows.Forms.Padding(3);
            this.tabSet.Size = new System.Drawing.Size(454, 426);
            this.tabSet.TabIndex = 0;
            this.tabSet.Text = "Set Overview";
            this.tabSet.UseVisualStyleBackColor = true;
            // 
            // saveJson
            // 
            this.saveJson.Location = new System.Drawing.Point(206, 394);
            this.saveJson.Name = "saveJson";
            this.saveJson.Size = new System.Drawing.Size(80, 23);
            this.saveJson.TabIndex = 13;
            this.saveJson.Text = "Save JSON";
            this.saveJson.UseVisualStyleBackColor = true;
            this.saveJson.Click += new System.EventHandler(this.saveJson_Click);
            // 
            // loadJson
            // 
            this.loadJson.Location = new System.Drawing.Point(120, 394);
            this.loadJson.Name = "loadJson";
            this.loadJson.Size = new System.Drawing.Size(80, 23);
            this.loadJson.TabIndex = 12;
            this.loadJson.Text = "Load JSON";
            this.loadJson.UseVisualStyleBackColor = true;
            this.loadJson.Click += new System.EventHandler(this.loadJson_Click);
            // 
            // setBoostsAllowed
            // 
            this.setBoostsAllowed.Location = new System.Drawing.Point(120, 140);
            this.setBoostsAllowed.Name = "setBoostsAllowed";
            this.setBoostsAllowed.Size = new System.Drawing.Size(320, 20);
            this.setBoostsAllowed.TabIndex = 8;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(32, 143);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 13);
            this.label21.TabIndex = 126;
            this.label21.Text = "Boosts Allowed:";
            // 
            // setIconName
            // 
            this.setIconName.Location = new System.Drawing.Point(120, 61);
            this.setIconName.Name = "setIconName";
            this.setIconName.Size = new System.Drawing.Size(320, 20);
            this.setIconName.TabIndex = 3;
            this.setIconName.TextChanged += new System.EventHandler(this.setIconName_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(52, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 13);
            this.label20.TabIndex = 124;
            this.label20.Text = "Icon Name:";
            // 
            // bonusList
            // 
            this.bonusList.DisplayMember = "listDisplay";
            this.bonusList.FormattingEnabled = true;
            this.bonusList.Location = new System.Drawing.Point(120, 293);
            this.bonusList.Name = "bonusList";
            this.bonusList.Size = new System.Drawing.Size(320, 95);
            this.bonusList.TabIndex = 11;
            this.bonusList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bonusList_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(57, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 122;
            this.label9.Text = "Set Bonus:";
            // 
            // boostList
            // 
            this.boostList.DisplayMember = "listDisplay";
            this.boostList.FormattingEnabled = true;
            this.boostList.Location = new System.Drawing.Point(120, 192);
            this.boostList.Name = "boostList";
            this.boostList.Size = new System.Drawing.Size(320, 95);
            this.boostList.TabIndex = 10;
            this.boostList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.boostList_KeyDown);
            this.boostList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.boostList_MouseDoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 121;
            this.label8.Text = "Enhancements:";
            // 
            // createDataFiles
            // 
            this.createDataFiles.Location = new System.Drawing.Point(340, 394);
            this.createDataFiles.Name = "createDataFiles";
            this.createDataFiles.Size = new System.Drawing.Size(100, 23);
            this.createDataFiles.TabIndex = 14;
            this.createDataFiles.Text = "Create Data Files";
            this.createDataFiles.UseVisualStyleBackColor = true;
            this.createDataFiles.Click += new System.EventHandler(this.createDataFiles_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 120;
            this.label7.Text = "Min Slot Level:";
            // 
            // slotMinLevel
            // 
            this.slotMinLevel.Location = new System.Drawing.Point(390, 87);
            this.slotMinLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.slotMinLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.slotMinLevel.Name = "slotMinLevel";
            this.slotMinLevel.Size = new System.Drawing.Size(50, 20);
            this.slotMinLevel.TabIndex = 6;
            this.slotMinLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.slotMinLevel.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 119;
            this.label6.Text = "Max Level:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 118;
            this.label5.Text = "Minimum Level:";
            // 
            // setMaxLevel
            // 
            this.setMaxLevel.Location = new System.Drawing.Point(246, 87);
            this.setMaxLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.setMaxLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.setMaxLevel.Name = "setMaxLevel";
            this.setMaxLevel.Size = new System.Drawing.Size(50, 20);
            this.setMaxLevel.TabIndex = 5;
            this.setMaxLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.setMaxLevel.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // setMinLevel
            // 
            this.setMinLevel.Location = new System.Drawing.Point(120, 87);
            this.setMinLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.setMinLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.setMinLevel.Name = "setMinLevel";
            this.setMinLevel.Size = new System.Drawing.Size(50, 20);
            this.setMinLevel.TabIndex = 4;
            this.setMinLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.setMinLevel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.setMinLevel.ValueChanged += new System.EventHandler(this.setMinLevel_ValueChanged);
            // 
            // conversionGroups
            // 
            this.conversionGroups.Location = new System.Drawing.Point(120, 166);
            this.conversionGroups.Name = "conversionGroups";
            this.conversionGroups.Size = new System.Drawing.Size(320, 20);
            this.conversionGroups.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 114;
            this.label4.Text = "Conversion Groups:";
            // 
            // setGroupName
            // 
            this.setGroupName.DisplayMember = "displayName";
            this.setGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.setGroupName.FormattingEnabled = true;
            this.setGroupName.Location = new System.Drawing.Point(120, 113);
            this.setGroupName.Name = "setGroupName";
            this.setGroupName.Size = new System.Drawing.Size(320, 21);
            this.setGroupName.TabIndex = 7;
            this.setGroupName.SelectedIndexChanged += new System.EventHandler(this.setGroupName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Set Group:";
            // 
            // setName
            // 
            this.setName.Location = new System.Drawing.Point(120, 35);
            this.setName.Name = "setName";
            this.setName.Size = new System.Drawing.Size(320, 20);
            this.setName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 107;
            this.label2.Text = "Internal Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Set Name:";
            // 
            // setDisplayName
            // 
            this.setDisplayName.Location = new System.Drawing.Point(120, 9);
            this.setDisplayName.Name = "setDisplayName";
            this.setDisplayName.Size = new System.Drawing.Size(320, 20);
            this.setDisplayName.TabIndex = 1;
            this.setDisplayName.TextChanged += new System.EventHandler(this.setDisplayName_TextChanged);
            // 
            // tabBoosts
            // 
            this.tabBoosts.Controls.Add(this.boostSlotRequires);
            this.tabBoosts.Controls.Add(this.label26);
            this.tabBoosts.Controls.Add(this.boostShortHelp);
            this.tabBoosts.Controls.Add(this.label25);
            this.tabBoosts.Controls.Add(this.boostDescription);
            this.tabBoosts.Controls.Add(this.label19);
            this.tabBoosts.Controls.Add(this.boostSave);
            this.tabBoosts.Controls.Add(this.label14);
            this.tabBoosts.Controls.Add(this.boostAspectList);
            this.tabBoosts.Controls.Add(this.boostAddAspect);
            this.tabBoosts.Controls.Add(this.boostTypeList);
            this.tabBoosts.Controls.Add(this.label18);
            this.tabBoosts.Controls.Add(this.boostName);
            this.tabBoosts.Controls.Add(this.boostNameLabel);
            this.tabBoosts.Controls.Add(this.boostDisplayName);
            this.tabBoosts.Controls.Add(this.label16);
            this.tabBoosts.Controls.Add(this.boostLetter);
            this.tabBoosts.Controls.Add(this.boostLetterLabel);
            this.tabBoosts.Location = new System.Drawing.Point(4, 22);
            this.tabBoosts.Name = "tabBoosts";
            this.tabBoosts.Padding = new System.Windows.Forms.Padding(3);
            this.tabBoosts.Size = new System.Drawing.Size(454, 426);
            this.tabBoosts.TabIndex = 1;
            this.tabBoosts.Text = "Enhancement";
            this.tabBoosts.UseVisualStyleBackColor = true;
            // 
            // boostSlotRequires
            // 
            this.boostSlotRequires.Location = new System.Drawing.Point(120, 344);
            this.boostSlotRequires.Name = "boostSlotRequires";
            this.boostSlotRequires.Size = new System.Drawing.Size(320, 20);
            this.boostSlotRequires.TabIndex = 119;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(41, 347);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(73, 13);
            this.label26.TabIndex = 120;
            this.label26.Text = "Slot Requires:";
            // 
            // boostShortHelp
            // 
            this.boostShortHelp.Location = new System.Drawing.Point(120, 115);
            this.boostShortHelp.Name = "boostShortHelp";
            this.boostShortHelp.Size = new System.Drawing.Size(320, 20);
            this.boostShortHelp.TabIndex = 6;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(54, 118);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 13);
            this.label25.TabIndex = 118;
            this.label25.Text = "Short Help:";
            // 
            // boostDescription
            // 
            this.boostDescription.Location = new System.Drawing.Point(120, 141);
            this.boostDescription.Multiline = true;
            this.boostDescription.Name = "boostDescription";
            this.boostDescription.Size = new System.Drawing.Size(320, 96);
            this.boostDescription.TabIndex = 7;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(51, 144);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 13);
            this.label19.TabIndex = 116;
            this.label19.Text = "Description:";
            // 
            // boostSave
            // 
            this.boostSave.Location = new System.Drawing.Point(120, 370);
            this.boostSave.Name = "boostSave";
            this.boostSave.Size = new System.Drawing.Size(320, 23);
            this.boostSave.TabIndex = 9;
            this.boostSave.Text = "Add or Edit Enhancement";
            this.boostSave.UseVisualStyleBackColor = true;
            this.boostSave.Click += new System.EventHandler(this.boostSave_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(66, 245);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 114;
            this.label14.Text = "Aspects:";
            // 
            // boostAspectList
            // 
            this.boostAspectList.DisplayMember = "displayName";
            this.boostAspectList.FormattingEnabled = true;
            this.boostAspectList.Location = new System.Drawing.Point(120, 243);
            this.boostAspectList.Name = "boostAspectList";
            this.boostAspectList.Size = new System.Drawing.Size(320, 95);
            this.boostAspectList.TabIndex = 8;
            this.boostAspectList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.boostAspectList_KeyDown);
            // 
            // boostAddAspect
            // 
            this.boostAddAspect.Location = new System.Drawing.Point(390, 60);
            this.boostAddAspect.Name = "boostAddAspect";
            this.boostAddAspect.Size = new System.Drawing.Size(50, 23);
            this.boostAddAspect.TabIndex = 4;
            this.boostAddAspect.Text = "Add";
            this.boostAddAspect.UseVisualStyleBackColor = true;
            this.boostAddAspect.Click += new System.EventHandler(this.boostAddAspect_Click);
            // 
            // boostTypeList
            // 
            this.boostTypeList.DisplayMember = "displayName";
            this.boostTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boostTypeList.FormattingEnabled = true;
            this.boostTypeList.Location = new System.Drawing.Point(120, 61);
            this.boostTypeList.Name = "boostTypeList";
            this.boostTypeList.Size = new System.Drawing.Size(264, 21);
            this.boostTypeList.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(38, 65);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 13);
            this.label18.TabIndex = 113;
            this.label18.Text = "Select Aspect:";
            // 
            // boostName
            // 
            this.boostName.Location = new System.Drawing.Point(120, 35);
            this.boostName.Name = "boostName";
            this.boostName.ReadOnly = true;
            this.boostName.Size = new System.Drawing.Size(320, 20);
            this.boostName.TabIndex = 2;
            // 
            // boostNameLabel
            // 
            this.boostNameLabel.AutoSize = true;
            this.boostNameLabel.Location = new System.Drawing.Point(38, 38);
            this.boostNameLabel.Name = "boostNameLabel";
            this.boostNameLabel.Size = new System.Drawing.Size(76, 13);
            this.boostNameLabel.TabIndex = 111;
            this.boostNameLabel.Text = "Internal Name:";
            // 
            // boostDisplayName
            // 
            this.boostDisplayName.Location = new System.Drawing.Point(120, 89);
            this.boostDisplayName.Name = "boostDisplayName";
            this.boostDisplayName.Size = new System.Drawing.Size(320, 20);
            this.boostDisplayName.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(107, 13);
            this.label16.TabIndex = 109;
            this.label16.Text = "Enhancement Name:";
            // 
            // boostLetter
            // 
            this.boostLetter.Location = new System.Drawing.Point(120, 9);
            this.boostLetter.MaxLength = 1;
            this.boostLetter.Name = "boostLetter";
            this.boostLetter.Size = new System.Drawing.Size(50, 20);
            this.boostLetter.TabIndex = 1;
            this.boostLetter.TextChanged += new System.EventHandler(this.boostLetter_TextChanged);
            // 
            // boostLetterLabel
            // 
            this.boostLetterLabel.AutoSize = true;
            this.boostLetterLabel.Location = new System.Drawing.Point(19, 12);
            this.boostLetterLabel.Name = "boostLetterLabel";
            this.boostLetterLabel.Size = new System.Drawing.Size(95, 13);
            this.boostLetterLabel.TabIndex = 2;
            this.boostLetterLabel.Text = "Enhancement A-F:";
            // 
            // tabRecipes
            // 
            this.tabRecipes.Controls.Add(this.salvageAdd);
            this.tabRecipes.Controls.Add(this.salvageList);
            this.tabRecipes.Controls.Add(this.label17);
            this.tabRecipes.Controls.Add(this.label15);
            this.tabRecipes.Controls.Add(this.salvageTree);
            this.tabRecipes.Controls.Add(this.label24);
            this.tabRecipes.Controls.Add(this.recipeRarity);
            this.tabRecipes.Controls.Add(this.recipeSku);
            this.tabRecipes.Controls.Add(this.label23);
            this.tabRecipes.Controls.Add(this.label22);
            this.tabRecipes.Controls.Add(this.recipeTabName);
            this.tabRecipes.Location = new System.Drawing.Point(4, 22);
            this.tabRecipes.Name = "tabRecipes";
            this.tabRecipes.Size = new System.Drawing.Size(454, 426);
            this.tabRecipes.TabIndex = 3;
            this.tabRecipes.Text = "Recipes";
            this.tabRecipes.UseVisualStyleBackColor = true;
            // 
            // salvageAdd
            // 
            this.salvageAdd.Location = new System.Drawing.Point(390, 60);
            this.salvageAdd.Name = "salvageAdd";
            this.salvageAdd.Size = new System.Drawing.Size(50, 23);
            this.salvageAdd.TabIndex = 11;
            this.salvageAdd.Text = "Add";
            this.salvageAdd.UseVisualStyleBackColor = true;
            this.salvageAdd.Click += new System.EventHandler(this.salvageAdd_Click);
            // 
            // salvageList
            // 
            this.salvageList.DisplayMember = "listDisplay";
            this.salvageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.salvageList.FormattingEnabled = true;
            this.salvageList.Location = new System.Drawing.Point(120, 61);
            this.salvageList.Name = "salvageList";
            this.salvageList.Size = new System.Drawing.Size(264, 21);
            this.salvageList.TabIndex = 10;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(32, 65);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 128;
            this.label17.Text = "Select Salvage:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(65, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 125;
            this.label15.Text = "Salvage:";
            // 
            // salvageTree
            // 
            this.salvageTree.Location = new System.Drawing.Point(120, 89);
            this.salvageTree.Name = "salvageTree";
            this.salvageTree.Size = new System.Drawing.Size(320, 197);
            this.salvageTree.TabIndex = 12;
            this.salvageTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.salvageTree_NodeMouseClick);
            this.salvageTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.salvageTree_KeyDown);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(257, 320);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 13);
            this.label24.TabIndex = 112;
            this.label24.Text = "Set Rarity:";
            // 
            // recipeRarity
            // 
            this.recipeRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.recipeRarity.FormattingEnabled = true;
            this.recipeRarity.Items.AddRange(new object[] {
            "Uncommon",
            "Rare",
            "VeryRare"});
            this.recipeRarity.Location = new System.Drawing.Point(319, 317);
            this.recipeRarity.Name = "recipeRarity";
            this.recipeRarity.Size = new System.Drawing.Size(121, 21);
            this.recipeRarity.TabIndex = 15;
            // 
            // recipeSku
            // 
            this.recipeSku.Location = new System.Drawing.Point(120, 318);
            this.recipeSku.MaxLength = 7;
            this.recipeSku.Name = "recipeSku";
            this.recipeSku.Size = new System.Drawing.Size(100, 20);
            this.recipeSku.TabIndex = 14;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(39, 321);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 13);
            this.label23.TabIndex = 109;
            this.label23.Text = "Store Product:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(22, 295);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(92, 13);
            this.label22.TabIndex = 108;
            this.label22.Text = "Merit Vendor Tab:";
            // 
            // recipeTabName
            // 
            this.recipeTabName.Location = new System.Drawing.Point(120, 292);
            this.recipeTabName.Name = "recipeTabName";
            this.recipeTabName.Size = new System.Drawing.Size(320, 20);
            this.recipeTabName.TabIndex = 13;
            // 
            // tabBonus
            // 
            this.tabBonus.Controls.Add(this.bonusAddNew);
            this.tabBonus.Controls.Add(this.label13);
            this.tabBonus.Controls.Add(this.bonusRequires);
            this.tabBonus.Controls.Add(this.label12);
            this.tabBonus.Controls.Add(this.bonusAutoPowers);
            this.tabBonus.Controls.Add(this.bonusMaxBoosts);
            this.tabBonus.Controls.Add(this.label11);
            this.tabBonus.Controls.Add(this.bonusMinBoosts);
            this.tabBonus.Controls.Add(this.label10);
            this.tabBonus.Location = new System.Drawing.Point(4, 22);
            this.tabBonus.Name = "tabBonus";
            this.tabBonus.Size = new System.Drawing.Size(454, 426);
            this.tabBonus.TabIndex = 2;
            this.tabBonus.Text = "Set Bonus";
            this.tabBonus.UseVisualStyleBackColor = true;
            // 
            // bonusAddNew
            // 
            this.bonusAddNew.Location = new System.Drawing.Point(120, 88);
            this.bonusAddNew.Name = "bonusAddNew";
            this.bonusAddNew.Size = new System.Drawing.Size(320, 23);
            this.bonusAddNew.TabIndex = 5;
            this.bonusAddNew.Text = "Add New Set Bonus";
            this.bonusAddNew.UseVisualStyleBackColor = true;
            this.bonusAddNew.Click += new System.EventHandler(this.bonusAddNew_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(62, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Requires:";
            // 
            // bonusRequires
            // 
            this.bonusRequires.Location = new System.Drawing.Point(120, 61);
            this.bonusRequires.Name = "bonusRequires";
            this.bonusRequires.Size = new System.Drawing.Size(320, 20);
            this.bonusRequires.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Bonus Power:";
            // 
            // bonusAutoPowers
            // 
            this.bonusAutoPowers.Location = new System.Drawing.Point(120, 35);
            this.bonusAutoPowers.Name = "bonusAutoPowers";
            this.bonusAutoPowers.Size = new System.Drawing.Size(320, 20);
            this.bonusAutoPowers.TabIndex = 3;
            // 
            // bonusMaxBoosts
            // 
            this.bonusMaxBoosts.Location = new System.Drawing.Point(390, 8);
            this.bonusMaxBoosts.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.bonusMaxBoosts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bonusMaxBoosts.Name = "bonusMaxBoosts";
            this.bonusMaxBoosts.Size = new System.Drawing.Size(50, 20);
            this.bonusMaxBoosts.TabIndex = 2;
            this.bonusMaxBoosts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bonusMaxBoosts.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(295, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Maximum Boosts:";
            // 
            // bonusMinBoosts
            // 
            this.bonusMinBoosts.Location = new System.Drawing.Point(120, 9);
            this.bonusMinBoosts.Name = "bonusMinBoosts";
            this.bonusMinBoosts.Size = new System.Drawing.Size(50, 20);
            this.bonusMinBoosts.TabIndex = 1;
            this.bonusMinBoosts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bonusMinBoosts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Minimum Boosts:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 475);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "Inventor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabs.ResumeLayout(false);
            this.tabSet.ResumeLayout(false);
            this.tabSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slotMinLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setMaxLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setMinLevel)).EndInit();
            this.tabBoosts.ResumeLayout(false);
            this.tabBoosts.PerformLayout();
            this.tabRecipes.ResumeLayout(false);
            this.tabRecipes.PerformLayout();
            this.tabBonus.ResumeLayout(false);
            this.tabBonus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bonusMaxBoosts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonusMinBoosts)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabSet;
		private System.Windows.Forms.ListBox bonusList;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox boostList;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button createDataFiles;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown slotMinLevel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown setMaxLevel;
		private System.Windows.Forms.NumericUpDown setMinLevel;
		private System.Windows.Forms.TextBox conversionGroups;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox setGroupName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox setName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox setDisplayName;
		private System.Windows.Forms.TabPage tabBoosts;
		private System.Windows.Forms.TabPage tabBonus;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox bonusRequires;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox bonusAutoPowers;
		private System.Windows.Forms.NumericUpDown bonusMaxBoosts;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown bonusMinBoosts;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button bonusAddNew;
		private System.Windows.Forms.TextBox boostLetter;
		private System.Windows.Forms.Label boostLetterLabel;
		private System.Windows.Forms.TextBox boostDisplayName;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox boostName;
		private System.Windows.Forms.Label boostNameLabel;
		private System.Windows.Forms.ComboBox boostTypeList;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button boostAddAspect;
		private System.Windows.Forms.ListBox boostAspectList;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button boostSave;
		private System.Windows.Forms.TextBox boostDescription;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox setIconName;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox setBoostsAllowed;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TabPage tabRecipes;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox recipeTabName;
		private System.Windows.Forms.TextBox recipeSku;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.ComboBox recipeRarity;
		private System.Windows.Forms.Button loadJson;
		private System.Windows.Forms.Button saveJson;
		private System.Windows.Forms.TextBox boostShortHelp;
		private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TreeView salvageTree;
        private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button salvageAdd;
		private System.Windows.Forms.ComboBox salvageList;
		private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox boostSlotRequires;
        private System.Windows.Forms.Label label26;
    }
}

