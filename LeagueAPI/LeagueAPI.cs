using LeagueWallpaperSelector.FileUtils;
using LeagueWallpaperSelector.LeagueAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWallpaperSelector.LeagueAPI
{
    public class LeagueAPI
    {
        public static RestClient LeagueAPIClient()
        {
            LockFile lockFile = AppDataFile.GetLockFile();
            RestClient client = new RestClient($"https://127.0.0.1:{lockFile.Port}");

            string token = 
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"riot:{lockFile.Password}")
                );

            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, br");
            client.AddDefaultHeader("Accept-Language", "en-US,en;q=0.9");
            client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) LeagueOfLegendsClient/11.15.387.5736 (CEF 74) Safari/537.36");
            client.AddDefaultHeader("Authorization", $"Basic {token}");

            //ignore ssl validation
            client.RemoteCertificateValidationCallback = (s, cert, chain, policy) => true;

            return client;
        }

        public static void SetProfileWallpaper(int id)
        {
            RestClient client = LeagueAPIClient();
            RestRequest request = new RestRequest("lol-summoner/v1/current-summoner/summoner-profile");
            request.Method = Method.POST;

            ProfileData data = new ProfileData()
            {
                key = "backgroundSkinId",
                value = id
            };

            request.AddJsonBody(data);

            var result = client.Execute(request);
        }
    }
}
