using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LeagueWallpaperSelector.LeagueAPI.Models
{
    //code change
    public class Champion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Key { get; set; }
        public List<Skin> Skins { get; set; }
    }

    public class Skin
    {
        public int Id { get; set; }
        //[JsonProperty("num")]
        public int Num { get; set; }
        public string Name { get; set; }
    }
}
