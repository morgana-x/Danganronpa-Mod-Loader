using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    public static class ModPacker
    {
 
        public static void PackMods(string gameFolder, string modFolder, Config config, Game game = Game.Dr2 )
        {
            string gameStr = game == Game.Dr1 ? "dr1" : "dr2";
            string tempName = gameStr + "_data_keyboard_us";
            Console.WriteLine(gameFolder + "\\" + tempName + ".wad");
            Console.WriteLine(Directory.Exists(gameFolder));
            Console.WriteLine(File.Exists(gameFolder + "\\" + tempName + ".wad"));

            if (!File.Exists(gameFolder + "\\" + tempName + ".wad"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid path to the game folder!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please provide a valid path containing the .wad files for Danganronpa 2");
                Console.WriteLine("Paste the path below or close the program:\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                string newPath = Console.ReadLine();
                Console.WriteLine(newPath);
                config.ConfigurationValues.gamePath = newPath;
                config.saveConfigValues();
                Console.ForegroundColor = ConsoleColor.White;
                PackMods(newPath, modFolder, config, game);
            }
 
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
                
                Mod mod = new Mod(modPath, false, modId);
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
            string backUpWadLocation = gameFolder + "\\" + gameStr + "_data_keyboard_us_backup.wad";
            string originalWadLocation = gameFolder + "\\" + tempName;
            if (!File.Exists(backUpWadLocation))
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(gameStr + "_data_keyboard_us_backup.wad doesn't exist!\nCreating...\n");
                Console.ForegroundColor = ConsoleColor.White;
                File.Copy(originalWadLocation, backUpWadLocation, false);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Created a " + gameStr + "_data_keyboard_us_backup.wad!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Wad.ExtractWAD(backUpWadLocation, gameFolder + "\\"+ tempName);
            //Console.WriteLine(modList.Count);
            foreach (Mod mod in modList)
            {
                //Console.WriteLine("FILE TIME!");
                //Console.WriteLine(mod.FilePaths.Count);
                foreach (string dir in mod.Directories)
                {

                    string cleanDir = dir.Replace(mod.Path, string.Empty);
                    string destDirPath = gameFolder + "\\" + tempName + cleanDir;
                    destDirPath = destDirPath.Replace("\\wad", string.Empty);
                    //Console.WriteLine(destDirPath);
                    if (!Directory.Exists(destDirPath))
                        Directory.CreateDirectory(destDirPath);
                }
                foreach (string file in mod.FilePaths)
                {
                    //Console.WriteLine(file);
                    string cleanFile = file.Replace(mod.Path, string.Empty);
                    //Console.WriteLine(cleanFile);
                    string destFilePath = gameFolder + "\\" +  tempName + "\\" + cleanFile;
                    destFilePath = destFilePath.Replace("\\wad", string.Empty);
                    ////Console.WriteLine(destFilePath);
                    //File.Create(destFilePath);
                    byte[] fileBytes = File.ReadAllBytes(file);
                    File.WriteAllBytes(destFilePath, fileBytes);    //, destFilePath);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mod.loadOrder);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(")");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" Patched ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write( mod.id);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" by ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(mod.metaData.Author);
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Wad.RePackWAD(gameFolder + "\\" + tempName, gameFolder);
            Directory.Delete(gameFolder + "\\"+ tempName, true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rebuilt " + gameStr + "_data_keyboard_us.wad");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static List<Mod> GetMods(string modFolder)
        {
            string[] modPaths = Directory.GetDirectories(modFolder);
            List<Mod> modList = new List<Mod>();
            foreach (string modPath in modPaths)
            {
                modList.Add(new Mod(modPath, true, modPath.Replace(modFolder + "\\", "")));
            }
            return modList;
        }
    }
}
