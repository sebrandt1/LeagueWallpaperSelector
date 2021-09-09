using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWallpaperSelector.LeagueAPI.Models
{
    public class ChampionResponse
    {
        public Dictionary<string, Champion> Data { get; set; }
    }
}
