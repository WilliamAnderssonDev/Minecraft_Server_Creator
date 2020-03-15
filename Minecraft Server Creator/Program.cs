using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server_Creator
{
    internal class Program
    {
        private static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string version = "1.0";

        private static void Main(string[] args)
        {
            ConsoleSettings();
            Welcome();

            CreateFolderAndFiles();
            DownloadingJar();

            Completed();
        }

        private static void ConsoleSettings()
        {
            Console.Title = $"Minecraft Server Creator - Version: {version} | Programmed by: William Andersson";
            Console.SetWindowSize(130, 30);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        private static void Welcome()
        {
            Console.WriteLine("Welcome to Minecraft Server Creator");
            Console.WriteLine($"Version: {version} | Programmed by: William Andersson");
            Console.WriteLine();
        }

        private static void CreateFolderAndFiles()
        {
            Process.Start(@"https://account.mojang.com/documents/minecraft_eula");
            Console.WriteLine("Do you accept Minecraft server EULA? | Read the page opened in your browser.");
            Console.WriteLine("Please answer: yes/no");
            if (Console.ReadLine() == "yes")
            {
                Directory.CreateDirectory(desktop + @"\My Minecraft Server");
                string eulaFileName = desktop + @"\My Minecraft Server\eula.txt";
                using (StreamWriter sw = File.CreateText(eulaFileName))
                {
                    sw.WriteLine("#Eula has been accepted by your choice in the Minecraft Server Creator!");
                    sw.WriteLine("#You can read it again at: https://account.mojang.com/documents/minecraft_eula");
                    sw.WriteLine("eula = true");
                }
            }
            else
            {
                Console.WriteLine("Okay, no server for you then!");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.Clear();
            Console.WriteLine("How much ram do you want to allocate to your server? (In Gigabytes | Minimum 1 | Numbers only)");
            int ram = int.Parse(Console.ReadLine());

            string batName = desktop + @"\My Minecraft Server\Start Server.bat";

            using (StreamWriter sw = File.CreateText(batName))
            {
                sw.WriteLine("color 2");
                sw.WriteLine("echo off");
                sw.WriteLine("cls");
                sw.WriteLine(@"java -Xmx" + ram + @"G -Xms1G -jar server.jar nogui");
            }
        }

        private static void DownloadingJar()
        {
            Console.SetWindowSize(160, 30);
            Console.Clear();
            Console.WriteLine("What version of Minecraft would you like to host?");
            Console.WriteLine("Avalible versions: 1.15.2, 1.15.1, 1.15, 1.14.4, 1.14.3, 1.14.2, 1.14.1, 1.14, 1.13.2, 1.13.1, 1.13, 1.12.2, 1.12.1, 1.12, 1.11.2, 1.10.2, 1.9.4, 1.8.8");

            string[] avalibleVersions = { "1.15.2", "1.15.1", "1.15", "1.14.4", "1.14.3", "1.14.2", "1.14.1", "1.14", "1.13.2", "1.13.1", "1.13", "1.12.2", "1.12.1", "1.12", "1.11.2", "1.10.2", "1.9.4", "1.8.8" };
            Console.WriteLine();
            string selectedVersion = Console.ReadLine();

            Console.SetWindowSize(130, 30);
            Console.Clear();
            Console.WriteLine($"Connecting to download server...");

            Console.WriteLine($"Connection established!");
            if (avalibleVersions.Any(selectedVersion.Contains))
            {
                Console.WriteLine($"Downloading {selectedVersion} server jar....");
                DownloadPaperJar(selectedVersion);
            }
            else
            {
                Console.WriteLine($"Couldn't find {selectedVersion} please choose one of the avalible versions!");
                Console.ReadKey();
                Console.Clear();
                DownloadingJar();
            }
        }

        private static void DownloadPaperJar(string mcversion)
        {
            WebClient webClient = new WebClient();
            string paperUrl = @"https://papermc.io/api/v1/paper/" + mcversion + @"/latest/download";
            string paperJarName = desktop + @"\My Minecraft Server\server.jar";

            webClient.DownloadFile(paperUrl, paperJarName);
            Console.Clear();
            Console.WriteLine("Server jar downloaded!");
        }

        private static void Completed()
        {
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("Server Completed!");
            Console.WriteLine();
            Console.WriteLine("Look for a folder named 'My Minecraft Server' on your desktop.");
            Console.WriteLine("To start the server run 'Start Server.bat' inside the folder.");
            Console.WriteLine();
            Console.WriteLine("It's normal for the server to have errors the first time you start it");
            Console.WriteLine("because some config files doesn't exist but they should go away.");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Thread.Sleep(1500);
            Environment.Exit(0);
        }
    }
}