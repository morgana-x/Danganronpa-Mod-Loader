using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    public class ConfigValues
    {
        //public string gamePath { get; set; } = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa 2 Goodbye Despair";
        public Dictionary<Game, string> gamePath { get; set; } = new Dictionary<Game, string>() 
        {
            [Game.Dr1] = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa Trigger Happy Havoc",
            [Game.Dr2] = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa 2 Goodbye Despair"
        };
        public string modsPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Danganronpa2\\mods";

 
        public Dictionary<string, int> modLoadOrder { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, bool> modEnabled { get; set; } = new Dictionary<string, bool>();

    }

    public class Config
    {
        public ConfigValues ConfigurationValues { get; set; }
        public string configPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Danganronpa2\\modConfig.json";

        FileStream jsonFileStream { get; set; } = null;
        public ConfigValues getConfigValues()
        {
            //Console.WriteLine(configPath);
            if (!Directory.Exists(configPath.Replace("\\modConfig.json", "")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(configPath.Replace("\\modConfig.json", "") + " doesn't exist!");
                Directory.CreateDirectory(configPath.Replace("\\modConfig.json", ""));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "Created folder " + configPath.Replace("\\modConfig.json", ""));
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (!File.Exists(configPath))
            {
                ConfigurationValues = new ConfigValues();
                saveConfigValues();
                return ConfigurationValues;
            }
            if (jsonFileStream == null)
            {
                jsonFileStream = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            try
            {
                ConfigurationValues = JsonSerializer.Deserialize<ConfigValues>(jsonFileStream);
                jsonFileStream.Dispose();
                jsonFileStream.Close();
                jsonFileStream = null;
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e}");
                Console.ForegroundColor = ConsoleColor.White;
                ConfigurationValues = new ConfigValues();
                saveConfigValues();
                Console.WriteLine("Saving a new config!");
                return ConfigurationValues;
            }
            //jsonFileStream.Dispose();
            //jsonFileStream.Close();
            return ConfigurationValues;
        }
        public void saveConfigValues()
        {
            File.WriteAllText(configPath, ""); // Empty file incase it is broken!
            if (jsonFileStream == null)
            {
                jsonFileStream = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            JsonSerializer.Serialize<ConfigValues>(jsonFileStream, ConfigurationValues, options);
            jsonFileStream.Dispose();
            jsonFileStream.Close();
            jsonFileStream = null;
            //jsonFileStream.Dispose();
            //jsonFileStream.Close();
        }
        public Config()
        {
            getConfigValues();
        }

    }
}
