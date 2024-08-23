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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.SettingsOK = new System.Windows.Forms.Button();
            this.SettingsCancel = new System.Windows.Forms.Button();
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
            this.SettingsOK.UseVisualStyleBackColor = true;
            // 
            // SettingsCancel
            // 
            this.SettingsCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SettingsCancel.Location = new System.Drawing.Point(300, 334);
            this.SettingsCancel.Name = "SettingsCancel";
            this.SettingsCancel.Size = new System.Drawing.Size(75, 23);
            this.SettingsCancel.TabIndex = 1;
            this.SettingsCancel.Text = "Cancel";
            this.SettingsCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 369);
            this.Controls.Add(this.SettingsCancel);
            this.Controls.Add(this.SettingsOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SettingsOK;
        private System.Windows.Forms.Button SettingsCancel;
    }
}