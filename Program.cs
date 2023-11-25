// See https://aka.ms/new-console-template for more information
namespace DanganronpaAnotherModLoader;
static class Program
{
    static void Main(string[] args)
    {
        Config config = new Config();
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
        Console.WriteLine("\n\nSelected game " + game.ToString() + "\n\n\n");
        Console.ForegroundColor = ConsoleColor.White;

        ModPacker.PackMods(config.ConfigurationValues.gamePath[game], config.ConfigurationValues.modsPath, config, game);

        string exePath = config.ConfigurationValues.gamePath[game] +  "\\" + ((game==Game.Dr2) ? "DR2_us.exe" : "DR1_us.exe");
        if (File.Exists(exePath))
        {
            System.Diagnostics.Process.Start(exePath);
        }
    }
}


