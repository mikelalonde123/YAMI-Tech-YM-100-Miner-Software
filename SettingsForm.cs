using System;
using System.Windows.Forms;

namespace YAMI_Scanner
{

    public partial class SettingsForm : Form
    {
        private AppSettings tempSettings;
        private AppSettings mainSettings;

        public SettingsForm(AppSettings currentSettings)
        {
            InitializeComponent();

            // Create a temporary copy of the current settings
            tempSettings = new AppSettings
            {
                MaxConcurrentScans = currentSettings.MaxConcurrentScans,
                MaxRepeatScan = currentSettings.MaxRepeatScan,
                TimeoutTime = currentSettings.TimeoutTime,
                ConcurrentUpdate = currentSettings.ConcurrentUpdate,
                UpdateTimeout = currentSettings.UpdateTimeout,
                CheckForStaticIP = currentSettings.CheckForStaticIP

            };

            // Store the reference to the main settings
            mainSettings = currentSettings;

            // Initialize UI controls with tempSettings values
            maxConcurrentScanSetting.Value = tempSettings.MaxConcurrentScans;
            maxRepeatScanSetting.Value = tempSettings.MaxRepeatScan;
            timeoutTimeSetting.Value = (decimal)tempSettings.TimeoutTime;
            ConcurrentUpdateSetting.Value = tempSettings.ConcurrentUpdate;
            UpdateTimeoutSetting.Value = tempSettings.UpdateTimeout;
            CheckStaticIP.Checked = tempSettings.CheckForStaticIP;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void maxConcurrentScanSetting_ValueChanged(object sender, EventArgs e)
        {
            tempSettings.MaxConcurrentScans = (int)maxConcurrentScanSetting.Value;
        }

        private void maxRepeatScanSetting_ValueChanged(object sender, EventArgs e)
        {
            tempSettings.MaxRepeatScan = (int)maxRepeatScanSetting.Value;
        }

        private void timeoutTimeSetting_ValueChanged(object sender, EventArgs e)
        {
            tempSettings.TimeoutTime = (double)timeoutTimeSetting.Value;
        }

        private void UpdateTimeoutSetting_ValueChanged(object sender, EventArgs e)
        {
            tempSettings.UpdateTimeout = (int)UpdateTimeoutSetting.Value;
        }

        private void ConcurrentUpdateSetting_ValueChanged(object sender, EventArgs e)
        {
            tempSettings.ConcurrentUpdate = (int)ConcurrentUpdateSetting.Value;
        }

        private void CheckStaticIP_CheckedChanged(object sender, EventArgs e)
        {
            tempSettings.CheckForStaticIP = CheckStaticIP.Checked;
        }

        private void restoreDefaultBtn_Click(object sender, EventArgs e)
        {
            timeoutTimeSetting.Value = 10;
            maxConcurrentScanSetting.Value = 260;
            maxRepeatScanSetting.Value = 3;
            UpdateTimeoutSetting.Value = 180;
            ConcurrentUpdateSetting.Value = 20;
            CheckStaticIP.Checked = true;
        }

        private void SettingsOK_Click(object sender, EventArgs e)
        {
            // Apply changes from tempSettings to mainSettings
            mainSettings.MaxConcurrentScans = tempSettings.MaxConcurrentScans;
            mainSettings.MaxRepeatScan = tempSettings.MaxRepeatScan;
            mainSettings.TimeoutTime = tempSettings.TimeoutTime;
            mainSettings.UpdateTimeout = tempSettings.UpdateTimeout;
            mainSettings.ConcurrentUpdate = tempSettings.ConcurrentUpdate;
            mainSettings.CheckForStaticIP = tempSettings.CheckForStaticIP;

            // Close the form with DialogResult.OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SettingsCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
