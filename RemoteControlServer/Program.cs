﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;
using System.Threading;

namespace RemoteControlServer
{
    class Program : Commands
    {
        static int port = 8081;
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
                form = new ServerForm();
                System.Windows.Forms.Application.Run(form);
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
                ProcessStartInfo processStartInfo = new ProcessStartInfo(name + ".exe");
                var notepad = TestStack.White.Application.AttachOrLaunch(processStartInfo);
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

        void Commands.HideProgram(string name)
        {
            throw new NotImplementedException();
        }
    }
    //ProcessStartInfo processStartfffInfo = new ProcessStartInfo(@"C:\Windows\notepad.exe");
    ////Запускаем приложение
    //var notepad = Application.AttachOrLaunch(processStartInfo);
    ////Находим в нем главное окно
    //var winMain = notepad.GetWindow("Безымянный — Блокнот");
    ////Находим в окне область ввода текста и пешем в нее "Hello, world!"
    //var textArea = winMain.Get<TextBox>(SearchCriteria.ByAutomationId("15"));
    //textArea.Text = "Hello, world!";


    //        notepad.Close();
    //        ////еще вводить текст можно так: textArea.Enter("Hello, world!")
    //        ////На главном окне находим MenuBar
    //        //var menu = winMain.Get<MenuBar>(SearchCriteria.ByAutomationId("MenuBar"));
    //        ////Навигация по меню осуществляется методом MenuItem который принимает список подменю
    //        //var btnSaveAs = menu.MenuItem("Файл", "Сохранить как...");
    //        ////Кликаем по кнопке "Save As..."
    //        //btnSaveAs.Click();
    //        ////Получаем модальное диалоговое окно "Save As"
    //        //var winSaveFileDialog = winMain.ModalWindow("Save As");
    //        ////В диалоговом окне находим поле для ввода имени файла и пешем туда "D:\\test.txt"
    //        //var tbFileName = winSaveFileDialog.MdiChild(SearchCriteria.ByAutomationId("1001"));
    //        //tbFileName.Enter("D:\\test.txt");
    //        ////Ищем кнопочку "Save" и кликаем на нее
    //        //var btnSave = winSaveFileDialog.Get<Button>(SearchCriteria.ByText("Save"));
    //        //btnSave.Click();
    //        ////Если файл уже существует, то может появиться окно подтверждения перезаписи файла
    //        ////если такое окно нашлось, то кликаем кнопку "Yes"
    //        //var winConfirmationDialog = winSaveFileDialog.ModalWindow("Confirm Save As");
    //        //if (winConfirmationDialog != null)
    //        //{
    //        //    var btnYes = winConfirmationDialog.Get<Button>(SearchCriteria.ByText("Yes"));
    //        //    btnYes.Click();
    //        //}
    //        ////Закрываем приложение
    //        //notepad.Close();
}
