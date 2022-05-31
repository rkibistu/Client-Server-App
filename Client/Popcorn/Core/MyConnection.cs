using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ClientCinematograf {
    public sealed class MyConnection {

        // SINGLETON PART
        private static readonly MyConnection instance = new MyConnection();

        static MyConnection() { }
        private MyConnection() { }
        public static MyConnection Instance {
            get { return instance; }
        }
        // END OF SINGLETON PART


        //important constants
        System.Net.Sockets.TcpClient m_clientSocket;
        NetworkStream m_dataStream;
        const string m_ipServer = "5.15.179.94";
        const int m_portServer = 1080;
        const int m_streamSize = Protocol.MESSAGE_SIZE;

        //public functions
        public bool ConnectToServer() {

            m_clientSocket = new System.Net.Sockets.TcpClient();
            try {

                m_clientSocket.Connect(m_ipServer, m_portServer);
                m_dataStream = m_clientSocket.GetStream();
            }
            catch (SocketException e) {

                return false;
            }
            return true;
        }
        public void SendToServer(Message message) {

            if (!m_dataStream.CanWrite) {

                //log error message
                return;
            }
            byte[] bytes = Protocol.ConvertToBytes(message);
            m_dataStream.Write(bytes, 0, bytes.Length);
            m_dataStream.Flush();
        }
        
        public Message GetFromServer() {

            byte[] bytes = new byte[m_streamSize];
            m_dataStream.Read(bytes, 0, bytes.Length);
            
            Console.WriteLine(">> Client got message from client number ");
            return Protocol.ConvertFromBytes(bytes);
        }

        public byte[] GetFromServerBytes() {

            byte[] bytes = new byte[Protocol.MESSAGE_IMAGE_SIZE];

            
            m_dataStream.Read(bytes, 0, bytes.Length);
            m_dataStream.Flush();
            return bytes;
           
        }
    }
}
