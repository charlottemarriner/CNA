using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Packets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

namespace Client
{
    public class Client
    {
        private TcpClient m_TcpClient;
        private NetworkStream m_Stream;
        private BinaryFormatter m_Formatter;
        private BinaryWriter m_Writer;
        private BinaryReader m_Reader;

        UdpClient m_UdpClient;

        private ClientForm m_ClientForm;

        public Client()
        {
            m_TcpClient = new TcpClient();
        }

        public void SendMessage(Packet message)
        {
            //m_Writer.WriteLine(message);
            //m_Writer.Flush();
            MemoryStream memoryStream = new MemoryStream();
            m_Formatter.Serialize(memoryStream, message);
            byte[] buffer = memoryStream.GetBuffer();
            m_Writer.Write(buffer.Length);
            m_Writer.Write(buffer);
            m_Writer.Flush();
        }

        public void SendData(string message, string name, int option)
        {
            switch(option)
            {
                case 0:
                    SendMessage(new ChatMessagePacket(message));
                    break;
                case 1:
                    SendMessage(new PrivateMessagePacket(message, name));
                    break;
                case 2:
                    SendMessage(new ClientNamePacket(message, name));
                    break;
                case 3:
                    SendMessage(new GamePacket(message, name));
                    break;
            }
        }

        public bool Connect(string ip, int port)
        {
            try
            {
                m_TcpClient.Connect(ip, port);
                m_Stream = m_TcpClient.GetStream();
                m_Formatter = new BinaryFormatter();
                m_Writer = new BinaryWriter(m_Stream);
                m_Reader = new BinaryReader(m_Stream, Encoding.UTF8);

                m_UdpClient = new UdpClient();
                m_UdpClient.Connect(ip, port);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public void Run()
        {
            if(!m_TcpClient.Connected)
            {
                throw new Exception();
            }

            try
            {
                m_ClientForm = new ClientForm(this);
                Thread m_TcpReadingThread = new Thread(() => { ProcessServerResponse(); });
                m_TcpReadingThread.Start();

                Thread m_UdpReadingThread = new Thread(() => { UdpProcessServerResponse(); });
                m_UdpReadingThread.Start();

               
                m_ClientForm.ShowDialog();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected Error: " + e.Message);
            }
            finally
            {
                m_TcpClient.Close();
                m_UdpClient.Close();
            }
            Console.Read();
        }

        private void ProcessServerResponse()
        {
            while ((m_Reader != null))
            {
                int numberOfBytes;
                if ((numberOfBytes = m_Reader.ReadInt32()) != -1)
                {
                    byte[] buffer = m_Reader.ReadBytes(numberOfBytes);
                    MemoryStream memStream = new MemoryStream(buffer);
                    Packet packet = m_Formatter.Deserialize(memStream) as Packet;
                    switch (packet.packetType)
                    {
                        case PacketType.CHATMESSAGE:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                            m_ClientForm.UpdateChatWindow(chatPacket.m_Message);
                            break;
                        case PacketType.PRIVATEMESSAGE:
                            PrivateMessagePacket privatePacket = (PrivateMessagePacket)packet;
                            m_ClientForm.UpdateChatWindow(privatePacket.m_Name + " : " + privatePacket.m_Message);
                            break;
                        case PacketType.CLIENTNAME:
                            ClientNamePacket namePacket = (ClientNamePacket)packet;
                            m_ClientForm.UpdateClientList(namePacket.m_newName, namePacket.m_oldName);
                            break;
                        case PacketType.GAME:
                            GamePacket gamePacket = (GamePacket)packet;
                            m_ClientForm.UpdateChatWindow(gamePacket.m_name + " : " +gamePacket.m_message);
                            break;
                    }
                }
            }
        }

        public void Login(string name)
        {
            SendMessage(new LoginPacket(name, (IPEndPoint)m_UdpClient.Client.LocalEndPoint));
        }

        public void UdpSendMessage(Packet packet)
        {
            MemoryStream memStream = new MemoryStream();
            m_Formatter.Serialize(memStream, packet);
            byte[] buffer = memStream.GetBuffer();
            m_UdpClient.Send(buffer, buffer.Length);
        }

        public void UdpProcessServerResponse()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                while(true)
                {
                    byte[] bytes = m_UdpClient.Receive(ref endPoint);
                    MemoryStream memStream = new MemoryStream(bytes);
                    Packet packet = m_Formatter.Deserialize(memStream) as Packet;
                    if(packet.packetType == PacketType.CHATMESSAGE)
                    {
                        ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                        m_ClientForm.UpdateChatWindow(chatPacket.m_Message);
                    }
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("Client UDP Read Method Exception: ", e.Message);
            }
        }
    }
}
