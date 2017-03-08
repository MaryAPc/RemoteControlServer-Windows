using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteControlServer
{
    static class Program
    {
        static int port = 8081;
        static IPAddress ipAddress = IPAddress.Parse("0.0.0.0");
        const byte codeMsg = 1;
        const byte codeRotate = 2;
        const byte codePoff = 3;
        [STAThread]
        static void Main()
        {
            const string message = "Запустить сервер?";
            const string caption = "RemoteControlServer";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                StartServer();
            }
        }

        private static void StartServer()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(ipEndPoint);
                socket.Listen(1);
                while (true)
                {
                    Socket handler = socket.Accept();
                    Console.WriteLine("connect phone");
                    byte[] recBytes = new byte[1024];
                    int nBytes = handler.Receive(recBytes);
                    String msgCode = Encoding.UTF8.GetString(recBytes, 0, nBytes);
                    String code = msgCode.Remove(1);
                    Console.WriteLine(code);
                    switch (code)
                    {
                        case "1":
                            MessageBox.Show("закрыть", "команда");
                            break;
                        case "2":
                            MessageBox.Show("открыть", "команда");
                            break; 
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    Console.WriteLine("close connect");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
