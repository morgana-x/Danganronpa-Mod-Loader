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

            if (!File.Exists(gameFolder + "\\" + tempName + ".wad"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid path to the game folder!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please provide a valid path containing the .wad files for Danganronpa " + ((game == Game.Dr1) ? "1" : "2"));
                Console.WriteLine("Paste the path below or close the program:\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                string newPath = Console.ReadLine();
                Console.WriteLine(newPath);
                config.ConfigurationValues.gamePath[game] = newPath;
                config.saveConfigValues();
                Console.ForegroundColor = ConsoleColor.White;
                PackMods(newPath, modFolder, config, game);
            }


            List<Mod> modList = Mod.GetMods(modFolder,config, game);


            string backUpWadLocation = gameFolder + "\\" + gameStr + "_data_keyboard_us_backup.wad";
            string originalWadLocation = gameFolder + "\\" + tempName + ".wad";

            if (!File.Exists(backUpWadLocation)) // Create the backup wad file if it doesn't exist (a clean data_keyboard_us.wad without any mods that is used each time mods are loaded)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(gameStr + "_data_keyboard_us_backup.wad doesn't exist!\nCreating...\n");
                Console.ForegroundColor = ConsoleColor.White;
                File.Copy(originalWadLocation, backUpWadLocation, true);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Created a " + gameStr + "_data_keyboard_us_backup.wad!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Wad.ExtractWAD(backUpWadLocation, gameFolder + "\\"+ tempName); // Extract the clean backup wad file into a temporary folder
            //Console.WriteLine(modList.Count);
            Console.WriteLine("\n\n");
            foreach (Mod mod in modList)
            {
                foreach (string dir in mod.Directories) // Copy Directiories into temporary folder that will be repacked into a wad file
                {

                    string cleanDir = dir.Replace(mod.Path, string.Empty);
                    string destDirPath = gameFolder + "\\" + tempName + cleanDir;
                    destDirPath = destDirPath.Replace("\\wad", string.Empty);
                    if (!Directory.Exists(destDirPath))
                        Directory.CreateDirectory(destDirPath);
                }
                foreach (string file in mod.FilePaths) // Copy the files into a temporary folder that will be repacked into a wad file
                {
                    string cleanFile = file.Replace(mod.Path, string.Empty);
                    string destFilePath = gameFolder + "\\" +  tempName + "\\" + cleanFile;
                    destFilePath = destFilePath.Replace("\\wad", string.Empty);
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
                Console.Write( mod.metaData.Name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" by ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(mod.metaData.Author);
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Building " + gameStr + "_data_keyboard_us.wad...\nPlease wait...");
            Wad.RePackWAD(gameFolder + "\\" + tempName, gameFolder);
            Directory.Delete(gameFolder + "\\"+ tempName, true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rebuilt " + gameStr + "_data_keyboard_us.wad");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /*public static List<Mod> GetMods(string modFolder)
        {
            string[] modPaths = Directory.GetDirectories(modFolder);
            List<Mod> modList = new List<Mod>();
            foreach (string modPath in modPaths)
            {
                modList.Add(new Mod(modPath, true, modPath.Replace(modFolder + "\\", "")));
            }
            return modList;
        }*/
    }
}
