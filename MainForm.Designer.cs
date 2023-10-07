namespace MinerInfoApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.searchButton = new MaterialSkin.Controls.MaterialButton();
            this.rebootButton = new MaterialSkin.Controls.MaterialButton();
            this.startIPTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.endIPTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox2 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox3 = new MaterialSkin.Controls.MaterialCheckbox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
            this.stopScanButton = new MaterialSkin.Controls.MaterialButton();
            this.ScanningIPLabel = new MaterialSkin.Controls.MaterialLabel();
            this.addIPButton = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.nameTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.minerListView = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HashboardStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Uptime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FanSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DAG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Accepted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rejected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AcceptanceRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Pool = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PoolUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SelfCheckProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard1Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard1Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard1Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard2Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard2Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard2Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard3Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard3Hashrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hashboard3Temperature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.selfCheckButton = new MaterialSkin.Controls.MaterialButton();
            this.minerFoundCountLabel = new MaterialSkin.Controls.MaterialLabel();
            this.loadIPRangesDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveIPRangesDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            resources.ApplyResources(this.materialLabel1, "materialLabel1");
            this.materialLabel1.Depth = 0;
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            // 
            // materialLabel2
            // 
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.Depth = 0;
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
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
            // startIPTextBox
            // 
            this.startIPTextBox.AnimateReadOnly = false;
            this.startIPTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startIPTextBox.Depth = 0;
            resources.ApplyResources(this.startIPTextBox, "startIPTextBox");
            this.startIPTextBox.LeadingIcon = null;
            this.startIPTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.startIPTextBox.Name = "startIPTextBox";
            this.startIPTextBox.TrailingIcon = null;
            // 
            // endIPTextBox
            // 
            this.endIPTextBox.AnimateReadOnly = false;
            this.endIPTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endIPTextBox.Depth = 0;
            resources.ApplyResources(this.endIPTextBox, "endIPTextBox");
            this.endIPTextBox.LeadingIcon = null;
            this.endIPTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.endIPTextBox.Name = "endIPTextBox";
            this.endIPTextBox.TrailingIcon = null;
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
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
            this.tableLayoutPanel2.Controls.Add(this.startIPTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.endIPTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.stopScanButton, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ScanningIPLabel, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.addIPButton, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.searchButton, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.nameTextBox, 2, 1);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
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
            // ScanningIPLabel
            // 
            resources.ApplyResources(this.ScanningIPLabel, "ScanningIPLabel");
            this.tableLayoutPanel2.SetColumnSpan(this.ScanningIPLabel, 2);
            this.ScanningIPLabel.Depth = 0;
            this.ScanningIPLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.ScanningIPLabel.Name = "ScanningIPLabel";
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
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
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
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.minerListView, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.rebootButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.selfCheckButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.minerFoundCountLabel, 2, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // minerListView
            // 
            this.minerListView.CheckBoxes = true;
            this.minerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPAddress,
            this.Hashrate,
            this.HashboardStatus,
            this.Uptime,
            this.FanSpeed,
            this.Temperature,
            this.DAG,
            this.Accepted,
            this.Rejected,
            this.AcceptanceRate,
            this.Pool,
            this.PoolUser,
            this.SelfCheckProgress,
            this.Hashboard1Status,
            this.Hashboard1Hashrate,
            this.Hashboard1Temperature,
            this.Hashboard2Status,
            this.Hashboard2Hashrate,
            this.Hashboard2Temperature,
            this.Hashboard3Status,
            this.Hashboard3Hashrate,
            this.Hashboard3Temperature});
            this.tableLayoutPanel4.SetColumnSpan(this.minerListView, 3);
            resources.ApplyResources(this.minerListView, "minerListView");
            this.minerListView.HideSelection = false;
            this.minerListView.Name = "minerListView";
            this.minerListView.UseCompatibleStateImageBehavior = false;
            this.minerListView.View = System.Windows.Forms.View.Details;
            // 
            // IPAddress
            // 
            resources.ApplyResources(this.IPAddress, "IPAddress");
            // 
            // Hashrate
            // 
            resources.ApplyResources(this.Hashrate, "Hashrate");
            // 
            // HashboardStatus
            // 
            resources.ApplyResources(this.HashboardStatus, "HashboardStatus");
            // 
            // Uptime
            // 
            resources.ApplyResources(this.Uptime, "Uptime");
            // 
            // FanSpeed
            // 
            resources.ApplyResources(this.FanSpeed, "FanSpeed");
            // 
            // Temperature
            // 
            resources.ApplyResources(this.Temperature, "Temperature");
            // 
            // DAG
            // 
            resources.ApplyResources(this.DAG, "DAG");
            // 
            // Accepted
            // 
            resources.ApplyResources(this.Accepted, "Accepted");
            // 
            // Rejected
            // 
            resources.ApplyResources(this.Rejected, "Rejected");
            // 
            // AcceptanceRate
            // 
            resources.ApplyResources(this.AcceptanceRate, "AcceptanceRate");
            // 
            // Pool
            // 
            resources.ApplyResources(this.Pool, "Pool");
            // 
            // PoolUser
            // 
            resources.ApplyResources(this.PoolUser, "PoolUser");
            // 
            // SelfCheckProgress
            // 
            resources.ApplyResources(this.SelfCheckProgress, "SelfCheckProgress");
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
            // minerFoundCountLabel
            // 
            resources.ApplyResources(this.minerFoundCountLabel, "minerFoundCountLabel");
            this.minerFoundCountLabel.Depth = 0;
            this.minerFoundCountLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.minerFoundCountLabel.Name = "minerFoundCountLabel";
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
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialButton searchButton;
        private MaterialSkin.Controls.MaterialButton rebootButton;
        private MaterialSkin.Controls.MaterialTextBox startIPTextBox;
        private MaterialSkin.Controls.MaterialTextBox endIPTextBox;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox1;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox2;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView minerListView;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader Hashrate;
        private System.Windows.Forms.ColumnHeader HashboardStatus;
        private System.Windows.Forms.ColumnHeader Uptime;
        private System.Windows.Forms.ColumnHeader FanSpeed;
        private System.Windows.Forms.ColumnHeader Temperature;
        private System.Windows.Forms.ColumnHeader DAG;
        private System.Windows.Forms.ColumnHeader Accepted;
        private System.Windows.Forms.ColumnHeader Rejected;
        private System.Windows.Forms.ColumnHeader AcceptanceRate;
        private System.Windows.Forms.ColumnHeader Pool;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MaterialSkin.Controls.MaterialLabel ScanningIPLabel;
        private MaterialSkin.Controls.MaterialButton addIPButton;
        private System.Windows.Forms.ListView ipRangeListView;
        private System.Windows.Forms.ColumnHeader StartIP;
        private System.Windows.Forms.ColumnHeader EndIP;
        private System.Windows.Forms.ColumnHeader PoolUser;
        private MaterialSkin.Controls.MaterialButton selfCheckButton;
        private MaterialSkin.Controls.MaterialButton scanSelectedButton;
        private MaterialSkin.Controls.MaterialButton deleteIPRanges;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialLabel minerFoundCountLabel;
        private MaterialSkin.Controls.MaterialButton stopScanButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialTextBox nameTextBox;
        private System.Windows.Forms.ColumnHeader groupName;
        private MaterialSkin.Controls.MaterialButton loadIPRangesButton;
        private System.Windows.Forms.OpenFileDialog loadIPRangesDialog;
        private MaterialSkin.Controls.MaterialButton saveRangesButton;
        private System.Windows.Forms.SaveFileDialog saveIPRangesDialog;
        private System.Windows.Forms.ColumnHeader SelfCheckProgress;
        private System.Windows.Forms.ColumnHeader Hashboard1Status;
        private System.Windows.Forms.ColumnHeader Hashboard1Hashrate;
        private System.Windows.Forms.ColumnHeader Hashboard1Temperature;
        private System.Windows.Forms.ColumnHeader Hashboard2Status;
        private System.Windows.Forms.ColumnHeader Hashboard2Hashrate;
        private System.Windows.Forms.ColumnHeader Hashboard2Temperature;
        private System.Windows.Forms.ColumnHeader Hashboard3Status;
        private System.Windows.Forms.ColumnHeader Hashboard3Hashrate;
        private System.Windows.Forms.ColumnHeader Hashboard3Temperature;
    }
}

