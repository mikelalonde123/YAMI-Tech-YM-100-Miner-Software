namespace YAMI_Scanner
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.SettingsOK = new System.Windows.Forms.Button();
            this.SettingsCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.maxConcurrentScanSetting = new System.Windows.Forms.NumericUpDown();
            this.maxRepeatScanSetting = new System.Windows.Forms.NumericUpDown();
            this.timeoutTimeSetting = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.UpdateTimeoutSetting = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ConcurrentUpdateSetting = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.maxConcurrentScanSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRepeatScanSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutTimeSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateTimeoutSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConcurrentUpdateSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // SettingsOK
            // 
            this.SettingsOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SettingsOK.Location = new System.Drawing.Point(206, 334);
            this.SettingsOK.Name = "SettingsOK";
            this.SettingsOK.Size = new System.Drawing.Size(75, 23);
            this.SettingsOK.TabIndex = 0;
            this.SettingsOK.Text = "OK";
            this.toolTip1.SetToolTip(this.SettingsOK, "Save the current values");
            this.SettingsOK.UseVisualStyleBackColor = true;
            this.SettingsOK.Click += new System.EventHandler(this.SettingsOK_Click);
            // 
            // SettingsCancel
            // 
            this.SettingsCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SettingsCancel.Location = new System.Drawing.Point(300, 334);
            this.SettingsCancel.Name = "SettingsCancel";
            this.SettingsCancel.Size = new System.Drawing.Size(75, 23);
            this.SettingsCancel.TabIndex = 1;
            this.SettingsCancel.Text = "Cancel";
            this.toolTip2.SetToolTip(this.SettingsCancel, "Discard changes and keep previous settings values");
            this.SettingsCancel.UseVisualStyleBackColor = true;
            this.SettingsCancel.Click += new System.EventHandler(this.SettingsCancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Save";
            // 
            // toolTip2
            // 
            this.toolTip2.ToolTipTitle = "Discard";
            // 
            // maxConcurrentScanSetting
            // 
            this.maxConcurrentScanSetting.Location = new System.Drawing.Point(173, 36);
            this.maxConcurrentScanSetting.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxConcurrentScanSetting.Name = "maxConcurrentScanSetting";
            this.maxConcurrentScanSetting.Size = new System.Drawing.Size(53, 20);
            this.maxConcurrentScanSetting.TabIndex = 2;
            this.maxConcurrentScanSetting.ValueChanged += new System.EventHandler(this.maxConcurrentScanSetting_ValueChanged);
            // 
            // maxRepeatScanSetting
            // 
            this.maxRepeatScanSetting.Location = new System.Drawing.Point(173, 62);
            this.maxRepeatScanSetting.Name = "maxRepeatScanSetting";
            this.maxRepeatScanSetting.Size = new System.Drawing.Size(53, 20);
            this.maxRepeatScanSetting.TabIndex = 3;
            this.maxRepeatScanSetting.ValueChanged += new System.EventHandler(this.maxRepeatScanSetting_ValueChanged);
            // 
            // timeoutTimeSetting
            // 
            this.timeoutTimeSetting.Location = new System.Drawing.Point(173, 10);
            this.timeoutTimeSetting.Name = "timeoutTimeSetting";
            this.timeoutTimeSetting.Size = new System.Drawing.Size(53, 20);
            this.timeoutTimeSetting.TabIndex = 4;
            this.timeoutTimeSetting.ValueChanged += new System.EventHandler(this.timeoutTimeSetting_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Operation Timeout:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Concurrent Operations:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Repeat Operation On Failure:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Seconds";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Miners";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Times";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Seconds";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Update Timeout:";
            // 
            // UpdateTimeoutSetting
            // 
            this.UpdateTimeoutSetting.Location = new System.Drawing.Point(173, 88);
            this.UpdateTimeoutSetting.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UpdateTimeoutSetting.Name = "UpdateTimeoutSetting";
            this.UpdateTimeoutSetting.Size = new System.Drawing.Size(53, 20);
            this.UpdateTimeoutSetting.TabIndex = 11;
            this.UpdateTimeoutSetting.ValueChanged += new System.EventHandler(this.UpdateTimeoutSetting_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Miners";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Concurrent Update:";
            // 
            // ConcurrentUpdateSetting
            // 
            this.ConcurrentUpdateSetting.Location = new System.Drawing.Point(173, 114);
            this.ConcurrentUpdateSetting.Name = "ConcurrentUpdateSetting";
            this.ConcurrentUpdateSetting.Size = new System.Drawing.Size(53, 20);
            this.ConcurrentUpdateSetting.TabIndex = 14;
            this.ConcurrentUpdateSetting.ValueChanged += new System.EventHandler(this.ConcurrentUpdateSetting_ValueChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.SettingsCancel;
            this.ClientSize = new System.Drawing.Size(387, 369);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ConcurrentUpdateSetting);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.UpdateTimeoutSetting);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeoutTimeSetting);
            this.Controls.Add(this.maxRepeatScanSetting);
            this.Controls.Add(this.maxConcurrentScanSetting);
            this.Controls.Add(this.SettingsCancel);
            this.Controls.Add(this.SettingsOK);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.maxConcurrentScanSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRepeatScanSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutTimeSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateTimeoutSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConcurrentUpdateSetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SettingsOK;
        private System.Windows.Forms.Button SettingsCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.NumericUpDown maxConcurrentScanSetting;
        private System.Windows.Forms.NumericUpDown maxRepeatScanSetting;
        private System.Windows.Forms.NumericUpDown timeoutTimeSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown UpdateTimeoutSetting;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown ConcurrentUpdateSetting;
    }
}