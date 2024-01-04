namespace OpenFK
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.QualityCB = new System.Windows.Forms.ComboBox();
            this.CustomFToggle = new System.Windows.Forms.CheckBox();
            this.RPCToggle = new System.Windows.Forms.CheckBox();
            this.ScaleCB = new System.Windows.Forms.ComboBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RDFToggle = new System.Windows.Forms.CheckBox();
            this.USBToggle = new System.Windows.Forms.CheckBox();
            this.GraphicsSettingsLabel = new System.Windows.Forms.Label();
            this.QualityLabel = new System.Windows.Forms.Label();
            this.ScalingLabel = new System.Windows.Forms.Label();
            this.GameSettingsLabel = new System.Windows.Forms.Label();
            this.OnlineToggle = new System.Windows.Forms.CheckBox();
            this.HTTPHost1Box = new System.Windows.Forms.TextBox();
            this.HTTPHost1Label = new System.Windows.Forms.Label();
            this.TCPHostLabel = new System.Windows.Forms.Label();
            this.TCPHostBox = new System.Windows.Forms.TextBox();
            this.OpenFKVersionLabel = new System.Windows.Forms.Label();
            this.TCPPortLabel = new System.Windows.Forms.Label();
            this.TCPPortBox = new System.Windows.Forms.TextBox();
            this.HTTPHost2Label = new System.Windows.Forms.Label();
            this.HTTPHost2Box = new System.Windows.Forms.TextBox();
            this.NetworkSettingsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // QualityCB
            // 
            this.QualityCB.FormattingEnabled = true;
            this.QualityCB.Items.AddRange(new object[] {
            "0 - High",
            "1 - Medium",
            "2 - Low"});
            this.QualityCB.Location = new System.Drawing.Point(204, 64);
            this.QualityCB.Name = "QualityCB";
            this.QualityCB.Size = new System.Drawing.Size(121, 21);
            this.QualityCB.TabIndex = 0;
            this.QualityCB.SelectedIndexChanged += new System.EventHandler(this.QualityCB_SelectedIndexChanged);
            // 
            // CustomFToggle
            // 
            this.CustomFToggle.AutoSize = true;
            this.CustomFToggle.Location = new System.Drawing.Point(34, 69);
            this.CustomFToggle.Name = "CustomFToggle";
            this.CustomFToggle.Size = new System.Drawing.Size(107, 17);
            this.CustomFToggle.TabIndex = 1;
            this.CustomFToggle.Text = "CustomF Support";
            this.CustomFToggle.UseVisualStyleBackColor = true;
            this.CustomFToggle.CheckedChanged += new System.EventHandler(this.CustomFtoggle_CheckedChanged);
            // 
            // RPCToggle
            // 
            this.RPCToggle.AutoSize = true;
            this.RPCToggle.Location = new System.Drawing.Point(34, 92);
            this.RPCToggle.Name = "RPCToggle";
            this.RPCToggle.Size = new System.Drawing.Size(96, 17);
            this.RPCToggle.TabIndex = 2;
            this.RPCToggle.Text = "Rich Presence";
            this.RPCToggle.UseVisualStyleBackColor = true;
            this.RPCToggle.CheckedChanged += new System.EventHandler(this.RPCToggle_CheckedChanged);
            // 
            // ScaleCB
            // 
            this.ScaleCB.FormattingEnabled = true;
            this.ScaleCB.Items.AddRange(new object[] {
            "0 - Default (Hor+)",
            "1 - Crop (Vert-)",
            "2 - Stretch",
            "3 - Pixel-based"});
            this.ScaleCB.Location = new System.Drawing.Point(204, 104);
            this.ScaleCB.Name = "ScaleCB";
            this.ScaleCB.Size = new System.Drawing.Size(121, 21);
            this.ScaleCB.TabIndex = 3;
            this.ScaleCB.SelectedIndexChanged += new System.EventHandler(this.ScaleCB_SelectedIndexChanged);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(248, 283);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(101, 23);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "Save and Close";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RDFToggle
            // 
            this.RDFToggle.AutoSize = true;
            this.RDFToggle.Location = new System.Drawing.Point(34, 46);
            this.RDFToggle.Name = "RDFToggle";
            this.RDFToggle.Size = new System.Drawing.Size(89, 17);
            this.RDFToggle.TabIndex = 7;
            this.RDFToggle.Text = "RDF Loading";
            this.RDFToggle.UseVisualStyleBackColor = true;
            this.RDFToggle.CheckedChanged += new System.EventHandler(this.RDFToggle_CheckedChanged);
            // 
            // USBToggle
            // 
            this.USBToggle.AutoSize = true;
            this.USBToggle.Location = new System.Drawing.Point(34, 115);
            this.USBToggle.Name = "USBToggle";
            this.USBToggle.Size = new System.Drawing.Size(88, 17);
            this.USBToggle.TabIndex = 8;
            this.USBToggle.Text = "USB Support";
            this.USBToggle.UseVisualStyleBackColor = true;
            this.USBToggle.CheckedChanged += new System.EventHandler(this.USBToggle_CheckedChanged);
            // 
            // GraphicsSettingsLabel
            // 
            this.GraphicsSettingsLabel.AutoSize = true;
            this.GraphicsSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GraphicsSettingsLabel.Location = new System.Drawing.Point(199, 22);
            this.GraphicsSettingsLabel.Name = "GraphicsSettingsLabel";
            this.GraphicsSettingsLabel.Size = new System.Drawing.Size(130, 16);
            this.GraphicsSettingsLabel.TabIndex = 9;
            this.GraphicsSettingsLabel.Text = "Graphics Settings";
            // 
            // QualityLabel
            // 
            this.QualityLabel.AutoSize = true;
            this.QualityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QualityLabel.Location = new System.Drawing.Point(199, 48);
            this.QualityLabel.Name = "QualityLabel";
            this.QualityLabel.Size = new System.Drawing.Size(39, 13);
            this.QualityLabel.TabIndex = 10;
            this.QualityLabel.Text = "Quality";
            // 
            // ScalingLabel
            // 
            this.ScalingLabel.AutoSize = true;
            this.ScalingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScalingLabel.Location = new System.Drawing.Point(199, 88);
            this.ScalingLabel.Name = "ScalingLabel";
            this.ScalingLabel.Size = new System.Drawing.Size(42, 13);
            this.ScalingLabel.TabIndex = 11;
            this.ScalingLabel.Text = "Scaling";
            // 
            // GameSettingsLabel
            // 
            this.GameSettingsLabel.AutoSize = true;
            this.GameSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameSettingsLabel.Location = new System.Drawing.Point(34, 22);
            this.GameSettingsLabel.Name = "GameSettingsLabel";
            this.GameSettingsLabel.Size = new System.Drawing.Size(109, 16);
            this.GameSettingsLabel.TabIndex = 12;
            this.GameSettingsLabel.Text = "Game Settings";
            // 
            // OnlineToggle
            // 
            this.OnlineToggle.AutoSize = true;
            this.OnlineToggle.Location = new System.Drawing.Point(134, 176);
            this.OnlineToggle.Name = "OnlineToggle";
            this.OnlineToggle.Size = new System.Drawing.Size(92, 17);
            this.OnlineToggle.TabIndex = 14;
            this.OnlineToggle.Text = "Enable Online";
            this.OnlineToggle.UseVisualStyleBackColor = true;
            this.OnlineToggle.CheckedChanged += new System.EventHandler(this.OnlineToggle_CheckedChanged);
            // 
            // HTTPHost1Box
            // 
            this.HTTPHost1Box.Location = new System.Drawing.Point(78, 218);
            this.HTTPHost1Box.Name = "HTTPHost1Box";
            this.HTTPHost1Box.Size = new System.Drawing.Size(100, 20);
            this.HTTPHost1Box.TabIndex = 15;
            this.HTTPHost1Box.Text = "localhost";
            // 
            // HTTPHost1Label
            // 
            this.HTTPHost1Label.AutoSize = true;
            this.HTTPHost1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HTTPHost1Label.Location = new System.Drawing.Point(75, 202);
            this.HTTPHost1Label.Name = "HTTPHost1Label";
            this.HTTPHost1Label.Size = new System.Drawing.Size(70, 13);
            this.HTTPHost1Label.TabIndex = 16;
            this.HTTPHost1Label.Text = "HTTP Host 1";
            // 
            // TCPHostLabel
            // 
            this.TCPHostLabel.AutoSize = true;
            this.TCPHostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCPHostLabel.Location = new System.Drawing.Point(73, 241);
            this.TCPHostLabel.Name = "TCPHostLabel";
            this.TCPHostLabel.Size = new System.Drawing.Size(53, 13);
            this.TCPHostLabel.TabIndex = 17;
            this.TCPHostLabel.Text = "TCP Host";
            // 
            // TCPHostBox
            // 
            this.TCPHostBox.Location = new System.Drawing.Point(78, 257);
            this.TCPHostBox.Name = "TCPHostBox";
            this.TCPHostBox.Size = new System.Drawing.Size(100, 20);
            this.TCPHostBox.TabIndex = 18;
            this.TCPHostBox.Text = "localhost";
            // 
            // OpenFKVersionLabel
            // 
            this.OpenFKVersionLabel.AutoSize = true;
            this.OpenFKVersionLabel.Location = new System.Drawing.Point(12, 293);
            this.OpenFKVersionLabel.Name = "OpenFKVersionLabel";
            this.OpenFKVersionLabel.Size = new System.Drawing.Size(88, 13);
            this.OpenFKVersionLabel.TabIndex = 19;
            this.OpenFKVersionLabel.Text = "OpenFK v0.0.0.0";
            // 
            // TCPPortLabel
            // 
            this.TCPPortLabel.AutoSize = true;
            this.TCPPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCPPortLabel.Location = new System.Drawing.Point(184, 241);
            this.TCPPortLabel.Name = "TCPPortLabel";
            this.TCPPortLabel.Size = new System.Drawing.Size(50, 13);
            this.TCPPortLabel.TabIndex = 20;
            this.TCPPortLabel.Text = "TCP Port";
            // 
            // TCPPortBox
            // 
            this.TCPPortBox.Location = new System.Drawing.Point(187, 257);
            this.TCPPortBox.Name = "TCPPortBox";
            this.TCPPortBox.Size = new System.Drawing.Size(100, 20);
            this.TCPPortBox.TabIndex = 21;
            this.TCPPortBox.Text = "80";
            // 
            // HTTPHost2Label
            // 
            this.HTTPHost2Label.AutoSize = true;
            this.HTTPHost2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HTTPHost2Label.Location = new System.Drawing.Point(184, 202);
            this.HTTPHost2Label.Name = "HTTPHost2Label";
            this.HTTPHost2Label.Size = new System.Drawing.Size(70, 13);
            this.HTTPHost2Label.TabIndex = 23;
            this.HTTPHost2Label.Text = "HTTP Host 2";
            // 
            // HTTPHost2Box
            // 
            this.HTTPHost2Box.Location = new System.Drawing.Point(187, 218);
            this.HTTPHost2Box.Name = "HTTPHost2Box";
            this.HTTPHost2Box.Size = new System.Drawing.Size(100, 20);
            this.HTTPHost2Box.TabIndex = 22;
            this.HTTPHost2Box.Text = "localhost";
            // 
            // NetworkSettingsLabel
            // 
            this.NetworkSettingsLabel.AutoSize = true;
            this.NetworkSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NetworkSettingsLabel.Location = new System.Drawing.Point(118, 152);
            this.NetworkSettingsLabel.Name = "NetworkSettingsLabel";
            this.NetworkSettingsLabel.Size = new System.Drawing.Size(124, 16);
            this.NetworkSettingsLabel.TabIndex = 13;
            this.NetworkSettingsLabel.Text = "Network Settings";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 311);
            this.Controls.Add(this.HTTPHost2Label);
            this.Controls.Add(this.HTTPHost2Box);
            this.Controls.Add(this.TCPPortBox);
            this.Controls.Add(this.TCPPortLabel);
            this.Controls.Add(this.OpenFKVersionLabel);
            this.Controls.Add(this.TCPHostBox);
            this.Controls.Add(this.TCPHostLabel);
            this.Controls.Add(this.HTTPHost1Label);
            this.Controls.Add(this.HTTPHost1Box);
            this.Controls.Add(this.OnlineToggle);
            this.Controls.Add(this.NetworkSettingsLabel);
            this.Controls.Add(this.GameSettingsLabel);
            this.Controls.Add(this.ScalingLabel);
            this.Controls.Add(this.QualityLabel);
            this.Controls.Add(this.GraphicsSettingsLabel);
            this.Controls.Add(this.USBToggle);
            this.Controls.Add(this.RDFToggle);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.ScaleCB);
            this.Controls.Add(this.RPCToggle);
            this.Controls.Add(this.CustomFToggle);
            this.Controls.Add(this.QualityCB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "OpenFK Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox QualityCB;
        private System.Windows.Forms.CheckBox CustomFToggle;
        private System.Windows.Forms.CheckBox RPCToggle;
        private System.Windows.Forms.ComboBox ScaleCB;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.CheckBox RDFToggle;
        private System.Windows.Forms.CheckBox USBToggle;
        private System.Windows.Forms.Label GraphicsSettingsLabel;
        private System.Windows.Forms.Label QualityLabel;
        private System.Windows.Forms.Label ScalingLabel;
        private System.Windows.Forms.Label GameSettingsLabel;
        private System.Windows.Forms.CheckBox OnlineToggle;
        private System.Windows.Forms.TextBox HTTPHost1Box;
        private System.Windows.Forms.Label HTTPHost1Label;
        private System.Windows.Forms.Label TCPHostLabel;
        private System.Windows.Forms.TextBox TCPHostBox;
        private System.Windows.Forms.Label OpenFKVersionLabel;
        private System.Windows.Forms.Label TCPPortLabel;
        private System.Windows.Forms.TextBox TCPPortBox;
        private System.Windows.Forms.Label HTTPHost2Label;
        private System.Windows.Forms.TextBox HTTPHost2Box;
        private System.Windows.Forms.Label NetworkSettingsLabel;
    }
}