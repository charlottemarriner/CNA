using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packets;

namespace Client
{
    public partial class ClientForm : Form
    {

        private Client m_Client;
        private string m_clientName;
        private int m_option;
        private string m_receiverName;

        public ClientForm(Client client)
        {
            InitializeComponent();
            m_clientName = this.Text;
            m_Client = client;
            InputField.Select();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            m_option = (int)messageSelection.SelectedIndex;
            m_clientName = this.Text;
            m_receiverName = (string)ClientList.SelectedItem;
            switch(m_option)
            {
                case 0:
                    m_Client.SendData(InputField.Text, m_clientName, m_option);
                    break;
                case 1:
                    m_Client.SendData(InputField.Text, m_receiverName, m_option);
                    break;
                case 2:
                    this.Text = InputField.Text;
                    m_Client.SendData(InputField.Text, m_clientName, m_option);
                    break;
                case 3:
                    m_Client.SendData(InputField.Text, m_receiverName, m_option);
                    break;
                default:
                    break;
            }
            InputField.Clear();
        }

        public void UpdateChatWindow(string message)
        {
            if(MessageWindow.InvokeRequired)
            {
                Invoke(new Action(() =>
                    {
                    UpdateChatWindow(message);
                }));
                }
            else
            {
                MessageWindow.Text += message + Environment.NewLine;
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }
            }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if(loginButton.Text == "Login")
            {
                loginButton.Text = "Disconnect";
                
                m_Client.Login(m_clientName);
            }
            else
            {
                loginButton.Text = "Login";
                MessageWindow.Clear();
                m_Client.SendData("", m_clientName, 2);
            }
        }

        public void UpdateClientList(string newName, string oldName)
        {
            if(ClientList.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateClientList(newName, oldName);
                }));
            }
            else
            {
                if(ClientList.Items.Contains((object)oldName))
                {
                    ClientList.Items.Remove((object)oldName);
                }
                if (newName != "")
                {
                    ClientList.Items.Add((object)newName);
                }
            }
        }

        private void messageSelection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SubmitButton.Text = (string)messageSelection.SelectedItem;
        }
    }
}
