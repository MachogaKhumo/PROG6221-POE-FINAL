using System;
using System.Windows.Forms;

namespace CyberSecurityChatbotGUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new ChatBotForm());
        }
    }
}