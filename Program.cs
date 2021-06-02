using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CinemaConsoleApplication
{
    class Program
    {

        public static void Main()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        public static bool MainMenu()
        {


            Console.Clear();
            Console.WriteLine(@"  _________      .__                         ___.                         _________ .__                              ");
            Console.WriteLine(@" /   _____/ ____ |  |__   ____  __ ____  _  _\_ |__  __ _________  ____   \_   ___ \|__| ____   ____   _____ _____   ");
            Console.WriteLine(@" \_____  \_/ ___\|  |  \ /  _ \|  |  \ \/ \/ /| __ \|  |  \_  __ \/ ___\  /    \  \/|  |/    \_/ __ \ /     \\__  \  ");
            Console.WriteLine(@" /        \  \___|   Y  (  <_> )  |  /\     / | \_\ \  |  /|  | \/ /_/  > \     \___|  |   |  \  ___/|  Y Y  \/ __ \_");
            Console.WriteLine(@"/_______  /\___  >___|  /\____/|____/  \/\_/  |___  /____/ |__|  \___  /   \______  /__|___|  /\___  >__|_|  (____  /");
            Console.WriteLine(@"        \/     \/     \/                          \/            /_____/           \/        \/     \/      \/     \/ ");
            Console.WriteLine("Hello, welcome to the movie-app. Please choose your action:");
            Console.WriteLine("0) Exit our app");
            Console.WriteLine("1) See the playing movies and order tickets");
            Console.WriteLine("2) See our snack and food deals");
            Console.WriteLine("3) Our contact page");
            Console.WriteLine("4) Login/Register");
            Console.Write("Please select an option: ");



            switch (Console.ReadLine())
            {
                case "0":
                    Exit_Program();
                    return false;
                case "1":
                    Screens.Movies();
                    return true;
                case "2":
                    Screens.DealsPage();
                    return true;
                case "3":
                    Console.Clear();
                    Screens.ContactPage();
                    return true;
                case "4":
                    Console.Clear();
                    Screens.LoginPage();
                    return true;
                case "5":
                default:
                    return true;
            }
        }

        private static void Exit_Program()
        {
            Console.WriteLine("You have exited the app, cya!");
            //Console.ReadLine();
        }

    }
}
