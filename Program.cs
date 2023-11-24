// See https://aka.ms/new-console-template for more information
namespace DanganronpaAnotherModLoader;
static class Program
{
    static void Main(string[] args)
    {
        Config config = new Config();
        ModPacker.PackMods(config.ConfigurationValues.gamePath, config.ConfigurationValues.modsPath, config);
        System.Diagnostics.Process.Start(config.ConfigurationValues.gamePath + "\\DR2_us.exe");
    }
}


