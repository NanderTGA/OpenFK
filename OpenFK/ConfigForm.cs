using System;
using System.Reflection;
using System.Windows.Forms;
using OpenFK.Properties;

namespace OpenFK
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            CustomFToggle.Checked = Settings.Default.customF;
            RPCToggle.Checked = Settings.Default.RPC;
            RDFToggle.Checked = Settings.Default.RDF;
            QualityCB.SelectedIndex = Settings.Default.Quality;
            ScaleCB.SelectedIndex = Settings.Default.ScaleMode;
            USBToggle.Checked = Settings.Default.USBSupport;
            OnlineToggle.Checked = Settings.Default.IsOnline;
            HTTPHost1Box.Text = Settings.Default.HTTPHost1;
            HTTPHost2Box.Text = Settings.Default.HTTPHost2;
            TCPHostBox.Text = Settings.Default.TCPHost;
            TCPPortBox.Text = Settings.Default.TCPPort;
            BiggerViewModToggle.Checked = Settings.Default.biggerViewModSupport;
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            OpenFKVersionLabel.Text = "OpenFK v" + currentVersion.Substring(0, currentVersion.LastIndexOf("."));
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
            Settings.Default.customF = CustomFToggle.Checked;
            Settings.Default.Save();
        }

        private void RPCToggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.RPC = RPCToggle.Checked;
            Settings.Default.Save();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Settings.Default.HTTPHost1 = HTTPHost1Box.Text;
            Settings.Default.HTTPHost2 = HTTPHost2Box.Text;
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

        private void UpdateTextboxes()
        {
            HTTPHost1Box.Enabled = Settings.Default.IsOnline;
            HTTPHost2Box.Enabled = Settings.Default.IsOnline;
            TCPHostBox.Enabled = Settings.Default.IsOnline;
            TCPPortBox.Enabled = Settings.Default.IsOnline;
        }

        private void BiggerViewModToggle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.biggerViewModSupport = BiggerViewModToggle.Checked;
            if (BiggerViewModToggle.Checked)
            {
                Settings.Default.ScaleMode = 3;
                ScaleCB.SelectedIndex = 3;
            }
            ScaleCB.Enabled = !BiggerViewModToggle.Checked;
            Settings.Default.Save();
        }
    }
}
