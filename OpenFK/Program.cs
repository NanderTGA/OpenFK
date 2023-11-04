using OpenFK.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace OpenFK
{
    static class Program
    {
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
                    "Flash.ocx could not be found! Do you want to fetch a compatible OCX? Pressing no will close openFK.",
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
            
            try
            {
                Application.Run(new Form1(args));
            }
            catch
            {
                _ = MessageBox.Show(
                    "There was an error starting the game! This could happen because of a 64 bit OCX running on a 32 bit OpenFK. This could also happen for any other reason.",
                    "OpenFK",
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
            File.WriteAllText("FetchOCX.bat", Resources.FetchOCX);

            ProcessStartInfo fetchOCXProcessStartInfo = new ProcessStartInfo("FetchOCX.bat")
            {
                UseShellExecute = false
            };
            Process fetchOCXProcess = Process.Start(fetchOCXProcessStartInfo);
            fetchOCXProcess.WaitForExit();

            File.Delete("FetchOCX.bat");
            Application.Restart();
        }
    }
}
