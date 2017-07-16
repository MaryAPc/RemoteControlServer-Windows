namespace RemoteControlServer
{
    partial class ServerForm
    {
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        
        private void InitializeComponent(string initIp)
        {
            this.listCommands = new System.Windows.Forms.ListBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.ipAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listCommands
            // 
            this.listCommands.FormattingEnabled = true;
            this.listCommands.ItemHeight = 16;
            this.listCommands.Location = new System.Drawing.Point(13, 74);
            this.listCommands.Margin = new System.Windows.Forms.Padding(4);
            this.listCommands.Name = "listCommands";
            this.listCommands.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listCommands.ScrollAlwaysVisible = true;
            this.listCommands.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listCommands.Size = new System.Drawing.Size(301, 244);
            this.listCommands.TabIndex = 0;
            this.listCommands.Leave += new System.EventHandler(this.listCommands_Leave);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Location = new System.Drawing.Point(13, 51);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(303, 15);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "Последние команды:";
            // 
            // ipAddress
            // 
            this.ipAddress.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ipAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ipAddress.Location = new System.Drawing.Point(16, 13);
            this.ipAddress.Margin = new System.Windows.Forms.Padding(4);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(303, 15);
            this.ipAddress.TabIndex = 2;
            this.ipAddress.Text = "IP-адрес компьютера: " + initIp;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 332);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.listCommands);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemoteControlServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listCommands;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox ipAddress;

        public System.Windows.Forms.ListBox getListCommands()
        {
            return listCommands;
        }
    }
}