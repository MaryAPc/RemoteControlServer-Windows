using System;
using TestStack.White;
using TestStack.White.UIItems;
using System.Threading;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using System.Diagnostics;
using TestStack.White.UIItems.WindowStripControls;

namespace RemoteControlServer
{
    class NotepadCommands
    {
        public static void EnterText(String text)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("notepad.exe");
            var app = Application.AttachOrLaunch(processStartInfo);
            var winMain = app.GetWindow("Безымянный — Блокнот");
            var textArea = winMain.Get<TextBox>(SearchCriteria.ByAutomationId("15"));
            textArea.Text = text;
        }

        public static void SaveText(String timeStamp)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("notepad.exe");
            var app = Application.AttachOrLaunch(processStartInfo);
            var winMain = app.GetWindow("Безымянный — Блокнот");
            var menu = winMain.Get<MenuBar>(SearchCriteria.ByAutomationId("MenuBar"));
            var btnSaveAs = menu.MenuItem("Файл", "Сохранить как...");
            btnSaveAs.Click();
            var winSaveFileDialog = winMain.ModalWindow("Сохранение");
            var tbFileName = winSaveFileDialog.MdiChild(SearchCriteria.ByAutomationId("1001"));
            tbFileName.Enter(timeStamp + ".txt");
            var btnSave = winSaveFileDialog.Get<Button>(SearchCriteria.ByText("Сохранить"));
            btnSave.Click();
        }

    }
}
