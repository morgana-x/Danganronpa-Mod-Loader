// See https://aka.ms/new-console-template for more information
namespace DanganronpaAnotherModLoader;
static class Program
{
    static void Main(string[] args)
    {
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

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Selected game " + game.ToString() + "\n\n\n");
        Console.ForegroundColor = ConsoleColor.White;

        ModPacker.PackMods(config.ConfigurationValues.gamePath[game], config.ConfigurationValues.modsPath, config, game);
        string exePath = config.ConfigurationValues.gamePath[game] +  "\\" + ((game==Game.Dr2) ? "DR2_us.exe" : "DR1_us.exe");
        if (File.Exists(exePath))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting " + (((game == Game.Dr2) ? "DR2_us.exe" : "DR1_us.exe")));
            System.Diagnostics.Process.Start(exePath);
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Console.WriteLine("Press any button to close the program");
        Console.ReadLine();
    }
}


