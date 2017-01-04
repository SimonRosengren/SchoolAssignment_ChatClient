using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment5Client
{
    class Client
    {
        ClientForm form;

        TcpClient client;
        NetworkStream clientStream;
        BinaryReader reader;
        BinaryWriter writer;

        TextBox outPutTextBox;
        TextBox inputTextBox;
        TextBox nameTextBox;

        string name;

        public Client(ClientForm form, TextBox outPutTextBox, TextBox inputTextBox, TextBox nameTextBox)
        {
            this.form = form;
            client = new TcpClient();

            this.inputTextBox = inputTextBox;
            this.outPutTextBox = outPutTextBox;
            this.nameTextBox = nameTextBox;
        }
        public void Run(object obj)
        {
            while (true)
            {
                //Read
                byte[] bytes = new byte[128];
                clientStream.Read(bytes, 0, 128);

                string message = Encoding.UTF8.GetString(bytes);

                outPutTextBox.BeginInvoke((Action)delegate() { outPutTextBox.Text += message; });
                outPutTextBox.BeginInvoke((Action)delegate() { outPutTextBox.Text += "\r\n"; });   
            }            
        }
        public void SendMessage()
        {         
            string test = inputTextBox.Text;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(test);

            clientStream.Write(data, 0, data.Length);

            inputTextBox.BeginInvoke((Action)delegate() { inputTextBox.Clear(); });
        }
        public void ConnectToChat()
        {
            //Set the name
            this.name = nameTextBox.Text;
            if (name == string.Empty)
            {
                name = "Unknown";
            }

            //Make the connection
            client.Connect(IPAddress.Parse("127.0.0.1"), 50000);
            clientStream = client.GetStream();

            //Send the name to the client
            byte[] data = System.Text.Encoding.ASCII.GetBytes(name);

            clientStream.Write(data, 0, data.Length);
        }
    }
}
