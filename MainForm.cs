﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Collections;
using MaterialSkin;
using MaterialSkin.Controls;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Text;
using System.Linq;
using YAMI_Scanner;
using OfficeOpenXml;
using Newtonsoft.Json;
using System.Net.NetworkInformation;


namespace MinerInfoApp
{

    public partial class Main : MaterialForm
    {
        // *** SUPRESS IDE WARNINGS/ERRORS
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0044 // Make Field Readonly

        // *** DECLARE GLOBAL VARIABLES ***

        private SemaphoreSlim semaphore; // Declare the semaphore

        private CancellationTokenSource cancellationTokenSource;

        private CancellationTokenSource udpCancellationToken;

        private bool isScanning = false;

        private AppSettings appSettings;

        //CONCURRENT SCAN VARS
        private CancellationTokenSource cancellationTokenSourceSelfCheck;

        //SETTINGS VARIABLES
        public int maxConcurrentScans = YAMI_Scanner.Properties.Settings.Default.MaxConcurrentScans;
        public int maxRepeatScan = YAMI_Scanner.Properties.Settings.Default.MaxRepeatScan;
        public double timeoutTime = YAMI_Scanner.Properties.Settings.Default.TimeoutTime;
        public int concurrentUpdate = YAMI_Scanner.Properties.Settings.Default.ConcurrentUpdate;
        public int updateTimeout = YAMI_Scanner.Properties.Settings.Default.UpdateTimeout;
        public bool checkForStaticIP = YAMI_Scanner.Properties.Settings.Default.CheckForStaticIP;

        int concurrentReboot = YAMI_Scanner.Properties.Settings.Default.MaxConcurrentScans;
        private SemaphoreSlim semaphoreReboot;
        private CancellationTokenSource cancellationTokenSourceReboot;

        //Tracks the current language
        private bool isEnglish = true;

        //Dictionary for the miners
        private readonly Dictionary<string, MinerInfo> minerInfoDict = new Dictionary<string, MinerInfo>();

        // Key for accessing saved ranges in app.config
        private const string SavedRangesKey = "SavedIPRanges";

        //Variables for sorting by column
        private int lastColumnClicked = 0;
        private SortOrder lastSortOrder = SortOrder.Descending;

        //Variable to track miners found in a search
        private int minersFoundCount = 0;

        //Initialize IWebDrivere
        IWebDriver driver;

        //UdpClient for listening for YAMI IP Report
        UdpClient udpListener = new UdpClient(6687);


        //Timer
        System.Windows.Forms.Timer timer;


        //Declare and initialize controls
        public Main()
        {
            InitializeComponent();

            //Initialized necessary elements to use MaterialSkinManager
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green900, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            //Initialize settings menu
            appSettings = new AppSettings
            {
                MaxConcurrentScans = YAMI_Scanner.Properties.Settings.Default.MaxConcurrentScans,
                MaxRepeatScan = YAMI_Scanner.Properties.Settings.Default.MaxRepeatScan,
                TimeoutTime = YAMI_Scanner.Properties.Settings.Default.TimeoutTime,
                ConcurrentUpdate = YAMI_Scanner.Properties.Settings.Default.ConcurrentUpdate,
                UpdateTimeout = YAMI_Scanner.Properties.Settings.Default.UpdateTimeout,
                CheckForStaticIP = YAMI_Scanner.Properties.Settings.Default.CheckForStaticIP
            };

            //Makes the controls smoother when updating/scrolling
            minerListView.DoubleBuffered(true);

            ipRangeListView.DoubleBuffered(true);

            flowLayoutPanel1.DoubleBuffered(true);

            this.DoubleBuffered(true);

            //calls this function, described below
            AttachNumericTextBoxEventHandlers();

            //Custom Code to handle what happens when an item is checked
            minerListView.ItemChecked += new ItemCheckedEventHandler(minerListView_ItemChecked);

            //Adds the saved ip ranges to their listview
            LoadSavedIPRanges();

            //Loads the saved values of all textboxes for multi session continuity
            loadTextBoxData();

            // Trigger the sorting process manually
            minerListView_ColumnClick(minerListView, new ColumnClickEventArgs(lastColumnClicked));

            //Allow the user to use the ip report button of the miner
            ReceiveDataAsync();
        }


        //Code to run when the form first loads
        private void MainForm_Load(object sender, EventArgs e)
        {
            //gets UI cultire info based on user language
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            string language = currentCulture.TwoLetterISOLanguageName;

            //If the language is Chinese (2 character code is "zh"), sets the language flag to chinese and translates
            if (language == "zh")
            {
                Console.WriteLine("Chinese detected");
                isEnglish = false;
                translateLanguage();
            }

            //starts new instance of a timer to perform certain functions at regular intervals (see timer_tick function)
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 250;
            timer.Tick += Timer_Tick;
            timer.Start();

            //Loads and changes the minerListview control with the previously saved listview column settings (width, order)
            LoadListViewSettings();
        }


