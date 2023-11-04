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
            this.CustomFtoggle = new System.Windows.Forms.CheckBox();
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
            this.HTTPBox1 = new System.Windows.Forms.TextBox();
            this.HTTPHost1Label = new System.Windows.Forms.Label();
            this.TCPHostLabel = new System.Windows.Forms.Label();
            this.TCPHostBox = new System.Windows.Forms.TextBox();
            this.OpenFKVersionLabel = new System.Windows.Forms.Label();
            this.TCPPortLabel = new System.Windows.Forms.Label();
            this.TCPPortBox = new System.Windows.Forms.TextBox();
            this.HTTPHost2Label = new System.Windows.Forms.Label();
            this.HTTPBox2 = new System.Windows.Forms.TextBox();
            this.NetworkSettingsLabel = new System.Windows.Forms.Label();
            this.FSGUISettingsLabel = new System.Windows.Forms.Label();
            this.StartFSGUIToggle = new System.Windows.Forms.CheckBox();
            this.KeepFSGUIToggle = new System.Windows.Forms.CheckBox();
            this.CloseFSGUIToggle = new System.Windows.Forms.CheckBox();
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
            // CustomFtoggle
            // 
            this.CustomFtoggle.AutoSize = true;
            this.CustomFtoggle.Location = new System.Drawing.Point(34, 69);
            this.CustomFtoggle.Name = "CustomFtoggle";
            this.CustomFtoggle.Size = new System.Drawing.Size(107, 17);
            this.CustomFtoggle.TabIndex = 1;
            this.CustomFtoggle.Text = "CustomF Support";
            this.CustomFtoggle.UseVisualStyleBackColor = true;
            this.CustomFtoggle.CheckedChanged += new System.EventHandler(this.CustomFtoggle_CheckedChanged);
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
            this.ExitButton.Location = new System.Drawing.Point(259, 401);
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
            // HTTPBox1
            // 
            this.HTTPBox1.Location = new System.Drawing.Point(78, 218);
            this.HTTPBox1.Name = "HTTPBox1";
            this.HTTPBox1.Size = new System.Drawing.Size(100, 20);
            this.HTTPBox1.TabIndex = 15;
            this.HTTPBox1.Text = "localhost";
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
            this.OpenFKVersionLabel.Location = new System.Drawing.Point(12, 411);
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
            // HTTPBox2
            // 
            this.HTTPBox2.Location = new System.Drawing.Point(187, 218);
            this.HTTPBox2.Name = "HTTPBox2";
            this.HTTPBox2.Size = new System.Drawing.Size(100, 20);
            this.HTTPBox2.TabIndex = 22;
            this.HTTPBox2.Text = "localhost";
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
            // FSGUISettingsLabel
            // 
            this.FSGUISettingsLabel.AutoSize = true;
            this.FSGUISettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FSGUISettingsLabel.Location = new System.Drawing.Point(82, 295);
            this.FSGUISettingsLabel.Name = "FSGUISettingsLabel";
            this.FSGUISettingsLabel.Size = new System.Drawing.Size(202, 16);
            this.FSGUISettingsLabel.TabIndex = 24;
            this.FSGUISettingsLabel.Text = "FunkeySelectorGUI Settings";
            this.FSGUISettingsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StartFSGUIToggle
            // 
            this.StartFSGUIToggle.AutoSize = true;
            this.StartFSGUIToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartFSGUIToggle.Location = new System.Drawing.Point(8, 323);
            this.StartFSGUIToggle.Name = "StartFSGUIToggle";
            this.StartFSGUIToggle.Size = new System.Drawing.Size(365, 17);
            this.StartFSGUIToggle.TabIndex = 25;
            this.StartFSGUIToggle.Text = "Start FunkeySelectorGUI when starting OpenFK and CustomF is enabled";
            this.StartFSGUIToggle.UseVisualStyleBackColor = true;
            this.StartFSGUIToggle.CheckedChanged += new System.EventHandler(this.StartFSGUIToggle_CheckedChanged);
            // 
            // KeepFSGUIToggle
            // 
            this.KeepFSGUIToggle.AutoSize = true;
            this.KeepFSGUIToggle.Location = new System.Drawing.Point(8, 346);
            this.KeepFSGUIToggle.Name = "KeepFSGUIToggle";
            this.KeepFSGUIToggle.Size = new System.Drawing.Size(290, 17);
            this.KeepFSGUIToggle.TabIndex = 26;
            this.KeepFSGUIToggle.Text = "Always keep an instance of FunkeySelectorGUI running";
            this.KeepFSGUIToggle.UseVisualStyleBackColor = true;
            this.KeepFSGUIToggle.CheckedChanged += new System.EventHandler(this.KeepFSGUIToggle_CheckedChanged);
            // 
            // CloseFSGUIToggle
            // 
            this.CloseFSGUIToggle.AutoSize = true;
            this.CloseFSGUIToggle.Location = new System.Drawing.Point(8, 369);
            this.CloseFSGUIToggle.Name = "CloseFSGUIToggle";
            this.CloseFSGUIToggle.Size = new System.Drawing.Size(255, 17);
            this.CloseFSGUIToggle.TabIndex = 27;
            this.CloseFSGUIToggle.Text = "Close FunkeySelectorGUI when closing OpenFK";
            this.CloseFSGUIToggle.UseVisualStyleBackColor = true;
            this.CloseFSGUIToggle.CheckedChanged += new System.EventHandler(this.CloseFSGUIToggle_CheckedChanged);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 431);
            this.Controls.Add(this.CloseFSGUIToggle);
            this.Controls.Add(this.KeepFSGUIToggle);
            this.Controls.Add(this.StartFSGUIToggle);
            this.Controls.Add(this.FSGUISettingsLabel);
            this.Controls.Add(this.HTTPHost2Label);
            this.Controls.Add(this.HTTPBox2);
            this.Controls.Add(this.TCPPortBox);
            this.Controls.Add(this.TCPPortLabel);
            this.Controls.Add(this.OpenFKVersionLabel);
            this.Controls.Add(this.TCPHostBox);
            this.Controls.Add(this.TCPHostLabel);
            this.Controls.Add(this.HTTPHost1Label);
            this.Controls.Add(this.HTTPBox1);
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
            this.Controls.Add(this.CustomFtoggle);
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
        private System.Windows.Forms.CheckBox CustomFtoggle;
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
        private System.Windows.Forms.TextBox HTTPBox1;
        private System.Windows.Forms.Label HTTPHost1Label;
        private System.Windows.Forms.Label TCPHostLabel;
        private System.Windows.Forms.TextBox TCPHostBox;
        private System.Windows.Forms.Label OpenFKVersionLabel;
        private System.Windows.Forms.Label TCPPortLabel;
        private System.Windows.Forms.TextBox TCPPortBox;
        private System.Windows.Forms.Label HTTPHost2Label;
        private System.Windows.Forms.TextBox HTTPBox2;
        private System.Windows.Forms.Label NetworkSettingsLabel;
        private System.Windows.Forms.Label FSGUISettingsLabel;
        private System.Windows.Forms.CheckBox StartFSGUIToggle;
        private System.Windows.Forms.CheckBox KeepFSGUIToggle;
        private System.Windows.Forms.CheckBox CloseFSGUIToggle;
    }
}