using LeagueWallpaperSelector.LeagueAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;

namespace LeagueWallpaperSelector.LeagueAPI
{
    public class DataDragonAPI
    {
        public static List<Champion> FetchAllChampions()
        {
            string version = GetLeaguePatchVersion();
            RestClient client = new RestClient("https://ddragon.leagueoflegends.com");
            RestRequest request = new RestRequest($"cdn/{version}/data/en_US/champion.json");

            var result = client.Execute<ChampionResponse>(request);
            return result.Data.Data.Select(x => x.Value).ToList();
        }

        public static string GetLeaguePatchVersion()
        {
            RestClient client = new RestClient("https://ddragon.leagueoflegends.com");
            RestRequest request = new RestRequest("api/versions.json");

            var versionResult = client.Execute(request);
            Regex items = new Regex("[\\[\\]\"]");
            string[] versions = items.Replace(versionResult.Content, "").Split(',');

            return versions[0];
        }

        /// <summary>
        /// Fetches a champion by their internal id (ie Chogath NOT Cho'Gath).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Champion FetchChampion(string name)
        {
            string version = GetLeaguePatchVersion();
            RestClient client = new RestClient("https://ddragon.leagueoflegends.com");
            RestRequest request = new RestRequest($"cdn/{version}/data/en_US/champion/{name}.json");

            var result = client.Execute<ChampionResponse>(request);
            return result.Data.Data.Values.First();
        }

        public static byte[] DownloadSplashArt(string id, int index)
        {
            RestClient client = new RestClient("https://ddragon.leagueoflegends.com");
            RestRequest request = new RestRequest($"cdn/img/champion/splash/{id}_{index}.jpg");

            byte[] data = client.DownloadData(request);

            return data;
        }
    }
}
