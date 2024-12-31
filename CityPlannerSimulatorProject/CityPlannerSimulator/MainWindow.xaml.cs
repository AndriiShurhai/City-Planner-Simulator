using CityPlannerSimulator.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CityPlannerSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MapRows = 40;
        private const int MapColumns = 60;
        public MainWindow()
        {
            InitializeComponent();
            InitializeMap();
        }

        private ImageBrush GetTile(int tileX, int tileY, int tileWidth = 8, int tileHeight = 8)
        {
            int tileSize = 8;
            int spacing = 1;

            var rect = new Int32Rect(
                tileX * (tileSize + spacing),
                tileY * (tileSize + spacing),
                tileWidth,
                tileHeight
                );

            var tileSheet = new BitmapImage(new Uri("pack://application:,,,/Assets/Tilemap/tilemap.png", UriKind.Absolute));
            var croppedBitmap = new CroppedBitmap(tileSheet, rect);

            return new ImageBrush(croppedBitmap);
        }

        private void InitializeMap()
        {
            for (int row = 0; row < MapRows; row++)
            {
                for (int col = 0; col < MapColumns; col++)
                {
                    var cell = new Border
                    {
                        Background = Brushes.LightGreen,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(0.5)
                    };

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);

                    GameMap.Children.Add(cell);
                }
            }

            var testTile = new Border
            {
                Width = 50,
                Height = 50,
                Background = GetTile(20, 0)
            };

            GameMap.Children.Add(testTile);
        }
    }
}