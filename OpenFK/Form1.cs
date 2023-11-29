﻿using AxShockwaveFlashObjects;
using DiscordRPC;
using Microsoft.Win32;
using OpenFK.OFK.Common;
using OpenFK.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace OpenFK
{

    // ===================================
    //  ____                   ______ _  __
    // / __ \                 |  ____| |/ /
    //| |  | |_ __   ___ _ __ | |__  | ' / 
    //| |  | | '_ \ / _ \ '_ \|  __| |  <  
    //| |__| | |_) |  __/ | | | |    | . \ 
    // \____/| .__/ \___|_| |_|_|    |_|\_\
    //       | |                           
    //       |_|                       
    // ===================================

    public partial class Form1 : Form
    {
        //Online Data
        public string Host; //Host
        public string Host1; //Host2
        public string Store; //FilestoreV2 (For updates)
        public string TStore; //Trunk

        public XDocument netStore; //GitHub update.xml
        public XDocument fsnetStore; //GitHub update.xml for FSGUI
        public bool WasUpdated = false; //Determines if the OpenFK update script should run.

        //Rich Presence Data
        public string currentBitty;
        public string currentBittyName;
        public string currentWorld;
        public string currentActivity;

        //Debug Flags
        public bool DebugMB;
        public bool DebugOnline;

        //MegaByte Data
        private System.Windows.Forms.Timer bittyTimer; //Timer to check connected bitty.

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        const int PROCESS_WM_READ = 0x0010;

        public string bittyID; //Current bitty connected.
        public string usbBittyID;


        //Items
        public XmlDocument bittyData;
        public XmlDocument userData;
        public DiscordRpcClient client;
        private FileSystemWatcher watcher;
        private Process FSGUI_process;

        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Contains("/debug")) 
            {
                DebugWindow debug = new();
                debug.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //RP Initialize
            if (Settings.Default.RPC == true)
            {
                client = new DiscordRpcClient("506150783893504001");
                client.Initialize();
                SetRP("Main Menu", "At the main menu", "fffffff0", "U.B.");
            }
            //End of RP Initialize

            DebugOnline = Settings.Default.IsOnline;

            //Flash initialization
            AS2Container.Quality = Settings.Default.Quality;
            AS2Container.Quality2 = "High";
            AS2Container.ScaleMode = Settings.Default.ScaleMode;
            AS2Container.Movie = Directory.GetCurrentDirectory() + @"\Main.swf"; //Sets Main.swf as the Flash Movie to Play.
            AS2Container.Play(); //Plays Main.swf
            LogManager.LogGeneral("[AS2Container] Main.swf is Loaded");
            AS2Container.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashPlayer_FSCommand); //This sets up the FSCommand handler, which CCommunicator likes to use a lot.

            try
            {
                AS3Container.Quality = Settings.Default.Quality;
                AS3Container.Quality2 = "High";
                AS3Container.ScaleMode = Settings.Default.ScaleMode;
                AS3Container.Movie = Directory.GetCurrentDirectory() + @"\MainAS3.swf"; //Sets MainAS3.swf as the Flash Movie to Play.
                LogManager.LogGeneral("[AS3Container] MainAS3.swf is Loaded");
                AS3Container.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashPlayerAS3_FSCommand);
                AS3Container.FlashCall += new _IShockwaveFlashEvents_FlashCallEventHandler(FlashPlayerAS3_FlashCall);
            }
            catch
            {
                LogManager.LogGeneral("[AS3Container] AS3 Failed to Load! Potentially an older version.");
            }
            //End of Flash initialization

            //customF Initialization
            if (Settings.Default.customF == true) //If using no USB
            {
                watcher = new FileSystemWatcher
                {
                    Path = Directory.GetCurrentDirectory(),
                    NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    Filter = "customF.txt"
                };
                watcher.Changed += OnChanged;
                watcher.SynchronizingObject = AS2Container;
                watcher.EnableRaisingEvents = true;

                if (Settings.Default.startFSGUI == true) StartFSGUI();
            }
            //End of customF Initialization

            //USB Initialization

            if (Settings.Default.USBSupport == true)
            {
                var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true);
                if (key == null)
                    throw new InvalidOperationException(@"Cannot open registry key HKCU\SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers.");
                using (key)
                    key.SetValue(Directory.GetParent(Directory.GetCurrentDirectory()) + @"\MegaByte\" + "MegaByte.exe", "VISTASP2");
                Process MBRun = new();
                ProcessStartInfo MBData = new()
                {
                    FileName = Directory.GetParent(Directory.GetCurrentDirectory()) + @"\MegaByte\" + "MegaByte.exe",
                    Arguments = "-MBRun -MBDebug",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Minimized
                };
                MBRun.StartInfo = MBData;
                MBRun.Start();
                InitTimer();
            }

            //End of USB Initialization
            TopMost = true;
            TopMost = false;
        }

        private void FlashPlayerAS3_FSCommand(object sender, _IShockwaveFlashEvents_FSCommandEvent e)
        {
            LogManager.LogIncoming($"[AS3] [{e.command}] {e.args}");
            if(e.args.Contains("<save_jpeg ")) //Saving jpegs for UG game thumbnails or game over backgrounds.
            {
                XmlDocument request = new(); //e.args to xml
                request.LoadXml(e.args);
                XmlNodeList xnList = request.SelectNodes("/commands/save_jpeg"); //filters xml to the load info
                foreach (XmlNode xn in xnList) //fetches the information to load
                {
                    //Saves Base64 input as a JPEG.
                    string jpegBase64 = xn.Attributes["str"].Value;
                    string filePath = xn.Attributes["name"].Value;
                    var bytes = Convert.FromBase64String(jpegBase64);
                    Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + @"\" + filePath));
                    using (var jpegToSave = new FileStream(Directory.GetCurrentDirectory() + @"\" + filePath, FileMode.Create))
                    {
                        jpegToSave.Write(bytes, 0, bytes.Length);
                        jpegToSave.Flush();
                    }
                    AS3Container.CallFunction(@"<invoke name=""WrapperCall"" returntype=""xml""><arguments><string>save_jpeg</string><string>0</string><string></string></arguments></invoke>"); //Gives result to game.
                }
            }

            if (e.args.Contains("<as3_transit"))
            {
                SetVar(e.args); //Sends the AS3 command to AS2.
            }
        }

        //
        //USB READING
        //
        public void InitTimer()
        {
            bittyTimer = new System.Windows.Forms.Timer();
            bittyTimer.Tick += new EventHandler(BittyT_Tick);
            bittyTimer.Interval = 1000;
            bittyTimer.Start();
        }

        private void BittyT_Tick(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.GetProcessesByName("MegaByte")[0]; //Get the process
                int bytesRead = 0;
                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                byte[] buffer = new byte[4]; //BittyID is 4 bytes

                //This reads the memory to fetch the current bittyID
                ReadProcessMemory((int)processHandle, 0x0049F020, buffer, buffer.Length, ref bytesRead); //With address
                Int32 baseValue = BitConverter.ToInt32(buffer, 0);

                Int32 firstAddress = baseValue + 0xF88; //With pointer
                ReadProcessMemory((int)processHandle, firstAddress, buffer, buffer.Length, ref bytesRead);
                Int32 firstValue = BitConverter.ToInt32(buffer, 0);

                ReadProcessMemory((int)processHandle, firstValue, buffer, buffer.Length, ref bytesRead);

                //Converts bytes to BittyID
                int bittyIDInt = BitConverter.ToInt32(buffer, 0);
                string s = bittyIDInt.ToString("X").PadLeft(8, '0');
                if (s == "00000000" || s == "3F3F3F3F") //If no bitty is connected.
                {
                    s = "FFFFFFF0";
                }
                if (s != usbBittyID) //If it's still the same, it won't repeat the actions
                {
                    usbBittyID = s;
                    LogManager.LogGeneral("[Bitty] USB bitty - " + s);
                    SetBitty(s);
                }
            }
            catch
            {
                
            }
        }
        //
        //END OF USB READING
        //


        private void FlashPlayerAS3_FlashCall(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
        {
            LogManager.LogIncoming("[AS3] [FlashCall] " + e.request);
            if(e.request.Contains("<as3_loaded "))
            {
                SetVar(@"<?xml version=""1.0"" encoding=""UTF - 8""?><commands><as3_loaded id=""1"" path=""MainAS3.swf"" result=""0"" err="""" /></commands>");
            }
        }

        //
        //CUSTOMF
        //
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try //Runs a loop to keep reading until the file is not being saved.
            {
                SetBitty(File.ReadAllText(Directory.GetCurrentDirectory() + @"\customF.txt").Remove(0, 14));
            }
            catch
            {
                OnChanged(sender, e);
            }
        }

        //
        //FSCOMMAND HANDLER
        //

        void FlashPlayer_FSCommand(object sender, _IShockwaveFlashEvents_FSCommandEvent e) //FSCommand Handler
        {
            // We put these here because these use a different xml scheme and to prevent clutter in the general logs.
            // It is also important to return, since only we call this and know to not put anything else in it to prevent bugs due to bad code below.

            if (e.command != "SendMsg")
            {
                Debug.WriteLine("WARNING: unhandled FSCommand");
                LogManager.LogIncoming($"[AS2] [UNHANDLED] [{e.command}] {e.args}");
                return;
            }

            if (e.args.Contains("<log")) // logs from modified CLogger
            {
                var log = new XmlDocument();
                log.LoadXml(e.args);
                var node = log.SelectSingleNode("/log");

                string level = node.Attributes["type"].Value;
                string message = node.InnerText;
                LogManager.LogLog(message, level);
                return;
            }
            else if (e.args.Contains("<staticstorage"))
            {
                var log = new XmlDocument();
                log.LoadXml(e.args);
                var node = log.SelectSingleNode("/commands/staticstorage");

                string type = node.Attributes["type"].Value;
                string key = WebUtility.UrlDecode(node.Attributes["key"].Value);

                switch (type)
                {
                    case "get":
                        string value = WebUtility.UrlDecode(node.Attributes["value"].Value);
                        string defaultValue = WebUtility.UrlDecode(node.Attributes["defaultvalue"].Value);
                        LogManager.LogStaticStorageGet(key, value, defaultValue);
                        break;

                    case "set":
                        string oldValue = WebUtility.UrlDecode(node.Attributes["original"].Value);
                        string newValue = WebUtility.UrlDecode(node.Attributes["value"].Value); ;
                        LogManager.LogStaticStorageSet(key, oldValue, newValue);
                        break;

                    case "delete":
                        string original = WebUtility.UrlDecode(node.Attributes["original"].Value);
                        LogManager.LogStaticStorageDelete(key, original);
                        break;

                }
                return;
            }
            else
            {   // Don't log incoming message that are logs to prevent clutter.
                LogManager.LogIncoming($"[AS2] [{e.command}] {e.args}");
            }

            //
            // XML LOAD COMMANDS
            //

            if (e.args.Contains("<load ")) //load
            {
                //XML PARSING
                string filename; //section
                string foldername; //name
                XmlDocument request = new(); //e.args to xml
                request.LoadXml(e.args);
                XmlNodeList xnList = request.SelectNodes("/commands/load"); //filters xml to the load info
                foreach (XmlNode xn in xnList) //fetches the information to load
                {
                    //XML LOADING
                    filename = xn.Attributes["section"].Value;
                    foldername = xn.Attributes["name"].Value;

                    LogManager.LogFile($"[Load] {foldername}/{filename}");
                    LoadFile(filename, foldername);

                    //Rich Prescense
                    if (Settings.Default.RPC == true)
                    {
                        if (e.args.Contains(@"=""city"""))
                        {
                            SetRP("Exploring", "Funkeystown", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""lava"""))
                        {
                            SetRP("Exploring", "Magma Gorge", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""space"""))
                        {
                            SetRP("Exploring", "Laputta Station", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""underwater"""))
                        {
                            SetRP("Exploring", "Kelpy Basin", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""island"""))
                        {
                            SetRP("Exploring", "Funkiki Island", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""racer"""))
                        {
                            SetRP("Exploring", "Royalton Racing Complex", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""night"""))
                        {
                            SetRP("Exploring", "Nightmare Rift", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""day"""))
                        {
                            SetRP("Exploring", "Daydream Oasis", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""realm"""))
                        {
                            SetRP("Exploring", "Hidden Realm", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""ssl"""))
                        {
                            SetRP("Exploring", "Angus Manor", currentBitty, currentBittyName);
                        }
                        else if (e.args.Contains(@"=""green"""))
                        {
                            SetRP("Exploring", "Paradox Green", currentBitty, currentBittyName);
                        }
                    }
                }
            }

            //
            // END OF XML LOAD COMMANDS
            //

            //
            //LOADED
            //
            if(e.args.Contains("<loaded ")){
                //BityByte
                if (Settings.Default.customF == true) //If using no USB
                {
                    SetVar(@"<bitybyte id=""FFFFFFF000000000"" />");
                }
            }
            
            //
            //END OF LOADED
            //

            //
            // XML SAVE COMMANDS
            //

            if (e.args.Contains("<save "))
            {
                //XML PARSING

                string filename; //section
                string foldername; //name
                XmlDocument request = new(); //e.args to xml
                request.LoadXml(e.args);
                XmlNodeList xnList = request.SelectNodes("/commands/save"); //filters xml to the load info;
                foreach (XmlNode xn in xnList) //fetches the information to load
                {
                    filename = xn.Attributes["section"].Value;
                    foldername = xn.Attributes["name"].Value;

                    LogManager.LogFile($"[Save] {foldername}/{filename}");

                    XDocument args = XDocument.Parse(e.args);

                    var save = args.Root.Element("save"); //Removing save element
                    save.Remove();
                    args.Root.Add(save.Elements());

                    XElement firstChild = args.Root.Elements().First(); //Removing commands element
                    XDocument output = new(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        firstChild
                    );

                    if(!Directory.Exists(Directory.GetCurrentDirectory() + @"\data\" + foldername))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\data\" + foldername);
                    }
                    if (Settings.Default.RDF == true)
                    {
                        Encoding iso_8859_1 = Encoding.GetEncoding("iso-8859-1");
                        byte[] RDFData = iso_8859_1.GetBytes(output.ToString());
                        File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\data\" + foldername + @"\" + filename + ".rdf", iso_8859_1.GetBytes(RDFTool.encode(iso_8859_1.GetString(RDFData))));
                    }
                    else File.WriteAllText(Directory.GetCurrentDirectory() + @"\data\" + foldername + @"\" + filename + ".xml", output.ToString()); //saves
                    LogManager.LogFile($"[Save] [Success] {foldername}/{filename}");
                }
            }

            //
            // END OF XML SAVE COMMANDS
            //

            //
            // AS3 COMMANDS
            //

            if (e.args.Contains("<as3_load "))
            {
                AS3Container.Play();
                AS2Container.SendToBack(); //Goes to AS3 container.
                SetVar(@"<getstaticdata />");
                SetVar(@"<getgamedata />");
                SetVar(@"<getugsettings />");
            }

            if (e.args.Contains("<as3_close"))
            {
                SetVar(@"<leavegame />"); //Tells the AS3 game to end.
                AS3Container.SendToBack(); //Returns to AS2 container.
                AS3Container.Stop();
            }

            if(e.args.Contains("<as3_setcurrentid "))
            {
                //The game unescapes this string. "<commands><setid id="0" /></commands>" is the string. Unsure if it does anything, but it does not give a failure.
                AS3Container.CallFunction(@"<invoke name=""WrapperCall"" returntype=""xml""><arguments><string>setid</string><string>%3c%63%6f%6d%6d%61%6e%64%73%3e%3c%73%65%74%69%64%20%69%64%3d%22%30%22%20%2f%3e%3c%2f%63%6f%6d%6d%61%6e%64%73%3e</string><string>%3c%63%6f%6d%6d%61%6e%64%73%3e%3c%73%65%74%69%64%20%69%64%3d%22%30%22%20%2f%3e%3c%2f%63%6f%6d%6d%61%6e%64%73%3e</string></arguments></invoke>");
                AS3Container.SendToBack();
                Directory.Delete(Directory.GetCurrentDirectory() + @"\misc\tmp\", true); //Deletes the temporary folder used for the results.
            }

            if (e.args.Contains("<del_file ")) //Deletes files, only for UG thumnails.
            {
                XmlDocument request = new(); //e.args to xml
                request.LoadXml(e.args);
                XmlNodeList xnList = request.SelectNodes("/commands/save_jpeg"); //filters xml to the load info
                foreach (XmlNode xn in xnList) //fetches the information to load
                {
                    //XML LOADING
                    string fileToDelete = xn.Attributes["path"].Value;
                    File.Delete(Directory.GetCurrentDirectory() + @"\" + fileToDelete);
                }
            }

            //
            // EMD OF AS3 COMMANDS
            //

            //
            // CLOSE COMMAND
            //

            if (e.args.Contains("radicaclose")) //Exit
            {
                LogManager.LogGeneral("[OpenFK] Radicaclose was called");

                if (Settings.Default.RPC == true)
                {
                    client.Dispose(); //Disposes RP
                }

                if(Settings.Default.USBSupport == true)
                {
                    try
                    {
                        Process process = Process.GetProcessesByName("MegaByte")[0];
                        process.Kill();
                    }
                    catch { }
                }

                if (Settings.Default.customF && Settings.Default.closeFSGUI && FSGUI_process != null)
                {
                    FSGUI_process.EnableRaisingEvents = false;
                    FSGUI_process.Kill();
                }

                if(WasUpdated == true) //If the game was updated. I don't know why it doesn't use a special command, but fine I guess...
                {
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\update.bat", Resources.Update);
                    ProcessStartInfo updatescript = new(Directory.GetCurrentDirectory() + @"\update.bat");
                    updatescript.UseShellExecute = true;
                    _ = Process.Start(updatescript);
                }

                if (WasUpdated && fsnetStore != null) Application.Restart();
                else Application.Exit();
            }

            //
            //END OF CLOSE COMMAND
            //

            //
            //FULLSCREEN COMMAND
            //

            if (e.args.Contains("fullscreen")) //Fullscreen
            {

                //PARSE OF XML DATA

                XmlDocument doc = new();
                doc.LoadXml(e.args.ToString());
                var nodeList = doc.SelectNodes("commands");
                string commandLimited = "";
                foreach (XmlNode node in nodeList)
                {
                    commandLimited = node.InnerXml;
                }
                XmlDocument doc2 = new();
                doc2.LoadXml(commandLimited);
                XmlElement root = doc2.DocumentElement;
                string name = root.GetAttribute("state"); //Fullscreen status

                //AFTER PARSE

                if (name == "1") //Fullscreen Mode
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                }
                else if (name == "0") //Window Mode
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                }
            }

            //
            //END OF FULLSCREEN COMMAND
            //

            //
            //HTTP NETCOMMANDS
            //

            //HTTP POST (CRIB SAVING + POSTCARDS)
            if (e.args.Contains("<netcommands"))
            {
                LogManager.LogNetwork("Netcommand called.", "NetCommand");

                string tnurl = "";
                if(e.args.Contains("<save_level "))
                {
                    XmlDocument request = new(); //e.args to xml
                    request.LoadXml(e.args);
                    XmlNodeList xnList = request.SelectNodes("/netcommands/save_level"); //filters xml to the load info
                    foreach (XmlNode xn in xnList)
                    {
                        tnurl = xn.Attributes["tnurl"].Value;
                    }

                    using WebClient client = new();
                    client.UploadFile(Host + "/" + tnurl, "PUT", Directory.GetCurrentDirectory() + @"\" + tnurl);
                }

                if (DebugOnline == true)
                {
                    AS2Container.SetVariable("msg", HTTPPost(e.args, Host).ToString()); //Sends the result of the POST request. It's usually a command for the game to handle.
                }
            }

            //TRUNK UPDATE CHECKS
            if (e.args.Contains("<commands><checktrunkupdate"))
            {
                SetVar(@"<checktrunkupdate result=""0"" reason=""Everything is up to date."" />");
            }

            //UPDATE CHECKS (Not standard netcommands)
            if (e.args.Contains("<commands><checkupdate"))
            {
                string localVersion = "";
                string localVerNum = "1.8";
                string fslocalVersion = "";
                string fslocalVerNum = "1.0";
                LogManager.LogNetwork("[Update] Update Requested", "NetCommand");
                SetVar(@"<progress percent=""0.25"" />");
                try
                {
                    var localStore = XDocument.Load(Directory.GetCurrentDirectory() + @"\update.xml");
                    localVersion = localStore.Root.Attribute("name").Value;
                    localVerNum = localStore.Root.Attribute("version").Value;
                }
                catch
                {
                    LogManager.LogNetwork("[Update] Update.xml was not found", "NetCommand");
                }
                SetVar(@"<progress percent=""25.00"" />");
                try
                {
                    LogManager.LogNetwork("[Update] Downloading Update.xml from GitHub", "NetCommand");
                    netStore = XDocument.Parse(Get(@"https://raw.githubusercontent.com/GittyMac/OpenFK/master/update.xml"));
                    LogManager.LogNetwork("[Update] Update.xml was downloaded", "NetCommand");
                    string netVersion = netStore.Root.Attribute("name").Value;
                    string netVersionNum = netStore.Root.Attribute("version").Value;
                    string netVersionSize = netStore.Root.Attribute("size").Value;
                    SetVar(@"<progress percent=""50.00"" />");
                    if (localVersion != netVersion)
                    {
                        LogManager.LogNetwork("[Update] An update is needed", "NetCommand");
                        netStore.Save(Directory.GetCurrentDirectory() + @"\update.xml");
                        SetVar(@"<checkupdate result=""2"" reason=""New version of OpenFK found."" version=""2009_07_16_544"" size=""" + netVersionSize + @""" curversion=""" + localVerNum + @""" extversion=""" + netVersionNum + @""" extname=""" + netVersion + @""" />");
                    }
                    else if(File.Exists(Directory.GetCurrentDirectory() + @"\FunkeySelectorGUI.exe"))
                    {
                        try
                        {
                            var localStore = XDocument.Load(Directory.GetCurrentDirectory() + @"\fsguiupdate.xml");
                            fslocalVersion = localStore.Root.Attribute("name").Value;
                            fslocalVerNum = localStore.Root.Attribute("version").Value;
                        }
                        catch
                        {
                            LogManager.LogNetwork("[Update] FSGUI Update.xml was not found", "NetCommand");
                        }
                        SetVar(@"<progress percent=""75.00"" />");
                        try
                        {
                            LogManager.LogNetwork("[Update] Downloading FSGUI Update.xml from GitHub", "NetCommand");
                            fsnetStore = XDocument.Parse(Get(@"https://raw.githubusercontent.com/GittyMac/FunkeySelectorGUI/master/update.xml"));
                            LogManager.LogNetwork("[Update] FSGUI Update.xml was downloaded", "NetCommand");
                            string fsnetVersion = fsnetStore.Root.Attribute("name").Value;
                            string fsnetVersionNum = fsnetStore.Root.Attribute("version").Value;
                            string fsnetVersionSize = fsnetStore.Root.Attribute("size").Value;
                            SetVar(@"<progress percent=""90.00"" />");
                            if (fslocalVersion != fsnetVersion)
                            {
                                try
                                {
                                    Process process = Process.GetProcessesByName("FunkeySelectorGUI")[0];
                                    process.Kill();
                                }
                                catch
                                {
                                    LogManager.LogNetwork("[Update] Cannot close FSGUI", "NetCommand");
                                }
                                LogManager.LogNetwork("[Update] A FSGUI update is needed", "NetCommand");
                                SetVar(@"<checkupdate result=""2"" reason=""New version of FSGUI found."" version=""2009_07_16_544"" size=""" + fsnetVersionSize + @""" curversion=""" + fslocalVerNum + @""" extversion=""" + fsnetVersionNum + @""" extname=""" + fsnetVersion + @""" />");
                            }
                            else
                            {
                                SetVar(@"<checkupdate result=""0"" reason=""Everything is up to date."" />");
                            }
                        }
                        catch
                        {
                            LogManager.LogNetwork("[Update] No FSGUI update", "NetCommand");
                            SetVar(@"<checkupdate result=""1"" reason=""Could not find the FunkeySelectorGUI update!"" />");
                        }
                    }
                    else
                    {
                        SetVar(@"<checkupdate result=""0"" reason=""Everything is up to date."" />");
                    }
                }
                catch
                {
                    LogManager.LogNetwork("[Update] No update", "NetCommand");
                    SetVar(@"<checkupdate result=""1"" reason=""Could not find the OpenFK update!"" />");
                }
            }

            if(e.args.Contains(@"<loadupdate "))
            {
                try
                {
                    if (fsnetStore != null)
                    {
                        string fsnetDL = fsnetStore.Root.Attribute("url").Value;
                        using (WebClient client = new())
                        {
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            client.DownloadFile(fsnetDL, Directory.GetCurrentDirectory() + @"\FunkeySelectorGUI.exe");
                        }
                        fsnetStore.Save(Directory.GetCurrentDirectory() + @"\fsguiupdate.xml");
                        SetVar(@"<loadupdate result=""0"" reason=""good"" />");
                    }
                    else
                    {
                        string netDL = "";
                        if (Environment.Is64BitProcess)
                        {
                            netDL = netStore.Root.Attribute("url64").Value;
                        }
                        else
                        {
                            netDL = netStore.Root.Attribute("url32").Value;
                        }

                        using (var client = new WebClient())
                        {
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            client.DownloadFile(netDL, Directory.GetCurrentDirectory() + @"\tmpdl.zip");
                        }
                        netStore.Save(Directory.GetCurrentDirectory() + @"\update.xml");
                        Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + @"\tmpdl\"));
                        System.IO.Compression.ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + @"\tmpdl.zip", Directory.GetCurrentDirectory() + @"\tmpdl\");
                        SetVar(@"<loadupdate result=""0"" reason=""good"" />");
                        WasUpdated = true;
                    }
                }
                catch
                {
                    SetVar(@"<loadupdate result=""1"" reason=""The update has failed! Try restarting OpenFK..."" />");
                }
            }

            //
            //END OF HTTP NETCOMMANDS
            //

            //
            // XML LOAD COMMANDS
            //

            if (e.args.Contains("<createuser ")) //load
            {
                //XML PARSING
                string username;
                string password;
                string savepassword;
                string hinta;
                string hintq;
                XmlDocument request = new(); //e.args to xml
                request.LoadXml(e.args);
                XmlNodeList xnList = request.SelectNodes("/commands/createuser"); //filters xml to the load info
                foreach (XmlNode xn in xnList) //fetches the information to load
                {
                    //XML LOADING
                    username = xn.Attributes["name"].Value;
                    password = xn.Attributes["password"].Value;
                    savepassword = xn.Attributes["savepassword"].Value;
                    hintq = xn.Attributes["hintq"].Value;
                    hinta = xn.Attributes["hinta"].Value;
                    LogManager.LogFile("[Load] File Requested - system/users");
                    LoadFile("users", "system");
                    string userString = userData.OuterXml;
                    string data2send = userString.Replace("</users>", "") + @"<user gname=""" + username + @""" hinta=""" + hinta + @""" hintq=""" + hintq + @""" savepassword=""" + savepassword + @""" password=""" + password + @""" name=""" + username + @""" /></users>";
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\data\" + "system"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\data\" + "system");
                    }
                    if (Settings.Default.RDF == true)
                    {
                        Encoding iso_8859_1 = Encoding.GetEncoding("iso-8859-1");
                        byte[] RDFData = iso_8859_1.GetBytes(data2send.ToString());
                        File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\data\" + "system" + @"\" + "users" + ".rdf", iso_8859_1.GetBytes(RDFTool.encode(iso_8859_1.GetString(RDFData))));
                    }
                    else File.WriteAllText(Directory.GetCurrentDirectory() + @"\data\" + "system" + @"\" + "users" + ".xml", data2send.ToString()); //saves
                    LogManager.LogFile("[UserAdd] [Succes] " + username);
                }
            }

            //
            // END OF XML LOAD COMMANDS
            //

            //
            //FSGUI
            //

            if (e.args.Contains("<fsgui ") && Settings.Default.startFSGUI == false)
            {
                StartFSGUI();
            }

            //
            //END OF FSGUI
            //
        }

        //
        //END OF FSCOMMAND HANDLER
        //

        //
        //CLOSE BUTTON
        //

        void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (e.CloseReason == CloseReason.UserClosing);
            if (e.CloseReason == CloseReason.UserClosing) //If user clicked the close button
            {
                SetVar(@"<radicaclose />");
            }
            if (e.CloseReason == CloseReason.WindowsShutDown) //If windows is shutting down
            {
                if (Settings.Default.RPC == true)
                {
                    client.Dispose(); //Disposes RP
                }
                if (Settings.Default.USBSupport == true)
                {
                    Process process = Process.GetProcessesByName("MegaByte")[0];
                    process.Kill();
                }
                if (Settings.Default.customF && Settings.Default.closeFSGUI && FSGUI_process != null)
                {
                    FSGUI_process.EnableRaisingEvents = false;
                    FSGUI_process.Kill();
                }
                Application.Exit(); //Closes OpenFK
            }
        }

        //
        //END OF CLOSE BUTTON
        //

        //
        //FILE LOADING
        //

        public void LoadFile(string file, string folder)
        {
            Encoding iso_8859_1 = Encoding.GetEncoding("iso-8859-1");
            string index;
            string filedata;
            try
            {
                if (Settings.Default.RDF == true)
                {
                    byte[] RDFData = File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\data\" + folder + @"\" + file + ".rdf");
                    filedata = RDFTool.decode(iso_8859_1.GetString(RDFData));
                }
                else filedata = File.ReadAllText(Directory.GetCurrentDirectory() + @"\data\" + folder + @"\" + file + ".xml"); //Puts XML file to string
                index = @"<commands><load section=""" + file + @""" name=""" + folder + @""" result=""0"" reason="""">" + filedata + @"</load></commands>";


                if (file == "funkeys")
                {
                    bittyData = new XmlDocument();
                    bittyData.LoadXml(filedata);
                }

                if (file == "users")
                {
                    userData = new XmlDocument();
                    userData.LoadXml(filedata);
                }

                if (file == "config")
                {
                    XmlDocument configData = new();
                    configData.LoadXml(filedata);

                    if (Settings.Default.IsOnline)
                    {
                        XmlAttribute host = (XmlAttribute)configData.SelectSingleNode("/settings/host/@value");
                        host.Value = Settings.Default.HTTPHost1;

                        XmlAttribute host1 = (XmlAttribute)configData.SelectSingleNode("/settings/host1/@value");
                        host1.Value = Settings.Default.HTTPHost2;

                        XmlAttribute tcpHost = (XmlAttribute)configData.SelectSingleNode("/settings/arkone_host/@value");
                        tcpHost.Value = Settings.Default.TCPHost;

                        XmlAttribute tcpPort = (XmlAttribute)configData.SelectSingleNode("/settings/arkone_port/@value");
                        tcpPort.Value = Settings.Default.TCPPort;

                        filedata = configData.OuterXml;
                        index = @"<commands><load section=""" + file + @""" name=""" + folder + @""" result=""0"" reason="""">" + filedata + @"</load></commands>";
                    }

                    XmlNodeList xnList1 = configData.SelectNodes("/settings/host"); //filters xml to the load info;
                    foreach (XmlNode xn in xnList1) //fetches the information to load
                    {
                        Host = xn.Attributes["value"].Value;
                    }

                    XmlNodeList xnList2 = configData.SelectNodes("/settings/host1"); //filters xml to the load info;
                    foreach (XmlNode xn in xnList2) //fetches the information to load
                    {
                        Host1 = xn.Attributes["value"].Value;
                    }

                    XmlNodeList xnList3 = configData.SelectNodes("/settings/store"); //filters xml to the load info;
                    foreach (XmlNode xn in xnList3) //fetches the information to load
                    {
                        Store = xn.Attributes["value"].Value;
                    }

                    XmlNodeList xnList4 = configData.SelectNodes("/settings/trunkstore"); //filters xml to the load info;
                    foreach (XmlNode xn in xnList4) //fetches the information to load
                    {
                        TStore = xn.Attributes["value"].Value;
                    }
                }

            }
            catch
            {
                index = @"<commands><load section=""" + file + @""" name=""" + folder + @""" result=""1"" reason=""Error loading file!"" /></commands>"; //I would just let dotNET handle this, but UGLevels needs an error to continue.
            }
            SetVar(index.ToString()); //Sends XML data to the game
            LogManager.LogFile($"[Load] [Success] {folder}/{file}");
        }

        //
        //END OF FILE LOADING
        //

        //
        //SET FLASH VARIABLE
        //

        public void SetVar(string msg)
        {
            LogManager.LogOutgoing("[SetVar/Return] Returned Message - " + msg);
            AS2Container.SetVariable("msg", msg); //Sends message (msg) to the game
        }

        //
        //END OF SET FLASH VARIABLE
        //

        //
        //RICH PRESENCE
        //

        void SetRP(string title, string info, string bittyID, string bittyName)
        {
            currentWorld = title;
            currentActivity = info;
            currentBitty = bittyID;
            currentBittyName = bittyName;
            client.SetPresence(new RichPresence()
            {
                Details = info,
                State = title,
                Assets = new Assets()
                {
                    LargeImageKey = bittyID,
                    LargeImageText = bittyName
                }
            });
        }

        //
        //END OF RICH PRESENCE
        //

        //
        //SET BITTY
        //

        void SetBitty(string localBittyID)
        {
            if (bittyID == localBittyID) return;

            SetVar(@$"<bitybyte id=""{localBittyID}00000000"" />");
            bittyID = localBittyID;
            currentBitty = localBittyID.ToLower();

            if (!Settings.Default.RPC) return;

            XmlNode xn = bittyData.SelectSingleNode($"//funkey[@id='{localBittyID}']");
            if (xn == null) return;

            currentBittyName = xn.Attributes["name"].Value;
            SetRP(currentWorld, currentActivity, currentBitty, currentBittyName);
        }

        //
        //END OF SET BITTY
        //

        //
        //POST REQUESTS
        //
        public string HTTPPost(string info, string uri)
        {
            LogManager.LogNetwork($"{uri} {info}", "POST");

            var request = (HttpWebRequest)WebRequest.Create(uri);
            var data = Encoding.ASCII.GetBytes(info);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            string tnurl = "";

            if(responseString.Contains("<get_level "))
            {
                XmlDocument xRequest = new(); //e.args to xml
                xRequest.LoadXml(responseString);
                XmlNodeList xnList = xRequest.SelectNodes("/get_level/level"); //filters xml to the load info
                foreach (XmlNode xn in xnList)
                {
                    if(xn.Attributes.GetNamedItem("tnurl") != null) 
                    {
                        tnurl = xn.Attributes["tnurl"].Value;
                    }
                }

                if (tnurl != "")
                {
                    using WebClient client = new();
                    client.DownloadFile(Host + "/" + tnurl, Directory.GetCurrentDirectory() + @"\" + tnurl);
                }
            }
            else if (responseString.Contains("<get_top "))
            {
                XmlDocument xRequest = new(); //e.args to xml
                xRequest.LoadXml(responseString);
                XmlNodeList xnList = xRequest.SelectNodes("/get_top/levels/level"); //filters xml to the load info
                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes.GetNamedItem("tnurl") != null)
                    {
                        tnurl = xn.Attributes["tnurl"].Value;
                    }
                    if (tnurl != "")
                    {
                        using WebClient client = new();
                        client.DownloadFile(Host + "/" + tnurl, Directory.GetCurrentDirectory() + @"\" + tnurl);
                    }
                }
            }
            else if (responseString.Contains("<get_sh_levels "))
            {
                XmlDocument xRequest = new(); //e.args to xml
                xRequest.LoadXml(responseString);
                XmlNodeList xnList = xRequest.SelectNodes("/get_sh_levels/levels/level"); //filters xml to the load info
                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes.GetNamedItem("tnurl") != null)
                    {
                        tnurl = xn.Attributes["tnurl"].Value;
                    }
                    if(tnurl != "") 
                    {
                        using WebClient client = new();
                        client.DownloadFile(Host + "/" + tnurl, Directory.GetCurrentDirectory() + @"\" + tnurl);
                    }
                }
            }

            return responseString;
        }
        //
        //END OF POST REQUESTS
        //

        //
        //HTTP GET
        //
        public string Get(string uri)
        {
            LogManager.LogNetwork(uri, "GET");

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
        //
        //END OF HTTP GET
        //

        public void StartFSGUI(object sender = null, EventArgs e = null)
        {
            ProcessStartInfo FSGUI_processStartInfo = new()
            {
                FileName = Directory.GetCurrentDirectory() + @"\FunkeySelectorGUI.exe",
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Minimized // this doesn't work for whatever reason
            };

            FSGUI_process = new Process
            {
                StartInfo = FSGUI_processStartInfo
            };

            if (Settings.Default.keepFSGUI)
            {
                FSGUI_process.EnableRaisingEvents = true;
                FSGUI_process.Exited += StartFSGUI;
            }

            FSGUI_process.Start();
        }
    }
}
