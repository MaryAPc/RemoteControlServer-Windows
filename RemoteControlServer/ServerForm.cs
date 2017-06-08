using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteControlServer
{
    public partial class ServerForm : Form
    {
       
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", "RemoteControlServer",
                   MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
        
        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void listCommands_Leave(object sender, EventArgs e)
        {
            this.listCommands.Items.Add(sender);
        }
    }
}
