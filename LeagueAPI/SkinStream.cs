using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWallpaperSelector.LeagueAPI
{
    //comment to test pipeline 
    public class SkinStream
    {
        public string ChampName { get; set; }
        public string ChampId { get; set; }
        public string SkinName { get; set; }
        public int SkinIndex { get; set; }
        public int SkinId { get; set; }
        public byte[] SkinBuffer { get; set; }
    }
}
