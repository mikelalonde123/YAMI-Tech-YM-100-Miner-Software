using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Collections;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Specialized;
using System.Windows.Documents;
using System.IO;
using Microsoft.Win32;

namespace MinerInfoApp
{

    public partial class MainForm : MaterialForm { 
        //How long before searching next IP
        private const double timeoutTime = 0.05;

        //Tracks whether a scan Is running or not
        private int scanRunning = 0;//0 = not running, 1 = running

        //Dictionary for the miners
        private readonly Dictionary<string, MinerInfo> minerInfoDict = new Dictionary<string, MinerInfo>();

        // Key for accessing saved ranges in app.config
        private const string SavedRangesKey = "SavedIPRanges";

        //Variables for sorting by column
        private int lastColumnClicked = -1;
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

            minerListView.ColumnClick += minerListView_ColumnClick;

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
            Console.WriteLine("Search Clicked");
            //Try-Catch to clear data from the minerListView
            try
            {
                minerListView.Items.Clear();
            }
            catch (Exception ex)
            {

            }
            //Declare variables which store the IPs that will be used to search the range
            string startIP = startIPTextBox.Text;
            string endIP = endIPTextBox.Text;
            //Checks to make sure the IP boxes are not empty
            if (string.IsNullOrWhiteSpace(startIP) || string.IsNullOrWhiteSpace(endIP))
            {
                MessageBox.Show("Please enter both start and end IP addresses.");
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
                    Console.WriteLine($"{ip} Miner found");
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
                    Console.WriteLine($"{ip} not found.");
                };
            }
            //After itterating through the IP List sets the scanRunning variable to false and updated the label to show scanning is done
            scanRunning = 0;
            ScanningIPLabel.Text = "Scanning Done";
        }

        //Similar to the SearchButton_Click function, Just using the ip range list view instead
        private async void scanSelectedButton_Click(object sender, EventArgs e)
        {
            scanRunning = 0;
            await Task.Delay(500);
            scanRunning = 1;

            minersFoundCount = 0;
            List<string> ipList = new List<string>();
            Console.WriteLine("Search Clicked");
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
                    Console.WriteLine($"{ip} Miner found");
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
                    Console.WriteLine($"{ip} not found.");
                };
            }
            ScanningIPLabel.Text = "Scanning Done";
        }


        //This is the function that gets the data of each miner
        private async Task<MinerInfo> GetData(string minerIP)
        {
            ScanningIPLabel.Text = ($"Scanning {minerIP}");
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
                                AcceptedRate = Math.Round((100 - (100 * Convert.ToDouble(minerInfo.data.uptime.reject_num) / Convert.ToDouble(minerInfo.data.uptime.accept_num))),2).ToString(),
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
                            Console.WriteLine("error getting pool info");
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
            Console.WriteLine("Calling GetIPRange");
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
                        MessageBox.Show("Make sure the starting IP is lower than the end");
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

                        Console.WriteLine("Processing IP address: " + currentIp.ToString());
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
                        Console.WriteLine("Returning IPList");
                        return ipList; // Exit the program when done
                    }
                }
            }
            else
            {
                Console.WriteLine("Returning null");
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
                            Console.WriteLine(minerListView.Items[i].Text + " Rebooted");
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
            Console.WriteLine("calling selfCheckButtn");
            for (int i = 0; i < minerListView.Items.Count; i++)
            {
                // Check if the item is a Control containing a CheckBox
                if (minerListView.Items[i].Checked)
                {


                    using (HttpClient selfCheckClient = new HttpClient())
                    {
                        selfCheckClient.Timeout = TimeSpan.FromSeconds(1);
                        selfCheckClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        await Task.Delay(50);
                        minerListView.Items.RemoveAt(i);
                        Console.WriteLine(minerListView.Items[i].Text + " Started Check");
                        ScanningIPLabel.Text = minerListView.Items[i].Text + " Started Check";
                        i -= 1;
                    }
                }

            }
            ScanningIPLabel.Text = "Done Starting Self Check";
        }


        private void addIPButton_Click(object sender, EventArgs e)
        {
            string startIP1 = startIPTextBox.Text;
            string endIP1 = endIPTextBox.Text;
            string rangeName = nameTextBox.Text;

            if (string.IsNullOrWhiteSpace(startIP1) || string.IsNullOrWhiteSpace(endIP1))
            {
                MessageBox.Show("Please enter both start and end IP addresses.");
                return;
            }

            // Create a new ListViewItem
            ListViewItem newItem = new ListViewItem(startIP1);
            newItem.SubItems.Add(endIP1);
            newItem.SubItems.Add(rangeName);
            // Add the new item to the ListView
            ipRangeListView.Items.Add(newItem);

            //Clear the text boxes
            startIPTextBox.Clear();
            endIPTextBox.Clear();
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
                Console.WriteLine("Saving IPs");
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
                    MessageBox.Show("Error reading the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    MessageBox.Show("IP ranges have been successfully saved to the file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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