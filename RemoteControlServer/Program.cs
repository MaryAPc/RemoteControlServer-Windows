using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using TestStack.White;

namespace RemoteControlServer
{
    class Program : Commands
    {
        static int port = 8381;
        static IPAddress ipAddress = IPAddress.Parse("0.0.0.0");

        private static Socket serverSocket;
        private static ProgramNameDictionary dictionary = new ProgramNameDictionary();
        private static Commands commands = new Program();
        private static ServerForm form;

        [STAThread]
        static void Main()
        {
            dictionary.initialDictionary();
            const string message = "Запустить сервер?";
            const string caption = "RemoteControlServer";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Thread newThread = new Thread(StartServer);
                newThread.Start();
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                form = new ServerForm(GetIpAddress());
                System.Windows.Forms.Application.Run(form);
            }
        }

        private static String GetIpAddress()
        {
            String host = Dns.GetHostName();
            IPAddress ip = Dns.GetHostByName(host).AddressList[0];
            return ip.ToString();
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
                    serverSocket = socket.Accept();
                    Console.WriteLine("connect phone");
                    byte[] buffer = new byte[1024];
                    String command = EncodeToString(serverSocket.Receive(buffer), buffer);
                    AddCommandToForm(command);
                    HandleCommand(command);
                    serverSocket.Shutdown(SocketShutdown.Both);
                    serverSocket.Close();
                    Console.WriteLine("close connect");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void HandleCommand(string command)
        {
            if (command.Contains("открыть"))
            {
                string program = command.Replace("открыть ", "");
                commands.OpenProgram(dictionary.FindProgramName(program));
                return;
            }
            if (command.Contains("закрыть"))
            {
                string program = command.Replace("закрыть ", "");
                commands.CloseProgram(dictionary.FindProgramName(program));
                return;
            }
            if (command.Contains("увеличить звук"))
            {
                commands.VolumeUp();
                return;
            }
            if (command.Contains("уменьшить звук"))
            {
                commands.VolumeDown();
                return;
                
            }
            if (command.Contains("записать"))
            {
                string text = command.Replace("записать ", "");
                form.Invoke(new Action(() => NotepadCommands.EnterText(text)));
                SendMessage("success");
                return;
            }
            if (command.Contains("сохранить"))
            {
                String timeStamp = DateTime.Now.ToShortDateString();
                commands.SaveNotepadText(timeStamp);
                return;
            }
            else
            {
                SendMessage("error");
            }
        }

        private static void AddCommandToForm(string command)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action(() => form.getListCommands().Items.Insert(0, command)));

            }
        }

        private static String EncodeToString(int nBytes, byte[] recBytes)
        {
            String msg = Encoding.UTF8.GetString(recBytes, 0, nBytes);
            string formatMsg = Regex.Replace(msg, @"\t|\n|\r", string.Empty);
            string command = formatMsg.Remove(formatMsg.Length - 1);
            return command;
        }

        private static void SendMessage(String msg)
        {
            byte[] message = Encoding.UTF8.GetBytes(msg);
            try
            {
                int i = serverSocket.Send(message);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
        }

        void Commands.OpenProgram(string name)
        {
            try
            {
                form.Invoke(new Action(() => {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(name + ".exe");
                    var application = TestStack.White.Application.AttachOrLaunch(processStartInfo);
                })); 
                SendMessage("success");
            } catch (Win32Exception e)
            {
                SendMessage("error");
            }
        }

        void Commands.CloseProgram(string name)
        {
            try
            {
                Process[] process = Process.GetProcessesByName(name);
                foreach (Process proc in process)
                {
                    TestStack.White.Application application = TestStack.White.Application.Attach(proc);
                    application.Close();
                }
                SendMessage("success");
            } catch (Win32Exception e)
            {
                SendMessage("error");
            }
        }

         void Commands.VolumeUp()
        {
            VolumeControl.VolumeUp();
            SendMessage("success");
        }

         void Commands.VolumeDown()
        {
            VolumeControl.VolumeDown();
            SendMessage("success");
        }

        void Commands.EditTextMessage(String text)
        {
           
        }

         void Commands.SaveNotepadText(String timeStamp)
        {
            form.Invoke(new Action(() => NotepadCommands.SaveText(timeStamp)));
            SendMessage("success");
        }
    }
}
