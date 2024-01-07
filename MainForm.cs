using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Collections;
using MaterialSkin;
using MaterialSkin.Controls;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace MinerInfoApp
{

    public partial class MainForm : MaterialForm
    {
        //How long before searching next IP
        private const double timeoutTime = 0.05;

        private bool isEnglish = true;

        //Tracks whether a scan Is running or not
        private int scanRunning = 0;//0 = not running, 1 = running

        //Dictionary for the miners
        private readonly Dictionary<string, MinerInfo> minerInfoDict = new Dictionary<string, MinerInfo>();

        // Key for accessing saved ranges in app.config
        private const string SavedRangesKey = "SavedIPRanges";

        //Variables for sorting by column
        private int lastColumnClicked = 0;
        private SortOrder lastSortOrder = SortOrder.Ascending;

        //Variable to track miners found in a search
        private int minersFoundCount = 0;


        //Declare and initialize controls
        public MainForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green900, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            this.FormClosing += MainForm_FormClosing;

            minerListView.DoubleBuffered(true);

            AttachNumericTextBoxEventHandlers();

            minerListView.ColumnClick += minerListView_ColumnClick;

            minerListView.DoubleBuffered(true);

            LoadSavedIPRanges();
        }


        //Class for Miner Info
        public class MinerInfo
        {
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
        }

        //Only allow numbers to be entered in the IP address
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


        private void MainForm_Load(object sender, EventArgs e)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;

            string language = currentCulture.TwoLetterISOLanguageName;

            if (language == "zh")
            {
                Console.WriteLine("Chinese detected");
                isEnglish = false;
                translateLanguage();
            }
            else
            {
                
            }
        }


        private void translateLanguage() {
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
                ipRangeListView.Columns[0].Text = "Start IP";
                ipRangeListView.Columns[1].Text = "End IP";
                ipRangeListView.Columns[2].Text = "Name";
                minerListView.Columns[0].Text = "IP Address";
                minerListView.Columns[1].Text = "Hashrate";
                minerListView.Columns[2].Text = "Hashboard Status";
                minerListView.Columns[3].Text = "Uptime";
                minerListView.Columns[4].Text = "Fan Speed";
                minerListView.Columns[5].Text = "Temperature";
                minerListView.Columns[6].Text = "DAG Progress";
                minerListView.Columns[7].Text = "Accepted Shares";
                minerListView.Columns[8].Text = "Rejected Shares";
                minerListView.Columns[9].Text = "Acceptance Rate";
                minerListView.Columns[10].Text = "Pool";
                minerListView.Columns[11].Text = "Pool User";
                minerListView.Columns[12].Text = "Self Check Progress";
                minerListView.Columns[13].Text = "Board 1 Status";
                minerListView.Columns[14].Text = "Board 1 Hashrate";
                minerListView.Columns[15].Text = "Board 1 Temp";
                minerListView.Columns[16].Text = "Board 2 Status";
                minerListView.Columns[17].Text = "Board 2 Hashrate";
                minerListView.Columns[18].Text = "Board 2 Temp";
                minerListView.Columns[19].Text = "Board 3 Status";
                minerListView.Columns[20].Text = "Board 3 Hashrate";
                minerListView.Columns[21].Text = "Board 3 Temp";
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
                ipRangeListView.Columns[0].Text = "开始IP";
                ipRangeListView.Columns[1].Text = "结束IP";
                ipRangeListView.Columns[2].Text = "名称";
                minerListView.Columns[0].Text = "IP地址";
                minerListView.Columns[1].Text = "算力";
                minerListView.Columns[2].Text = "算力板状态";
                minerListView.Columns[3].Text = "运行时间";
                minerListView.Columns[4].Text = "风扇转速";
                minerListView.Columns[5].Text = "温度";
                minerListView.Columns[6].Text = "DAG进度";
                minerListView.Columns[7].Text = "接受额";
                minerListView.Columns[8].Text = "拒绝额";
                minerListView.Columns[9].Text = "接受率";
                minerListView.Columns[10].Text = "矿池";
                minerListView.Columns[11].Text = "用户";
                minerListView.Columns[12].Text = "自检进度";
                minerListView.Columns[13].Text = "板1状态";
                minerListView.Columns[14].Text = "板1算力";
                minerListView.Columns[15].Text = "板1温度";
                minerListView.Columns[16].Text = "板2状态";
                minerListView.Columns[17].Text = "板2算力";
                minerListView.Columns[18].Text = "板2温度";
                minerListView.Columns[19].Text = "板3状态";
                minerListView.Columns[20].Text = "板3算力";
                minerListView.Columns[21].Text = "板3温度";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            }
        }
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

        private void numericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and the delete key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Runs when the '<- search" button is pressed
        private async void searchButton_Click(object sender, EventArgs e)
        {
            //Cancels other scans then waits 0.5 seconds
            scanRunning = 0;
            await Task.Delay(500);
            //Sets the scanRunning variable to sow there is an active scan running
            scanRunning = 1;
            //Resets minerFound variable to 0
            minersFoundCount = 0;
            //Try-Catch to clear data from the minerListView
            try
            {
                minerListView.Items.Clear();
            }
            catch (Exception ex)
            {

            }
            //Declare variables which store the IPs that will be used to search the range
            string startIP = (startIPTextBoxA.Text + "." + startIPTextBoxB.Text + "." + startIPTextBoxC.Text + "." + startIPTextBoxD.Text);
            string endIP = (endIPTextBoxA.Text + "." + endIPTextBoxB.Text + "." + endIPTextBoxC.Text + "." + endIPTextBoxD.Text);
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

            //Calls the GetIPRange function with the start and end IP variables and stores the returned list as 'ipList'
            List<string> ipList = GetIPRange(startIP, endIP);

            //Clears the MinerInfo Dictionary
            minerInfoDict.Clear(); // Clear existing data

            //for each ip in the ip list, it checks that the scanRunning variable is not set to 'Scan not running'
            foreach (string ip in ipList)
            {
                if (scanRunning == 0)
                {
                    break;
                }
                //Calls the GetData function on the current ip from the ipList, and stores it as a new instance of teh MinerInfo class
                MinerInfo minerInfo = await GetData(ip);
                //If the ip is found and the data can be read properly
                if (minerInfo != null)
                {
                    //Add one to the counter for found miners and update it's label
                    minersFoundCount++;
                    minerFoundCountLabel.Text = ($"{minersFoundCount} Miners Found");
                    //Creates a new entry to the minerInfo Dictionary with the ip as the identifier
                    minerInfoDict[ip] = minerInfo;
                    //Adds all the other data to the minerListView entry
                    ListViewItem item = new ListViewItem(ip);
                    item.SubItems.Add(minerInfo.HashRate);
                    item.SubItems.Add(minerInfo.HashboardStatus);
                    item.SubItems.Add(minerInfo.Uptime);
                    item.SubItems.Add(minerInfo.FanSpeed);
                    item.SubItems.Add(minerInfo.Temperature);
                    item.SubItems.Add(minerInfo.DagProgress);
                    item.SubItems.Add(minerInfo.Accepted);
                    item.SubItems.Add(minerInfo.Rejected);
                    item.SubItems.Add(minerInfo.AcceptedRate);
                    item.SubItems.Add(minerInfo.Pool);
                    item.SubItems.Add(minerInfo.PoolUser);
                    item.SubItems.Add(minerInfo.SelfCheckProgress);
                    item.SubItems.Add(minerInfo.Hashboard1Status);
                    item.SubItems.Add(minerInfo.Hashboard1HashRate);
                    item.SubItems.Add(minerInfo.Hashboard1Temperature);
                    item.SubItems.Add(minerInfo.Hashboard2Status);
                    item.SubItems.Add(minerInfo.Hashboard2HashRate);
                    item.SubItems.Add(minerInfo.Hashboard2Temperature);
                    item.SubItems.Add(minerInfo.Hashboard3Status);
                    item.SubItems.Add(minerInfo.Hashboard3HashRate);
                    item.SubItems.Add(minerInfo.Hashboard3Temperature);
                    //Adds the Entry to the listView
                    minerListView.Items.Add(item);

                }
                //if no response is recieved from the IP address, or the data recieved is unreadable
                else
                {
                };
            }
            //After itterating through the IP List sets the scanRunning variable to false and updated the label to show scanning is done
            scanRunning = 0;
            if (isEnglish)
            {
                ScanningIPLabel.Text = "Scanning Done";
            }
            else {
                ScanningIPLabel.Text = "扫描完成";
            }
        }

        //Similar to the SearchButton_Click function, Just using the ip range list view instead
        private async void scanSelectedButton_Click(object sender, EventArgs e)
        {
            scanRunning = 0;
            await Task.Delay(500);
            scanRunning = 1;

            minersFoundCount = 0;
            List<string> ipList = new List<string>();
            try
            {
                minerListView.Items.Clear(); // Clear existing items
            }
            catch (Exception ex)
            {

            }
            //Cycles through the items in the ip range list view
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
            //Runs the same logic as in the searchButton_Click funtion
            foreach (string ip in ipList)
            {
                if (scanRunning == 0)
                {
                    break;
                }
                MinerInfo minerInfo = await GetData(ip);
                if (minerInfo != null)
                {
                    minersFoundCount++;
                    minerFoundCountLabel.Text = ($"{minersFoundCount} Miners Found");
                    minerInfoDict[ip] = minerInfo;
                    ListViewItem item = new ListViewItem(ip);
                    item.SubItems.Add(minerInfo.HashRate);
                    item.SubItems.Add(minerInfo.HashboardStatus);
                    item.SubItems.Add(minerInfo.Uptime);
                    item.SubItems.Add(minerInfo.FanSpeed);
                    item.SubItems.Add(minerInfo.Temperature);
                    item.SubItems.Add(minerInfo.DagProgress);
                    item.SubItems.Add(minerInfo.Accepted);
                    item.SubItems.Add(minerInfo.Rejected);
                    item.SubItems.Add(minerInfo.AcceptedRate);
                    item.SubItems.Add(minerInfo.Pool);
                    item.SubItems.Add(minerInfo.PoolUser);
                    item.SubItems.Add(minerInfo.SelfCheckProgress);
                    item.SubItems.Add(minerInfo.Hashboard1Status);
                    item.SubItems.Add(minerInfo.Hashboard1HashRate);
                    item.SubItems.Add(minerInfo.Hashboard1Temperature);
                    item.SubItems.Add(minerInfo.Hashboard2Status);
                    item.SubItems.Add(minerInfo.Hashboard2HashRate);
                    item.SubItems.Add(minerInfo.Hashboard2Temperature);
                    item.SubItems.Add(minerInfo.Hashboard3Status);
                    item.SubItems.Add(minerInfo.Hashboard3HashRate);
                    item.SubItems.Add(minerInfo.Hashboard3Temperature);

                    minerListView.Items.Add(item);
                }
                else
                {
                };
            }
            if (isEnglish)
            {
                ScanningIPLabel.Text = "Scanning Done";
            }
            else
            {
                ScanningIPLabel.Text = "扫描完成";
            }
        }


        //This is the function that gets the data of each miner
        private async Task<MinerInfo> GetData(string minerIP)
        {
            if (isEnglish)
            {
                ScanningIPLabel.Text = ($"Scanning {minerIP}");
            }
            else
            {
                ScanningIPLabel.Text = ($"扫描 {minerIP}");
            }
            try
            {
                //Create new instance of httpChient
                using (HttpClient client = new HttpClient())
                {
                    //sets the timeout of each ip to the timeout variable declared at the top
                    client.Timeout = TimeSpan.FromSeconds(timeoutTime);
                    //Allows improperly formatted data to still be accpted
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    try
                    {
                        //Get data about Miner
                        //send a request for the data which comes back as a JSON and gets parsed
                        //Multiple get requests are needed for all the information
                        var response = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_uptime_pools_minerinfo");
                        var minerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                        var selfResponse = await client.GetStringAsync($"http://{minerIP}/cgi-bin/cgiNetService.cgi?request=request_selfcheck_progress");
                        var selfMinerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(selfResponse);
                        //Determine hashboard status
                        try
                        {
                            string hash1, hash1a, hash2, hash2a, hash3, hash3a;
                            if (minerInfo.data.minerinfo[0].hash_rate > 10)
                            {
                                hash1 = "O";
                                hash1a = "Working";
                            }
                            else
                            {
                                hash1 = "X";
                                hash1a = "Not Working";
                            }
                            if (minerInfo.data.minerinfo[1].hash_rate > 10)
                            {
                                hash2 = "O";
                                hash2a = "Working";
                            }
                            else
                            {
                                hash2 = "X";
                                hash2a = "Not Working";
                            }
                            if (minerInfo.data.minerinfo[2].hash_rate > 10)
                            {
                                hash3 = "O";
                                hash3a = "Working";
                            }
                            else
                            {
                                hash3 = "X";
                                hash3a = "Not Working";
                            }
                            //Uses the parsed JSON to extract data and set it to the various attributes of the MinerInfo class
                            MinerInfo info = new MinerInfo
                            {

                                HashRate = minerInfo.data.uptime.hash_rate.ToString(),
                                Uptime = minerInfo.data.uptime.up_time.ToString(),
                                HashboardStatus = ($"{hash1} , {hash2} , {hash3}"),
                                FanSpeed = minerInfo.data.uptime.fan_speed.ToString(),
                                Temperature = ((minerInfo.data.minerinfo[0].temperature) + (minerInfo.data.minerinfo[1].temperature) + (minerInfo.data.minerinfo[2].temperature)) / 3.ToString(),
                                DagProgress = minerInfo.data.uptime.dag.progress.ToString(),
                                Accepted = minerInfo.data.uptime.accept_num.ToString(),
                                Rejected = minerInfo.data.uptime.reject_num.ToString(),
                                AcceptedRate = Math.Round((100 - (100 * Convert.ToDouble(minerInfo.data.uptime.reject_num) / Convert.ToDouble(minerInfo.data.uptime.accept_num))), 2).ToString(),
                                Pool = minerInfo.data.pooldata[0].pool.ToString(),
                                PoolUser = minerInfo.data.pooldata[0].user.ToString(),
                                SelfCheckProgress = Math.Round(Convert.ToDouble(selfMinerInfo.data.progress), 2).ToString(),
                                Hashboard1Status = hash1a,
                                Hashboard1HashRate = minerInfo.data.minerinfo[0].hash_rate,
                                Hashboard1Temperature = minerInfo.data.minerinfo[0].temperature,
                                Hashboard2Status = hash2a,
                                Hashboard2HashRate = minerInfo.data.minerinfo[1].hash_rate,
                                Hashboard2Temperature = minerInfo.data.minerinfo[1].temperature,
                                Hashboard3Status = hash3a,
                                Hashboard3HashRate = minerInfo.data.minerinfo[2].hash_rate,
                                Hashboard3Temperature = minerInfo.data.minerinfo[2].temperature,
                            };
                            return info;
                        }
                        catch (Exception ex)
                        {
                            //if there is an error reading the parsed data it returns null
                            Console.WriteLine(ex.Message);
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
            catch (HttpRequestException ex)
            {

                return null;
            }
        }


        //This function takes the start and end ip and returns a list with all the IPs in the range
        private List<string> GetIPRange(string startIPString, string endIPString)
        {
            //declaring new list
            List<string> ipList = new List<string>();
            //declaring IPAddress variables
            IPAddress startIp;
            IPAddress endIp;

            // Convert the start and end IP addresses to integers
            if (System.Net.IPAddress.TryParse(startIPString, out startIp) && System.Net.IPAddress.TryParse(endIPString, out endIp))
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
                            MessageBox.Show("Chinese for Make sure the starting IP is lower than the end");
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


        //This reboots the miners if they are selected
        private async void rebootButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < minerListView.Items.Count; i++)
            {
                if (minerListView.Items[i].Checked)
                {
                    try
                    {
                        using (HttpClient rebootClient = new HttpClient())
                        {

                            rebootClient.Timeout = TimeSpan.FromSeconds(1);
                            rebootClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                            var rebootResponse = await rebootClient.GetStringAsync($"http://{minerListView.Items[i].Text}/cgi-bin/cgiNetService.cgi?send_reboot_miner=send_reboot_miner");
                            ScanningIPLabel.Text = minerListView.Items[i].Text + " Rebooted";
                            minerListView.Items.RemoveAt(i);
                            i -= 1;
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
            ScanningIPLabel.Text = "Reboot Done";
        }


        private async void selfCheckButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < minerListView.Items.Count; i++)
            {
                // Check if the item is a Control containing a CheckBox
                try
                {
                    if (minerListView.Items[i].Checked)
                    {
                        using (HttpClient selfCheckClient = new HttpClient())
                        {
                            selfCheckClient.Timeout = TimeSpan.FromSeconds(1);
                            selfCheckClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                            var selfCheckResponse = await selfCheckClient.GetStringAsync($"http://{minerListView.Items[i].Text}/cgi-bin/cgiNetService.cgi?send_selfcheck_instructions=send_selfcheck_instructions");
                            minerListView.Items.RemoveAt(i);
                            ScanningIPLabel.Text = minerListView.Items[i].Text + " Started Check";
                            i -= 1;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            ScanningIPLabel.Text = "Done Starting Self Check";
        }


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

            //Clear the text boxes
            startIPTextBoxC.Clear();
            startIPTextBoxD.Clear();
            endIPTextBoxC.Clear();
            endIPTextBoxD.Clear();
            nameTextBox.Clear();
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



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the current IP range to app.config
            Console.WriteLine("Exiting program");
            SaveCurrentIPRange();
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
            scanRunning = 0;
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
                catch (IOException ex)
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

        private void minerListView_DoubleClick(object sender, EventArgs e)
        {
            
            if (minerListView.SelectedItems.Count > 0)
            {
                string selectedIPAddress = minerListView.SelectedItems[0].Text;

                // Open the selected IP address in a browser
                OpenInBrowser(selectedIPAddress);
            }
        }

        private void OpenInBrowser(string ipAddress)
        {
            // Replace "http://" with the appropriate protocol if needed
            string url = $"http://{ipAddress}";

            // Open the URL in the default web browser
            Process.Start(url);
        }

        private bool isUpdatingEndTextBox = false;

        private void focusText(MaterialTextBox newBox)
        {
            newBox.Focus();
            newBox.SelectionStart = newBox.Text.Length;
        }

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
    }



    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new MainForm());
        }
    }

    }

    //Class to sort listview by column
    public class ListViewItemComparer : IComparer
{
    private int columnToSort;
    private SortOrder order;

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
            case 2: //Hashboard Status
            case 3: //Uptime
            case 10: //Pool URL
            case 11: //Pool Username
            case 13: //Board 1 sataus
            case 16: //Board 2 sataus
            case 19: //Board 3 sataus
                compareResult = string.Compare(itemX.SubItems[columnToSort].Text, itemY.SubItems[columnToSort].Text);
                break;
            //Doubles
            case 1: //HashRate
            case 4: //Fan Speed
            case 5: //Temperature
            case 6: //DAG rogress
            case 7: //Accepted Shares
            case 8: //Rejected Shares
            case 9: //Acceptance Rate
            case 12: // Self Check Progress
            case 14: //Board 1 Hashrate
            case 15: //Board 1 Temp
            case 17: //Board 2 Hashrate
            case 18: //Board 2 Temp
            case 20: //Board 3 Hashrate
            case 21: //Board 3 Temp
                double hashRateX = double.Parse(itemX.SubItems[columnToSort].Text);
                double hashRateY = double.Parse(itemY.SubItems[columnToSort].Text);
                compareResult = hashRateX.CompareTo(hashRateY);
                break;
            default:
                Console.WriteLine("No case defined for the selected column");
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
//Add a class to change the doubleBuffered property of the list view to true, reducing flickering
public static class ControlExtensions
{
    public static void DoubleBuffered(this Control control, bool enable)
    {
        var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        doubleBufferPropertyInfo?.SetValue(control, enable, null);
    }
}