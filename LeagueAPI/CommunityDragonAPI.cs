using LeagueWallpaperSelector.LeagueAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWallpaperSelector.LeagueAPI
{
    public class CommunityDragonAPI
    {
        public static byte[] DownloadChampionIconData(Champion champ)
        {
            RestClient client = new RestClient("https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-icons/");
            RestRequest request = new RestRequest(champ.Key + ".png");

            var data = client.DownloadData(request);
            return data;
        }
    }
}
