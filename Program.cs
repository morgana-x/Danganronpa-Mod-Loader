// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

namespace DanganronpaAnotherModLoader;
static class Program
{
    public static void OpenFolder(string path)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
        {
            FileName = path,
            UseShellExecute = true,
            Verb = "open"
        });
    }
    static bool MainLoop(Config config, Game game = Game.Dr1)
    {
        Console.Clear();
        Console.Title = "Danganronpa Mod Loader";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Selected game: " + game.ToString());
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Options:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("1. ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Pack mods and start game\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("2. ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Open Mod Folder\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("3. ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Open Config\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("4. ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Refresh Mods\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("5. ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Switch Game\n");
        bool switchGame = false;
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            string k = key.KeyChar.ToString();
            if (k == "1")
            {
                ModPacker.PackMods(config.ConfigurationValues.gamePath[game], config.ConfigurationValues.modsPath, config, game);
                string exePath = config.ConfigurationValues.gamePath[game] + "\\" + ((game == Game.Dr2) ? "DR2_us.exe" : "DR1_us.exe");
                if (File.Exists(exePath))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Starting " + (((game == Game.Dr2) ? "DR2_us.exe" : "DR1_us.exe")));
                    Process proc = System.Diagnostics.Process.Start(exePath);
                }
                break;
            }
            else if (k == "2")
            {
                OpenFolder(config.ConfigurationValues.modsPath);
                break;
            }
            else if (k == "3")
            {
                OpenFolder(config.configPath);
                break;
            }
            else if (k == "4")
            {
                Mod.PrintModList(config, game);
                break;
            }
            else if (k == "5")
            {
                switchGame = true;
                break;
            }
        }
        return switchGame;
    }
    static void Main(string[] args)
    {
        Console.Clear();
        Console.Title = "Danganronpa Mod Loader";
        Config config = new Config();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Select game");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(" 1 ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Danganronpa Trigger Happy Havoc\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(" 2 ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Danganronpa 2 Goodbye Despair\n");
        Game game = Game.Dr1;
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            string k = key.KeyChar.ToString();
            if (k == "1" || k == "2")
            {
                game = k == "1" ? Game.Dr1 : Game.Dr2;
                break;
            }
        }
        while (true)
        {
            if (MainLoop(config, game))
            {
                break;
            }
        }
        Main(args);
        /*return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Console.WriteLine("Press any button to close the program");
        Console.ReadLine();*/
    }
}


