using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    public class Mod
    {
        public List<string> FilePaths = new List<string>();
        public List<string> Directories = new List<string>();

        public string Path;
        public bool finishedProcessing = false;
        public int loadOrder = 0;
        public void spiralIntoDespair(string directory)
        {
            Directories.Add(directory);

            string[] files = Directory.GetFiles(directory);

            List<string> fileList = files.ToList();

            FilePaths = FilePaths.Concat(fileList).ToList();

  
            string[] directories = Directory.GetDirectories(directory);

            if (directories.Length == 0)
            {
                finishedProcessing= true;
                return;
            }

            foreach (string dir in directories)
            {
                spiralIntoDespair(dir);
            }

        }
        public Mod(string path, bool dontFileSearch=false)
        {
            Path = path;
            if (dontFileSearch)
                return;
            FileSearch();
        }
        public void FileSearch()
        {
            FilePaths.Clear();
            spiralIntoDespair(Path);
        }
    }
}
