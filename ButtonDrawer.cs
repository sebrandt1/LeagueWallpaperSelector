using LeagueWallpaperSelector.FileUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LeagueWallpaperSelector
{
    public class ButtonDrawer
    {
        //code change pipeline test
        public static void LoadChampionIcons(Grid champGrid, RoutedEventHandler clickMethod)
        {
            champGrid.HorizontalAlignment = HorizontalAlignment.Center;
            champGrid.VerticalAlignment = VerticalAlignment.Center;
            champGrid.Height = AppDataFile.FetchAllIconPaths().Length / 10 * 70;

            for (int rows = 0; rows < AppDataFile.FetchAllIconPaths().Length / 10 + 1; rows++)
            {
                champGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int cols = 0; cols < 10; cols++)
                {
                    champGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                }
            }

            int x = 0;
            int y = 0;
            foreach (string str in AppDataFile.FetchAllIconPaths())
            {
                Button btn = new Button();
                btn.Height = 64;
                btn.Width = 64;
                string name = str.Split('\\').Last().Replace(".png", "");
                name = new string(name.Where(c => char.IsLetter(c)).ToArray());
                btn.Click += clickMethod;

                btn.Name = name;

                BitmapImage bitImg = new BitmapImage();
                bitImg.BeginInit();
                bitImg.UriSource = new Uri(str);
                bitImg.EndInit();

                ImageBrush img = new ImageBrush(bitImg);
                btn.Background = img;
                btn.Foreground = img;
                Grid.SetColumn(btn, x);
                Grid.SetRow(btn, y);
                champGrid.Children.Add(btn);

                x++;
                if (x == 10)
                {
                    y++;
                    x = 0;
                }
            }
        }
    }
}
