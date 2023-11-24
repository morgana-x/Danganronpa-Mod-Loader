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
        public string gamePath { get; set; } = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa 2 Goodbye Despair";
        public string modsPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Danganronpa2\\mods"; //Assembly.GetEntryAssembly().Location + "/mods";
        public Dictionary<string, int> modLoadOrder { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, bool> modEnabled { get; set; } = new Dictionary<string, bool>();

    }

    public class Config
    {
        public ConfigValues ConfigurationValues { get; set; }
        public string configPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Danganronpa2\\modConfig.json";
        public ConfigValues getConfigValues()
        {
            Console.WriteLine(configPath);
            if (!File.Exists(configPath))
            {
                ConfigurationValues = new ConfigValues();
                saveConfigValues();
                return ConfigurationValues;
            }
            FileStream jsonFileStream = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            ConfigurationValues = JsonSerializer.Deserialize<ConfigValues>(jsonFileStream);
            jsonFileStream.Dispose();
            jsonFileStream.Close();
            return ConfigurationValues;
        }
        public void saveConfigValues()
        {
            FileStream jsonFileStream = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            JsonSerializer.Serialize<ConfigValues>(jsonFileStream, ConfigurationValues);
            jsonFileStream.Dispose();
            jsonFileStream.Close();
        }
        public Config()
        {
            getConfigValues();
        }

    }
}
