using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
using OpenFK.OFK.Common;
using OpenFK.Properties;

namespace OpenFK
{
    static class Program
    {
        public const string feelFreeToAskForHelp = "If you need help, feel free to ask for help in the U.B. Funkeys Discord server or on the subreddit (preferably the Discord server).";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists("update.bat")) File.Delete("update.bat");

            if (args.Contains("/config"))
            {
                Application.Run(new ConfigForm());
                return;
            }

            if (!File.Exists("Flash.ocx"))
            {
                bool downloadOCX = MessageBox.Show(
                    "Flash.ocx could not be found! Do you want to fetch a compatible OCX? Pressing no will close OpenFK.",
                    "Flash.ocx not found",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error
                ) == DialogResult.Yes;

                if (!downloadOCX) return;

                FetchOCX();
                return;
            }

            string ocxMD5 = CalculateMD5("Flash.ocx");
            if (
                (
                    ocxMD5 == "0c8fbd12f40dcd5a1975b671f9989900" ||
                    ocxMD5 == "28642aa6626e42701677a1f3822306b0"
                ) &&
                MessageBox.Show(
                    "The current Flash.ocx is a buggy version! It causes several problems in the game. Do you want to fetch a compatible OCX?",
                    "OpenFK",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                ) == DialogResult.Yes
            )
            {
                FetchOCX();
                return;
            }
            
            if (!File.Exists("Main.swf"))
            {
                MessageBox.Show(
                    $"Could not find Main.swf! Did you put OpenFK in the right location? {feelFreeToAskForHelp}",
                    "OpenFK",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Application.Exit();
                return;
            }

            try
            {
                Application.Run(new Form1(args));
            }
            catch
            {
                bool? flashOCXIs64Bit = ArchitectureUtils.UnmanagedDllIs64Bit("Flash.ocx");
                string errorInformation = "";

                if (
                    flashOCXIs64Bit != Environment.Is64BitProcess &&
                    MessageBox.Show(
                        $"There was an error starting the game! Our checks have concluded this is happening because an incompatible OCX. Do you want to fetch a compatible OCX?",
                        "OpenFK has crashed!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation
                    ) == DialogResult.Yes
                )
                {
                    FetchOCX();
                    return;
                }

                _ = MessageBox.Show(
                    $"There was an error starting the game!{errorInformation} {feelFreeToAskForHelp}",
                    "OpenFK has crashed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                throw;
            }
        }

        private static string CalculateMD5(string filename)
        {
            using MD5 md5 = MD5.Create();
            using FileStream stream = File.OpenRead(filename);
            byte[] hash = md5.ComputeHash(stream); // Converts the hash to a readable string to compare.
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private static void FetchOCX()
        {
            Directory.CreateDirectory("tempdl");
            string tempdlPath = Path.Combine(Environment.CurrentDirectory, "tempdl");

            using (WebClient client = new())
            {
                client.DownloadFile(
                    "http://download.windowsupdate.com/c/msdownload/update/software/secu/2019/06/windows10.0-kb4503308-x64_b6478017674279c8ba4f06e60fc3bab04ed7ae02.msu",
                    Path.Combine(tempdlPath, "update.msu")
                );
            }

            const string cabFile = "Windows10.0-KB4503308-x64.cab";
            ExtractFilesFromArchive(tempdlPath, "update.msu", cabFile);
            ExtractFilesFromArchive(tempdlPath, cabFile, "flash.ocx");

            string flashOCXPath = Environment.Is64BitProcess ?
                @"tempdl\amd64_adobe-flash-for-windows_31bf3856ad364e35_10.0.18362.172_none_815470a5fb446c4e\flash.ocx" :
                @"tempdl\wow64_adobe-flash-for-windows_31bf3856ad364e35_10.0.18362.172_none_8ba91af82fa52e49\flash.ocx";
            File.Copy(flashOCXPath, "Flash.ocx");

            Directory.Delete("tempdl", true);
            Application.Restart();
        }

        private static void ExtractFilesFromArchive(string workingDirectory, string archive, string files)
        {
            ProcessStartInfo expandProcessStartInfo = new()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true,
                WorkingDirectory = workingDirectory,
                FileName = "expand.exe",
                Arguments = $@"{archive} ""-f:{files}"" ./",
            };

            Process expandProcess = new()
            {
                StartInfo = expandProcessStartInfo,
            };
            expandProcess.Start();
            expandProcess.WaitForExit();
        }
    }
}
