using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Server {
    public sealed class MyConnection {

        // SINGLETON PART
        private static readonly MyConnection instance = new MyConnection();
        static MyConnection() { }
        private MyConnection() { }
        public static MyConnection Instance {
            get { return instance; }
        }
        // END OF SINGLETON PART


        private TcpListener m_serverSocket;
        private TcpClient m_clientSocket;
        private const int m_portServer = 1080;

        private int m_clientNo;

        public void Start() {

            m_serverSocket = new TcpListener(IPAddress.Any, m_portServer);
            m_clientSocket = default(TcpClient);

            m_serverSocket.Start();
            m_clientNo = 0;

            Console.WriteLine(">> Server Started");

            while (true) {

                m_clientSocket = m_serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> Client No: " + Convert.ToString(m_clientNo) + " started!");
                handleClinet client = new handleClinet();
                client.startClient(m_clientSocket, Convert.ToString(m_clientNo));
            }

            m_clientSocket.Close();
            m_serverSocket.Stop();
            Console.WriteLine(">> Server has stoped very well!");
            Console.ReadLine();
        }
    }
}
