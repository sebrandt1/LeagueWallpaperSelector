using LeagueWallpaperSelector.LeagueAPI;
using LeagueWallpaperSelector.LeagueAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWallpaperSelector.FileUtils
{
    public class AppDataFile
    {
        private static readonly string FILEPATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LeagueWallpaperSelector";
        private static readonly string FILENAME = @"\lockdir.txt";
        private static readonly string ICONFOLDER = @"\icons";

        public static void CreateDirsAndFilesOnStartup()
        {
            if (!Directory.Exists(FILEPATH))
                Directory.CreateDirectory(FILEPATH);

            if (!Directory.Exists(FILEPATH + ICONFOLDER))
                Directory.CreateDirectory(FILEPATH + ICONFOLDER);

            List<Champion> champions = DataDragonAPI.FetchAllChampions();

            foreach (Champion champ in champions)
            {
                string path = FILEPATH + ICONFOLDER + $"\\{champ.Name}.png";
                if (!File.Exists(path))
                {
                    byte[] iconStream = CommunityDragonAPI.DownloadChampionIconData(champ);
                    File.WriteAllBytes(path, iconStream);
                }
            }
        }

        public static string[] FetchAllIconPaths()
        {
            return Directory.GetFiles(FILEPATH + ICONFOLDER);
        }

        public static void StoreLockDirectory(string path)
        {
            if (!Directory.Exists(FILEPATH))
                Directory.CreateDirectory(FILEPATH);

            File.WriteAllText(FILEPATH + FILENAME, path);
        }

        public static LockFile GetLockFile()
        {
            if (!Directory.Exists(FILEPATH) || !File.Exists(FILEPATH + FILENAME))
                throw new FileNotFoundException("Could not locate lockfile path, are you sure you've put it in the lockdir.txt file?");

            string path = File.ReadAllText(FILEPATH + FILENAME);
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("The file path to the league of legends directory is invalid: " + path);
            }
            else if (!File.Exists(path + "\\lockfile"))
            {
                throw new FileNotFoundException("Could not find league client connection data, are you logged into the league client?");
            }
            else
            {
                using (FileStream fs = new FileStream(path + "\\lockfile", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string[] lockdata = sr.ReadToEnd().Split(':');
                        return new LockFile()
                        {
                            ProcessName = lockdata[0],
                            ProcessID = int.Parse(lockdata[1]),
                            Port = ushort.Parse(lockdata[2]),
                            Password = lockdata[3],
                            Protocol = lockdata[4]
                        };
                    }
                }
            }
        }
    }

    public struct LockFile
    {
        public string ProcessName { get; set; }
        public int ProcessID { get; set; }
        public ushort Port { get; set; }
        public string Password { get; set; }
        public string Protocol { get; set; }
    }
}
