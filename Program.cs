// See https://aka.ms/new-console-template for more information
namespace DanganronpaAnotherModLoader;
static class Program
{
    static void Main(string[] args)
    {
        Config config = new Config();
        ModPacker.PackMods(config.ConfigurationValues.gamePath, config.ConfigurationValues.modsPath, config);
        string exePath = config.ConfigurationValues.gamePath + "\\DR2_us.exe";
        if (File.Exists(exePath))
        {
            System.Diagnostics.Process.Start(exePath);
        }
    }
}


