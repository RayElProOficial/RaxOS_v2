using Cosmos.System.Graphics;
using RaxOS_BETA.Programs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace RaxOS_BETA
{
    public class Kernel : Sys.Kernel 
    {
        public static string LastVersion { get; set; } = "0.0.0.2";
        protected override void BeforeRun()
        {
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            if (!File.Exists("0:\\SYSTEM\\System.cs"))
            {
                //Custom Installer (Now you see why

                Console.WriteLine("Welcome to RAXOS Installer");
                Directory.CreateDirectory("0:\\SYSTEM\\");
                Console.WriteLine("Creating 0:\\SYSTEM...");
                File.WriteAllText("0:\\SYSTEM\\System.cs", "");
                Console.WriteLine("Creating 0:\\SYSTEM\\System.cs...");
                File.WriteAllText("0:\\SYSTEM\\Kernel.dll", "");
                Console.WriteLine("Creating 0:\\SYSTEM\\Kernel.dll...");
                File.WriteAllText("0:\\SYSTEM\\sysinfo.inf", "" +
                    "[SYSINFO]\n" +/*0*/
                    "Installed = true\n" +/*1*/
                    "Userspecified = true\n" +//2
                    "Passwordspecified = true\n" +//3
                    "RaxOS_Channel = Beta\n" +//4
                    "RaxOS_Version = {\n" +
                    "0.0.0.2\n" +
                    "}");//5
                Console.WriteLine("Creating 0:\\SYSTEM\\sysinfo.inf...");
                Console.Write("Please enter username:");
                string usr = Console.ReadLine();
            Z:
                Console.WriteLine("Please press enter and after enter password");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Press Enter.");
                    goto Z;
                }

                string pss = Console.ReadLine();
                Console.Clear();
                string[] usrpss = { usr, pss };
                File.WriteAllLines("0:\\SYSTEM\\users.db", usrpss);
                Console.WriteLine("Creating users.db...");
                Directory.CreateDirectory($"0:\\Users\\{usr}");
                Directory.Delete("0:\\Dir Testing\\", true);
                Console.WriteLine("Deleting cache...");
                Directory.Delete("0:\\TEST\\", true);
                Console.WriteLine("Deleting Setup Data...");
                File.Delete("0:\\Kudzu.txt");
                Console.WriteLine("Deleting logs...");
                File.Delete("0:\\Root.txt");
                Console.WriteLine("Deleting raxos_setup");
                fs.CreateDirectory($"0:\\{usr}\\Documents\\");
                Console.WriteLine("Creating 0:\\SYSTEM...");
                Console.WriteLine("Press any key to reboot");
                Console.ReadKey();
                Sys.Power.Reboot();
                // There is no SYSTEM directory yet, so we just shut the computer down there
            }
            else
            {

            }
            Console.WriteLine("Loading Filesystem...");
            Console.Clear();
            string loading_text = "Booting";
            Console.WriteLine(loading_text);
            if (!File.Exists("0:\\SYSTEM\\resol.conf"))
            {
                File.WriteAllText("0:\\SYSTEM\\resol.conf", "1");
            }
            if (!File.Exists("0:\\SYSTEM\\color.conf"))
            {
                File.WriteAllText("0:\\SYSTEM\\color.conf", "1");
            }
            string[] userData = File.ReadAllLines("0:\\SYSTEM\\users.db");
            Console.WriteLine("Reading user data...");
            string Resolution = File.ReadAllText("0:\\SYSTEM\\resol.conf");
            string ColorDepth = File.ReadAllText("0:\\SYSTEM\\color.conf");
            /*
             --Px320Bt4
             --Px320Bt8
             --Px640Bt4
             --Px720Bt16
             *//*
            if (Resolution == "1" && ColorDepth == "1")
            {
                VGAScreen.SetGraphicsMode(Cosmos.HAL.VGADriver.ScreenSize.Size320x200, Sys.Graphics.ColorDepth.ColorDepth4);
            }
            if (Resolution == "1" && ColorDepth == "2")
            {
                VGAScreen.SetGraphicsMode(Cosmos.HAL.VGADriver.ScreenSize.Size320x200, Sys.Graphics.ColorDepth.ColorDepth8);
            }
            if (Resolution == "2" && ColorDepth == "1")
            {
                VGAScreen.SetGraphicsMode(Cosmos.HAL.VGADriver.ScreenSize.Size640x480, Sys.Graphics.ColorDepth.ColorDepth4);
            }
            if (Resolution == "3" && ColorDepth == "3")
            {
                VGAScreen.SetGraphicsMode(Cosmos.HAL.VGADriver.ScreenSize.Size720x480, Sys.Graphics.ColorDepth.ColorDepth16);
            }*/
            Console.WriteLine("Reading configuration...");
            string[] SYSINFO = File.ReadAllLines("0:\\SYSTEM\\sysinfo.inf");
            string currver = SYSINFO[6];
            Console.WriteLine("Reading SysInfo...");
            if (SYSINFO[6] != LastVersion)
            {
                Console.WriteLine("Please update RaxOS Now, if you don't update, some files and system will be broken!");
                Console.WriteLine("To update open raxupd (run -a raxupd) in command line and put \"--check\".");
            }
            Console.Clear();

        LOGIN:
            string username = userData[0];
            string password = userData[1];
            Console.Write("Please, log in with Password:");
            string input = Console.ReadLine();
            if (input == password)
            {
                Console.Clear();
                Console.WriteLine($"Hello, {username}!");
                Console.WriteLine($"Welcome to RaxOS Alpha {LastVersion}");
                Run();
            }
            else
            {
                Console.WriteLine("WRONG PASSWORD. TRY AGAIN!");
                goto LOGIN;
            }

        }
        /// <summary>
        /// Current directory
        /// </summary>
        public static string current_directory { get; set; } = "0:\\";
        public static List<string> AppsList;
        public static void GetApps()
        {
            AppsList.Add("cli.scif");
            AppsList.Add("core.notepad");
            AppsList.Add("RaxOS.Settings");
            AppsList.Add("RaxOS.RaxGET");
            AppsList.Add("Utils.RaxUPD");
        }
        protected override void Run()
        {
            GetApps();
            string[] dirs = GetDirFadr(current_directory);
            string[] fils = GetFilFadr(current_directory);
            Console.Write(current_directory + "> ");
            string input = Console.ReadLine();
            if (input.StartsWith("echo "))
            {
                Console.WriteLine(input.Remove(0, 5));
            }
            else if (input == "shutdown")
            {
                Sys.Power.Shutdown();
            }
            else if (input == "cd..")
            {
                DirectoryInfo currdir = new DirectoryInfo(current_directory);
                current_directory = currdir.Parent.ToString();
            }
            else if (input.StartsWith("raxget "))
            {
                string пц/*gw*/ = input.Remove(0, 7);
                if (пц == "list")
                {
                    string[] apps = AppsList.ToArray();
                    Console.WriteLine($"raxget function: Applications:\n" +
                        $"{0} | FUNCTION | 1.88KB \n" +
                        $"{1} | APP | 1.81KB\n" +
                        $"{2} | SYSTEM APP | 0.00KB\n" +
                        $"{3} | FUNCTION | 0.07KB", apps[0], apps[1], apps[2], apps[3]);
                }
            }
            else if (input.StartsWith("cd "))
            {
                string ол/*jk*/ = input.Remove(0, 3);
                if (Directory.Exists(current_directory + ол))
                {
                    current_directory += ол;
                }
                else
                {
                    Console.WriteLine($"The directory {ол} does not exist!");
                }

            }
            else if (input.StartsWith("mkdir "))
            {
                string вь = input.Remove(0, 6);
                Directory.CreateDirectory("0:\\" + вь/*[вь = dm]*/);
            }
            else if (input.StartsWith("run -a "))
            {

                string app = input.Remove(0, 7);
                if (app == "notepad")
                {
                    notepad.Launch();
                }
                if (app == "settings")
                {
                    Settings.SettingsMenu.Launch();
                }

            }
            else if (input.StartsWith("run -s "))
            {

                string settings = input.Remove(0, 7);
                if (settings == "sc40016pc")
                {
                    File.Delete("0:\\SYSTEM\\resol.conf");
                    File.Delete("0:\\SYSTEM\\color.conf");
                    File.WriteAllText("0:\\SYSTEM\\resol.conf", "1");
                    File.WriteAllText("0:\\SYSTEM\\color.conf", "1");
                    Sys.Power.Reboot();
                }
                if (settings == "SetDisplayResolution-1080p")
                {
                    try
                    {
                        Sys.Graphics.VGAScreen.SetGraphicsMode(Cosmos.HAL.Drivers.Video.VGADriver.ScreenSize.Size720x480, Sys.Graphics.ColorDepth.ColorDepth32);
                    }
                    catch (Exception)
                    {
                        VGAScreen.SetGraphicsMode(Cosmos.HAL.Drivers.Video.VGADriver.ScreenSize.Size640x480, ColorDepth.ColorDepth4);
                        Console.WriteLine("ERROR.");
                        throw;
                    }

                    Sys.MouseManager.ScreenWidth = 1920;
                    Sys.MouseManager.ScreenHeight = 1080;
                }
                else if (settings == "SDR-200p")
                {
                    File.Delete("0:\\SYSTEM\\resol.conf");
                    File.Delete("0:\\SYSTEM\\color.conf");
                    File.WriteAllText("0:\\SYSTEM\\resol.conf", "1");
                    File.WriteAllText("0:\\SYSTEM\\color.conf", "1");
                }
                else if (settings == "SDR-480p")
                {
                    File.WriteAllText("0:\\SYSTEM\\resol.conf", "2");
                    File.WriteAllText("0:\\SYSTEM\\color.conf", "1");
                }
                else if (settings == "MouseState-Enable")
                {
                    Sys.MouseManager.HandleMouse(1920 / 2, 1080 / 2, 0, 0);
                }
                else if (settings == "help")
                {
                    Console.WriteLine("Syntax: run -s <setting>");
                    Console.WriteLine(
                        "Examples:\n" +
                        "run -s MouseState-Enable\n" +
                        "run -s SDR-480p" +
                        "run -s SDR-200p"
                        );
                }

            }
            else if (input == "scif")
            {
                Console.WriteLine("Loadi" +
                    "ng SCIF...");
                scif.Launch();
            }
            else if (input == "dir")
            {
                foreach (var item in dirs)
                {
                    Console.WriteLine(item);
                }
                foreach (var item in fils)
                {
                    Console.WriteLine(item);
                }
                Run();
            }

            string[] GetDirFadr(string adr) // Get Directories From Address
            {
                var dirs = Directory.GetDirectories(adr);
                return dirs;
            }
            string[] GetFilFadr(string adr)
            {
                var fils = Directory.GetFiles(adr);
                return fils;
            }

            switch (input)
            {
                default:
                    Console.WriteLine(".");
                    break;

                case "help":
                    Console.WriteLine("help -- show list\n");
                    Console.WriteLine("info -- Show system information\n");
                    Console.WriteLine("shutdown -- Shutdown ACPI\n");
                    Console.WriteLine("wpeutil [action] - Reboot\n");
                    Console.WriteLine("test [component] - -audio, -network, -graphics...\n");
                    Console.WriteLine("ls -- ContentList with files & folders in 0:\\ \n");
                    Console.WriteLine("dir -- DIR v2: Shows all files and folders in the location specified in app.\n");
                    Console.WriteLine("scif <file> -- Read content of <file>\n");
                    Console.WriteLine("kbl <layout> -- PLEASE CHECK RAXOS x86.\n");
                    Console.WriteLine("run -a <program> -- Open application\n");
                    Console.WriteLine("mkdir <name> -- Make a folder with <name>\n");
                    Console.WriteLine("echo <text> -- Display <text> on screen\n");
                    Console.WriteLine("run -s <setting> -- Open <setting> UI\n");
                    Run();
                    break;

                case "info":
                    Run();
                    break;
                case "wpeutil reboot":
                    Sys.Power.Reboot();
                    break;
                case "wpeutil":
                    Console.WriteLine("Use these commands:\nwpeutil reboot: Restart RaxOS");
                    Run();
                    break;
                case "test -audio":
                    Sys.PCSpeaker.Beep();
                    Run();
                    break;
                case "test":
                    Console.WriteLine("Use -test (component), ex. test -audio");
                    Run();
                    break;
                case "kbl es":
                    Console.WriteLine("PLEASE CHECK RAXOS x86.");
                    Run();
                    break;
                case "kbl fr":
                    Console.WriteLine("PLEASE CHECK RAXOS x86.");
                    Run();
                    break;
                case "kbl us":
                    Console.WriteLine("PLEASE CHECK RAXOS x86.");
                    Run();
                    break;
                case "kbl de":
                    Console.WriteLine("PLEASE CHECK RAXOS x86.");
                    Run();
                    break;
                case "net":
                    Net();
                    Run();
                    break;
            }
        }
        public static void Net()
        {
            Console.WriteLine("In develop.");
        }
    }
}
