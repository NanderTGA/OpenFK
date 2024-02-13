using Microsoft.Win32;
using OpenFK.OFK.Net;
using OpenFK.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace OpenFK.OFK.Core
{
    static class BittyManager
    {
        // ===================================
        // Bitty Manager
        // Handles the Bitty transmission via both MegaByte and customF.
        // ===================================

        private static FileSystemWatcher BittyWatcher;
        private static string BittyID;

        //For MegaByte's config.
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        //FSGUI Focusing
        private const int SW_SHOWNORMAL = 1;
        private const int SW_MINIMIZE = 6;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static void InitBitty()
        {
            //customF Initialization
            if (Settings.Default.customF == true)
            {
                BittyWatcher = new FileSystemWatcher
                {
                    Path = Directory.GetCurrentDirectory(),
                    NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    Filter = "customF.txt",
                    SynchronizingObject = Globals.AS2Container,
                    EnableRaisingEvents = true
                };
                BittyWatcher.Changed += OnFileSystemChanged;

                if (File.Exists("FunkeySelectorGUI.exe"))
                {
                    Process.Start("FunkeySelectorGUI.exe");
                    Thread.Sleep(500);
                    ShowGUI();
                } else
                {
                    MessageBox.Show("FunkeySelectorGUI could not be found! It is possible you may be using an outdated version. If the problem doesn't fix itself within two days, please ask for help on the U.B. Funkeys Discord server or on r/UBFunkeys", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Settings.Default.USBSupport == true)
            {
                // WinForms uses a randomized class name, so we fill in Config.ini with OpenFK's info.
                StringBuilder className = new(256);
                GetClassName(Globals.GameForm.Handle, className, className.Capacity);

                string configFile = @"..\Config.ini";
                string[] configLines = File.ReadAllLines(configFile);
                configLines[11] = @$"ClassName=""{className}""";
                configLines[12] = @$"WindowName=""{Globals.GameForm.Text}""";
                File.WriteAllLines(configFile, configLines);

                var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true);
                if (key == null)
                    throw new InvalidOperationException(@"Cannot open registry key HKCU\SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers.");
                using (key)
                    key.SetValue(Path.GetFullPath(@"..\MegaByte\MegaByte.exe"), "VISTASP2");

                ProcessStartInfo MBData = new()
                {
                    FileName = @"..\MegaByte\MegaByte.exe",
                    Arguments = Globals.IsDebug ? "-MBRun -MBDebug" : "-MBRun",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Minimized
                };
                _ = Process.Start(MBData);
            }
        }

        static void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            try //Runs a loop to keep reading until the file is not being saved.
            {
                SetBitty(File.ReadAllText("customF.txt").Remove(0, 14), false);
            }
            catch
            {
                OnFileSystemChanged(sender, e);
            }
        }

        public static void SetBitty(string localBittyID, bool isMB)
        {
            if (BittyID == localBittyID) return;
            if (isMB)
            {
                string mbBitty = Regex.Replace(localBittyID, @"[^\w\d]", "");
                Globals.GameForm.SetVar(@$"<bitybyte id=""{mbBitty}"" />");
            }
            else
            {
                Globals.GameForm.SetVar(@$"<bitybyte id=""{localBittyID}00000000"" />");
            }
            BittyID = localBittyID;

            RichPresenceManager.CurrentBitty = localBittyID.ToLower();
            if (!Settings.Default.RPC) return;
            try
            {
                XmlNode funkeyXmlNode = RichPresenceManager.BittyData?.SelectSingleNode($"//funkey[@id='{localBittyID}']");
                RichPresenceManager.CurrentBittyName = funkeyXmlNode != null ? funkeyXmlNode.Attributes["name"].Value : "Unknown funkey";
                RichPresenceManager.SetRP(RichPresenceManager.CurrentWorld, RichPresenceManager.CurrentActivity, RichPresenceManager.CurrentBitty, RichPresenceManager.CurrentBittyName);
            }
            catch { }
        }

        public static void ShowGUI() 
        {
            //TODO - There's an issue with opening anything other than the primary form.
            try
            {
                IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, "FunkeySelectorGUI");
                if (!IsIconic(hwnd))
                {
                    ShowWindow(hwnd, SW_MINIMIZE);
                    return;
                }

                ShowWindow(hwnd, SW_SHOWNORMAL);
                int gameFormCenterX = Globals.GameForm.Location.X + 50;
                int gameFormCenterY = Globals.GameForm.Location.Y + 50;
                SetWindowPos(hwnd, IntPtr.Zero, gameFormCenterX, gameFormCenterY, gameFormCenterX, gameFormCenterY, SWP_NOSIZE | SWP_NOZORDER);
            }
            catch {  }
        }
    }
}
