using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using RestSharp;
using Newtonsoft.Json;


namespace ScrapAndCheck
{
    public partial class Main : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public Main()
        {
            InitializeComponent();
            aboutPanel.Hide();
            typeSelect.SelectedIndex = 0;
            sslSelect.SelectedIndex = 0;
            anonymitySelect.SelectedIndex = 0;
            proxyView.View = View.Details;
            proxyView.FullRowSelect = true;
            checkBtn.Enabled = false;
            saveBtn.Enabled = false;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void Title_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.Red;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.White;
        }

        private void minimizeBtn_MouseEnter(object sender, EventArgs e)
        {
            minimizeBtn.BackColor = Color.Gray;
        }

        private void minimizeBtn_MouseLeave(object sender, EventArgs e)
        {
            minimizeBtn.BackColor = Color.White;
        }

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            if (aboutBtn.BackColor == Color.Gray){
                aboutPanel.Show();
                panel3.Hide();
                panel4.Hide();
                panel5.Hide();
                panel6.Hide();
                proxiesPanel.Hide();
                aboutBtn.BackColor = Color.DeepSkyBlue;
                aboutBtn.ForeColor = Color.DeepSkyBlue;
                mainBtn.ForeColor = Color.Gray;
                mainBtn.BackColor = Color.Gray;
            }
        }

        private void mainBtn_Click(object sender, EventArgs e)
        {
            if (mainBtn.BackColor == Color.Gray)
            {
                aboutPanel.Hide();
                panel3.Show();
                panel4.Show();
                panel5.Show();
                panel6.Show();
                proxiesPanel.Show();
                mainBtn.BackColor = Color.DeepSkyBlue;
                mainBtn.ForeColor = Color.DeepSkyBlue;
                aboutBtn.ForeColor = Color.Gray;
                aboutBtn.BackColor = Color.Gray;
            }
        }

        private void scrapeBtn_MouseEnter(object sender, EventArgs e)
        {
            scrapeBtn.FlatStyle = FlatStyle.Flat;
        }

        private void checkBtn_MouseEnter(object sender, EventArgs e)
        {
            checkBtn.FlatStyle = FlatStyle.Flat;
        }

        private void scrapeBtn_MouseLeave(object sender, EventArgs e)
        {
            scrapeBtn.FlatStyle = FlatStyle.Popup;
        }

        private void checkBtn_MouseLeave(object sender, EventArgs e)
        {
            checkBtn.FlatStyle = FlatStyle.Popup;
        }
        private bool unknownFlag = false;
        private void unknownProxies() {
            if (typeSelect.SelectedIndex == 0)
            {
                httpproxy = new List<string>();
                socks4proxy = new List<string>();
                socks5proxy = new List<string>();
                temporary = new List<string>();
                unknownproxy = new List<string>();
                string[] strArrays = File.ReadAllLines("Proxies/http.txt");

                int remaining = 0;
                for (int i = 0; i < (int)strArrays.Length; i = checked(i + 1))
                {
                    string str = strArrays[i];
                    httpproxy.Add(str);
                    remaining = i;
                }
                remains += remaining;
                remaining = 0;

                string[] strArrays1 = File.ReadAllLines("Proxies/socks4.txt");
                for (int i = 0; i < (int)strArrays1.Length; i = checked(i + 1))
                {
                    string str = strArrays1[i];
                    socks4proxy.Add(str);
                    remaining = i;

                }
                remains += remaining;
                remaining = 0;

                string[] strArrays2 = File.ReadAllLines("Proxies/socks5.txt");
                for (int i = 0; i < (int)strArrays2.Length; i = checked(i + 1))
                {
                    string str = strArrays2[i];
                    socks5proxy.Add(str);
                    remaining = i;
                }
                remains += remaining;
                remaining += 3;
                remaining = 0;
            }
            else if (typeSelect.SelectedIndex == 1)
            {
                httpproxy = new List<string>();
                string[] strArrays = File.ReadAllLines("Proxies/http.txt");

                int remaining = 0;
                for (int i = 0; i < (int)strArrays.Length; i = checked(i + 1))
                {
                    string str = strArrays[i];
                    httpproxy.Add(str);
                    remaining = i;
                }
                remains += remaining;
            }
            else if (typeSelect.SelectedIndex == 2)
            {
                socks4proxy = new List<string>();
                int remaining = 0;
                string[] strArrays1 = File.ReadAllLines("Proxies/socks4.txt");
                for (int i = 0; i < (int)strArrays1.Length; i = checked(i + 1))
                {
                    string str = strArrays1[i];
                    socks4proxy.Add(str);
                    remaining = i;

                }
                remains += remaining;
            }
            else if (typeSelect.SelectedIndex == 3)
            {
                socks5proxy = new List<string>();
                int remaining = 0;
                string[] strArrays1 = File.ReadAllLines("Proxies/socks5.txt");
                for (int i = 0; i < (int)strArrays1.Length; i = checked(i + 1))
                {
                    string str = strArrays1[i];
                    socks5proxy.Add(str);
                    remaining = i;

                }
                remains += remaining;
            }
        }
        WebClient web = new WebClient();
        List<string> httpproxy = new List<string>();
        List<string> socks4proxy = new List<string>();
        List<string> socks5proxy = new List<string>();
        List<string> temporary = new List<string>();
        List<string> unknownproxy = new List<string>();
        private int remains = 0;
        public void ScrapeAll()
        {
            /*
            IEnumerator enumerator = null;
            string[] strArrays3 = File.ReadAllLines("Proxies/Sources.txt");
            for (int i = 0; i < (int)strArrays3.Length; i = checked(i + 1))
            {
                string str = strArrays3[i];
                
                string empty = string.Empty;
                try
                {
                    empty = (new WebClient()).DownloadString(str);
                    if (str.Length >= 55)
                    {
                        str = str.Substring(0, 36) + ".....";
                    }
                    scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
                }
                catch (Exception exception)
                {
                }
                try
                {
                    enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Match current = (Match)enumerator.Current;
                        temporary.Add(current.Value);
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }

            unknownproxy = temporary.Distinct().ToList();
            remains += unknownproxy.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            using (TextWriter tw = new StreamWriter("Proxies/unknownProxies.txt"))
            {
                foreach (String s in unknownproxy)
                {
                    tw.WriteLine(s);
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                } 
            }
            httpAllAll();
            socks4AllAll();
            socks5AllAll();
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));*/
            // pointer
            httpYesAnon();
            httpYesElite();
            httpYesTrans();
            httpNoAnon();
            httpNoElite();
            httpNoTrans();
            socks4AllAll();
            socks5AllAll();
        }
        string typo = "";
        string sslo = "";
        string anono = "";
        public void startUp()
        {
            timer.Interval = Int32.Parse(timeCheck.Text) * 60000;
            proxyView.Items.Clear();
            remains = 0;
            unknownFlag = false;
            l = 0;
            m = 0;
            n = 0;
            timer.Stop();
            scrapeBtn.Enabled = false;
            scraping.Visible = true;
            scraping.Text = "Scrapping.......";
            timeOutRange.Enabled = false;
            typeSelect.Enabled = false;
            sslSelect.Enabled = false;
            anonymitySelect.Enabled = false;
            checkBtn.Enabled = false;
            remainingCheck.Text = "0";
            checkedCheck.Text = "0";
            liveCheck.Text = "0";
            deadCheck.Text = "0";
            typo = typeSelect.Text;
            sslo = sslSelect.Text;
            anono = anonymitySelect.Text;
        }
        private int x = 0;
        private void scrapeBtn_Click(object sender, EventArgs e)
        {
            if (!HasInternet())
            {
                MessageBox.Show(string.Concat("You do NOT have access to the internet.", Environment.NewLine, "Press 'OK' to Terminate."), "No Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            startUp();
            if (unknowProxy.Checked == true)
            {
                unknownFlag = true;
                unknownProxies();
            }
            if (typeSelect.SelectedIndex == 0)
            {
                // ALL to ALL
                l = 0;
                m = 0;
                n = 0;
                x = 0;
                backScrapper.RunWorkerAsync();
            }
            else if (typeSelect.SelectedIndex == 1)
            {
                if (sslSelect.SelectedIndex == 0)
                {
                    if (anonymitySelect.SelectedIndex == 0)
                    {
                        // HTTP, ALL, ALL 6 functions
                        l = 1;
                        m = 0;
                        n = 0;
                        x = 11;
                        backScrapper.RunWorkerAsync();
                        
                    }
                    else if (anonymitySelect.SelectedIndex == 1)
                    {
                        // HTTP, ALL, Transparent 2 functions
                        l = 1;
                        m = 0;
                        n = 1;
                        x = 12;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 2)
                    {
                        // HTTP, ALL, Anonymous 2 functions
                        l = 1;
                        m = 0;
                        n = 2;
                        x = 13;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 3)
                    {
                        // HTTP, ALL, Elite 2 functions
                        l = 1;
                        m = 0;
                        n = 3;
                        x = 14;
                        backScrapper.RunWorkerAsync();
                    }
                }
                else if (sslSelect.SelectedIndex == 1)
                {
                    if (anonymitySelect.SelectedIndex == 0)
                    {
                        // HTTP, YES, ALL 3 functions
                        l = 1;
                        m = 1;
                        n = 0;
                        x = 21;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 1)
                    {
                        // HTTP, YES, Transparent 1 functions
                        l = 1;
                        m = 1;
                        n = 1;
                        x = 22;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 2)
                    {
                        // HTTP, YES, Anonymous 1 functions
                        l = 1;
                        m = 1;
                        n = 2;
                        x = 23;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 3)
                    {
                        // HTTP, YES,Elite 1 functions
                        l = 1;
                        m = 1;
                        n = 3;
                        x = 24;
                        backScrapper.RunWorkerAsync();
                    }
                }
                else if (sslSelect.SelectedIndex == 2)
                {
                    if (anonymitySelect.SelectedIndex == 0)
                    {
                        // HTTP, NO, ALL 3 functions
                        l = 1;
                        m = 2;
                        n = 0;
                        x = 31;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 1)
                    {
                        // HTTP, NO, Transparent 1 functions
                        l = 1;
                        m = 2;
                        n = 1;
                        x = 32;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 2)
                    {
                        // HTTP, NO, Anonymous 1 functions
                        l = 1;
                        m = 2;
                        n = 2;
                        x = 33;
                        backScrapper.RunWorkerAsync();
                    }
                    else if (anonymitySelect.SelectedIndex == 3)
                    {
                        // HTTP, NO, Elite 1 functions
                        l = 1;
                        m = 2;
                        n = 3;
                        x = 34;
                        backScrapper.RunWorkerAsync();
                    }
                }
            }
            else if (typeSelect.SelectedIndex == 2){
                // SOCKS4, ALL, ALL
                l = 2;
                m = 0;
                n = 0;
                x = 4;
                backScrapper.RunWorkerAsync();
            }
            else if (typeSelect.SelectedIndex == 3)
            {
                // SOCKS5, ALL, ALL
                l = 3;
                m = 0;
                n = 0;
                x = 5;
                backScrapper.RunWorkerAsync();
            }
        }
        List<string> temp = new List<string>();
        public void Checker(int a, int b, int c)
        {
            proxyView.Items.Clear();
            temp = new List<string>();
            if (a == 0 && b == 0 && c == 0)
            {
                temp.AddRange(httpyestrans);
                AddToListView(temp, 111, 1);
                temp = new List<string>();
                temp.AddRange(httpyeselite);
                AddToListView(temp, 112, 1);
                temp = new List<string>();
                temp.AddRange(httpyesanon);
                AddToListView(temp, 113, 1);
                temp = new List<string>();
                temp.AddRange(httpnotrans);
                AddToListView(temp, 121, 1);
                temp = new List<string>();
                temp.AddRange(httpnoelite);
                AddToListView(temp, 122, 1);
                temp = new List<string>();
                temp.AddRange(httpnoanon);
                AddToListView(temp, 123, 1);
                temp = new List<string>();
                temp.AddRange(socks4allall);
                AddToListView(temp, 211, 2);
                temp = new List<string>();
                temp.AddRange(socks5allall);
                AddToListView(temp, 311, 3);
                if (unknownFlag == true)
                {
                    temp = new List<string>();
                    temp.AddRange(httpproxy);
                    AddToListView(temp, -111, 1);
                    temp = new List<string>();
                    temp.AddRange(socks4proxy);
                    AddToListView(temp, 211, 2);
                    temp = new List<string>();
                    temp.AddRange(socks5proxy);
                    AddToListView(temp, 311, 3);
                }
            }
            else if (a == 1 && b == 0 && c == 0)
            {
                temp.AddRange(httpyestrans);
                AddToListView(temp, 111, 1);
                temp = new List<string>();
                temp.AddRange(httpyeselite);
                AddToListView(temp, 112, 1);
                temp = new List<string>();
                temp.AddRange(httpyesanon);
                AddToListView(temp, 113, 1);
                temp = new List<string>();
                temp.AddRange(httpnotrans);
                AddToListView(temp, 121, 1);
                temp = new List<string>();
                temp.AddRange(httpnoelite);
                AddToListView(temp, 122, 1);
                temp = new List<string>();
                temp.AddRange(httpnoanon);
                AddToListView(temp, 123, 1);
                if (unknownFlag == true)
                {
                    temp = new List<string>();
                    temp.AddRange(httpproxy);
                    AddToListView(temp, -111, 1);
                }

            }
            else if (a == 1 && b == 0 && c == 1)
            {
                temp.AddRange(httpyestrans);
                AddToListView(temp, 111, 1);
                temp = new List<string>();
                temp.AddRange(httpnotrans);
                AddToListView(temp, 121, 1);
            }
            else if (a == 1 && b == 0 && c == 2)
            {
                temp.AddRange(httpyesanon);
                AddToListView(temp, 112, 1);
                temp = new List<string>();
                temp.AddRange(httpnoanon);
                AddToListView(temp, 122, 1);
            }
            else if (a == 1 && b == 0 && c == 3)
            {
                temp.AddRange(httpyeselite);
                AddToListView(temp, 113, 1);
                temp = new List<string>();
                temp.AddRange(httpnoelite);
                AddToListView(temp, 123, 1);
            }
            else if (a == 1 && b == 1 && c == 0)
            {
                temp.AddRange(httpyestrans);
                AddToListView(temp, 111, 1);
                temp = new List<string>();
                temp.AddRange(httpyeselite);
                AddToListView(temp, 112, 1);
                temp = new List<string>();
                temp.AddRange(httpyesanon);
                AddToListView(temp, 113, 1);
            }
            else if (a == 1 && b == 1 && c == 1)
            {
                temp.AddRange(httpyestrans);
                AddToListView(temp, 111, 1);
            }
            else if (a == 1 && b == 1 && c == 2)
            {
                temp.AddRange(httpyesanon);
                AddToListView(temp, 112, 1);
            }
            else if (a == 1 && b == 1 && c == 3)
            {
                temp.AddRange(httpyeselite);
                AddToListView(temp, 113, 1);
            }
            else if (a == 1 && b == 2 && c == 0)
            {
                temp.AddRange(httpnotrans);
                AddToListView(temp, 121, 1);
                temp = new List<string>();
                temp.AddRange(httpnoelite);
                AddToListView(temp, 122, 1);
                temp = new List<string>();
                temp.AddRange(httpnoanon);
                AddToListView(temp, 123, 1);
            }
            else if (a == 1 && b == 2 && c == 1)
            {
                temp.AddRange(httpnotrans);
                AddToListView(temp, 121, 1);
            }
            else if (a == 1 && b == 2 && c == 2)
            {
                temp.AddRange(httpnoanon);
                AddToListView(temp, 122, 1);
            }
            else if (a == 1 && b == 2 && c == 3)
            {
                temp.AddRange(httpnoelite);
                AddToListView(temp, 123, 1);
            }
            else if (a == 2 && b == 0 && c == 0)
            {
                temp.AddRange(socks4allall);
                AddToListView(temp, 211, 2);
                if (unknownFlag == true)
                {
                    temp = new List<string>();
                    temp.AddRange(socks4proxy);
                    AddToListView(temp, 211, 2);
                }
            }
            else if (a == 3 && b == 0 && c == 0)
            {
                temp.AddRange(socks5allall);
                AddToListView(temp, 311, 3);
                if (unknownFlag == true)
                {
                    temp = new List<string>();
                    temp.AddRange(socks5proxy);
                    AddToListView(temp, 311, 3);
                }
            }
            checkBtn.Enabled = true;
            checking.Visible = false;
            threads1.Enabled = true;
            timeOutTest.Enabled = true;
            scrapeBtn.Enabled = true;
            saveBtn.Enabled = true;
            country.Enabled = true;
            blacklisted.Enabled = true;
            remainingCheck.Text = "0";
            checkedCheck.Text = checkked.ToString();
            deadCheck.Text = (checkked - lastList.Count).ToString();
            liveCheck.Text = lastList.Count.ToString();
            country.Items.Add("All");
            country.SelectedIndex = 0;
            blacklisted.Items.Add("All");
            blacklisted.SelectedIndex = 0;

            // Sorting
            countryOptions = new List<string>();
            blacklistedOptions = new List<string>();
            for (int i = 0; i < lastList.Count; i++)
            {
                countryOptions.Add(lastList[i].SubItems[3].Text);
                blacklistedOptions.Add(lastList[i].SubItems[4].Text);
            }
            uniqueCountry = countryOptions.Distinct().ToList();
            uniqueBlacklist = blacklistedOptions.Distinct().ToList();
            for (int j = 0; j < uniqueCountry.Count; j++)
            {
                country.Items.Add(uniqueCountry[j]);
            }
            for (int k = 0; k < uniqueBlacklist.Count; k++)
            {
                blacklisted.Items.Add(uniqueBlacklist[k]);
            }
            saveList = new List<string>();
            for (int i = 0; i < lastList.Count; i++)
            {
                saveList.Add(lastList[i].SubItems[0].Text);
            }
            timer.Start();
        }
        List<string> uniqueCountry = new List<string>();
        List<string> uniqueBlacklist = new List<string>();
        List<string> countryOptions = new List<string>();
        List<string> blacklistedOptions = new List<string>();

        public void AddToListView(List<string> p, int w, int v)
        {
            using (TextWriter tw = new StreamWriter("Proxies/Proxies.txt"))
            {
                foreach (string s in p)
                {
                    tw.WriteLine(s);
                }
            }
            // Configure Config file for Check.exe
            using (TextWriter tw = new StreamWriter("Proxies/Config.txt"))
            {
                if (v == 1)
                    tw.WriteLine("Type: http");
                else if (v == 2)
                    tw.WriteLine("Type: socks4");
                else if(v == 3)
                    tw.WriteLine("Type: socks4");
                tw.WriteLine(string.Concat("Threads: ", threads1.Value.ToString()));
                tw.WriteLine(string.Concat("MaxTimeout: ", timeOutTest.Value.ToString()));
                tw.WriteLine("DefaultJudges: true");
                tw.WriteLine("CustomJudge: .");
                tw.WriteLine("ScrapeProxies: false");
            }
            // Checking Proxies with another software
            // Because Winform UI gets slower with multi threads
            System.Diagnostics.Process pRun = new System.Diagnostics.Process();
            pRun.StartInfo.WorkingDirectory = "Proxies";
            pRun.StartInfo.FileName = "Check.exe";
            pRun.Start();
            pRun.WaitForExit();

            // Extract Output
            List<string> eProxy = new List<string>();
            List<string> eLatency = new List<string>();
            using (StreamReader sr = File.OpenText("Proxies/Output.txt"))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    try
                    {
                        string[] str = s.Split('|');
                        eProxy.Add(str[0].Split(' ')[0]);
                        eLatency.Add(str[1].Split(' ')[1]);
                    }
                    catch
                    {
                        eProxy.Add(s);
                        eLatency.Add("Interupted");
                    }
                }
            }

            string privacy = "public";

            string type = "";
            string ssl = "";
            string level = "";

            // find via if/else
            if (w == 111)
            {
                type = "Http";
                ssl = "Yes";
                level = "Transparent";
            }
            else if (w == 112)
            {
                type = "Http";
                ssl = "Yes";
                level = "Elite";
            }
            else if (w == 113)
            {
                type = "Http";
                ssl = "Yes";
                level = "Anonymous";
            }
            else if (w == 121)
            {
                type = "Http";
                ssl = "No";
                level = "Transparent";
            }
            else if (w == 122)
            {
                type = "Http";
                ssl = "No";
                level = "Elite";
            }
            else if (w == 123)
            {
                type = "Http";
                ssl = "No";
                level = "Anonymous";
            }
            else if (w == 211)
            {
                type = "Socks4";
                ssl = "N/A";
                level = "N/A";
            }
            else if (w == 311)
            {
                type = "Socks5";
                ssl = "N/A";
                level = "N/A";
            }
            else if (w == -111)
            {
                type = "http";
                ssl = "N/A";
                level = "N/A";
            }

            proxyView.Hide();
            string city = "";
            string country = "";
            string ip = "";
            string blacklisted = "";
            
            for (int i = 0; i < eProxy.Count; i++)
            {
                city = "N/A";
                country = "N/A";

                // find city/country
                if (countryCheckBox.Checked == true)
                {
                    try
                    {
                        ip = eProxy[i].Split(':')[0];
                        var client = new RestClient("http://api.ipstack.com/" + ip + "?access_key=d71eb4b4be5e243b16f1940010811be4");
                        // Console.WriteLine(ip);
                        var request = new RestRequest()
                        {
                            Method = Method.GET
                        };
                        var response = client.Execute(request);
                        var dictionary = JsonConvert.DeserializeObject<IDictionary>(response.Content);
                        foreach (var key in dictionary.Keys)
                        {
                            if (key.ToString() == "country_name")
                            {
                                if (dictionary[key] == null)
                                    country = "Not Found";
                                else
                                    country = dictionary[key].ToString();
                            }
                            else if (key.ToString() == "city")
                            {
                                if (dictionary[key] == null)
                                    city = "Not Found";
                                else
                                    city = dictionary[key].ToString();
                            }
                        }
                    }
                    catch
                    {
                        country = "Not Found";
                        city = "Not Found";
                    }
                }
                
                
                blacklisted = "";
                // find blacklisted
                for (int j = 0; j < black.Count; j++)
                {
                    if (black[j].Contains(eProxy[i]))
                    {
                        blacklisted = "Yes";
                    }
                }
                if (blacklisted != "Yes")
                {
                    blacklisted = "No";
                }

                try
                {
                    string[] row = { eProxy[i], eLatency[i], city, country, blacklisted, privacy, type, ssl, level };
                    ListViewItem L = new ListViewItem(row);
                    tempList.Add(L);
                    lastList.Add(L);
                    proxyView.Items.Add(L);
                }
                catch
                {

                }
            }
            proxyView.Show();
        }
        List<string> black = new List<string>();
        private void blacklist()
        {
            try
            {
                var client = new RestClient("https://myip.ms/files/blacklist/csf/latest_blacklist.txt");
                var request = new RestRequest()
                {
                    Method = Method.GET
                };
                var response = client.Execute(request);

                string str = response.Content;
                string[] arr = str.Split('\n');
                black = new List<string>();
                for (int i = 18; i < arr.Length; i++)
                {
                    black.Add(arr[i]);
                }
                for (int i = 0; i < 18; i++)
                {
                    black.RemoveAt(black.Count - 1);
                }
            }
            catch
            {
                black = new List<string>();
            }
        }
        private int checkked = 0;
        List<ListViewItem> lastList = new List<ListViewItem>();
        private void checkBtn_Click(object sender, EventArgs e)
        {
            if (!HasInternet())
            {
                MessageBox.Show(string.Concat("You do NOT have access to the internet.", Environment.NewLine, "Press 'OK' to Terminate."), "No Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            blacklist();
            lastList = new List<ListViewItem>();
            remainingCheck.Text = remains.ToString();
            checkked = Int32.Parse(remainingCheck.Text);
            checkBtn.Enabled = false;
            checking.Visible = true;
            threads1.Enabled = false;
            timeOutTest.Enabled = false;
            scrapeBtn.Enabled = false;
            saveBtn.Enabled = false;
            //latency.Enabled = false;
            country.Enabled = false;
            blacklisted.Enabled = false;
            country.Items.Clear();
            blacklisted.Items.Clear();
            Checker(l, m, n);
            
        }
        private int l = 0;
        private int m = 0;
        private int n = 0;
        private void timeOutRange_Scroll(object sender, EventArgs e)
        {
            timeCheck.Text = timeOutRange.Value.ToString();
            if (timeOutRange.Value > 99)
            {
                label17.Location = new Point(248, 89);
            }
            else if (timeOutRange.Value < 100 && timeOutRange.Value > 9)
            {
                label17.Location = new Point(238, 89);
            }
            else
            {
                label17.Location = new Point(230, 89);
            }
        }

        private void timeOutTest_Scroll(object sender, EventArgs e)
        {
            timeTest.Text = timeOutTest.Value.ToString() + " ms";
        }

        private void threads_Scroll(object sender, EventArgs e)
        {
            thread.Text = threads1.Value.ToString();
        }

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeSelect.SelectedIndex == 1)
            {
                if (sslSelect.Items.Count < 2)
                {
                    sslSelect.Items.Add("Yes");
                    sslSelect.Items.Add("No");
                    anonymitySelect.Items.Add("Transparent");
                    anonymitySelect.Items.Add("Elite");
                    anonymitySelect.Items.Add("Anonymous");

                }
                if (sslSelect.SelectedIndex == 0 && anonymitySelect.SelectedIndex == 0 && (typeSelect.SelectedIndex == 1 || typeSelect.SelectedIndex == 0)){
                    unknowProxy.Show();
                    label19.Show();
                }
            }
            else if (typeSelect.SelectedIndex == 0)
            {
                sslSelect.Items.Remove("Yes");
                sslSelect.Items.Remove("No");
                anonymitySelect.Items.Remove("Transparent");
                anonymitySelect.Items.Remove("Elite");
                anonymitySelect.Items.Remove("Anonymous");
                sslSelect.SelectedIndex = 0;
                anonymitySelect.SelectedIndex = 0;
                if (sslSelect.SelectedIndex == 0 && anonymitySelect.SelectedIndex == 0 && (typeSelect.SelectedIndex == 1 || typeSelect.SelectedIndex == 0))
                {
                    unknowProxy.Show();
                    label19.Show();
                }
            }
            else{
                sslSelect.Items.Remove("Yes");
                sslSelect.Items.Remove("No");
                anonymitySelect.Items.Remove("Transparent");
                anonymitySelect.Items.Remove("Elite");
                anonymitySelect.Items.Remove("Anonymous");
                sslSelect.SelectedIndex = 0;
                anonymitySelect.SelectedIndex = 0;
                unknowProxy.Hide();
                label19.Hide();
            }
            label6.Focus();
        }

        private void SSL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sslSelect.SelectedIndex == 0 && anonymitySelect.SelectedIndex == 0 && (typeSelect.SelectedIndex == 1 || typeSelect.SelectedIndex == 0))
            {
                unknowProxy.Show();
                label19.Show();
            }
            else
            {
                unknowProxy.Hide();
                label19.Hide();
            }
            label6.Focus();
        }

        private void Anonymity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (anonymitySelect.SelectedIndex == 0 && sslSelect.SelectedIndex == 0 && (typeSelect.SelectedIndex == 1 || typeSelect.SelectedIndex == 0))
            {
                unknowProxy.Show();
                label19.Show();
            }
            else
            {
                unknowProxy.Hide();
                label19.Hide();
            }
            label6.Focus();
        }

        private void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Focus();
        }

        private void privacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Focus();
        }

        private void ssl_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            label6.Focus();
        }

        private void type_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            label6.Focus();
        }

        private void blacklisted_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Focus();
        }

        private void anonymity_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            label6.Focus();
        }
        List<string> httpyesanon = new List<string>();

        private void httpYesAnon()
        {
            httpyesanon = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=http&timeout=7000&country=all&anonymity=anonymous&ssl=yes";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 35)
                {
                    str = str.Substring(0, 26) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            httpyesanon = temporary.Distinct().ToList();
            remains += httpyesanon.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in httpyesanon)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }

        //List<string> httpallall = new List<string>();
        private void httpAllAll()
        {
            httpYesAnon();
            httpYesElite();
            httpYesTrans();
            httpNoAnon();
            httpNoElite();
            httpNoTrans();
        }

        //List<string> httpalltrans = new List<string>();
        private void httpAllTrans()
        {
            httpYesTrans();
            httpNoTrans();
        }
        private void httpAllAnon()
        {
            httpYesAnon();
            httpNoAnon();
        }
        private void httpAllElite()
        {
            httpYesElite();
            httpNoElite();
        }
        private void httpYesAll()
        {
            httpYesElite();
            httpYesAnon();
            httpYesTrans();
        }
        List<string> httpyestrans = new List<string>();
        private void httpYesTrans()
        {
            httpyestrans = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=http&timeout=7000&country=all&anonymity=transparent&ssl=yes";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            httpyestrans = temporary.Distinct().ToList();
            remains += httpyestrans.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in httpyestrans)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        List<string> httpyeselite = new List<string>();
        private void httpYesElite()
        {
            httpyeselite = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=http&timeout=7000&country=all&anonymity=elite&ssl=yes";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            httpyeselite = temporary.Distinct().ToList();
            remains += httpyeselite.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in httpyeselite)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        List<string> httpnoall = new List<string>();
        private void httpNoAll()
        {
            httpNoElite();
            httpNoAnon();
            httpNoTrans();
        }
        List<string> httpnotrans = new List<string>();
        private void httpNoTrans()
        {
            httpnotrans = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=http&timeout=7000&country=all&anonymity=transparent&ssl=no";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            httpnotrans = temporary.Distinct().ToList();
            remains += httpnotrans.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in httpnotrans)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        List<string> httpnoanon = new List<string>();
        private void httpNoAnon()
        {
            httpnoanon = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=http&timeout=7000&country=all&anonymity=anonymous&ssl=no";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            httpnoanon = temporary.Distinct().ToList();
            remains += httpnoanon.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in httpnoanon)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        List<string> httpnoelite = new List<string>();
        private void httpNoElite()
        {
            httpnoelite = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=http&timeout=7000&country=all&anonymity=elite&ssl=no";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            httpnoelite = temporary.Distinct().ToList();
            remains += httpnoelite.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in httpnoelite)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        List<string> socks4allall = new List<string>();
        private void socks4AllAll()
        {
            socks4allall = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=socks4&timeout=7000&country=all&anonymity=all&ssl=all";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            socks4allall = temporary.Distinct().ToList();
            remains += socks4allall.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in socks4allall)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        List<string> socks5allall = new List<string>();
        private void socks5AllAll()
        {
            socks5allall = new List<string>();
            temporary = new List<string>();
            ;
            IEnumerator enumerator = null;
            string str = "https://api.proxyscrape.com/?request=displayproxies&proxytype=socks5&timeout=7000&country=all&anonymity=all&ssl=all";

            string empty = string.Empty;
            try
            {
                empty = (new WebClient()).DownloadString(str);
                if (str.Length >= 55)
                {
                    str = str.Substring(0, 36) + ".....";
                }
                scraping.Invoke(new Action(() => scraping.Text = "Scrapping From: " + str));
            }
            catch (Exception exception)
            {
            }
            try
            {
                enumerator = (new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})")).Matches(empty).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Match current = (Match)enumerator.Current;
                    temporary.Add(current.Value);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }

            socks5allall = temporary.Distinct().ToList();
            remains += socks5allall.Count;
            proxyView.Invoke(new Action(() => proxyView.Hide()));
            
                foreach (string s in socks5allall)
                {
                    
                    proxyView.Invoke(new Action(() => proxyView.Items.Add(s)));
                }
            
            proxyView.Invoke(new Action(() => proxyView.Show()));
            remainingCheck.Invoke(new Action(() => remainingCheck.Text = remains.ToString()));
        }
        private void cleanUp()
        {
            scrapeBtn.Invoke(new Action(() => scrapeBtn.Text = "Scrape Proxies"));
            scrapeBtn.Invoke(new Action(() => scrapeBtn.Enabled = true));
            scraping.Invoke(new Action(() => scraping.Text = "Done Scraping!"));
            timeOutRange.Invoke(new Action(() => timeOutRange.Enabled = true));
            typeSelect.Invoke(new Action(() => typeSelect.Enabled = true));
            sslSelect.Invoke(new Action(() => sslSelect.Enabled = true));
            anonymitySelect.Invoke(new Action(() => anonymitySelect.Enabled = true));
            //latency.Invoke(new Action(() => latency.Enabled = true));
            country.Invoke(new Action(() => country.Enabled = true));
            blacklisted.Invoke(new Action(() => blacklisted.Enabled = true));
            checkBtn.Invoke(new Action(() => checkBtn.Enabled = true));
            saveBtn.Invoke(new Action(() => saveBtn.Enabled = true));
            flag = true;
        }

        private void backScrapper_DoWork(object sender, DoWorkEventArgs e)
        {
            if (x == 0){
                ScrapeAll();
            }
            else if (x == 11)
            {
                httpAllAll();
            }
            else if (x == 12)
            {
                httpAllTrans();
            }
            else if (x == 13)
            {
                httpAllAnon();
            }
            else if (x == 14)
            {
                httpAllElite();
            }
            else if (x == 21)
            {
                httpYesAll();
            }
            else if (x == 22)
            {
                httpYesTrans();
            }
            else if (x == 23)
            {
                httpYesAnon();
            }
            else if (x == 24)
            {
                httpYesElite();
            }
            else if (x == 31)
            {
                httpNoAll();
            }
            else if (x == 32)
            {
                httpNoTrans();
            }
            else if (x == 33)
            {
                httpNoAnon();
            }
            else if (x == 34)
            {
                httpNoElite();
            }
            else if (x == 4)
            {
                socks4AllAll();
            }
            else if (x == 5)
            {
                socks5AllAll();
            }
            cleanUp();
        }

        private void saveBtn_MouseEnter(object sender, EventArgs e)
        {
            saveBtn.FlatStyle = FlatStyle.Flat;
        }

        private void saveBtn_MouseLeave(object sender, EventArgs e)
        {
            saveBtn.FlatStyle = FlatStyle.Popup;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // repeat fresh scrape and proxies
            timer.Stop();
            scrapeBtn_Click(sender, e);
            timer1.Start();
        }

        private void proxyView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proxyView.SelectedItems.Count == 0)
                return;

            ListViewItem item = proxyView.SelectedItems[0];
            proxyCopy.Text = item.Text;
        }
        private bool flag = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                timer1.Stop();
                checkBtn_Click(sender, e);
            }
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        private static bool HasInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (!HasInternet())
            {
                MessageBox.Show(string.Concat("You do NOT have access to the internet.", Environment.NewLine, "Press 'OK' to Terminate."), "No Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void countryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (countryCheckBox.Checked == true)
            {
                slower.Visible = true;
            }
            else if (countryCheckBox.Checked == false)
            {
                slower.Visible = false;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            checkBtn_Click(sender, e);
        }
        List<ListViewItem> tempList = new List<ListViewItem>();
        List<ListViewItem> tempL = new List<ListViewItem>();
        private void country_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            saveList = new List<string>();
            if (country.SelectedIndex == 0 && blacklisted.SelectedIndex == 0)
            {
                proxyView.Hide();
                proxyView.Items.Clear();
                for (int i = 0; i < lastList.Count; i++){
                    proxyView.Items.Add(lastList[i]);
                    saveList.Add(lastList[i].SubItems[0].Text);
                }
                proxyView.Show();
            }
            else if (country.SelectedIndex != 0 && blacklisted.SelectedIndex != 0)
            {
                tempL = new List<ListViewItem>();
                for (int j = 0; j < uniqueCountry.Count; j++)
                {
                    if (country.Text == uniqueCountry[j])
                    {
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            if (tempList[i].SubItems[3].Text == uniqueCountry[j])
                            {
                                tempL.Add(tempList[i]);
                            }
                        }
                    }
                }
                proxyView.Hide();
                proxyView.Items.Clear();
                for (int i = 0; i < tempL.Count; i++)
                {
                    proxyView.Items.Add(tempL[i]);
                    saveList.Add(tempL[i].SubItems[0].Text);
                }
                proxyView.Show();
            }
            else
            {
                if (blacklisted.SelectedIndex == 0)
                {
                    tempList = new List<ListViewItem>();
                    for (int j = 0; j < uniqueCountry.Count; j++)
                    {
                        if (country.Text == uniqueCountry[j])
                        {
                            for (int i = 0; i < lastList.Count; i++)
                            {
                                if (lastList[i].SubItems[3].Text == uniqueCountry[j])
                                {
                                    tempList.Add(lastList[i]);
                                }
                            }
                        }
                    }
                    proxyView.Hide();
                    proxyView.Items.Clear();
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        proxyView.Items.Add(tempList[i]);
                        saveList.Add(tempList[i].SubItems[0].Text);
                    }
                    proxyView.Show();
                }
                else
                {
                    proxyView.Hide();
                    proxyView.Items.Clear();
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        proxyView.Items.Add(tempList[i]);
                        saveList.Add(tempList[i].SubItems[0].Text);
                    }
                    proxyView.Show();
                }
            }
        }

        private void blacklisted_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            saveList = new List<string>();
            if (country.SelectedIndex == 0 && blacklisted.SelectedIndex == 0)
            {
                proxyView.Hide();
                proxyView.Items.Clear();
                for (int i = 0; i < lastList.Count; i++)
                {
                    proxyView.Items.Add(lastList[i]);
                    saveList.Add(lastList[i].SubItems[0].Text);
                }
                proxyView.Show();
            }
            else if (country.SelectedIndex != 0 && blacklisted.SelectedIndex != 0)
            {
                tempL = new List<ListViewItem>();
                for (int j = 0; j < uniqueBlacklist.Count; j++)
                {
                    if (blacklisted.Text == uniqueBlacklist[j])
                    {
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            if (tempList[i].SubItems[4].Text == uniqueBlacklist[j])
                            {
                                tempL.Add(tempList[i]);
                            }
                        }
                    }
                }
                proxyView.Hide();
                proxyView.Items.Clear();
                for (int i = 0; i < tempL.Count; i++)
                {
                    proxyView.Items.Add(tempL[i]);
                    saveList.Add(tempL[i].SubItems[0].Text);
                }
                proxyView.Show();
            }
            else
            {
                if (country.SelectedIndex == 0)
                {
                    tempList = new List<ListViewItem>();
                    for (int j = 0; j < uniqueBlacklist.Count; j++)
                    {
                        if (blacklisted.Text == uniqueBlacklist[j])
                        {
                            for (int i = 0; i < lastList.Count; i++)
                            {
                                if (lastList[i].SubItems[4].Text == uniqueBlacklist[j])
                                {
                                    tempList.Add(lastList[i]);
                                }
                            }
                        }
                    }
                    proxyView.Hide();
                    proxyView.Items.Clear();
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        proxyView.Items.Add(tempList[i]);
                        saveList.Add(tempList[i].SubItems[0].Text);
                    }
                    proxyView.Show();
                }
                else
                {
                    proxyView.Hide();
                    proxyView.Items.Clear();
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        proxyView.Items.Add(tempList[i]);
                        saveList.Add(tempList[i].SubItems[0].Text);
                    }
                    proxyView.Show();
                }
            }
        }
        List<string> saveList = new List<string>();
        private void saveBtn_Click(object sender, EventArgs e)
        {
            label20.Text = "Saving...";
            label20.Visible = true;

            TextWriter tw = new StreamWriter("FinalProxies.txt");

            foreach (string s in saveList)
                tw.WriteLine(s);

            tw.Close();

            label20.Text = "Saved!";
        }
    }
}
