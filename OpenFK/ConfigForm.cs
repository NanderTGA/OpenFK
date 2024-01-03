using System;
using System.Windows.Forms;
using OpenFK.Properties;

namespace OpenFK
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            CustomFtoggle.Checked = Settings.Default.customF;
            RPCToggle.Checked = Settings.Default.RPC;
            RDFToggle.Checked = Settings.Default.RDF;
            QualityCB.SelectedIndex = Settings.Default.Quality;
            ScaleCB.SelectedIndex = Settings.Default.ScaleMode;
            USBToggle.Checked = Settings.Default.USBSupport;
            OnlineToggle.Checked = Settings.Default.IsOnline;
            HTTPBox1.Text = Settings.Default.HTTPHost1;
            HTTPBox2.Text = Settings.Default.HTTPHost2;
            TCPHostBox.Text = Settings.Default.TCPHost;
            TCPPortBox.Text = Settings.Default.TCPPort;
            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            label8.Text = "OpenFK v" + currentVersion.Substring(0, currentVersion.LastIndexOf("."));
            UpdateTextboxes();
        }

        private void QualityCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Quality = QualityCB.SelectedIndex;
            Settings.Default.Save();
        }

        private void ScaleCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.ScaleMode = ScaleCB.SelectedIndex;
            Settings.Default.Save();
        }

        private void CustomFtoggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.customF = CustomFtoggle.Checked;
            Settings.Default.Save();
        }

        private void RPCToggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.RPC = RPCToggle.Checked;
            Settings.Default.Save();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Settings.Default.HTTPHost1 = HTTPBox1.Text;
            Settings.Default.HTTPHost2 = HTTPBox2.Text;
            Settings.Default.TCPHost = TCPHostBox.Text;
            Settings.Default.TCPPort = TCPPortBox.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void RDFToggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.RDF = RDFToggle.Checked;
            Settings.Default.Save();
        }

        private void USBToggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.USBSupport = USBToggle.Checked;
            Settings.Default.Save();
        }

        private void OnlineToggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsOnline = OnlineToggle.Checked;
            Settings.Default.Save();
            UpdateTextboxes();
        }

        void UpdateTextboxes()
        {
            if (Settings.Default.IsOnline)
            {
                HTTPBox1.Enabled = true;
                HTTPBox2.Enabled = true;
                TCPHostBox.Enabled = true;
                TCPPortBox.Enabled = true;
            }
            else
            {
                HTTPBox1.Enabled = false;
                HTTPBox2.Enabled = false;
                TCPHostBox.Enabled = false;
                TCPPortBox.Enabled = false;
            }
        }
    }
}
