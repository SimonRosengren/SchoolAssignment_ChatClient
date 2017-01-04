using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment5Client
{
    public partial class ClientForm : Form
    {
        Client client;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            client.SendMessage();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            client = new Client(this, displayTextBox, inputTextBox, nameTextBox);
            client.ConnectToChat();
            ThreadPool.QueueUserWorkItem(client.Run); 
        }
    }
}
