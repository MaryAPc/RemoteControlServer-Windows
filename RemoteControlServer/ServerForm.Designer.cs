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
        
        private void InitializeComponent()
        {
            this.listCommands = new System.Windows.Forms.ListBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listCommands
            // 
            this.listCommands.FormattingEnabled = true;
            this.listCommands.Location = new System.Drawing.Point(12, 38);
            this.listCommands.Name = "listCommands";
            this.listCommands.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listCommands.ScrollAlwaysVisible = true;
            this.listCommands.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listCommands.Size = new System.Drawing.Size(227, 199);
            this.listCommands.TabIndex = 0;
            this.listCommands.SelectedIndexChanged += new System.EventHandler(this.listCommands_SelectedIndexChanged);
            this.listCommands.Leave += new System.EventHandler(this.listCommands_Leave);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Location = new System.Drawing.Point(12, 13);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(227, 13);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "Последние команды:";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 254);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.listCommands);
            this.Name = "ServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemoteControlServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerForm_FormClosed);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listCommands;
        private System.Windows.Forms.TextBox textBox;

        public System.Windows.Forms.ListBox getListCommands()
        {
            return listCommands;
        }
    }
}