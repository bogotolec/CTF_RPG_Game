using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

namespace CTF_RPG_Game_Client_WPF
{
    public partial class MainWindow : Window
    {
        public static ConnectionManager Manager;
        private JsonData LastAnswer;
        private Map CurrentMap;
        private List<UIElement> ImagesMap = new List<UIElement>();
        private Dictionary<string, BitmapImage> ImageLibrary = new Dictionary<string, BitmapImage>();

        public MainWindow()
        {
            InitializeComponent();
            Manager = ConnectionManager.GetManager();
            LastAnswer = JsonData.Parse(Manager.Get("map"));

            InitImageLibrary();

            ButtonW.Click += ButtonW_Click;
            ButtonA.Click += ButtonA_Click;
            ButtonS.Click += ButtonS_Click;
            ButtonD.Click += ButtonD_Click;

            ButtonTaskInfo.Click += ButtonTaskInfo_Click;
            ButtonInputFlag.Click += ButtonInputFlag_Click;

            DrowMap();
        }

        private void ButtonInputFlag_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("submit " + FlagTextBox.Text));
            MessageTextBlock.Text = LastAnswer.Message;
        }

        private void ButtonTaskInfo_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("task"));
            if (LastAnswer.Info != null)
                new TaskInfoWindow(LastAnswer.Message, LastAnswer.Info).Show();
        }

        private void ButtonD_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("d"));
            DrowMap();
        }

        private void ButtonS_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("s"));
            DrowMap();
        }

        private void ButtonA_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("a"));
            DrowMap();
        }

        private void ButtonW_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("w"));
            DrowMap();
        }

        private void InitImageLibrary()
        {
            string dir = Directory.GetCurrentDirectory();

            BitmapImage Bricks = new BitmapImage(new Uri(dir + "\\textures\\bricks.png")),
                        Desert = new BitmapImage(new Uri(dir + "\\textures\\desert.png")),
                        DesertNoPass = new BitmapImage(new Uri(dir + "\\textures\\desert_no_pass.png")),
                        Field = new BitmapImage(new Uri(dir + "\\textures\\field.png")),
                        Forest = new BitmapImage(new Uri(dir + "\\textures\\forest.png")),
                        ForestNoPass = new BitmapImage(new Uri(dir + "\\textures\\forest_no_pass.png")),
                        Lava = new BitmapImage(new Uri(dir + "\\textures\\lava.png")),
                        Sign = new BitmapImage(new Uri(dir + "\\textures\\sign.png")),
                        Water = new BitmapImage(new Uri(dir + "\\textures\\water.png")),
                        WaterNoPass = new BitmapImage(new Uri(dir + "\\textures\\water_no_pass.png")),
                        Hero = new BitmapImage(new Uri(dir + "\\textures\\hero.png"));

            ImageLibrary.Add("Bricks", Bricks);
            ImageLibrary.Add("Desert", Desert);
            ImageLibrary.Add("DesertNoPass", DesertNoPass);
            ImageLibrary.Add("Field", Field);
            ImageLibrary.Add("Forest", Forest);
            ImageLibrary.Add("ForestNoPass", ForestNoPass);
            ImageLibrary.Add("Lava", Lava);
            ImageLibrary.Add("Sign", Sign);
            ImageLibrary.Add("Water", Water);
            ImageLibrary.Add("WaterNoPass", WaterNoPass);
            ImageLibrary.Add("Hero", Hero);
        }

        private void Clear()
        {
            foreach(var c in ImagesMap)
            {
                MapGrid.Children.Remove(c);
            }
            ImagesMap = new List<UIElement>();
        }

        private void DrowMap()
        {
            MessageTextBlock.Text = LastAnswer.Message;
            Clear();

            CurrentMap = new Map(LastAnswer.BigWindow, 25, 51);
            for (int i = 0; i < Map.HEIGHT; i++)
            {
                for (int j = 0; j < Map.WIDTH; j++)
                {
                    Image img = new Image();
                    switch (CurrentMap[i, j].Land)
                    {
                        case Landskape.Bricks:
                            img.Source = ImageLibrary["Bricks"];
                            break;
                        case Landskape.Desert:
                            if (CurrentMap[i, j].IsPassable)
                                img.Source = ImageLibrary["Desert"];
                            else
                                img.Source = ImageLibrary["DesertNoPass"];
                            break;
                        case Landskape.Field:
                            img.Source = ImageLibrary["Field"];
                            break;
                        case Landskape.Forest:
                            if (CurrentMap[i, j].IsPassable)
                                img.Source = ImageLibrary["Forest"];
                            else
                                img.Source = ImageLibrary["ForestNoPass"];
                            break;
                        case Landskape.Lava:
                            img.Source = ImageLibrary["Lava"];
                            break;
                        case Landskape.Sign:
                            img.Source = ImageLibrary["Sign"];
                            TextBlock s = new TextBlock();
                            s.Text = CurrentMap[i, j].SignSymbol.ToString();
                            s.FontFamily = new FontFamily("Consolas");
                            s.FontSize = 20;
                            s.TextAlignment = TextAlignment.Center;
                            Grid.SetColumn(s, j);
                            Grid.SetRow(s, i);
                            ImagesMap.Add(s);
                            MapGrid.Children.Add(s);
                            break;
                        case Landskape.Water:
                            if (CurrentMap[i, j].IsPassable)
                                img.Source = ImageLibrary["Water"];
                            else
                                img.Source = ImageLibrary["WaterNoPass"];
                            break;
                    }
                    Grid.SetColumn(img, j);
                    Grid.SetRow(img, i);
                    ImagesMap.Add(img);
                    MapGrid.Children.Add(img);
                    if (i == 12 && j == 12)
                    {
                        Image img1 = new Image();
                        img1.Source = ImageLibrary["Hero"];
                        Grid.SetColumn(img1, j);
                        Grid.SetRow(img1, i);
                        ImagesMap.Add(img1);
                        MapGrid.Children.Add(img1);
                    }
                    if (CurrentMap[i,j].IsTaskable)
                    {
                        TextBlock s = new TextBlock();
                        s.Text = "!";
                        s.FontFamily = new FontFamily("Consolas");
                        s.FontSize = 20;
                        s.TextAlignment = TextAlignment.Center;
                        s.Foreground = Brushes.Red;
                        Grid.SetColumn(s, j);
                        Grid.SetRow(s, i);
                        ImagesMap.Add(s);
                        MapGrid.Children.Add(s);
                    }
                }
            }
        }
    }
}