        //Checks if an item is selected and sets checked to true if it is and false if not
        private void UpdateListViewItems()
        {
            // If called from a non-UI thread, invoke the method on the UI thread
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateListViewItems));
                return;
            }

            // Update each ListViewItem in minerListView
            foreach (ListViewItem ipItem in minerListView.Items)
            {
                // Check if the form is disposed before accessing its controls
                if (IsDisposed)
                    return;

                // Update ListViewItem.Checked based on ListViewItem.Selected
                ipItem.Checked = ipItem.Selected;
            }
        }

        private void minerListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                e.Item.Selected = true;
            }
            else
            {
                //e.Item.Selected = false;
            }
        }

        bool isManualCheckChange = false;

        private void minerListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Only allow the checkbox state of the clicked item to change
            if (!isManualCheckChange && minerListView.SelectedItems.Count > 1)
            {
                // Prevent multiple selection check changes by clearing the selection momentarily
                isManualCheckChange = true;

                // Uncheck only the item that was clicked
                for (int i = 0; i < minerListView.SelectedItems.Count; i++)
                {
                    if (minerListView.SelectedItems[i] != minerListView.Items[e.Index])
                    {
                        minerListView.SelectedItems[i].Checked = minerListView.SelectedItems[i].Checked; // Keep their original state
                    }
                    else
                    {
                        minerListView.SelectedItems[i].Selected = !minerListView.SelectedItems[i].Selected;
                    }
                }

                isManualCheckChange = false;
            }
        }

        //Runs each time the timer "ticks"
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update UI based on selected items in minerListView
            UpdateListViewItems();
            calculateListviewTotals();
            UpdateSelectAllCheckBoxState();
        }


        //Function to call when updating the count of the list view
        private void minersFoundCountUpdate()
        {
            if (isEnglish)
            {
                minerFoundCountLabel.Text = ($"{minersFoundCount} Miners Found");
            }
            else
            {
                minerFoundCountLabel.Text = ($"找到{minersFoundCount}台矿机");
            }
        }


        //TRANSLATION CODE AND LOGIC
        private void translateLanguage()
        {
            //If the 'isEnglish' tracking variable is true, set everything to english, otherwise set it to chinese
            if (isEnglish)
            {
                //translate all the items to english
                startIPLabel.Text = "Start IP";
                endIPLabel.Text = "End IP";
                nameLabel.Text = "Name";
                translate.Text = "中文";
                searchButton.Text = "<-Scan";
                addIPButton.Text = "Add IP Range";
                stopScanButton.Text = "Stop Scanning";
                scanSelectedButton.Text = "Scan Selected Ranges";
                saveRangesButton.Text = "Save Selected Ranges To File";
                loadIPRangesButton.Text = "Load Ranges From File";
                deleteIPRanges.Text = "Delete Selected Ranges";
                rebootButton.Text = "Reboot Selected";
                selfCheckButton.Text = "Self Check";
                tabControl.TabPages[0].Text = "Search";
                tabControl.TabPages[1].Text = "Commands";
                performanceDropdown.Items.Clear();
                performanceDropdown.Items.Add("Performance");
                performanceDropdown.Items.Add("Efficiency");
                performanceDropdown.Items.Add("Balanced");
                performanceDropdown.Items.Add("Factory");
                performanceButton.Text = "Set Performance Mode";
                openInBrowserButton.Text = "Open In Browser";
                clearListview.Text = "Clear Miner Data";
                setDynamicIPButton.Text = "Set Dynamic IP";
                setStaticIPButton.Text = "Set Static IP";
                setPasswordButton.Text = "Set New Password";
                setPoolButton.Text = "Set Pools";
                newStaticLabel.Text = "Last Number Of New Static IP";
                minerPasswordLabel.Text = "New Password";
                poolURLLabel.Text = "Pool URL";
                poolAccountLabel.Text = "Pool Account";
                poolPasswordLabel.Text = "Pool Password";
                updateButton.Text = "Update Firmware";

                ipRangeListView.Columns[0].Text = "Start IP";
                ipRangeListView.Columns[1].Text = "End IP";
                ipRangeListView.Columns[2].Text = "Name";

                minerListView.Columns[0].Text = "IP Address";
                minerListView.Columns[1].Text = "Hashrate";
                minerListView.Columns[2].Text = "Pool";
                minerListView.Columns[3].Text = "Pool User";
                minerListView.Columns[4].Text = "Account";
                minerListView.Columns[5].Text = "DAG";
                minerListView.Columns[6].Text = "Self Check";
                minerListView.Columns[7].Text = "Hashboard Status";
                minerListView.Columns[8].Text = "Acceptance Rate";
                minerListView.Columns[9].Text = "Accepted";
                minerListView.Columns[10].Text = "Rejected";
                minerListView.Columns[11].Text = "Mac Address";
                minerListView.Columns[12].Text = "Uptime";
                minerListView.Columns[13].Text = "Temperature";
                minerListView.Columns[14].Text = "Fan Speed";
                minerListView.Columns[15].Text = "Firmware";
                minerListView.Columns[16].Text = "Board 1 Status";
                minerListView.Columns[17].Text = "Board 1 Hashrate";
                minerListView.Columns[18].Text = "Board 1 Temp";
                minerListView.Columns[19].Text = "Board 2 Status";
                minerListView.Columns[20].Text = "Board 2 Hashrate";
                minerListView.Columns[21].Text = "Board 2 Temp";
                minerListView.Columns[22].Text = "Board 3 Status";
                minerListView.Columns[23].Text = "Board 3 Hashrate";
                minerListView.Columns[24].Text = "Board 3 Temp";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
            else
            {
                //Translate all the buttons to chinese
                startIPLabel.Text = "开始IP";
                endIPLabel.Text = "结束IP";
                nameLabel.Text = "名称";
                translate.Text = "English";
                searchButton.Text = "<- 扫描";
                addIPButton.Text = "添加IP范围";
                stopScanButton.Text = "停止扫描";
                scanSelectedButton.Text = "扫描选定IP段";
                saveRangesButton.Text = "保存选定IP段到本地文件";
                loadIPRangesButton.Text = "从文件添加IP段";
                deleteIPRanges.Text = "删除选定IP段";
                rebootButton.Text = "重启选定矿机";
                selfCheckButton.Text = "自检选定矿机";
                tabControl.TabPages[0].Text = "查询";
                tabControl.TabPages[1].Text = "指令";
                performanceDropdown.Items.Clear();
                performanceDropdown.Items.Add("状态");
                performanceDropdown.Items.Add("效率");
                performanceDropdown.Items.Add("平衡");
                performanceDropdown.Items.Add("厂家");
                performanceButton.Text = "设置工作模式";
                openInBrowserButton.Text = "打开浏览器";
                clearListview.Text = "Clear Miner Data";
                setDynamicIPButton.Text = "设置动态IP";
                setStaticIPButton.Text = "设置静态IP";
                setPasswordButton.Text = "设置新密码";
                setPoolButton.Text = "配置矿池";
                newStaticLabel.Text = "静态IP尾数";
                minerPasswordLabel.Text = "新密码";
                poolURLLabel.Text = "挖矿地址";
                poolAccountLabel.Text = "子账户";
                poolPasswordLabel.Text = "密码";
                updateButton.Text = "更新固件";

                ipRangeListView.Columns[0].Text = "开始IP";
                ipRangeListView.Columns[1].Text = "结束IP";
                ipRangeListView.Columns[2].Text = "名称";

                minerListView.Columns[0].Text = "IP地址";
                minerListView.Columns[1].Text = "算力";
                minerListView.Columns[2].Text = "矿池";
                minerListView.Columns[3].Text = "用户";
                minerListView.Columns[4].Text = "子账户";
                minerListView.Columns[5].Text = "DAG进度";
                minerListView.Columns[6].Text = "自检进度";
                minerListView.Columns[7].Text = "算力板状态";
                minerListView.Columns[8].Text = "接受率";
                minerListView.Columns[9].Text = "接受额";
                minerListView.Columns[10].Text = "拒绝额";
                minerListView.Columns[11].Text = "MAC地址";
                minerListView.Columns[12].Text = "运行时间";
                minerListView.Columns[13].Text = "温度";
                minerListView.Columns[14].Text = "风扇转速";
                minerListView.Columns[15].Text = "固件";
                minerListView.Columns[16].Text = "板1状态";
                minerListView.Columns[17].Text = "板1算力";
                minerListView.Columns[18].Text = "板1温度";
                minerListView.Columns[19].Text = "板2状态";
                minerListView.Columns[20].Text = "板2算力";
                minerListView.Columns[21].Text = "板2温度";
                minerListView.Columns[22].Text = "板3状态";
                minerListView.Columns[23].Text = "板3算力";
                minerListView.Columns[24].Text = "板3温度";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            }
        }


        //Logic for determining when to translate to what
        private void translate_Click(object sender, EventArgs e)
        {
            if (isEnglish)
            {
                isEnglish = false;
                translateLanguage();
            }
            else
            {
                isEnglish = true;
                translateLanguage();
            }
        }

        
        //Runs when the '<- search" button is pressed
        private async void searchButton_Click(object sender, EventArgs e)
        {
            if (isScanning)
            {
                // Cancel the ongoing scan
                cancellationTokenSource.Cancel();

                while (isScanning)
                {
                    await Task.Delay(100); // Check every 100ms if the scan has completed
                }
            }
            //Resets minerFound variable to 0
            // Reset the flag and cancellation token source
            isScanning = true;
            cancellationTokenSource = new CancellationTokenSource();

            //Try-Catch to clear data from the minerListView
            try
            {
                minerListView.Items.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            minersFoundCount = 0;
            minersFoundCountUpdate();

            if (string.IsNullOrWhiteSpace(startIPTextBoxA.Text) || string.IsNullOrWhiteSpace(startIPTextBoxB.Text) || string.IsNullOrWhiteSpace(startIPTextBoxC.Text) || string.IsNullOrWhiteSpace(startIPTextBoxD.Text) || string.IsNullOrWhiteSpace(endIPTextBoxA.Text) || string.IsNullOrWhiteSpace(endIPTextBoxB.Text) || string.IsNullOrWhiteSpace(endIPTextBoxC.Text) || string.IsNullOrWhiteSpace(endIPTextBoxD.Text))
            {
                if (isEnglish)
                {
                    MessageBox.Show("Please fill in all fields for the start and end IP address");
                }
                else
                {
                    MessageBox.Show("请填写开始与结束IP地址");
                }
                return;
            }
            //Declare variables which store the IPs that will be used to search the range
            string startIP = (Int32.Parse(startIPTextBoxA.Text).ToString() + "." + Int32.Parse(startIPTextBoxB.Text).ToString() + "." + Int32.Parse(startIPTextBoxC.Text).ToString() + "." + Int32.Parse(startIPTextBoxD.Text).ToString());
            string endIP = (Int32.Parse(endIPTextBoxA.Text).ToString() + "." + Int32.Parse(endIPTextBoxB.Text).ToString() + "." + Int32.Parse(endIPTextBoxC.Text).ToString() + "." + Int32.Parse(endIPTextBoxD.Text).ToString());


            //Calls the GetIPRange function with the start and end IP variables and stores the returned list as 'ipList'
            List<string> ipList = GetIPRange(startIP, endIP);

            //Initialize a semaphore with 500 parallel tasks
            semaphore = new SemaphoreSlim(maxConcurrentScans);

            try
            {
                // Run the scanning process with cancellation support
                await ScanIPsAsync(ipList, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Scanning was canceled.");
            }
            finally
            {
                // Release resources
                semaphore.Dispose();
                isScanning = false;
            }
        }


        //Similar to the SearchButton_Click function, Just using the ip range list view instead
        private async void scanSelectedButton_Click(object sender, EventArgs e)
        {
            if (isScanning)
            {
                // Cancel the ongoing scan
                cancellationTokenSource.Cancel();

                while (isScanning)
                {
                    await Task.Delay(100); // Check every 100ms if the scan has completed
                }
            }
            //Calls the GetIPRange function with the start and end IP variables and stores the returned list as 'ipList'
            List<string> ipList = new List<string> { };

            //Resets minerFound variable to 0
            // Reset the flag and cancellation token source
            isScanning = true;
            cancellationTokenSource = new CancellationTokenSource();

            //Try-Catch to clear data from the minerListView
            try
            {
                minerListView.Items.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            minersFoundCount = 0;
            minersFoundCountUpdate();

            foreach (ListViewItem ipItem in ipRangeListView.Items)
            {
                //If the items checkbox is checked, it runs the GetIPRange function and adds that list to the others
                if (ipItem.Checked)
                {
                    string startIP = ipItem.SubItems[0].Text;
                    string endIP = ipItem.SubItems[1].Text;
                    ipList.AddRange(GetIPRange(startIP, endIP));

                }

            }

            //Initialize a semaphore with the specified amount of concurrent operations (can be changed with the settings menu)
            semaphore = new SemaphoreSlim(maxConcurrentScans);

            try
            {
                // Run the scanning process with cancellation support
                await ScanIPsAsync(ipList, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Scanning was canceled.");
            }
            finally
            {
                // Release resources
                semaphore.Dispose();
                isScanning = false;
            }
        }


        private async Task ScanIPsAsync(List<string> ipList, CancellationToken cancellationToken)
        {
            List<Task> tasks = new List<Task>();

            foreach (var ip in ipList)
            {
                // Start scanning each IP asynchronously, limiting concurrency with the semaphore
                tasks.Add(ScanAndPingIPAsync(ip, cancellationToken));
            }

            // Await all ping tasks to complete
            await Task.WhenAll(tasks);
            if (isEnglish)
            {
                ScanningIPLabel.Text = "Scanning Done";
            }
            else
            {
                ScanningIPLabel.Text = "扫描完成";
            }
        }


        private async Task ScanAndPingIPAsync(string ip, CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken); // Wait to enter the semaphore

            try
            {
                int maxRetries = maxRepeatScan;
                bool success = false;
                double currentTimeout = timeoutTime;

                for (int attempt = 1; attempt <= maxRetries && !success; attempt++)
                {
                    try
                    {
                        // Adjust the client timeout dynamically
                        success = await GetData(ip, currentTimeout, cancellationToken);

                        if (!success)
                        {
                            currentTimeout += 0.2; // Increase the timeout by 0.2 seconds for the next attempt
                            if (attempt < maxRetries)
                            {
                                Console.WriteLine($"Retrying {ip}: Attempt {attempt + 1} with {currentTimeout}s timeout.");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to scan {ip} after {maxRetries} attempts.");
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Scanning was canceled.");
                        break; // Stop retries if cancellation is requested
                    }
                }
            }
            finally
            {
                semaphore.Release(); // Release the semaphore slot
            }
        }


        private async Task<bool> GetData(string minerIP, double currentTimeout, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (isEnglish)
            {
                cancellationToken.ThrowIfCancellationRequested();
                ScanningIPLabel.Text = ($"Scanning {minerIP}");
            }
            else
            {
                ScanningIPLabel.Text = ($"扫描 {minerIP}");
            }
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                //Create new instance of httpChient
                using (HttpClient client = new HttpClient())
                {
                    //sets the timeout of each ip to the timeout variable declared at the top
                    client.Timeout = TimeSpan.FromSeconds(currentTimeout);
                    //Allows improperly formatted data to still be accpted
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    try
                    {
                        //Get data about Miner
                        //send a request for the data which comes back as a JSON and gets parsed
                        //Multiple get requests are needed for all the information

                        // Check for cancellation before each request to stop the scan as soon as possible
                        cancellationToken.ThrowIfCancellationRequested();

                        var response = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_uptime_pools_minerinfo");
                        cancellationToken.ThrowIfCancellationRequested();
                        var minerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response.ToString());

                        var selfResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_selfcheck_progress");
                        cancellationToken.ThrowIfCancellationRequested();
                        var selfMinerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(selfResponse.ToString());

                        var poolResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_pools");
                        cancellationToken.ThrowIfCancellationRequested();
                        var poolInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(poolResponse.ToString());

                        var firmwareResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_overview");
                        cancellationToken.ThrowIfCancellationRequested();
                        var firmwareInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(firmwareResponse.ToString());

                        //Make sure the entry doesnt already exist
                        bool alreadyExists = false;
                        minerListView.Invoke((Action)(() =>
                        {
                            foreach (ListViewItem item in minerListView.Items)
                            {
                                if (item.Text == minerIP) // Compare with IP in the list
                                {
                                    alreadyExists = true;
                                    break;
                                }
                            }
                        }));

                        if (alreadyExists)
                        {
                            return true; // Skip adding this entry if it already exists
                        }

                        cancellationToken.ThrowIfCancellationRequested();
                        
                        try
                        {
                            //Determine hashboard status
                            string hash1 = minerInfo.data.minerinfo[0].hash_rate > 10 ? "O" : "X";
                            string hash1a = minerInfo.data.minerinfo[0].hash_rate > 10 ? "Working" : "Not Working";

                            string hash2 = minerInfo.data.minerinfo[1].hash_rate > 10 ? "O" : "X";
                            string hash2a = minerInfo.data.minerinfo[1].hash_rate > 10 ? "Working" : "Not Working";

                            string hash3 = minerInfo.data.minerinfo[2].hash_rate > 10 ? "O" : "X";
                            string hash3a = minerInfo.data.minerinfo[2].hash_rate > 10 ? "Working" : "Not Working";

                            //Determine Miner Status
                            string minerStatus = "";
                            if (minerInfo.data.uptime.dag.progress > 0 && minerInfo.data.uptime.dag.progress < 100)
                            {
                                minerStatus = "Writing DAG";
                            }
                            else if(Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2) > 0 && Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2) < 100)
                            {
                                minerStatus = "Running Self Check";
                            }
                            else if (minerInfo.data.uptime.hash_rate > 1)
                            {
                                minerStatus = "Hashing";
                            }
                            else
                            {
                                minerStatus = "Needs Self Check";
                            }

                            //Uses the parsed JSON to extract data and set it to the various attributes of the MinerInfo class
                            MinerInfo info = new MinerInfo
                            {
                                MinerIPAddress = minerIP,
                                HashRate = minerInfo.data.uptime.hash_rate.ToString(),
                                Uptime = minerInfo.data.uptime.up_time.ToString(),
                                HashboardStatus = ($"{hash1} , {hash2} , {hash3}"),
                                FanSpeed = minerInfo.data.uptime.fan_speed.ToString(),
                                Temperature = ((minerInfo.data.minerinfo[0].temperature) + (minerInfo.data.minerinfo[1].temperature) + (minerInfo.data.minerinfo[2].temperature)) / 3.ToString(),
                                DagProgress = (Math.Round(Convert.ToDouble(minerInfo.data.uptime.dag.progress), 2).ToString()) + " %",
                                Accepted = minerInfo.data.uptime.accept_num.ToString(),
                                Rejected = minerInfo.data.uptime.reject_num.ToString(),
                                AcceptedRate = Math.Round((100 - (100 * Convert.ToDouble(minerInfo.data.uptime.reject_num) / Convert.ToDouble(minerInfo.data.uptime.accept_num))), 2).ToString(),
                                Pool = minerInfo.data.pooldata[0].pool.ToString(),
                                PoolUser = poolInfo.data.username,
                                SelfCheckProgress = (Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2).ToString()) + " %",
                                Hashboard1Status = hash1a,
                                Hashboard1HashRate = minerInfo.data.minerinfo[0].hash_rate,
                                Hashboard1Temperature = minerInfo.data.minerinfo[0].temperature,
                                Hashboard2Status = hash2a,
                                Hashboard2HashRate = minerInfo.data.minerinfo[1].hash_rate,
                                Hashboard2Temperature = minerInfo.data.minerinfo[1].temperature,
                                Hashboard3Status = hash3a,
                                Hashboard3HashRate = minerInfo.data.minerinfo[2].hash_rate,
                                Hashboard3Temperature = minerInfo.data.minerinfo[2].temperature,
                                Account = minerInfo.data.pooldata[0].user.ToString(),
                                FirmwareVersion = firmwareInfo.data.form1.platform_version,
                                MacAddress = firmwareInfo.data.form1.mac_address
                            };
                            minersFoundCount++;
                            minersFoundCountUpdate();
                            ListViewItem item = new ListViewItem(minerIP);
                            item.SubItems.Add(info.HashRate);
                            item.SubItems.Add(info.Pool);
                            item.SubItems.Add(info.PoolUser);
                            item.SubItems.Add(info.Account);
                            item.SubItems.Add(info.DagProgress);
                            item.SubItems.Add(info.SelfCheckProgress);
                            item.SubItems.Add(info.HashboardStatus);
                            item.SubItems.Add(info.AcceptedRate);
                            item.SubItems.Add(info.Accepted);
                            item.SubItems.Add(info.Rejected);
                            item.SubItems.Add(info.MacAddress);
                            item.SubItems.Add(info.Uptime);
                            item.SubItems.Add(info.Temperature);
                            item.SubItems.Add(info.FanSpeed);
                            item.SubItems.Add(info.FirmwareVersion);
                            item.SubItems.Add(info.Hashboard1Status);
                            item.SubItems.Add(info.Hashboard1HashRate);
                            item.SubItems.Add(info.Hashboard1Temperature);
                            item.SubItems.Add(info.Hashboard2Status);
                            item.SubItems.Add(info.Hashboard2HashRate);
                            item.SubItems.Add(info.Hashboard2Temperature);
                            item.SubItems.Add(info.Hashboard3Status);
                            item.SubItems.Add(info.Hashboard3HashRate);
                            item.SubItems.Add(info.Hashboard3Temperature);
                            item.SubItems.Add(minerStatus);

                            cancellationToken.ThrowIfCancellationRequested();

                            minerListView.Items.Add(item);

                            await Task.Yield();
                            return true;
                        }
                        catch (OperationCanceledException)
                        {
                            // Handle cancellation exception if triggered during any operation
                            Console.WriteLine("Scanning was canceled.");
                            return false;
                        }
                        catch (Exception ex)
                        {
                            //if there is an error reading the parsed data it returns null
                            Console.WriteLine(ex.Message);
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        //This function takes the start and end ip and returns a list with all the IPs in the range
        private List<string> GetIPRange(string startIPString, string endIPString)
        {
            //declaring new list
            List<string> ipList = new List<string>();
            //declaring IPAddress variables
            try
            {
                // Convert the start and end IP addresses to integers
                if (System.Net.IPAddress.TryParse(startIPString, out IPAddress startIp) && System.Net.IPAddress.TryParse(endIPString, out IPAddress endIp))
                {
                    byte[] startIpBytes = startIp.GetAddressBytes();
                    byte[] endIpBytes = endIp.GetAddressBytes();

                    // Compare the numeric values of startIP and endIP
                    for (int i = 0; i < 4; i++)
                    {
                        if (startIpBytes[i] > endIpBytes[i])
                        {
                            //Makes sure the start IP is lower than the end ip
                            if (isEnglish)
                            {
                                MessageBox.Show("Make sure the starting IP is lower than the end");
                            }
                            else
                            {
                                MessageBox.Show("请输入可用IP段");
                            }
                            return new List<string>();
                        }
                        else if (startIpBytes[i] < endIpBytes[i])
                        {
                            //if the start IP is lower, continue with the loop
                            break;
                        }
                    }

                    while (true)
                    {
                        // Iterate through the range of IP addresses
                        for (int i = startIpBytes[3]; i <= 255; i++)
                        {
                            startIpBytes[3] = (byte)i;
                            IPAddress currentIp = new IPAddress(startIpBytes);

                            ipList.Add(currentIp.ToString());

                            // Check if the last octet reached the end IP's last octet
                            if (startIpBytes[3] == endIpBytes[3] &&
                                startIpBytes[2] == endIpBytes[2])
                            {
                                return ipList; // Exit the program when done
                            }
                        }

                        // Increment the second-to-last octet
                        startIpBytes[2]++;

                        // Reset the last octet to 0
                        startIpBytes[3] = 0;

                        // Check if the second-to-last octet reached the end IP's second-to-last octet
                        if (startIpBytes[2] > endIpBytes[2])
                        {
                            return ipList; // Exit the program when done
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("There was an issue with the IP Address. Please double check you have entered a valid IP.    Error: " + ex.Message);
                return null;
            }
        }




        //***************************************************************************



        private void addIPButton_Click(object sender, EventArgs e)
        {
            string startIP1 = (startIPTextBoxA.Text + "." + startIPTextBoxB.Text + "." + startIPTextBoxC.Text + "." + startIPTextBoxD.Text);
            string endIP1 = (endIPTextBoxA.Text + "." + endIPTextBoxB.Text + "." + endIPTextBoxC.Text + "." + endIPTextBoxD.Text);
            string rangeName = nameTextBox.Text;
            //Checks to make sure the IP boxes are not empty
            if (string.IsNullOrWhiteSpace(startIPTextBoxA.Text) || string.IsNullOrWhiteSpace(startIPTextBoxB.Text) || string.IsNullOrWhiteSpace(startIPTextBoxC.Text) || string.IsNullOrWhiteSpace(startIPTextBoxD.Text) || string.IsNullOrWhiteSpace(endIPTextBoxA.Text) || string.IsNullOrWhiteSpace(endIPTextBoxB.Text) || string.IsNullOrWhiteSpace(endIPTextBoxC.Text) || string.IsNullOrWhiteSpace(endIPTextBoxD.Text))
            {
                if (isEnglish)
                {
                    MessageBox.Show("Please fill in all fields for the start and end IP address");
                }
                else
                {
                    MessageBox.Show("请填写开始与结束IP地址");
                }
                return;
            }

            // Create a new ListViewItem
            ListViewItem newItem = new ListViewItem(startIP1);
            newItem.SubItems.Add(endIP1);
            newItem.SubItems.Add(rangeName);
            // Add the new item to the ListView
            ipRangeListView.Items.Add(newItem);
        }


        private void minerListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lastColumnClicked)
            {
                // Reverse the sort order if the same column is clicked again
                lastSortOrder = (lastSortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                lastSortOrder = SortOrder.Ascending;
            }

            lastColumnClicked = e.Column;

            // Create a new ListViewItemComparer for sorting
            minerListView.ListViewItemSorter = new ListViewItemComparer(e.Column, lastSortOrder);

            // Sort the items
            minerListView.Sort();
        }


        private void LoadSavedIPRanges()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                foreach (string key in appSettings.AllKeys)
                {
                    if (key.StartsWith(SavedRangesKey))
                    {
                        string range = appSettings[key];

                        // Split the range string into start and end IPs
                        string[] ipRange = range.Split('-');
                        if (ipRange.Length == 3)
                        {
                            string startIP = ipRange[0].Trim();
                            string endIP = ipRange[1].Trim();
                            string name = ipRange[2].Trim();

                            // Create a new ListViewItem with start and end IPs
                            if (startIP != "" && startIP != null)
                            {
                                ListViewItem newItem = new ListViewItem(startIP);
                                newItem.SubItems.Add(endIP);
                                newItem.SubItems.Add(name);
                                // Add the new item to the ipRangeListView
                                ipRangeListView.Items.Add(newItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle configuration loading error, if any
                Console.WriteLine("Error loading saved IP ranges: " + ex.Message);
            }
        }


        private void SaveCurrentIPRange()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                foreach (ListViewItem item in ipRangeListView.Items)
                {
                    string startIP = item.SubItems[0].Text;
                    string endIP = item.SubItems[1].Text;
                    string name = item.SubItems[2].Text;

                    // Generate a unique key based on timestamp
                    string newKey = SavedRangesKey + "_" + startIP + "-" + endIP + "-" + name;

                    // Check if the key already exists before adding it
                    if (config.AppSettings.Settings[newKey] == null)
                    {
                        // Combine start and end IPs into a range string
                        string currentRange = startIP + "-" + endIP + "-" + name;

                        // Add the current IP range to the app settings
                        config.AppSettings.Settings.Add(newKey, currentRange);
                    }
                }

                // Save the configuration
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (ConfigurationErrorsException ex)
            {
                // Handle configuration saving error, if any
                Console.WriteLine("Error saving current IP range: " + ex.Message);
            }
        }


        private void UpdateConfiguration()
        {
            // Clear existing saved IP ranges from the configuration
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var appSettings = config.AppSettings.Settings;

            foreach (string key in appSettings.AllKeys)
            {
                appSettings.Remove(key);
            }

            // Re-add IP ranges from the ipRangeListView to the configuration
            int index = 1;
            foreach (ListViewItem item in ipRangeListView.Items)
            {
                string startIP = item.SubItems[0].Text;
                string endIP = item.SubItems[1].Text;
                string name = item.SubItems[2].Text;
                string range = $"{startIP}-{endIP}-{name}";

                // Generate a unique key based on the index
                string key = $"{SavedRangesKey}_{index}";
                appSettings.Add(key, range);
                index++;
            }

            // Save the updated configuration
            config.Save(ConfigurationSaveMode.Modified);
        }


        private void deleteIPRanges_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem thisItem in ipRangeListView.Items)
            {
                if (thisItem.Checked)
                {
                    ipRangeListView.Items.Remove(thisItem);
                }
            }
            UpdateConfiguration();
        }


        private void stopScanButton_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            ScanningIPLabel.Text = "Stopped Scan";
        }


        private void loadIPRangesButton_Click(object sender, EventArgs e)
        {
            if (loadIPRangesDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = loadIPRangesDialog.FileName;

                // Read the IP ranges from the selected file
                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    // Process each line in the file (assuming each line contains an IP range)
                    foreach (string line in lines)
                    {
                        // Split the line into start and end IP addresses
                        string[] parts = line.Split('-');
                        if (parts.Length == 3)
                        {
                            string startIP = parts[0].Trim();
                            string endIP = parts[1].Trim();
                            string name = parts[2].Trim();

                            // Create a new ListViewItem with start and end IPs
                            ListViewItem newItem = new ListViewItem(startIP);
                            newItem.SubItems.Add(endIP);
                            newItem.SubItems.Add(name);

                            // Add the new item to the ipRangeListView
                            ipRangeListView.Items.Add(newItem);
                        }
                    }
                }
                catch (IOException ex)
                {
                    if (isEnglish)
                    {
                        MessageBox.Show("Error reading the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("读取文件时出错: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }


        private void saveRangesButton_Click(object sender, EventArgs e)
        {
            if (saveIPRangesDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveIPRangesDialog.FileName;

                try
                {
                    // Create a list to store the IP ranges in the format "startIP - endIP"
                    List<string> ipRanges = new List<string>();

                    // Iterate through the items in ipRangeListView and add them to the list
                    foreach (ListViewItem item in ipRangeListView.Items)
                    {
                        if (item.Checked)
                        {
                            string startIP = item.SubItems[0].Text;
                            string endIP = item.SubItems[1].Text;
                            string name = item.SubItems[2].Text;
                            string ipRange = $"{startIP}-{endIP}-{name}";
                            ipRanges.Add(ipRange);
                        }
                    }

                    // Write the IP ranges to the selected file
                    File.WriteAllLines(filePath, ipRanges);


                    if (isEnglish)
                    {
                        MessageBox.Show("IP ranges have been successfully saved to the file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("IP段已成功添加到本地文件", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {

                    if (isEnglish)
                    {
                        MessageBox.Show("Error saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("保存文件时出错: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
        private async Task<string> UploadFileAsync(string fileToUpload, string server)
        {
            try
            {
                string url = $"http://{server}/cgi-bin/cgiNetService.cgi";
                using (WebClient myClient = new WebClient())
                {
                    myClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] responseBytes = await myClient.UploadFileTaskAsync(url, "POST", fileToUpload);
                    return Encoding.UTF8.GetString(responseBytes);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return $"Error uploading to {server}: {ex.Message}";
            }
        }


        //Open IP In browser and login
        private async Task<ChromeDriver> GetNewDriverAsync()
        {
            return await Task.Run(() =>
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                return new ChromeDriver(chromeDriverService, new ChromeOptions());
            });
        }


        private async Task OpenInBrowserAsync(string ipAddress)
        {
            string url = $"http://{ipAddress}";
            bool success = false;
            int retryCount = 0;
            int maxRetries = 2;

            while (!success && retryCount < maxRetries)
            {
                try
                {
                    // Ensure driver is initialized
                    if (driver == null)
                    {
                        driver = await GetNewDriverAsync();
                    }

                    // Open a new tab or handle the existing driver window
                    string driverWindowHandle = driver.CurrentWindowHandle;
                    driver?.SwitchTo().NewWindow(WindowType.Tab);


                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);  // Timeout for page load
                    driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);  // Timeout for scripts
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);  // Timeout for finding elements

                    // Navigate to the URL and automatically log in
                    driver.Navigate().GoToUrl(url);
                    driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("Admin-Token", "admin-token"));
                    driver.Navigate().Refresh();

                    success = true; // Mark success if no exceptions occur
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine($"Timeout loading {url}. Retrying...");
                    retryCount++;  // Increment retry count and retry the IP
                }
                catch (NoSuchWindowException)
                {
                    driver?.Quit();
                    driver = await GetNewDriverAsync();
                    retryCount++;  // Increment retry count and retry the IP
                }
                catch (WebDriverException)
                {
                    driver?.Quit();
                    driver = await GetNewDriverAsync();
                    retryCount++;  // Increment retry count and retry the IP
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    retryCount++;  // Increment retry count for any other exception
                }
            }

            if (!success)
            {
                Console.WriteLine($"Failed to open {url} after {maxRetries} attempts.");
            }
        }


        //End of open In Browser


        private async void minerListView_DoubleClick(object sender, EventArgs e)
        {
            if (minerListView.SelectedItems.Count > 0)
            {
                string selectedIPAddress = minerListView.SelectedItems[0].Text;

                // Open the selected IP address in a browser asynchronously
                await OpenInBrowserAsync(selectedIPAddress);
            }

            minerListView.SelectedItems[0].Checked = false;
        }

        private void SaveListViewSettings()
        {
            List<string> columnSettings = new List<string>();
            foreach (ColumnHeader column in minerListView.Columns)
            {
                columnSettings.Add($"{column.Index},{column.DisplayIndex},{column.Width}");
            }
            YAMI_Scanner.Properties.Settings.Default.MinerListViewSettings = string.Join(";", columnSettings);

            Console.WriteLine(columnSettings);
            Console.WriteLine(YAMI_Scanner.Properties.Settings.Default.MinerListViewSettings);
            YAMI_Scanner.Properties.Settings.Default.Save();
        }


        private void LoadListViewSettings()
        {
            string savedSettings = YAMI_Scanner.Properties.Settings.Default.MinerListViewSettings;
            if (!string.IsNullOrEmpty(savedSettings))
            {
                string[] columns = savedSettings.Split(';');
                foreach (string column in columns)
                {
                    string[] details = column.Split(',');
                    int index = int.Parse(details[0]);
                    int displayIndex = int.Parse(details[1]);
                    int width = int.Parse(details[2]);

                    minerListView.Columns[index].DisplayIndex = displayIndex;
                    minerListView.Columns[index].Width = width;
                }
            }
        }

        private void minerListView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            SaveListViewSettings();
        }

        private void minerListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
        }

        //* * * COMMANDS TAB * * * * * * * * * * * * * * * * * * COMMANDS TAB * * * * * * * * * * * * * * * * * * COMMANDS TAB * * * * * * * * * * * * * * * * *

        //********** Button Listener Functions **********

        //Reboot Selected Miners
        private async void rebootButton_Click(object sender, EventArgs e)
        {
            // Ensure cancellationTokenSource is initialized
            if (cancellationTokenSourceReboot == null)
            {
                cancellationTokenSourceReboot = new CancellationTokenSource();
            }

            // Initialize or reinitialize semaphore
            semaphoreReboot = new SemaphoreSlim(maxConcurrentScans);

            try
            {
                await ExecuteMinerCommandAsync("send_reboot_miner=send_reboot_miner", "Reboot", cancellationTokenSourceReboot.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during reboot: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Dispose();
                // Update status after completion
                if (isEnglish)
                {
                    ScanningIPLabel.Text = "Reboot Done";
                }
                else
                {
                    ScanningIPLabel.Text = "重启完毕";
                }
            }
        }


        //Start A Self Check On Selected Miners
        private async void selfCheckButton_Click(object sender, EventArgs e)
        {
            // Ensure cancellationTokenSource is initialized
            if (cancellationTokenSourceSelfCheck == null)
            {
                cancellationTokenSourceSelfCheck = new CancellationTokenSource();
            }

            // Initialize or reinitialize semaphore
            semaphoreReboot = new SemaphoreSlim(maxConcurrentScans);

            try
            {
                await ExecuteMinerCommandAsync("send_selfcheck_instructions=send_selfcheck_instructions", "Self-Check", cancellationTokenSourceSelfCheck.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during reboot: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Dispose();
                // Update status after completion
                if (isEnglish)
                {
                    ScanningIPLabel.Text = "Done Starting Self Check";
                }
                else
                {
                    ScanningIPLabel.Text = "结束自检";
                }
            }
        }


        //Open and Login Selected Miners In A Browser
        private async void openInBrowserButton_Click(object sender, EventArgs e)
        {
            List<Task> browserTasks = new List<Task>();

            for (int i = 0; i < minerListView.Items.Count; i++)
            {
                if (minerListView.Items[i].Checked)
                {
                    string ipAddress = minerListView.Items[i].Text;
                    browserTasks.Add(OpenInBrowserAsync(ipAddress));
                }
            }

            // Await the completion of all browser opening tasks
            await Task.WhenAll(browserTasks);
        }


        // Sets The Selected Miners To Use A Dynamic IP
        private async void setDynamicIPButton_Click(object sender, EventArgs e)
        {
            // Ensure cancellationTokenSource is initialized
            if (cancellationTokenSourceSelfCheck == null)
            {
                cancellationTokenSourceSelfCheck = new CancellationTokenSource();
            }

            // Initialize or reinitialize semaphore
            semaphoreReboot = new SemaphoreSlim(maxConcurrentScans);

            try
            {
                await ExecuteMinerCommandAsync("setDynamicIP", "", cancellationTokenSourceReboot.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during setDynamicIP: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Dispose();
                // Update status after completion
                if (isEnglish)
                {
                    ScanningIPLabel.Text = "Done Setting Dynamic";
                }
                else
                {
                    ScanningIPLabel.Text = "动态IP设置完成";
                }
            }
            /*
            // /cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22dhcp%22,%22ip%22:%2210.100.5.36%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.100.5.254%22,%22dns1%22:%228.8.8.8%22,%22dns2%22:%22114.114.114.114%22%7D
            for (int i = 0; i < minerListView.Items.Count; i++)
                {

                    try
                    {
                        if (minerListView.Items[i].Checked)
                        {
                            string minerIp = minerListView.Items[i].Text;
                            string newIp = newIpTextBox.Text;
                            if (isEnglish)
                            {
                                ScanningIPLabel.Text = minerIp + " Setting Dynamic IP";
                            }
                            else
                            {
                                ScanningIPLabel.Text = minerIp + " 设置动态IP";
                            }
                            using (HttpClient setPool = new HttpClient())
                            {
                                setPool.Timeout = TimeSpan.FromSeconds(1);
                                setPool.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                                var setPoolResponse = await setPool.GetStringAsync($"http://{minerIp}//cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22dhcp%22,%22ip%22:%22{minerListView.Items[i].Text}%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.100.5.254%22,%22dns1%22:%228.8.8.8%22,%22dns2%22:%22114.114.114.114%22%7D");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("set dynamic exception: " + ex);
                    }

                }
                if (isEnglish)
                {
                    ScanningIPLabel.Text = "Done Setting Dynamic";
                }
                else
                {
                    ScanningIPLabel.Text = "动态IP设置完成";
                }
                */
        }

        private bool pingIp(string ip)
        {

            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send(ip, 1000); // Timeout is 1000 ms (1 second)
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine(ip + " Successful ping");
                    return true;
                }
                else
                {
                    Console.WriteLine(ip + " Unsuccessful ping");
                    return false;
                }
            }
        }

        // Sets A Static IP Of The Selected Miner (Implements check to prevent duplicate IPs)
        private async void setStaticIPButton_Click(object sender, EventArgs e)
        {
            // /cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22static%22,%22ip%22:%2210.100.5.32%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.100.5.254%22,%22dns1%22:%221.1.1.1%22,%22dns2%22:%22%22%7D
            if (minerListView.SelectedItems.Count == 1)
            {

                if (cancellationTokenSourceReboot == null)
                {
                    cancellationTokenSourceReboot = new CancellationTokenSource();
                }

                // Initialize or reinitialize semaphore
                semaphoreReboot = new SemaphoreSlim(maxConcurrentScans);

                try
                {
                    await ExecuteMinerCommandAsync("setStaticIP", "Set Static IP", cancellationTokenSourceReboot.Token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while setting static ip: {ex.Message}");
                }
                finally
                {
                    semaphoreReboot.Dispose();
                    // Update status after completion
                    if (isEnglish)
                    {
                        ScanningIPLabel.Text = "Done Setting Static IP";
                    }
                    else
                    {
                        ScanningIPLabel.Text = "静态IP设置完成";
                    }
                }
            }
            else if(minerListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a miner");
            }
            else
            {
                MessageBox.Show("Please select only 1 miner");
            }
        }


        //Change The Performance Mode Of Selected Miners
        private async void performanceButton_Click(object sender, EventArgs e)
        {
            // /cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22dhcp%22,%22ip%22:%2210.100.5.36%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.100.5.254%22,%22dns1%22:%228.8.8.8%22,%22dns2%22:%22114.114.114.114%22%7D
            for (int i = 0; i < minerListView.Items.Count; i++)
            {

                try
                {
                    if (minerListView.Items[i].Checked)
                    {
                        string minerIp = minerListView.Items[i].Text;
                        string newIp = newIpTextBox.Text;
                        if (isEnglish)
                        {
                            ScanningIPLabel.Text = minerIp + " Setting Dynamic IP";
                        }
                        else
                        {
                            ScanningIPLabel.Text = minerIp + " 设置动态IP";
                        }
                        using (HttpClient setPool = new HttpClient())
                        {
                            setPool.Timeout = TimeSpan.FromSeconds(1);
                            setPool.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                            var setPoolResponse = await setPool.GetStringAsync($"http://{minerIp}//cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22dhcp%22,%22ip%22:%22{minerListView.Items[i].Text}%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.100.5.254%22,%22dns1%22:%228.8.8.8%22,%22dns2%22:%22114.114.114.114%22%7D");
                        }
                        string poolUrl = minerListView.Items[i].SubItems[2].Text;
                        string poolUser = minerListView.Items[i].SubItems[4].Text;
                        string poolIpSet = newIp.Replace('.', 'x');
                        poolUrl = WebUtility.UrlEncode(poolUrl);
                        poolUser = WebUtility.UrlEncode(poolUser);
                        using (HttpClient setPool = new HttpClient())
                        {
                            setPool.Timeout = TimeSpan.FromSeconds(1);
                            setPool.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                            var setPoolResponse = await setPool.GetStringAsync($"http://{minerIp}//cgi-bin/cgiNetService.cgi?send_pools_params=%7B%22poolurl%22:%22{poolUrl}%22,%22username%22:%22{poolUser}.{poolIpSet}%22,%22password%22:%22X%22,%22currency%22:%22ETC%22%7D");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("set dynamic exception: " + ex);
                }

            }
            if (isEnglish)
            {
                ScanningIPLabel.Text = "Done Setting Dynamic";
            }
            else
            {
                ScanningIPLabel.Text = "动态IP设置完成";
            }
        }


        //Set New Pool(Account, Pool Address And Worker Name) For Selected Miners
        private async void setPoolButton_Click(object sender, EventArgs e)
        {
            // Ensure cancellationTokenSource is initialized
            if (cancellationTokenSourceReboot == null)
            {
                cancellationTokenSourceReboot = new CancellationTokenSource();
            }

            // Initialize or reinitialize semaphore
            semaphoreReboot = new SemaphoreSlim(maxConcurrentScans);

            try
            {
                await ExecuteMinerCommandAsync("setPool", "", cancellationTokenSourceReboot.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during setPool: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Dispose();
                // Update status after completion
                if (isEnglish)
                {
                    ScanningIPLabel.Text = "Done Setting Pools";
                }
                else
                {
                    ScanningIPLabel.Text = "矿池配置完成";
                }
            }

        }


        //Clears The Listview Of All Miners
        private void clearListview_Click(object sender, EventArgs e)
        {
            minerListView.Items.Clear();
            minersFoundCount = 0;
            minersFoundCountUpdate();
        }


        //Refresh The Data Of Selected Miners
        private async void RefreshDataBtn_Click(object sender, EventArgs e)
        {
            semaphoreReboot = new SemaphoreSlim(maxConcurrentScans);
            if (cancellationTokenSourceReboot != null && !cancellationTokenSourceReboot.IsCancellationRequested)
            {
                cancellationTokenSourceReboot.Cancel();
                await Task.Delay(500); // Give some time to cancel the previous operation
            }

            cancellationTokenSourceReboot = new CancellationTokenSource();
            await ExecuteMinerCommandAsync("refresh", "Refreshing miners", cancellationTokenSourceReboot.Token);
        }


        //Set A New Password For Selected Miners
        private async void setPasswordButton_Click(object sender, EventArgs e)
        {
            // /cgi-bin/cgiNetService.cgi?send_change_password_param=%7B%22user%22:%22admin%22,%22current_admin_password%22:%22admin%22,%22new_password%22:%22admin%22,%22confirm_password%22:%22admin%22%7D
            for (int i = 0; i < minerListView.Items.Count; i++)
            {

                try
                {
                    if (minerListView.Items[i].Checked)
                    {
                        string minerIp = minerListView.Items[i].Text;
                        string newPass = newPasswordTextBox.Text;
                        if (isEnglish)
                        {
                            ScanningIPLabel.Text = minerIp + " Setting Password";
                        }
                        else
                        {
                            ScanningIPLabel.Text = minerIp + " 正在设置密码";
                        }
                        newPass = WebUtility.UrlEncode(newPass);
                        using (HttpClient setPool = new HttpClient())
                        {
                            setPool.Timeout = TimeSpan.FromSeconds(1);
                            setPool.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                            var setPoolResponse = await setPool.GetStringAsync($"http://{minerIp}//cgi-bin/cgiNetService.cgi?send_change_password_param=%7B%22user%22:%22admin%22,%22current_admin_password%22:%22admin%22,%22new_password%22:%22{newPass}%22,%22confirm_password%22:%22a{newPass}%22%7D");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("set password exception: " + ex);
                }

            }
            if (isEnglish)
            {
                ScanningIPLabel.Text = "Done Setting Passwords";
            }
            else
            {
                ScanningIPLabel.Text = "设置密码完成";
            }
        }


        //Update Selected Miners
        private async void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Title = "Select The Update File";
                openFileDialog.Filter = "Tar Gzip Files (*.tar.gz)|*.tar.gz|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileToUpload = openFileDialog.FileName;

                    List<string> ipAddressesToUpdate = new List<string>();

                    // Iterate over the items in the minerListView
                    foreach (ListViewItem item in minerListView.Items)
                    {
                        // Check if the item is checked
                        if (item.Checked)
                        {
                            // Assuming the IP address is stored in the first subitem
                            string ipAddress = item.Text;
                            ipAddressesToUpdate.Add(ipAddress);
                        }
                    }

                    List<Task<string>> uploadTasks = new List<Task<string>>();

                    foreach (string ipAddr in ipAddressesToUpdate)
                    {
                        // Offload each upload operation to a background thread and store the task
                        uploadTasks.Add(Task.Run(() => UploadFileAsync(fileToUpload, ipAddr)));
                        ScanningIPLabel.Text = "Updating " + ipAddr;
                    }

                    // Wait for all tasks to complete
                    string[] responses = await Task.WhenAll(uploadTasks);

                    // Display responses for each server
                    for (int i = 0; i < responses.Length; i++)
                    {
                        string numericResponse = new string(responses[i].Where(char.IsDigit).ToArray());
                        if (numericResponse != "200") // Check if response code is not 200
                        {
                            MessageBox.Show($"Error updating {ipAddressesToUpdate[i]}:\n{responses[i]}");
                        }
                    }
                    if (isEnglish)
                    {
                        MessageBox.Show("Finished uploading the update. Please wait 10 minutes for the miner(s) to finish updating");
                    }
                    else
                    {
                        MessageBox.Show("已完成固件上传，固件更新将在十分钟左右完成");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        //End *Button Listener Functions*



        //********** Async Task Functions for non-blocking command execution **********

        //function to update the status column of a miner when needed
        private void UpdateStatusColumn(ListViewItem item, string status)
        {
            minerListView.Invoke((Action)(() =>
            {
                item.SubItems[25].Text = status; // Update the "Status" column
            }));
        }

        //Handles the creation of tasks for each desired function
        private async Task ExecuteMinerCommandAsync(string command, string statusMessage, CancellationToken cancellationToken)
        {
            List<Task> tasks = new List<Task>();
            try
            {
                foreach (ListViewItem item in minerListView.SelectedItems)
                {
                    if (command == "setPool")
                    {
                        tasks.Add(ConfigurePoolForMinerAsync(item, cancellationToken));
                    }
                    else if (command == "setDynamicIP")
                    {
                        tasks.Add(SetDynamicIPAsync(item, cancellationToken));
                    }
                    else if (command == "setStaticIP")
                    {
                        tasks.Add(SetStaticIPAsync(item, cancellationToken));
                    }
                    else if (command == "refresh")
                    {
                        tasks.Add(RefreshMinerInfoAsync(item, cancellationToken));
                    }
                    else if (command == "")
                    {

                    }
                    else
                    {
                        tasks.Add(SendCommandToMinerAsync(item, command, statusMessage, cancellationToken));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in command execution: {ex.Message}");
            }
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error after task completion: {ex.Message}");
            }
            // Update status after all tasks complete

            ScanningIPLabel.Text = $"{statusMessage} Done";
        }

        //Send Generic command Function For When the only info needed is the command itself(EX. rebooting or self check)
        private async Task SendCommandToMinerAsync(ListViewItem item, string command, string statusMessage, CancellationToken cancellationToken)
        {
            await semaphoreReboot.WaitAsync(cancellationToken); // Wait to enter the semaphore

            try
            {
                int maxRetries = maxRepeatScan;
                bool success = false;
                double currentTimeout = timeoutTime;

                for (int attempt = 1; attempt <= maxRetries && !success; attempt++)
                {
                    try
                    {
                        UpdateStatusColumn(item, $"{statusMessage} (Attempt {attempt})");

                        using (HttpClient client = new HttpClient())
                        {
                            client.Timeout = TimeSpan.FromSeconds(currentTimeout);
                            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                            var response = await client.GetStringAsync($"http://{item.Text}/cgi-bin/cgiNetService.cgi?{command}");

                            success = true;
                            UpdateStatusColumn(item, $"{statusMessage} Success");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to execute {statusMessage} on {item.Text}: {ex.Message}");

                        if (attempt < maxRetries)
                        {
                            currentTimeout += 0.25; // Increase timeout for next attempt
                            UpdateStatusColumn(item, $"{statusMessage} (Retry {attempt + 1})");
                        }
                        else
                        {
                            UpdateStatusColumn(item, $"{statusMessage} Failed");
                        }
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation canceled.");
                        UpdateStatusColumn(item, $"{statusMessage} Canceled");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SentCommandToMiner function error: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Release(); // Release the semaphore slot
            }
        }

        //Set Pool
        private async Task ConfigurePoolForMinerAsync(ListViewItem item, CancellationToken cancellationToken)
        {
            await semaphoreReboot.WaitAsync(cancellationToken); // Wait to enter the semaphore

            try
            {
                int maxRetries = maxRepeatScan;
                bool success = false;
                double currentTimeout = timeoutTime;
                string poolIp = item.Text;

                string poolUrl = WebUtility.UrlEncode(poolUrlTextBox.Text);
                string poolUser = WebUtility.UrlEncode(poolUserTextBox.Text);
                string poolPass = WebUtility.UrlEncode(poolPasswordTextBox.Text);
                string poolIpSet = poolIp.Replace('.', 'x');

                for (int attempt = 1; attempt <= maxRetries && !success; attempt++)
                {
                    try
                    {
                        // Update status to indicate the current attempt
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Pools" : "配置矿池中")} (Attempt {attempt})");

                        using (HttpClient setPool = new HttpClient())
                        {
                            setPool.Timeout = TimeSpan.FromSeconds(currentTimeout);
                            setPool.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                            // Make the HTTP request to configure the pool
                            var setPoolResponse = await setPool.GetStringAsync(
                                $"http://{poolIp}/cgi-bin/cgiNetService.cgi?send_pools_params=%7B%22poolurl%22:%22{poolUrl}%22,%22username%22:%22{poolUser}.{poolIpSet}%22,%22password%22:%22{poolPass}%22,%22currency%22:%22ETC%22%7D");

                            // If the request succeeds, mark as success and update status
                            success = true;
                            UpdateStatusColumn(item, $"{(isEnglish ? "Pool Set Successfully" : "矿池配置成功")}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to set pool on {poolIp}: {ex.Message}");

                        if (attempt < maxRetries)
                        {
                            currentTimeout += 0.25; // Increase timeout for the next attempt
                            UpdateStatusColumn(item, $"{(isEnglish ? "Setting Pools" : "配置矿池中")} (Retry {attempt + 1})");
                        }
                        else
                        {
                            UpdateStatusColumn(item, $"{(isEnglish ? "Pool Set Failed" : "矿池配置失败")}");
                        }
                    }

                    // Check if the operation was canceled
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation canceled.");
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Pools" : "配置矿池中")} Canceled");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConfigurePoolForMinerAsync error: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Release(); // Release the semaphore slot
            }
        }

        //Set Static IP
        private async Task SetStaticIPAsync(ListViewItem item, CancellationToken cancellationToken)
        {
            await semaphoreReboot.WaitAsync(cancellationToken);

            string minerIp = item.Text;
            string[] ipParts = minerIp.Split('.');
            int newIPTwo = int.Parse(ipParts[1]);
            int newIPThree = int.Parse(ipParts[2]);
            string newIPEnd = newIpTextBox.Text;
            string newIP = "10" + "." + newIPTwo.ToString() + "." + newIPThree.ToString() + "." + newIPEnd.ToString();

            if (isEnglish)
            {
                ScanningIPLabel.Text = minerIp + " Setting Static IP";
            }
            else
            {
                ScanningIPLabel.Text = minerIp + " 设置静态IP";
            }
            if (YAMI_Scanner.Properties.Settings.Default.CheckForStaticIP)
            {
                if (pingIp(newIP))
                {
                    var dialogResult = MessageBox.Show($"The IP Address {newIP} is already in use. Do you want to continue anyway?", "IP Address In Use", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Cancel)
                    {
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Static IP" : "设置静态IP")} Canceled");
                        semaphoreReboot.Release();
                        return; // Exit the method if the user cancels
                    }
                }
            }
            try
            {
                int maxRetries = maxRepeatScan;
                bool success = false;
                double currentTimeout = timeoutTime;


                for (int attempt = 1; attempt <= maxRetries && !success; attempt++)
                {
                    try
                    {
                        // Update status to indicate the current attempt
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Static IP" : "设置动态IP")} (Attempt {attempt}/{maxRetries})");

                        using (HttpClient setStaticIP = new HttpClient())
                        {
                            setStaticIP.Timeout = TimeSpan.FromSeconds(1);
                            setStaticIP.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                            var setPoolResponse = await setStaticIP.GetStringAsync($"http://{minerIp}//cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22static%22,%22ip%22:%22{newIP}%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.{newIPTwo}.{newIPThree}.254%22,%22dns1%22:%221.1.1.1%22,%22dns2%22:%22114.114.114.114%22%7D");
                            item.Text = newIP;
                            // If the request succeeds, mark as success and update status
                            success = true;
                            UpdateStatusColumn(item, $"{(isEnglish ? "Static IP Set Successfully" : "Static IP Set Successfully")}");
                        }
                    }
                    catch (Exception ex)
                    {

                        if (attempt < maxRetries)
                        {
                            currentTimeout += 0.25; // Increase timeout for the next attempt
                        }
                        else
                        {
                            UpdateStatusColumn(item, $"{(isEnglish ? "Static IP Set Failed" : "Static IP Set Failed")}");
                        }
                    }

                    // Check if the operation was canceled
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation canceled.");
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Static IP" : "设置静态IP")} Canceled");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConfigurePoolForMinerAsync error: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Release();
            }
        }

        //Set Dynamic IP
        private async Task SetDynamicIPAsync(ListViewItem item, CancellationToken cancellationToken)
        {
            await semaphoreReboot.WaitAsync(cancellationToken); // Wait to enter the semaphore

            try
            {
                int maxRetries = maxRepeatScan;
                bool success = false;
                double currentTimeout = timeoutTime;
                string minerIp = item.Text;

                for (int attempt = 1; attempt <= maxRetries && !success; attempt++)
                {
                    try
                    {
                        // Update status to indicate the current attempt
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Dynamic IP" : "设置动态IP")} (Attempt {attempt}/{maxRetries})");

                    using (HttpClient setPool = new HttpClient())
                    {
                        setPool.Timeout = TimeSpan.FromSeconds(1);
                        setPool.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        var setPoolResponse = await setPool.GetStringAsync($"http://{minerIp}//cgi-bin/cgiNetService.cgi?send_network_params=%7B%22netmode%22:%22dhcp%22,%22ip%22:%22{minerIp}%22,%22netmask%22:%22255.255.255.0%22,%22gateway%22:%2210.100.5.254%22,%22dns1%22:%228.8.8.8%22,%22dns2%22:%22114.114.114.114%22%7D");
                        
                        // If the request succeeds, mark as success and update status
                        success = true;
                            UpdateStatusColumn(item, $"{(isEnglish ? "Dynamic IP Set Successfully" : "Dynamic IP Set Successfully")}");
                        }
                    }
                    catch (Exception ex)
                    {

                        if (attempt < maxRetries)
                        {
                            currentTimeout += 0.25; // Increase timeout for the next attempt
                        }
                        else
                        {
                            UpdateStatusColumn(item, $"{(isEnglish ? "Dynamic IP Set Failed" : "Dynamic IP Set Failed")}");
                        }
                    }

                    // Check if the operation was canceled
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation canceled.");
                        UpdateStatusColumn(item, $"{(isEnglish ? "Setting Dynamic IP" : "Setting Dynamic IP")} Canceled");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConfigurePoolForMinerAsync error: {ex.Message}");
            }
            finally
            {
                semaphoreReboot.Release(); // Release the semaphore slot
            }
        }

        //Refresh Miner Info
        private async Task RefreshMinerInfoAsync(ListViewItem item, CancellationToken cancellationToken)
            {

                await semaphoreReboot.WaitAsync(cancellationToken);
                try
                {
                    string minerIP = item.Text;
                    int maxRetries = maxRepeatScan;
                    double currentTimeout = timeoutTime;
                    bool success = false;

                    for (int attempt = 1; attempt <= maxRetries && !success; attempt++)
                    {
                        try
                        {
                            UpdateStatusColumn(item, $"{(isEnglish ? $"Refreshing" : $"刷新")} (Attempt {attempt})");

                            cancellationToken.ThrowIfCancellationRequested();

                            using (HttpClient client = new HttpClient())
                            {
                                client.Timeout = TimeSpan.FromSeconds(currentTimeout);
                                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                                var response = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_uptime_pools_minerinfo");
                                var minerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                                cancellationToken.ThrowIfCancellationRequested();

                                var selfResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_selfcheck_progress");
                                var selfMinerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(selfResponse);
                                cancellationToken.ThrowIfCancellationRequested();

                                var poolResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_pools");
                                var poolInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(poolResponse);
                                cancellationToken.ThrowIfCancellationRequested();

                                var firmwareResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_overview");
                                var firmwareInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(firmwareResponse);
                                if (minerInfo?.data == null)
                                {
                                    UpdateStatusColumn(item, "Invalid miner data");
                                    continue; // Skip to next attempt
                                }

                                cancellationToken.ThrowIfCancellationRequested();

                                //Determine hashboard status
                                string hash1 = minerInfo.data.minerinfo[0].hash_rate > 10 ? "O" : "X";
                                string hash1a = minerInfo.data.minerinfo[0].hash_rate > 10 ? "Working" : "Not Working";

                                string hash2 = minerInfo.data.minerinfo[1].hash_rate > 10 ? "O" : "X";
                                string hash2a = minerInfo.data.minerinfo[1].hash_rate > 10 ? "Working" : "Not Working";

                                string hash3 = minerInfo.data.minerinfo[2].hash_rate > 10 ? "O" : "X";
                                string hash3a = minerInfo.data.minerinfo[2].hash_rate > 10 ? "Working" : "Not Working";

                                //Determine Miner Status
                                string minerStatus = "";
                                if (minerInfo.data.uptime.dag.progress > 0 && minerInfo.data.uptime.dag.progress < 100)
                                {
                                    minerStatus = "Writing DAG";
                                }
                                else if (Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2) > 0 && Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2) < 100)
                                {
                                    minerStatus = "Running Self Check";
                                }
                                else if (minerInfo.data.uptime.hash_rate > 1)
                                {
                                    minerStatus = "Hashing";
                                }
                                else
                                {
                                    minerStatus = "Needs Self Check";
                                }

                                minerListView.Invoke((Action)(() =>
                                {
                                    if (!minerListView.IsHandleCreated) return;
                                    // Update columns with refreshed data (add more null-checks here as needed)

                                    item.SubItems[1].Text = minerInfo.data.uptime.hash_rate.ToString(); //Hashrate
                                    item.SubItems[2].Text = minerInfo.data.pooldata[0].pool.ToString(); // Pool
                                    item.SubItems[3].Text = poolInfo.data.username.ToString(); // Pool User
                                    item.SubItems[4].Text = minerInfo.data.pooldata[0].user.ToString(); // Account
                                    item.SubItems[5].Text = Math.Round(Convert.ToDouble(minerInfo.data.uptime.dag.progress), 2).ToString() + " %"; // DAG Progress
                                    item.SubItems[6].Text = Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2).ToString() + " %"; // Self Check Progress

                                    // Update hashboard statuses
                                    item.SubItems[7].Text = ($"{hash1}, {hash2}, {hash3}");

                                    item.SubItems[8].Text = Math.Round((100 - (100 * Convert.ToDouble(minerInfo.data.uptime.reject_num) / Convert.ToDouble(minerInfo.data.uptime.accept_num))), 2).ToString(); // Accepted Rate
                                    item.SubItems[9].Text = minerInfo.data.uptime.accept_num.ToString(); // Accepted
                                    item.SubItems[10].Text = minerInfo.data.uptime.reject_num.ToString(); // Rejected
                                    item.SubItems[11].Text = firmwareInfo.data.form1.mac_address.ToString(); // MAC Address
                                    item.SubItems[12].Text = minerInfo.data.uptime.up_time.ToString(); // Uptime
                                    item.SubItems[13].Text = minerInfo.data.minerinfo[0].temperature.ToString(); // Temperature
                                    item.SubItems[14].Text = minerInfo.data.uptime.fan_speed.ToString(); // Fan Speed
                                    item.SubItems[15].Text = firmwareInfo.data.form1.platform_version.ToString(); // Firmware Version
                                    item.SubItems[16].Text = hash1a; //board 1 status 
                                    item.SubItems[17].Text = minerInfo.data.minerinfo[0].hash_rate; //board 1 hashrate
                                    item.SubItems[18].Text = minerInfo.data.minerinfo[0].temperature; //board 1 temp
                                    item.SubItems[19].Text = hash2a;
                                    item.SubItems[20].Text = minerInfo.data.minerinfo[1].hash_rate;
                                    item.SubItems[21].Text = minerInfo.data.minerinfo[1].temperature;
                                    item.SubItems[22].Text = hash3a;
                                    item.SubItems[23].Text = minerInfo.data.minerinfo[2].hash_rate;
                                    item.SubItems[24].Text = minerInfo.data.minerinfo[2].temperature;
                                    item.SubItems[25].Text = minerStatus;
                                }));
                                success = true;
                            }
                        }
                        catch (JsonSerializationException jsonEx)
                        {
                            Console.WriteLine($"Deserialization error for {minerIP}: {jsonEx.Message}");
                            UpdateStatusColumn(item, $"{(isEnglish ? "Invalid Miner Data" : "无效的矿工数据")}");
                        }
                        catch (HttpRequestException httpEx)
                        {
                            Console.WriteLine($"HTTP request error for {minerIP}: {httpEx.Message}");
                            UpdateStatusColumn(item, $"{(isEnglish ? "Network Error" : "网络错误")}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to refresh miner info for {minerIP}: {ex.Message}");

                            if (attempt < maxRetries)
                            {
                                currentTimeout += 0.25;
                                UpdateStatusColumn(item, $"{(isEnglish ? $"Retry {attempt + 1}" : $"重试 {attempt + 1}")}");
                            }
                            else
                            {
                                UpdateStatusColumn(item, $"{(isEnglish ? "Refresh Failed" : "刷新失败")}");
                            }
                        }

                        if (cancellationToken.IsCancellationRequested)
                        {
                            UpdateStatusColumn(item, $"{(isEnglish ? "Refresh Canceled" : "刷新取消")}");
                            break;
                        }
                    }
                }
                finally
                {
                    semaphoreReboot.Release();
                }
            }


        //End *Async Task Functions for non-blocking command executions*


        //End *COMMANDS TAB*



        //* * * UPDATE IP TEXT BOXES * * * * * * * * * * * * * * * * * * UPDATE IP TEXT BOXES * * * * * * * * * * * * * * * * * * UPDATE IP TEXT BOXES * * * * * * * * * * * * * * * * *

        private bool isUpdatingEndTextBox = false;
        

        private void focusText(MaterialTextBox newBox)
        {
            newBox.Focus();
            newBox.SelectionStart = newBox.Text.Length;
        }

        //All Logic For Handling Automatic Updating/Advancing Of The IP Boxes
        private void startIPTextBoxA_TextChanged(object sender, EventArgs e)
        {
            if (startIPTextBoxA.Text.Length == 3 || startIPTextBoxA.Text == "10")
            {
                focusText(startIPTextBoxB);
                isUpdatingEndTextBox = true;
                endIPTextBoxA.Text = startIPTextBoxA.Text;
                isUpdatingEndTextBox = false;
            }
        }
        private void startIPTextBoxB_TextChanged(object sender, EventArgs e)
        {
            if (startIPTextBoxB.Text.Length == 3)
            {
                focusText(startIPTextBoxC);
                isUpdatingEndTextBox = true;
                endIPTextBoxB.Text = startIPTextBoxB.Text;
                isUpdatingEndTextBox = false;
            }
        }
        private void startIPTextBoxC_TextChanged(object sender, EventArgs e)
        {
            if (startIPTextBoxC.Text.Length == 3)
            {
                isUpdatingEndTextBox = true;
                endIPTextBoxC.Text = startIPTextBoxC.Text;
                isUpdatingEndTextBox = false;
            }
        }
        private void startIPTextBoxD_TextChanged(object sender, EventArgs e)
        {
            if (startIPTextBoxD.Text.Length == 3 || startIPTextBoxD.Text == "0")
            {
                focusText(endIPTextBoxA);
            }
        }
        private void endIPTextBoxA_TextChanged(object sender, EventArgs e)
        {
            if (!isUpdatingEndTextBox && (endIPTextBoxA.Text.Length == 3 || endIPTextBoxA.Text == "10"))
            {
                focusText(endIPTextBoxB);
            }
        }
        private void endIPTextBoxB_TextChanged(object sender, EventArgs e)
        {
            if (!isUpdatingEndTextBox && endIPTextBoxB.Text.Length == 3)
            {
                focusText(endIPTextBoxC);
            }
        }
        private void endIPTextBoxC_TextChanged(object sender, EventArgs e)
        {
            if (!isUpdatingEndTextBox && endIPTextBoxC.Text.Length == 3)
            {
                focusText(endIPTextBoxD);
            }
        }
        private void endIPTextBoxD_TextChanged(object sender, EventArgs e)
        {

        }

        //Add event handlers for each textbox
        private void AttachNumericTextBoxEventHandlers()
        {
            startIPTextBoxA.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            startIPTextBoxB.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            startIPTextBoxC.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            startIPTextBoxD.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            endIPTextBoxA.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            endIPTextBoxB.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            endIPTextBoxC.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
            endIPTextBoxD.KeyPress += new KeyPressEventHandler(numericTextBox_KeyPress);
        }

        // Allow only digits, backspace, and the delete key
        private void numericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //End *UPDATE IP TEXT BOXES*


        // Listens for IP Report button from YAMI
        async Task ReceiveDataAsync()
        {
            
            try
            {
                udpCancellationToken = new CancellationTokenSource();
                while (true)
                {
                    UdpReceiveResult result = await udpListener.ReceiveAsync();

                    string receivedData = Encoding.ASCII.GetString(result.Buffer);

                    // Extract the IP address from the remote endpoint
                    IPAddress ip = result.RemoteEndPoint.Address;
                    string udpIp = ip.ToString();
                    //MessageBox.Show(udpIp);
                    try
                    {
                        // Run the scanning process with cancellation support
                        GetData(udpIp, 10.0, udpCancellationToken.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        MessageBox.Show("An Unexpected error occured. Please restart the program to continue using the \"ip Report\" Button", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Error: " + ex.Message + ". Please restart the program to continue using the \"ip Report\" Button", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SocketException ex)
            {
                // Handle socket exceptions
                MessageBox.Show("SocketException: " + ex.Message + ". Please restart the program to continue using the \"ip Report\" Button", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + ". Please restart the program to continue using the \"ip Report\" Button", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm(appSettings))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Settings were changed and saved, so you can apply them or save to a file
                    SaveSettings();
                    LoadSettings();
                }
                else
                {
                    // Settings were not changed (user clicked Cancel)
                }
            }
        }


        private void SaveSettings()
        {
            // Example: Save appSettings to application settings
            YAMI_Scanner.Properties.Settings.Default.MaxConcurrentScans = appSettings.MaxConcurrentScans;
            YAMI_Scanner.Properties.Settings.Default.MaxRepeatScan = appSettings.MaxRepeatScan;
            YAMI_Scanner.Properties.Settings.Default.TimeoutTime = appSettings.TimeoutTime;
            YAMI_Scanner.Properties.Settings.Default.ConcurrentUpdate = appSettings.ConcurrentUpdate;
            YAMI_Scanner.Properties.Settings.Default.UpdateTimeout = appSettings.UpdateTimeout;
            YAMI_Scanner.Properties.Settings.Default.CheckForStaticIP = appSettings.CheckForStaticIP;
            YAMI_Scanner.Properties.Settings.Default.Save();
        }


        //Initializes settings vars
        private void LoadSettings()
        {
            maxConcurrentScans = YAMI_Scanner.Properties.Settings.Default.MaxConcurrentScans;
            maxRepeatScan = YAMI_Scanner.Properties.Settings.Default.MaxRepeatScan;
            timeoutTime = YAMI_Scanner.Properties.Settings.Default.TimeoutTime;
            concurrentUpdate = YAMI_Scanner.Properties.Settings.Default.ConcurrentUpdate;
            updateTimeout = YAMI_Scanner.Properties.Settings.Default.UpdateTimeout;
            checkForStaticIP = YAMI_Scanner.Properties.Settings.Default.CheckForStaticIP;
        }


        private void ExportBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    string currentDate = DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss");
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "Save as Excel File";
                    sfd.FileName = $"YAMI Export {currentDate}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportListViewToExcel(minerListView, sfd.FileName);
                        MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch {
                MessageBox.Show("There was an error while attempting to export. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Function to write listview items to an xlsx
        public void ExportListViewToExcel(ListView listView, string filePath)
        {
            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("YAMIExport");

                // Add column headers from the ListView
                for (int i = 0; i < listView.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = listView.Columns[i].Text;
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true; // Make headers bold
                }

                // Add ListView items to the worksheet
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    ListViewItem item = listView.Items[i];
                    for (int j = 0; j < item.SubItems.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = item.SubItems[j].Text;
                    }
                }

                // Save the package to the file system
                FileInfo fi = new FileInfo(filePath);
                package.SaveAs(fi);
            }
        }


        private void StopAllBtn_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSourceReboot?.Cancel();
            cancellationTokenSourceSelfCheck?.Cancel();
            ScanningIPLabel.Text = "Stopped";
        }


        private void calculateListviewTotals()
        {
            int selected = 0;
            double avgHash = 0;
            double minHash = 0;
            double maxHash = 0;
            double totalHash = 0;
            bool minHashInit = false;

            foreach (ListViewItem item in minerListView.Items)
            {
                // Check if the form is disposed before accessing its controls
                if (IsDisposed)
                    return;

                if (item.Selected)
                {
                    //Get hashrate of item if selected
                    double itemHash = Convert.ToDouble(item.SubItems[1].Text);
                    //Increment selected count
                    selected += 1;
                    //reassign maxHash if itemHash is larger than the current hash
                    if (itemHash > maxHash)
                    {
                        maxHash = itemHash;
                    }
                    //Initialize or update minHash
                    if (itemHash < minHash || minHashInit == false)
                    {
                        minHashInit = true;
                        minHash = itemHash;
                    }
                    totalHash += itemHash;
                    avgHash = totalHash / selected;
                }
            }
            SelectedLabel.Text = "Selected: " + selected.ToString();
            AvgLabel.Text = "Average: " + Math.Round(avgHash, 2).ToString();
            MinLabel.Text = "Min: " + minHash.ToString();
            MaxLabel.Text = "Max: " + maxHash.ToString();
            TotalLabel.Text = "Total: " + Math.Round(totalHash, 2).ToString();
        }


        private Task SelectAllCheckUpdate(ListViewItem item, bool isChecked)
        {
            return Task.Run(() =>
            {
                item.Selected = isChecked;
                item.Checked = isChecked;
            });
        }


        bool isUserCheckChange = true;
        private async void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isUserCheckChange)
            {
                List<Task> tasks = new List<Task>();

                minerListView.BeginUpdate();
                bool isChecked = SelectAllCheckBox.CheckState == CheckState.Checked;

                foreach (ListViewItem ipItem in minerListView.Items)
                {
                    // Update ListViewItem.Checked based on ListViewItem.Selected
                    tasks.Add(SelectAllCheckUpdate(ipItem, isChecked));
                }
                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    // Resume drawing after all updates
                    minerListView.EndUpdate();

                }

                isUserCheckChange = true;
            }
        }


        private void UpdateSelectAllCheckBoxState()
        {
            int checkedCount = minerListView.SelectedItems.Count;

            isUserCheckChange = false;
            // Update the "Select All" checkbox state based on the checked items
            if (checkedCount == 0)
            {
                SelectAllCheckBox.CheckState = CheckState.Unchecked;
            }
            else if (checkedCount == minerListView.Items.Count)
            {
                SelectAllCheckBox.CheckState = CheckState.Checked;
            }
            else
            {
                SelectAllCheckBox.CheckState = CheckState.Indeterminate;
            }

            isUserCheckChange = true;
        }

        private void minerListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                minerListView.Tag = null;
                var hitTestInfo = minerListView.HitTest(e.Location);
                var clickedItem = hitTestInfo.Item;

                if (clickedItem != null)
                {
                    minerListView.Tag = clickedItem;  // Set the clicked item into Tag property
                    poolUrlTextBox.Text = clickedItem.SubItems[2].Text;
                    poolUserTextBox.Text = clickedItem.SubItems[4].Text;
                    poolPasswordTextBox.Text = "X";
                }
            }
        }

        private void saveTextBoxData()
        {
            List<string> TextBoxValues = new List<string>{ };
            List<MaterialTextBox> textBoxesToSave = new List<MaterialTextBox>
            {
                startIPTextBoxA,
                startIPTextBoxB,
                startIPTextBoxC,
                startIPTextBoxD,
                endIPTextBoxA,
                endIPTextBoxB,
                endIPTextBoxC,
                endIPTextBoxD,
                nameTextBox,
                newIpTextBox,
                newPasswordTextBox,
                poolUrlTextBox,
                poolUserTextBox,
                poolPasswordTextBox
            };
            foreach (MaterialTextBox textBox in textBoxesToSave)
            {
                TextBoxValues.Add(textBox.Text);
            }
            YAMI_Scanner.Properties.Settings.Default.SavedTextBoxValues = string.Join("|", TextBoxValues);
            YAMI_Scanner.Properties.Settings.Default.Save();
        }

        private void loadTextBoxData()
        {
            string savedValues = YAMI_Scanner.Properties.Settings.Default.SavedTextBoxValues;
            if (!string.IsNullOrEmpty(savedValues))
            {
                string[] TextBoxValues = savedValues.Split('|');

                List<MaterialTextBox> textBoxesToLoad = new List<MaterialTextBox>
                {
                    startIPTextBoxA,
                    startIPTextBoxB,
                    startIPTextBoxC,
                    startIPTextBoxD,
                    endIPTextBoxA,
                    endIPTextBoxB,
                    endIPTextBoxC,
                    endIPTextBoxD,
                    nameTextBox,
                    newIpTextBox,
                    newPasswordTextBox,
                    poolUrlTextBox,
                    poolUserTextBox,
                    poolPasswordTextBox
                };

                for (int i = 0; i < TextBoxValues.Length && i < textBoxesToLoad.Count; i++)
                {
                    textBoxesToLoad[i].Text = TextBoxValues[i];
                }
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the current IP range to app.config
            Console.WriteLine("Exiting program");
            try
            {
                
                driver?.Quit();
            }
            catch (Exception)
            {

            }
            SaveCurrentIPRange();
            SaveListViewSettings();
            saveTextBoxData();
            YAMI_Scanner.Properties.Settings.Default.Save();
        }

    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Main());
        }
    }

}


//Class to sort listview by column
public class ListViewItemComparer : IComparer
{

    private readonly int columnToSort;
    private readonly SortOrder order;

    public ListViewItemComparer()
    {
        columnToSort = 0;
        order = SortOrder.Ascending; // Default sorting order
    }

    public ListViewItemComparer(int columnToSort, SortOrder order)
    {
        this.columnToSort = columnToSort;
        this.order = order;
    }

    public int Compare(object x, object y)
    {
        int compareResult = 0;
        ListViewItem itemX = (ListViewItem)x;
        ListViewItem itemY = (ListViewItem)y;

        // Handle different data types in different columns
        switch (columnToSort)
        {
            //IP Address
            case 0: // IP Address
                //Get IP Bytes like in the ip iteration
                IPAddress IpX;
                IPAddress IpY;

                if (System.Net.IPAddress.TryParse(itemX.SubItems[columnToSort].Text, out IpX) && System.Net.IPAddress.TryParse(itemY.SubItems[columnToSort].Text, out IpY))
                {
                    byte[] ipBytesX = IpX.GetAddressBytes();
                    byte[] ipBytesY = IpY.GetAddressBytes();

                    for (int i = 0; i < ipBytesX.Length; i++)
                    {
                        if (ipBytesX[i] != ipBytesY[i])
                        {
                            compareResult = ipBytesX[i].CompareTo(ipBytesY[i]);
                            break;
                        }
                    }
                }
                else
                {
                    //Sort by string if something happens
                    compareResult = string.Compare(itemX.SubItems[columnToSort].Text, itemY.SubItems[columnToSort].Text);
                }

                break;
            //Strings
            case 2: //Pool
            case 3: //Pool User
            case 4: //Account
            case 7: //Hashboard status
            case 11: //Mac Address
            case 12: //UP Time
            case 15: //Firmware
            case 16: //Board 1 sataus
            case 19: //Board 2 sataus
            case 22: //Board 3 sataus
            case 25:
                compareResult = string.Compare(itemX.SubItems[columnToSort].Text, itemY.SubItems[columnToSort].Text);
                break;
            //Doubles
            case 1: //HashRate
            case 5: //DAG
            case 6: //Self Check Progress
            case 8: //Acceptance Rate
            case 9: //Accepted Shares
            case 10: //Rejected Shares
            case 13: //Temperature
            case 14: //Fan speed
            case 17: //Board 1 Hashrate
            case 18: //Board 1 Temp
            case 20: //Board 2 Hashrate
            case 21: //Board 2 Temp
            case 23: //Board 3 Hashrate
            case 24: //Board 3 Temp
                double hashRateX = double.Parse(itemX.SubItems[columnToSort].Text);
                double hashRateY = double.Parse(itemY.SubItems[columnToSort].Text);
                compareResult = hashRateX.CompareTo(hashRateY);
                break;
            default:
                Console.WriteLine("No case defined for the selected column, sorting alphabetically");
                compareResult = string.Compare(itemX.SubItems[columnToSort].Text, itemY.SubItems[columnToSort].Text);
                break;
        }

        // Reverse the result for descending order
        if (order == SortOrder.Descending)
        {
            compareResult = -compareResult;
        }

        return compareResult;
    }
}


//Add a class to change the doubleBuffered property to true, reducing flickering
public static class ControlExtensions
{
    public static void DoubleBuffered(this Control control, bool enable)
    {
        var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        doubleBufferPropertyInfo?.SetValue(control, enable, null);
    }
}


//Class for Miner Info
public class MinerInfo
{
    public string MinerIPAddress { get; set; }
    public string HashRate { get; set; }
    public string Uptime { get; set; }
    public string HashboardStatus { get; set; }
    public string Temperature { get; set; }
    public string FanSpeed { get; set; }
    public string Accepted { get; set; }
    public string Rejected { get; set; }
    public string DagProgress { get; set; }
    public string AcceptedRate { get; set; }
    public string Pool { get; set; }
    public string PoolUser { get; set; }
    public string SelfCheckProgress { get; set; }
    public string Hashboard1Status { get; set; }
    public string Hashboard1HashRate { get; set; }
    public string Hashboard1Temperature { get; set; }
    public string Hashboard2Status { get; set; }
    public string Hashboard2HashRate { get; set; }
    public string Hashboard2Temperature { get; set; }
    public string Hashboard3Status { get; set; }
    public string Hashboard3HashRate { get; set; }
    public string Hashboard3Temperature { get; set; }
    public string FirmwareVersion { get; set; }
    public string Account { get; set; }
    public String MacAddress { get; set; }
    //initialize without any properties
    public MinerInfo()
    {

    }
    //new class object when properties are provided
    public MinerInfo(string minerIPAddress, string hashRate, string uptime, string hashboardStatus, string temperature, string fanSpeed,
            string accepted, string rejected, string dagProgress, string acceptedRate, string pool, string poolUser,
            string selfCheckProgress, string hashboard1Status, string hashboard1HashRate, string hashboard1Temperature,
            string hashboard2Status, string hashboard2HashRate, string hashboard2Temperature, string hashboard3Status,
            string hashboard3HashRate, string hashboard3Temperature, string firmwareVersion, string account, string macAddress)
    {
        MinerIPAddress = minerIPAddress;
        HashRate = hashRate;
        Uptime = uptime;
        HashboardStatus = hashboardStatus;
        Temperature = temperature;
        FanSpeed = fanSpeed;
        Accepted = accepted;
        Rejected = rejected;
        DagProgress = dagProgress;
        AcceptedRate = acceptedRate;
        Pool = pool;
        PoolUser = poolUser;
        SelfCheckProgress = selfCheckProgress;
        Hashboard1Status = hashboard1Status;
        Hashboard1HashRate = hashboard1HashRate;
        Hashboard1Temperature = hashboard1Temperature;
        Hashboard2Status = hashboard2Status;
        Hashboard2HashRate = hashboard2HashRate;
        Hashboard2Temperature = hashboard2Temperature;
        Hashboard3Status = hashboard3Status;
        Hashboard3HashRate = hashboard3HashRate;
        Hashboard3Temperature = hashboard3Temperature;
        FirmwareVersion = firmwareVersion;
        Account = account;
        MacAddress = macAddress;
    }
}


//Class to track all settings values
public class AppSettings
{
    public int MaxConcurrentScans { get; set; }
    public int MaxRepeatScan { get; set; }
    public double TimeoutTime { get; set; }
    public int ConcurrentUpdate { get; set; }
    public int UpdateTimeout { get; set; }
    public bool CheckForStaticIP { get; set; }
}