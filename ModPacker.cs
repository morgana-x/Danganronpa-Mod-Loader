using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    public static class ModPacker
    {
        public static string tempName = "dr2_data_keyboard_us";
        public static void PackMods(string gameFolder, string modFolder, Config config )
        {
            if (!Directory.Exists(modFolder))
            {
                Directory.CreateDirectory(modFolder);
            }
            string[] modPaths = Directory.GetDirectories(modFolder);
            List<Mod> modList = new List<Mod>();
            bool configChanged = false;
            foreach (string modPath in modPaths)
            {
           

                //Console.WriteLine("Caching " + mod.Path.Replace(modFolder, ""));
                string modId = modPath.Replace(modFolder + "\\", "");
                if (!config.ConfigurationValues.modLoadOrder.ContainsKey(modId))
                {
                    config.ConfigurationValues.modLoadOrder.Add(modId, config.ConfigurationValues.modLoadOrder.Count + 1);
                    config.ConfigurationValues.modEnabled.Add(modId, true);
                    configChanged = true;
                }
                else
                {
                    if (!config.ConfigurationValues.modEnabled[modId])
                    {
                        continue;
                    }
                }
                
                Mod mod = new Mod(modPath);
                mod.loadOrder = config.ConfigurationValues.modLoadOrder[modId];
                while (!mod.finishedProcessing) { }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Finished Caching " + modId + "\n");
                Console.ForegroundColor = ConsoleColor.White;
                modList.Add(mod);
            }

            modList = modList.OrderBy(m => m.loadOrder).ToList();
            if (configChanged)
            {
                config.saveConfigValues();
            }
            string backUpWadLocation = gameFolder + "\\dr2_data_keyboard_us_backup.wad";
            string originalWadLocation = gameFolder + "\\dr2_data_keyboard_us.wad";
            if (!File.Exists(backUpWadLocation))
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("dr2_data_keyboard_us_backup.wad doesn't exist!\nCreating...\n");
                Console.ForegroundColor = ConsoleColor.White;
                File.Copy(originalWadLocation, backUpWadLocation, false);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Created a dr2_data_keyboard_us_backup.wad!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Wad.ExtractWAD(backUpWadLocation, gameFolder + tempName);
            //Console.WriteLine(modList.Count);
            foreach (Mod mod in modList)
            {
                //Console.WriteLine("FILE TIME!");
                //Console.WriteLine(mod.FilePaths.Count);
                foreach (string dir in mod.Directories)
                {

                    string cleanDir = dir.Replace(mod.Path, string.Empty);
                    string destDirPath = gameFolder + tempName + cleanDir;
                    if (!Directory.Exists(destDirPath))
                        Directory.CreateDirectory(destDirPath);
                }
                foreach (string file in mod.FilePaths)
                {
                    //Console.WriteLine(file);
                    string cleanFile = file.Replace(mod.Path, string.Empty);
                    //Console.WriteLine(cleanFile);
                    string destFilePath = gameFolder + tempName + "\\" + cleanFile;
                    //Console.WriteLine(destFilePath);
                    //File.Create(destFilePath);
                    byte[] fileBytes = File.ReadAllBytes(file);
                    File.WriteAllBytes(destFilePath, fileBytes);    //, destFilePath);
                }
                Console.ForegroundColor = ConsoleColor.Cyan; 
                Console.WriteLine( "(" + mod.loadOrder + ")Patched Mod " + mod.Path.Replace(config.ConfigurationValues.modsPath + "\\", string.Empty));
                Console.ForegroundColor = ConsoleColor.White;
            }
            Wad.RePackWAD(gameFolder + tempName, gameFolder);
            Directory.Delete(gameFolder + tempName, true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rebuilt dr2_data_keyboard_us.wad");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static List<Mod> GetMods(string modFolder)
        {
            string[] modPaths = Directory.GetDirectories(modFolder);
            List<Mod> modList = new List<Mod>();
            foreach (string modPath in modPaths)
            {
                modList.Add(new Mod(modPath, true));
            }
            return modList;
        }
    }
}
