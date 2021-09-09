using LeagueWallpaperSelector.FileUtils;
using LeagueWallpaperSelector.LeagueAPI;
using LeagueWallpaperSelector.LeagueAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeagueWallpaperSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "LeagueWallpaperSelector";
            AppDataFile.CreateDirsAndFilesOnStartup();

            ButtonDrawer.LoadChampionIcons(champGrid, DisplayAllChampionWallpapers);
        }
        public void DisplayAllChampionWallpapers(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Champion selectedChamp = DataDragonAPI.FetchAllChampions().
                Find(x =>
                        new string(x.Name
                            .Where(c => char.IsLetter(c))
                            .ToArray())
                        .ToLower() == btn.Name.ToLower()
                    );

            selectedChamp = DataDragonAPI.FetchChampion(selectedChamp.Id);


            champGrid.Children.Clear();
            champGrid.RowDefinitions.Clear();
            champGrid.ColumnDefinitions.Clear();
            champGrid.HorizontalAlignment = HorizontalAlignment.Center;
            champGrid.VerticalAlignment = VerticalAlignment.Center;

            List<SkinStream> skins = new List<SkinStream>();

            foreach(Skin skin in selectedChamp.Skins)
            {
                SkinStream stream = new SkinStream()
                {
                    ChampName = selectedChamp.Name,
                    ChampId = selectedChamp.Id,
                    SkinName = skin.Name,
                    SkinIndex = skin.Num,
                    SkinBuffer = DataDragonAPI.DownloadSplashArt(selectedChamp.Id, skin.Num),
                    SkinId = skin.Id
                };
                skins.Add(stream);
                champGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            champGrid.Height = skins.Count * 260;
            int index = 0;
            foreach(SkinStream stream in skins)
            {
                Button splashBtn = new Button();
                splashBtn.Width = 500;
                splashBtn.Height = 250;


                splashBtn.Name = new string(stream.SkinName.Where(x => char.IsLetter(x)).ToArray());
                splashBtn.Tag = stream.SkinId;

                splashBtn.Click += SetSkinWallpaper;

                BitmapImage bitImg = new BitmapImage();
                bitImg.BeginInit();
                bitImg.StreamSource = new MemoryStream(stream.SkinBuffer);
                bitImg.EndInit();

                ImageBrush img = new ImageBrush(bitImg);
                splashBtn.Background = img;
                splashBtn.Foreground = img;
                Grid.SetColumn(splashBtn, 0);
                Grid.SetRow(splashBtn, index);
                champGrid.Children.Add(splashBtn);
                index++;
            }
        }

        public void SetSkinWallpaper(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = (int)button.Tag;
            LeagueAPI.LeagueAPI.SetProfileWallpaper(id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Visibility = Visibility.Hidden;
        }
    }
}
