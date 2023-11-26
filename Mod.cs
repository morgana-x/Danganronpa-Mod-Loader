using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    public class ModMetaData
    {
        public string Name { get; set; } = "Insert Name Here";
        public string Description { get; set; } = "Insert Description Here";
        public string Author { get; set; } = "Insert Author Here";
        public string Version { get; set; } = "0";
        public string GameBanana { get; set; } = "";

        public Game Game { get; set; } = Game.Dr2;
    }

    public class Mod
    {
        public List<string> FilePaths = new List<string>();
        public List<string> Directories = new List<string>();

        public string Path;
        public bool finishedProcessing = false;
        public int loadOrder = 0;

        public string id;

        public ModMetaData metaData;
        private Dictionary<int, bool> branches= new Dictionary<int, bool>();
        public void spiralIntoDespair(string directory, int branchId)
        {
            //Console.WriteLine(directory);
            Directories.Add(directory);

            string[] files = Directory.GetFiles(directory);

            List<string> fileList = files.ToList();

            FilePaths = FilePaths.Concat(fileList).ToList();

  
            string[] directories = Directory.GetDirectories(directory);

            foreach (string dir in directories)
            {
                int did = branches.Count;
                branches.Add(did, false);
                spiralIntoDespair(dir, did);

            }

            branches[branchId] = true;
    
            foreach(var a in branches)
            {
                if (!a.Value)
                {
                    return;
                }
            }
            finishedProcessing= true;

        }
        public void readModJson()
        {
            FileStream jsonFileStream;
            if (!File.Exists(Path + "\\metaData.json"))
            {
                metaData = new ModMetaData();
                metaData.Name = id;
                jsonFileStream = new FileStream(Path + "\\metaData.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented= true;
                JsonSerializer.Serialize<ModMetaData>(jsonFileStream, metaData, options) ;
                jsonFileStream.Dispose();
                jsonFileStream.Close();
                return;
            }
            jsonFileStream = new FileStream(Path + "\\metaData.json", FileMode.Open, FileAccess.Read);
            metaData = JsonSerializer.Deserialize<ModMetaData>(jsonFileStream);
            jsonFileStream.Dispose();
            jsonFileStream.Close();
        }
        public Mod(string path, bool dontFileSearch=false, string modId = null)
        {
            Path = path;
            
            if (modId != null)
            {
                id = modId;
            }
            else
            {
                id = "dr1.mod.unknown" + path.Replace("\\", "");
            }
            readModJson();
            if (dontFileSearch)
                return;
            FileSearch();
        }
        public void FileSearch()
        {
            FilePaths.Clear();
            branches.Clear();
            spiralIntoDespair(Path + "\\wad", 0);
        }
    }
}
