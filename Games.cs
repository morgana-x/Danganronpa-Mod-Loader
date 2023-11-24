using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    public enum Game
    {
        Dr1 = 0,
        Dr2 = 1,
    }


    public class Games
    {
        public Dictionary<int, string> defaultSteamPaths = new Dictionary<int, string>()
        {
            [0] = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa Trigger Happy Havoc",
            [1] = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa 2 Goodbye Despair",
        };

        public Dictionary<int, string> gameString = new Dictionary<int, string>()
        {
            [0] = "dr1",
            [1] = "dr2"
        };
    }
}
