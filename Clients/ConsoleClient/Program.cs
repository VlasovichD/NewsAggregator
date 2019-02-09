using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseRequests.APP_PATH = "https://localhost:44311";
            NavigationPanel.MainMenu();
        }
    }
}
