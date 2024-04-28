namespace MinerInfoApp
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.rebootButton = new MaterialSkin.Controls.MaterialButton();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox2 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox3 = new MaterialSkin.Controls.MaterialCheckbox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.tabControl = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ipRangeListView = new System.Windows.Forms.ListView();
            this.StartIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scanSelectedButton = new MaterialSkin.Controls.MaterialButton();
            this.loadIPRangesButton = new MaterialSkin.Controls.MaterialButton();
            this.deleteIPRanges = new MaterialSkin.Controls.MaterialButton();
            this.saveRangesButton = new MaterialSkin.Controls.MaterialButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel13 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel12 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel11 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel10 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            this.startIPTextBoxD = new MaterialSkin.Controls.MaterialTextBox();
            this.startIPTextBoxC = new MaterialSkin.Controls.MaterialTextBox();
            this.startIPTextBoxB = new MaterialSkin.Controls.MaterialTextBox();
            this.startIPTextBoxA = new MaterialSkin.Controls.MaterialTextBox();
            this.startIPLabel = new MaterialSkin.Controls.MaterialLabel();
            this.stopScanButton = new MaterialSkin.Controls.MaterialButton();
            this.nameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.addIPButton = new MaterialSkin.Controls.MaterialButton();
            this.searchButton = new MaterialSkin.Controls.MaterialButton();
            this.nameTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.endIPLabel = new MaterialSkin.Controls.MaterialLabel();
            this.endIPTextBoxA = new MaterialSkin.Controls.MaterialTextBox();
            this.endIPTextBoxB = new MaterialSkin.Controls.MaterialTextBox();
            this.endIPTextBoxC = new MaterialSkin.Controls.MaterialTextBox();
            this.endIPTextBoxD = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.stopListeningButtons = new MaterialSkin.Controls.MaterialButton();
            this.startListeningButton = new MaterialSkin.Controls.MaterialButton();
            this.newIpTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.performanceButton = new MaterialSkin.Controls.MaterialButton();
            this.setStaticIPButton = new MaterialSkin.Controls.MaterialButton();
            this.newStaticLabel = new MaterialSkin.Controls.MaterialLabel();
            this.newPasswordTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.poolPasswordLabel = new MaterialSkin.Controls.MaterialLabel();
            this.selfCheckButton = new MaterialSkin.Controls.MaterialButton();
            this.setPasswordButton = new MaterialSkin.Controls.MaterialButton();
            this.minerPasswordLabel = new MaterialSkin.Controls.MaterialLabel();
            this.openInBrowserButton = new MaterialSkin.Controls.MaterialButton();
            this.setDynamicIPButton = new MaterialSkin.Controls.MaterialButton();
            this.performanceDropdown = new MaterialSkin.Controls.MaterialComboBox();
            this.setPoolButton = new MaterialSkin.Controls.MaterialButton();
            this.updateButton = new MaterialSkin.Controls.MaterialButton();
            this.poolAccountLabel = new MaterialSkin.Controls.MaterialLabel();
            this.poolUserTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.poolPasswordTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.poolUrlTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.poolURLLabel = new MaterialSkin.Controls.MaterialLabel();
            this.minerFoundCountLabel = new MaterialSkin.Controls.MaterialLabel();
            this.minerListView = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Pool = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PoolUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Account = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DAG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SelfCheckProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HashboardStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AcceptanceRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Accepted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rejected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MacAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Uptime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FanSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Firmware = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard1Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard1Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard1Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard2Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard2Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard2Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard3Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard3Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard3Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.translate = new MaterialSkin.Controls.MaterialButton();
            this.ScanningIPLabel = new MaterialSkin.Controls.MaterialLabel();
            this.loadIPRangesDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveIPRangesDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // rebootButton
            // 
            resources.ApplyResources(this.rebootButton, "rebootButton");
            this.rebootButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rebootButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.rebootButton.Depth = 0;
            this.rebootButton.HighEmphasis = true;
            this.rebootButton.Icon = null;
            this.rebootButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.rebootButton.Name = "rebootButton";
            this.rebootButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.rebootButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.rebootButton.UseAccentColor = false;
            this.rebootButton.UseVisualStyleBackColor = true;
            this.rebootButton.Click += new System.EventHandler(this.rebootButton_Click);
            // 
            // materialCheckbox1
            // 
            this.materialCheckbox1.Depth = 0;
            resources.ApplyResources(this.materialCheckbox1, "materialCheckbox1");
            this.materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox1.Name = "materialCheckbox1";
            this.materialCheckbox1.ReadOnly = false;
            this.materialCheckbox1.Ripple = true;
            this.materialCheckbox1.UseVisualStyleBackColor = true;
            // 
            // materialCheckbox2
            // 
            this.materialCheckbox2.Depth = 0;
            resources.ApplyResources(this.materialCheckbox2, "materialCheckbox2");
            this.materialCheckbox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox2.Name = "materialCheckbox2";
            this.materialCheckbox2.ReadOnly = false;
            this.materialCheckbox2.Ripple = true;
            this.materialCheckbox2.UseVisualStyleBackColor = true;
            // 
            // materialCheckbox3
            // 
            this.materialCheckbox3.Depth = 0;
            resources.ApplyResources(this.materialCheckbox3, "materialCheckbox3");
            this.materialCheckbox3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox3.Name = "materialCheckbox3";
            this.materialCheckbox3.ReadOnly = false;
            this.materialCheckbox3.Ripple = true;
            this.materialCheckbox3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.materialTabSelector1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.minerFoundCountLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.minerListView, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.tabControl;
            this.materialTabSelector1.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            this.materialTabSelector1.Depth = 0;
            resources.ApplyResources(this.materialTabSelector1, "materialTabSelector1");
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Depth = 0;
            this.tabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel6);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.ipRangeListView, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.scanSelectedButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.loadIPRangesButton, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.deleteIPRanges, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.saveRangesButton, 2, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // ipRangeListView
            // 
            this.ipRangeListView.CheckBoxes = true;
            this.ipRangeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StartIP,
            this.EndIP,
            this.groupName});
            resources.ApplyResources(this.ipRangeListView, "ipRangeListView");
            this.ipRangeListView.HideSelection = false;
            this.ipRangeListView.Name = "ipRangeListView";
            this.tableLayoutPanel3.SetRowSpan(this.ipRangeListView, 2);
            this.ipRangeListView.TabStop = false;
            this.ipRangeListView.UseCompatibleStateImageBehavior = false;
            this.ipRangeListView.View = System.Windows.Forms.View.Details;
            // 
            // StartIP
            // 
            resources.ApplyResources(this.StartIP, "StartIP");
            // 
            // EndIP
            // 
            resources.ApplyResources(this.EndIP, "EndIP");
            // 
            // groupName
            // 
            resources.ApplyResources(this.groupName, "groupName");
            // 
            // scanSelectedButton
            // 
            resources.ApplyResources(this.scanSelectedButton, "scanSelectedButton");
            this.scanSelectedButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scanSelectedButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.scanSelectedButton.Depth = 0;
            this.scanSelectedButton.HighEmphasis = true;
            this.scanSelectedButton.Icon = null;
            this.scanSelectedButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.scanSelectedButton.Name = "scanSelectedButton";
            this.scanSelectedButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.scanSelectedButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.scanSelectedButton.UseAccentColor = false;
            this.scanSelectedButton.UseVisualStyleBackColor = true;
            this.scanSelectedButton.Click += new System.EventHandler(this.scanSelectedButton_Click);
            // 
            // loadIPRangesButton
            // 
            resources.ApplyResources(this.loadIPRangesButton, "loadIPRangesButton");
            this.loadIPRangesButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.loadIPRangesButton.Depth = 0;
            this.loadIPRangesButton.HighEmphasis = true;
            this.loadIPRangesButton.Icon = null;
            this.loadIPRangesButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.loadIPRangesButton.Name = "loadIPRangesButton";
            this.loadIPRangesButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.loadIPRangesButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.loadIPRangesButton.UseAccentColor = false;
            this.loadIPRangesButton.UseVisualStyleBackColor = true;
            this.loadIPRangesButton.Click += new System.EventHandler(this.loadIPRangesButton_Click);
            // 
            // deleteIPRanges
            // 
            resources.ApplyResources(this.deleteIPRanges, "deleteIPRanges");
            this.deleteIPRanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteIPRanges.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.deleteIPRanges.Depth = 0;
            this.deleteIPRanges.HighEmphasis = true;
            this.deleteIPRanges.Icon = null;
            this.deleteIPRanges.MouseState = MaterialSkin.MouseState.HOVER;
            this.deleteIPRanges.Name = "deleteIPRanges";
            this.deleteIPRanges.NoAccentTextColor = System.Drawing.Color.Empty;
            this.deleteIPRanges.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.deleteIPRanges.UseAccentColor = false;
            this.deleteIPRanges.UseVisualStyleBackColor = true;
            this.deleteIPRanges.Click += new System.EventHandler(this.deleteIPRanges_Click);
            // 
            // saveRangesButton
            // 
            resources.ApplyResources(this.saveRangesButton, "saveRangesButton");
            this.saveRangesButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveRangesButton.Depth = 0;
            this.saveRangesButton.HighEmphasis = true;
            this.saveRangesButton.Icon = null;
            this.saveRangesButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveRangesButton.Name = "saveRangesButton";
            this.saveRangesButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveRangesButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.saveRangesButton.UseAccentColor = false;
            this.saveRangesButton.UseVisualStyleBackColor = true;
            this.saveRangesButton.Click += new System.EventHandler(this.saveRangesButton_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.materialLabel6, 15, 0);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel5, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel13, 15, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel12, 13, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel11, 11, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel10, 9, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel8, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel7, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel9, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.startIPTextBoxD, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.startIPTextBoxC, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.startIPTextBoxB, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.startIPTextBoxA, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.startIPLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.stopScanButton, 19, 1);
            this.tableLayoutPanel2.Controls.Add(this.nameLabel, 16, 0);
            this.tableLayoutPanel2.Controls.Add(this.addIPButton, 18, 1);
            this.tableLayoutPanel2.Controls.Add(this.searchButton, 17, 1);
            this.tableLayoutPanel2.Controls.Add(this.nameTextBox, 16, 1);
            this.tableLayoutPanel2.Controls.Add(this.endIPLabel, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.endIPTextBoxA, 8, 1);
            this.tableLayoutPanel2.Controls.Add(this.endIPTextBoxB, 10, 1);
            this.tableLayoutPanel2.Controls.Add(this.endIPTextBoxC, 12, 1);
            this.tableLayoutPanel2.Controls.Add(this.endIPTextBoxD, 14, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel4, 7, 1);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // materialLabel6
            // 
            resources.ApplyResources(this.materialLabel6, "materialLabel6");
            this.materialLabel6.Depth = 0;
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            // 
            // materialLabel5
            // 
            resources.ApplyResources(this.materialLabel5, "materialLabel5");
            this.materialLabel5.Depth = 0;
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            // 
            // materialLabel13
            // 
            resources.ApplyResources(this.materialLabel13, "materialLabel13");
            this.materialLabel13.Depth = 0;
            this.materialLabel13.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel13.Name = "materialLabel13";
            // 
            // materialLabel12
            // 
            resources.ApplyResources(this.materialLabel12, "materialLabel12");
            this.materialLabel12.Depth = 0;
            this.materialLabel12.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel12.Name = "materialLabel12";
            // 
            // materialLabel11
            // 
            resources.ApplyResources(this.materialLabel11, "materialLabel11");
            this.materialLabel11.Depth = 0;
            this.materialLabel11.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel11.Name = "materialLabel11";
            // 
            // materialLabel10
            // 
            resources.ApplyResources(this.materialLabel10, "materialLabel10");
            this.materialLabel10.Depth = 0;
            this.materialLabel10.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel10.Name = "materialLabel10";
            // 
            // materialLabel8
            // 
            resources.ApplyResources(this.materialLabel8, "materialLabel8");
            this.materialLabel8.Depth = 0;
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            // 
            // materialLabel7
            // 
            resources.ApplyResources(this.materialLabel7, "materialLabel7");
            this.materialLabel7.Depth = 0;
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            // 
            // materialLabel9
            // 
            resources.ApplyResources(this.materialLabel9, "materialLabel9");
            this.materialLabel9.Depth = 0;
            this.materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            // 
            // startIPTextBoxD
            // 
            this.startIPTextBoxD.AnimateReadOnly = false;
            this.startIPTextBoxD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startIPTextBoxD.Depth = 0;
            resources.ApplyResources(this.startIPTextBoxD, "startIPTextBoxD");
            this.startIPTextBoxD.LeadingIcon = null;
            this.startIPTextBoxD.LeaveOnEnterKey = true;
            this.startIPTextBoxD.MouseState = MaterialSkin.MouseState.OUT;
            this.startIPTextBoxD.Name = "startIPTextBoxD";
            this.startIPTextBoxD.TrailingIcon = null;
            this.startIPTextBoxD.TextChanged += new System.EventHandler(this.startIPTextBoxD_TextChanged);
            // 
            // startIPTextBoxC
            // 
            this.startIPTextBoxC.AnimateReadOnly = false;
            this.startIPTextBoxC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startIPTextBoxC.Depth = 0;
            resources.ApplyResources(this.startIPTextBoxC, "startIPTextBoxC");
            this.startIPTextBoxC.LeadingIcon = null;
            this.startIPTextBoxC.LeaveOnEnterKey = true;
            this.startIPTextBoxC.MouseState = MaterialSkin.MouseState.OUT;
            this.startIPTextBoxC.Name = "startIPTextBoxC";
            this.startIPTextBoxC.TrailingIcon = null;
            this.startIPTextBoxC.TextChanged += new System.EventHandler(this.startIPTextBoxC_TextChanged);
            // 
            // startIPTextBoxB
            // 
            this.startIPTextBoxB.AnimateReadOnly = false;
            this.startIPTextBoxB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startIPTextBoxB.Depth = 0;
            resources.ApplyResources(this.startIPTextBoxB, "startIPTextBoxB");
            this.startIPTextBoxB.LeadingIcon = null;
            this.startIPTextBoxB.LeaveOnEnterKey = true;
            this.startIPTextBoxB.MouseState = MaterialSkin.MouseState.OUT;
            this.startIPTextBoxB.Name = "startIPTextBoxB";
            this.startIPTextBoxB.TrailingIcon = null;
            this.startIPTextBoxB.TextChanged += new System.EventHandler(this.startIPTextBoxB_TextChanged);
            // 
            // startIPTextBoxA
            // 
            this.startIPTextBoxA.AnimateReadOnly = false;
            this.startIPTextBoxA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startIPTextBoxA.Depth = 0;
            resources.ApplyResources(this.startIPTextBoxA, "startIPTextBoxA");
            this.startIPTextBoxA.LeadingIcon = null;
            this.startIPTextBoxA.LeaveOnEnterKey = true;
            this.startIPTextBoxA.MouseState = MaterialSkin.MouseState.OUT;
            this.startIPTextBoxA.Name = "startIPTextBoxA";
            this.startIPTextBoxA.TrailingIcon = null;
            this.startIPTextBoxA.TextChanged += new System.EventHandler(this.startIPTextBoxA_TextChanged);
            // 
            // startIPLabel
            // 
            resources.ApplyResources(this.startIPLabel, "startIPLabel");
            this.tableLayoutPanel2.SetColumnSpan(this.startIPLabel, 7);
            this.startIPLabel.Depth = 0;
            this.startIPLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.startIPLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.startIPLabel.Name = "startIPLabel";
            // 
            // stopScanButton
            // 
            resources.ApplyResources(this.stopScanButton, "stopScanButton");
            this.stopScanButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.stopScanButton.Depth = 0;
            this.stopScanButton.HighEmphasis = true;
            this.stopScanButton.Icon = null;
            this.stopScanButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.stopScanButton.Name = "stopScanButton";
            this.stopScanButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.stopScanButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.stopScanButton.UseAccentColor = false;
            this.stopScanButton.UseVisualStyleBackColor = true;
            this.stopScanButton.Click += new System.EventHandler(this.stopScanButton_Click);
            // 
            // nameLabel
            // 
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Depth = 0;
            this.nameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.nameLabel.Name = "nameLabel";
            // 
            // addIPButton
            // 
            resources.ApplyResources(this.addIPButton, "addIPButton");
            this.addIPButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addIPButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.addIPButton.Depth = 0;
            this.addIPButton.HighEmphasis = true;
            this.addIPButton.Icon = null;
            this.addIPButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.addIPButton.Name = "addIPButton";
            this.addIPButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.addIPButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.addIPButton.UseAccentColor = false;
            this.addIPButton.UseVisualStyleBackColor = true;
            this.addIPButton.Click += new System.EventHandler(this.addIPButton_Click);
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.searchButton.Depth = 0;
            this.searchButton.HighEmphasis = true;
            this.searchButton.Icon = null;
            this.searchButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.searchButton.Name = "searchButton";
            this.searchButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.searchButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.searchButton.UseAccentColor = false;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.AnimateReadOnly = false;
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.Depth = 0;
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.LeadingIcon = null;
            this.nameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.TrailingIcon = null;
            // 
            // endIPLabel
            // 
            resources.ApplyResources(this.endIPLabel, "endIPLabel");
            this.tableLayoutPanel2.SetColumnSpan(this.endIPLabel, 7);
            this.endIPLabel.Depth = 0;
            this.endIPLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.endIPLabel.Name = "endIPLabel";
            // 
            // endIPTextBoxA
            // 
            this.endIPTextBoxA.AnimateReadOnly = false;
            this.endIPTextBoxA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endIPTextBoxA.Depth = 0;
            resources.ApplyResources(this.endIPTextBoxA, "endIPTextBoxA");
            this.endIPTextBoxA.LeadingIcon = null;
            this.endIPTextBoxA.LeaveOnEnterKey = true;
            this.endIPTextBoxA.MouseState = MaterialSkin.MouseState.OUT;
            this.endIPTextBoxA.Name = "endIPTextBoxA";
            this.endIPTextBoxA.TrailingIcon = null;
            this.endIPTextBoxA.TextChanged += new System.EventHandler(this.endIPTextBoxA_TextChanged);
            // 
            // endIPTextBoxB
            // 
            this.endIPTextBoxB.AnimateReadOnly = false;
            this.endIPTextBoxB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endIPTextBoxB.Depth = 0;
            resources.ApplyResources(this.endIPTextBoxB, "endIPTextBoxB");
            this.endIPTextBoxB.LeadingIcon = null;
            this.endIPTextBoxB.LeaveOnEnterKey = true;
            this.endIPTextBoxB.MouseState = MaterialSkin.MouseState.OUT;
            this.endIPTextBoxB.Name = "endIPTextBoxB";
            this.endIPTextBoxB.TrailingIcon = null;
            this.endIPTextBoxB.TextChanged += new System.EventHandler(this.endIPTextBoxB_TextChanged);
            // 
            // endIPTextBoxC
            // 
            this.endIPTextBoxC.AnimateReadOnly = false;
            this.endIPTextBoxC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endIPTextBoxC.Depth = 0;
            resources.ApplyResources(this.endIPTextBoxC, "endIPTextBoxC");
            this.endIPTextBoxC.LeadingIcon = null;
            this.endIPTextBoxC.LeaveOnEnterKey = true;
            this.endIPTextBoxC.MouseState = MaterialSkin.MouseState.OUT;
            this.endIPTextBoxC.Name = "endIPTextBoxC";
            this.endIPTextBoxC.TrailingIcon = null;
            this.endIPTextBoxC.TextChanged += new System.EventHandler(this.endIPTextBoxC_TextChanged);
            // 
            // endIPTextBoxD
            // 
            this.endIPTextBoxD.AnimateReadOnly = false;
            this.endIPTextBoxD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endIPTextBoxD.Depth = 0;
            resources.ApplyResources(this.endIPTextBoxD, "endIPTextBoxD");
            this.endIPTextBoxD.LeadingIcon = null;
            this.endIPTextBoxD.LeaveOnEnterKey = true;
            this.endIPTextBoxD.MouseState = MaterialSkin.MouseState.OUT;
            this.endIPTextBoxD.Name = "endIPTextBoxD";
            this.endIPTextBoxD.TrailingIcon = null;
            this.endIPTextBoxD.TextChanged += new System.EventHandler(this.endIPTextBoxD_TextChanged);
            // 
            // materialLabel4
            // 
            resources.ApplyResources(this.materialLabel4, "materialLabel4");
            this.materialLabel4.Depth = 0;
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.stopListeningButtons, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.startListeningButton, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.newIpTextBox, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.performanceButton, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.setStaticIPButton, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.newStaticLabel, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.newPasswordTextBox, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.rebootButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.poolPasswordLabel, 4, 2);
            this.tableLayoutPanel4.Controls.Add(this.selfCheckButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.setPasswordButton, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.minerPasswordLabel, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.openInBrowserButton, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.setDynamicIPButton, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.performanceDropdown, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.setPoolButton, 7, 2);
            this.tableLayoutPanel4.Controls.Add(this.updateButton, 4, 3);
            this.tableLayoutPanel4.Controls.Add(this.poolAccountLabel, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.poolUserTextBox, 6, 1);
            this.tableLayoutPanel4.Controls.Add(this.poolPasswordTextBox, 6, 2);
            this.tableLayoutPanel4.Controls.Add(this.poolUrlTextBox, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.poolURLLabel, 4, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // stopListeningButtons
            // 
            resources.ApplyResources(this.stopListeningButtons, "stopListeningButtons");
            this.stopListeningButtons.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopListeningButtons.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.stopListeningButtons.Depth = 0;
            this.stopListeningButtons.HighEmphasis = true;
            this.stopListeningButtons.Icon = null;
            this.stopListeningButtons.MouseState = MaterialSkin.MouseState.HOVER;
            this.stopListeningButtons.Name = "stopListeningButtons";
            this.stopListeningButtons.NoAccentTextColor = System.Drawing.Color.Empty;
            this.stopListeningButtons.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.stopListeningButtons.UseAccentColor = false;
            this.stopListeningButtons.UseVisualStyleBackColor = true;
            this.stopListeningButtons.Click += new System.EventHandler(this.stopListeningButtons_Click);
            // 
            // startListeningButton
            // 
            resources.ApplyResources(this.startListeningButton, "startListeningButton");
            this.startListeningButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startListeningButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.startListeningButton.Depth = 0;
            this.startListeningButton.HighEmphasis = true;
            this.startListeningButton.Icon = null;
            this.startListeningButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.startListeningButton.Name = "startListeningButton";
            this.startListeningButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.startListeningButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.startListeningButton.UseAccentColor = false;
            this.startListeningButton.UseVisualStyleBackColor = true;
            this.startListeningButton.Click += new System.EventHandler(this.startListeningButton_Click);
            // 
            // newIpTextBox
            // 
            this.newIpTextBox.AnimateReadOnly = false;
            this.newIpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newIpTextBox.Depth = 0;
            resources.ApplyResources(this.newIpTextBox, "newIpTextBox");
            this.newIpTextBox.LeadingIcon = null;
            this.newIpTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.newIpTextBox.Name = "newIpTextBox";
            this.newIpTextBox.TrailingIcon = null;
            // 
            // performanceButton
            // 
            resources.ApplyResources(this.performanceButton, "performanceButton");
            this.performanceButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.performanceButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.performanceButton.Depth = 0;
            this.performanceButton.HighEmphasis = true;
            this.performanceButton.Icon = null;
            this.performanceButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.performanceButton.Name = "performanceButton";
            this.performanceButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.performanceButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.performanceButton.UseAccentColor = false;
            this.performanceButton.UseVisualStyleBackColor = true;
            this.performanceButton.Click += new System.EventHandler(this.performanceButton_Click);
            // 
            // setStaticIPButton
            // 
            resources.ApplyResources(this.setStaticIPButton, "setStaticIPButton");
            this.tableLayoutPanel4.SetColumnSpan(this.setStaticIPButton, 2);
            this.setStaticIPButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.setStaticIPButton.Depth = 0;
            this.setStaticIPButton.HighEmphasis = true;
            this.setStaticIPButton.Icon = null;
            this.setStaticIPButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.setStaticIPButton.Name = "setStaticIPButton";
            this.setStaticIPButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.setStaticIPButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.setStaticIPButton.UseAccentColor = false;
            this.setStaticIPButton.UseVisualStyleBackColor = true;
            this.setStaticIPButton.Click += new System.EventHandler(this.setStaticIPButton_Click);
            // 
            // newStaticLabel
            // 
            resources.ApplyResources(this.newStaticLabel, "newStaticLabel");
            this.newStaticLabel.Depth = 0;
            this.newStaticLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.newStaticLabel.Name = "newStaticLabel";
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.AnimateReadOnly = false;
            this.newPasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newPasswordTextBox.Depth = 0;
            resources.ApplyResources(this.newPasswordTextBox, "newPasswordTextBox");
            this.newPasswordTextBox.LeadingIcon = null;
            this.newPasswordTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.TrailingIcon = null;
            // 
            // poolPasswordLabel
            // 
            resources.ApplyResources(this.poolPasswordLabel, "poolPasswordLabel");
            this.tableLayoutPanel4.SetColumnSpan(this.poolPasswordLabel, 2);
            this.poolPasswordLabel.Depth = 0;
            this.poolPasswordLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.poolPasswordLabel.Name = "poolPasswordLabel";
            // 
            // selfCheckButton
            // 
            resources.ApplyResources(this.selfCheckButton, "selfCheckButton");
            this.selfCheckButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selfCheckButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.selfCheckButton.Depth = 0;
            this.selfCheckButton.HighEmphasis = true;
            this.selfCheckButton.Icon = null;
            this.selfCheckButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.selfCheckButton.Name = "selfCheckButton";
            this.selfCheckButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.selfCheckButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.selfCheckButton.UseAccentColor = false;
            this.selfCheckButton.UseVisualStyleBackColor = true;
            this.selfCheckButton.Click += new System.EventHandler(this.selfCheckButton_Click);
            // 
            // setPasswordButton
            // 
            resources.ApplyResources(this.setPasswordButton, "setPasswordButton");
            this.tableLayoutPanel4.SetColumnSpan(this.setPasswordButton, 2);
            this.setPasswordButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.setPasswordButton.Depth = 0;
            this.setPasswordButton.HighEmphasis = true;
            this.setPasswordButton.Icon = null;
            this.setPasswordButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.setPasswordButton.Name = "setPasswordButton";
            this.setPasswordButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.setPasswordButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.setPasswordButton.UseAccentColor = false;
            this.setPasswordButton.UseVisualStyleBackColor = true;
            this.setPasswordButton.Click += new System.EventHandler(this.setPasswordButton_Click);
            // 
            // minerPasswordLabel
            // 
            resources.ApplyResources(this.minerPasswordLabel, "minerPasswordLabel");
            this.minerPasswordLabel.Depth = 0;
            this.minerPasswordLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.minerPasswordLabel.Name = "minerPasswordLabel";
            // 
            // openInBrowserButton
            // 
            resources.ApplyResources(this.openInBrowserButton, "openInBrowserButton");
            this.openInBrowserButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openInBrowserButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.openInBrowserButton.Depth = 0;
            this.openInBrowserButton.HighEmphasis = true;
            this.openInBrowserButton.Icon = null;
            this.openInBrowserButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.openInBrowserButton.Name = "openInBrowserButton";
            this.openInBrowserButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.openInBrowserButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.openInBrowserButton.UseAccentColor = false;
            this.openInBrowserButton.UseVisualStyleBackColor = true;
            this.openInBrowserButton.Click += new System.EventHandler(this.openInBrowserButton_Click);
            // 
            // setDynamicIPButton
            // 
            resources.ApplyResources(this.setDynamicIPButton, "setDynamicIPButton");
            this.setDynamicIPButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.setDynamicIPButton.Depth = 0;
            this.setDynamicIPButton.HighEmphasis = true;
            this.setDynamicIPButton.Icon = null;
            this.setDynamicIPButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.setDynamicIPButton.Name = "setDynamicIPButton";
            this.setDynamicIPButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.setDynamicIPButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.setDynamicIPButton.UseAccentColor = false;
            this.setDynamicIPButton.UseVisualStyleBackColor = true;
            this.setDynamicIPButton.Click += new System.EventHandler(this.setDynamicIPButton_Click);
            // 
            // performanceDropdown
            // 
            this.performanceDropdown.AutoResize = false;
            this.performanceDropdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.performanceDropdown.Depth = 0;
            resources.ApplyResources(this.performanceDropdown, "performanceDropdown");
            this.performanceDropdown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.performanceDropdown.DropDownHeight = 174;
            this.performanceDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.performanceDropdown.DropDownWidth = 121;
            this.performanceDropdown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.performanceDropdown.FormattingEnabled = true;
            this.performanceDropdown.Items.AddRange(new object[] {
            resources.GetString("performanceDropdown.Items"),
            resources.GetString("performanceDropdown.Items1"),
            resources.GetString("performanceDropdown.Items2"),
            resources.GetString("performanceDropdown.Items3")});
            this.performanceDropdown.MouseState = MaterialSkin.MouseState.OUT;
            this.performanceDropdown.Name = "performanceDropdown";
            this.performanceDropdown.StartIndex = 0;
            // 
            // setPoolButton
            // 
            resources.ApplyResources(this.setPoolButton, "setPoolButton");
            this.setPoolButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.setPoolButton.Depth = 0;
            this.setPoolButton.HighEmphasis = true;
            this.setPoolButton.Icon = null;
            this.setPoolButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.setPoolButton.Name = "setPoolButton";
            this.setPoolButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.setPoolButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.setPoolButton.UseAccentColor = false;
            this.setPoolButton.UseVisualStyleBackColor = true;
            this.setPoolButton.Click += new System.EventHandler(this.setPoolButton_Click);
            // 
            // updateButton
            // 
            resources.ApplyResources(this.updateButton, "updateButton");
            this.tableLayoutPanel4.SetColumnSpan(this.updateButton, 4);
            this.updateButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.updateButton.Depth = 0;
            this.updateButton.HighEmphasis = true;
            this.updateButton.Icon = null;
            this.updateButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.updateButton.Name = "updateButton";
            this.updateButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.updateButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.updateButton.UseAccentColor = false;
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // poolAccountLabel
            // 
            resources.ApplyResources(this.poolAccountLabel, "poolAccountLabel");
            this.tableLayoutPanel4.SetColumnSpan(this.poolAccountLabel, 2);
            this.poolAccountLabel.Depth = 0;
            this.poolAccountLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.poolAccountLabel.Name = "poolAccountLabel";
            // 
            // poolUserTextBox
            // 
            this.poolUserTextBox.AnimateReadOnly = false;
            this.poolUserTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel4.SetColumnSpan(this.poolUserTextBox, 2);
            this.poolUserTextBox.Depth = 0;
            resources.ApplyResources(this.poolUserTextBox, "poolUserTextBox");
            this.poolUserTextBox.LeadingIcon = null;
            this.poolUserTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.poolUserTextBox.Name = "poolUserTextBox";
            this.poolUserTextBox.TrailingIcon = null;
            // 
            // poolPasswordTextBox
            // 
            this.poolPasswordTextBox.AnimateReadOnly = false;
            this.poolPasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.poolPasswordTextBox.Depth = 0;
            resources.ApplyResources(this.poolPasswordTextBox, "poolPasswordTextBox");
            this.poolPasswordTextBox.LeadingIcon = null;
            this.poolPasswordTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.poolPasswordTextBox.Name = "poolPasswordTextBox";
            this.poolPasswordTextBox.TrailingIcon = null;
            // 
            // poolUrlTextBox
            // 
            this.poolUrlTextBox.AnimateReadOnly = false;
            this.poolUrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel4.SetColumnSpan(this.poolUrlTextBox, 2);
            this.poolUrlTextBox.Depth = 0;
            resources.ApplyResources(this.poolUrlTextBox, "poolUrlTextBox");
            this.poolUrlTextBox.LeadingIcon = null;
            this.poolUrlTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.poolUrlTextBox.Name = "poolUrlTextBox";
            this.poolUrlTextBox.TrailingIcon = null;
            // 
            // poolURLLabel
            // 
            resources.ApplyResources(this.poolURLLabel, "poolURLLabel");
            this.tableLayoutPanel4.SetColumnSpan(this.poolURLLabel, 2);
            this.poolURLLabel.Depth = 0;
            this.poolURLLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.poolURLLabel.Name = "poolURLLabel";
            // 
            // minerFoundCountLabel
            // 
            resources.ApplyResources(this.minerFoundCountLabel, "minerFoundCountLabel");
            this.minerFoundCountLabel.Depth = 0;
            this.minerFoundCountLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.minerFoundCountLabel.Name = "minerFoundCountLabel";
            // 
            // minerListView
            // 
            this.minerListView.CheckBoxes = true;
            this.minerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPAddress,
            this.Hashrate,
            this.Pool,
            this.PoolUser,
            this.Account,
            this.DAG,
            this.SelfCheckProgress,
            this.HashboardStatus,
            this.AcceptanceRate,
            this.Accepted,
            this.Rejected,
            this.MacAddress,
            this.Uptime,
            this.Temperature,
            this.FanSpeed,
            this.Firmware,
            this.Hashboard1Status,
            this.Hashboard1Hashrate,
            this.Hashboard1Temperature,
            this.Hashboard2Status,
            this.Hashboard2Hashrate,
            this.Hashboard2Temperature,
            this.Hashboard3Status,
            this.Hashboard3Hashrate,
            this.Hashboard3Temperature});
            resources.ApplyResources(this.minerListView, "minerListView");
            this.minerListView.FullRowSelect = true;
            this.minerListView.HideSelection = false;
            this.minerListView.Name = "minerListView";
            this.minerListView.TabStop = false;
            this.minerListView.UseCompatibleStateImageBehavior = false;
            this.minerListView.View = System.Windows.Forms.View.Details;
            this.minerListView.DoubleClick += new System.EventHandler(this.minerListView_DoubleClick);
            // 
            // IPAddress
            // 
            resources.ApplyResources(this.IPAddress, "IPAddress");
            // 
            // Hashrate
            // 
            resources.ApplyResources(this.Hashrate, "Hashrate");
            // 
            // Pool
            // 
            resources.ApplyResources(this.Pool, "Pool");
            // 
            // PoolUser
            // 
            resources.ApplyResources(this.PoolUser, "PoolUser");
            // 
            // Account
            // 
            resources.ApplyResources(this.Account, "Account");
            // 
            // DAG
            // 
            resources.ApplyResources(this.DAG, "DAG");
            // 
            // SelfCheckProgress
            // 
            resources.ApplyResources(this.SelfCheckProgress, "SelfCheckProgress");
            // 
            // HashboardStatus
            // 
            resources.ApplyResources(this.HashboardStatus, "HashboardStatus");
            // 
            // AcceptanceRate
            // 
            resources.ApplyResources(this.AcceptanceRate, "AcceptanceRate");
            // 
            // Accepted
            // 
            resources.ApplyResources(this.Accepted, "Accepted");
            // 
            // Rejected
            // 
            resources.ApplyResources(this.Rejected, "Rejected");
            // 
            // MacAddress
            // 
            resources.ApplyResources(this.MacAddress, "MacAddress");
            // 
            // Uptime
            // 
            resources.ApplyResources(this.Uptime, "Uptime");
            // 
            // Temperature
            // 
            resources.ApplyResources(this.Temperature, "Temperature");
            // 
            // FanSpeed
            // 
            resources.ApplyResources(this.FanSpeed, "FanSpeed");
            // 
            // Firmware
            // 
            resources.ApplyResources(this.Firmware, "Firmware");
            // 
            // Hashboard1Status
            // 
            resources.ApplyResources(this.Hashboard1Status, "Hashboard1Status");
            // 
            // Hashboard1Hashrate
            // 
            resources.ApplyResources(this.Hashboard1Hashrate, "Hashboard1Hashrate");
            // 
            // Hashboard1Temperature
            // 
            resources.ApplyResources(this.Hashboard1Temperature, "Hashboard1Temperature");
            // 
            // Hashboard2Status
            // 
            resources.ApplyResources(this.Hashboard2Status, "Hashboard2Status");
            // 
            // Hashboard2Hashrate
            // 
            resources.ApplyResources(this.Hashboard2Hashrate, "Hashboard2Hashrate");
            // 
            // Hashboard2Temperature
            // 
            resources.ApplyResources(this.Hashboard2Temperature, "Hashboard2Temperature");
            // 
            // Hashboard3Status
            // 
            resources.ApplyResources(this.Hashboard3Status, "Hashboard3Status");
            // 
            // Hashboard3Hashrate
            // 
            resources.ApplyResources(this.Hashboard3Hashrate, "Hashboard3Hashrate");
            // 
            // Hashboard3Temperature
            // 
            resources.ApplyResources(this.Hashboard3Temperature, "Hashboard3Temperature");
            // 
            // translate
            // 
            resources.ApplyResources(this.translate, "translate");
            this.translate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.translate.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.translate.Depth = 0;
            this.translate.HighEmphasis = true;
            this.translate.Icon = null;
            this.translate.MouseState = MaterialSkin.MouseState.HOVER;
            this.translate.Name = "translate";
            this.translate.NoAccentTextColor = System.Drawing.Color.Empty;
            this.translate.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.translate.UseAccentColor = false;
            this.translate.UseVisualStyleBackColor = true;
            this.translate.Click += new System.EventHandler(this.translate_Click);
            // 
            // ScanningIPLabel
            // 
            this.ScanningIPLabel.BackColor = System.Drawing.Color.Green;
            this.ScanningIPLabel.Depth = 0;
            resources.ApplyResources(this.ScanningIPLabel, "ScanningIPLabel");
            this.ScanningIPLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.ScanningIPLabel.Name = "ScanningIPLabel";
            // 
            // loadIPRangesDialog
            // 
            this.loadIPRangesDialog.FileName = "openFileDialog1";
            resources.ApplyResources(this.loadIPRangesDialog, "loadIPRangesDialog");
            // 
            // saveIPRangesDialog
            // 
            this.saveIPRangesDialog.FileName = "Yami_IP_Ranges";
            resources.ApplyResources(this.saveIPRangesDialog, "saveIPRangesDialog");
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.translate);
            this.Controls.Add(this.ScanningIPLabel);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MaterialSkin.Controls.MaterialButton rebootButton;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox1;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox2;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView ipRangeListView;
        private System.Windows.Forms.ColumnHeader StartIP;
        private System.Windows.Forms.ColumnHeader EndIP;
        private MaterialSkin.Controls.MaterialButton selfCheckButton;
        private MaterialSkin.Controls.MaterialButton scanSelectedButton;
        private MaterialSkin.Controls.MaterialButton deleteIPRanges;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ColumnHeader groupName;
        private MaterialSkin.Controls.MaterialButton loadIPRangesButton;
        private System.Windows.Forms.OpenFileDialog loadIPRangesDialog;
        private MaterialSkin.Controls.MaterialButton saveRangesButton;
        private System.Windows.Forms.SaveFileDialog saveIPRangesDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel13;
        private MaterialSkin.Controls.MaterialLabel materialLabel12;
        private MaterialSkin.Controls.MaterialLabel materialLabel11;
        private MaterialSkin.Controls.MaterialLabel materialLabel10;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
        private MaterialSkin.Controls.MaterialTextBox startIPTextBoxD;
        private MaterialSkin.Controls.MaterialTextBox startIPTextBoxC;
        private MaterialSkin.Controls.MaterialTextBox startIPTextBoxB;
        private MaterialSkin.Controls.MaterialTextBox startIPTextBoxA;
        private MaterialSkin.Controls.MaterialLabel startIPLabel;
        private MaterialSkin.Controls.MaterialButton stopScanButton;
        private MaterialSkin.Controls.MaterialButton translate;
        private MaterialSkin.Controls.MaterialLabel ScanningIPLabel;
        private MaterialSkin.Controls.MaterialLabel nameLabel;
        private MaterialSkin.Controls.MaterialButton addIPButton;
        private MaterialSkin.Controls.MaterialButton searchButton;
        private MaterialSkin.Controls.MaterialTextBox nameTextBox;
        private MaterialSkin.Controls.MaterialLabel endIPLabel;
        private MaterialSkin.Controls.MaterialTextBox endIPTextBoxA;
        private MaterialSkin.Controls.MaterialTextBox endIPTextBoxB;
        private MaterialSkin.Controls.MaterialTextBox endIPTextBoxC;
        private MaterialSkin.Controls.MaterialTextBox endIPTextBoxD;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialLabel minerFoundCountLabel;
        private MaterialSkin.Controls.MaterialButton performanceButton;
        private MaterialSkin.Controls.MaterialComboBox performanceDropdown;
        private System.Windows.Forms.ListView minerListView;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader Hashrate;
        private System.Windows.Forms.ColumnHeader Pool;
        private System.Windows.Forms.ColumnHeader PoolUser;
        private System.Windows.Forms.ColumnHeader Account;
        private System.Windows.Forms.ColumnHeader DAG;
        private System.Windows.Forms.ColumnHeader SelfCheckProgress;
        private System.Windows.Forms.ColumnHeader HashboardStatus;
        private System.Windows.Forms.ColumnHeader AcceptanceRate;
        private System.Windows.Forms.ColumnHeader Accepted;
        private System.Windows.Forms.ColumnHeader Rejected;
        private System.Windows.Forms.ColumnHeader MacAddress;
        private System.Windows.Forms.ColumnHeader Uptime;
        private System.Windows.Forms.ColumnHeader Temperature;
        private System.Windows.Forms.ColumnHeader FanSpeed;
        private System.Windows.Forms.ColumnHeader Firmware;
        private System.Windows.Forms.ColumnHeader Hashboard1Status;
        private System.Windows.Forms.ColumnHeader Hashboard1Hashrate;
        private System.Windows.Forms.ColumnHeader Hashboard1Temperature;
        private System.Windows.Forms.ColumnHeader Hashboard2Status;
        private System.Windows.Forms.ColumnHeader Hashboard2Hashrate;
        private System.Windows.Forms.ColumnHeader Hashboard2Temperature;
        private System.Windows.Forms.ColumnHeader Hashboard3Status;
        private System.Windows.Forms.ColumnHeader Hashboard3Hashrate;
        private System.Windows.Forms.ColumnHeader Hashboard3Temperature;
        private MaterialSkin.Controls.MaterialTextBox poolPasswordTextBox;
        private MaterialSkin.Controls.MaterialLabel poolPasswordLabel;
        private MaterialSkin.Controls.MaterialLabel poolAccountLabel;
        private MaterialSkin.Controls.MaterialButton setPoolButton;
        private MaterialSkin.Controls.MaterialTextBox poolUrlTextBox;
        private MaterialSkin.Controls.MaterialTextBox poolUserTextBox;
        private MaterialSkin.Controls.MaterialButton openInBrowserButton;
        private MaterialSkin.Controls.MaterialTextBox newPasswordTextBox;
        private MaterialSkin.Controls.MaterialLabel minerPasswordLabel;
        private MaterialSkin.Controls.MaterialButton setPasswordButton;
        private MaterialSkin.Controls.MaterialButton setStaticIPButton;
        private MaterialSkin.Controls.MaterialTextBox newIpTextBox;
        private MaterialSkin.Controls.MaterialLabel newStaticLabel;
        private MaterialSkin.Controls.MaterialButton setDynamicIPButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialButton stopListeningButtons;
        private MaterialSkin.Controls.MaterialButton startListeningButton;
        private MaterialSkin.Controls.MaterialButton updateButton;
        private MaterialSkin.Controls.MaterialLabel poolURLLabel;
    }
}

