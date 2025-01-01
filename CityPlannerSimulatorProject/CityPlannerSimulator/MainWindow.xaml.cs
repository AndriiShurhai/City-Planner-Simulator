using CityPlannerSimulator.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CityPlannerSimulator
{
    public partial class MainWindow : Window
    {
        private const int MapRows = 40;
        private const int MapColumns = 60;
        private const int TileSize = 8;
        private const int TileSpacing = 1;
        private readonly Border[,] mapCells;
        private Map map = new Map(MapRows, MapColumns);
        private Dictionary<(int X, int Y), ImageBrush> tileCache = new();

        public MainWindow()
        {
            InitializeComponent();
            mapCells = new Border[MapRows, MapColumns];
            InitializeGrid();
            InitializeMap();
            LoadTiles("pack://application:,,,/Assets/Tilemap/tilemap.png");
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < MapRows; i++)
                GameMap.RowDefinitions.Add(new RowDefinition { Height = new GridLength(TileSize) });

            for (int i = 0; i < MapColumns; i++)
                GameMap.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(TileSize) });
        }

        private ImageBrush GetTile(int tileX, int tileY)
        {
            var rect = new Int32Rect(
                tileX * (TileSize + TileSpacing),
                tileY * (TileSize + TileSpacing),
                TileSize,
                TileSize
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
                        BorderBrush = Brushes.DarkGray,
                        BorderThickness = new Thickness(0.5),
                        Background = GetTile(0, 0)
                    };

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);
                    GameMap.Children.Add(cell);
                    mapCells[row, col] = cell;

                    int capturedRow = row;
                    int capturedCol = col;
                    var house = new ResidentialBuilding();
                    cell.MouseLeftButtonDown += (s, e) => OnCellClick(capturedRow, capturedCol, house);
                }
            }
        }

        private void LoadTiles(string tilesetPath)
        {
            var tileSheet = new BitmapImage(new Uri(tilesetPath, UriKind.RelativeOrAbsolute));
            for (int y= 0 ; y < 15; y++)
            {
                for (int x= 0 ; x < 24; x++)
                {
                    var rect = new Int32Rect(
                        x * (TileSize + TileSpacing),
                        y * (TileSize + TileSpacing),
                        TileSize,
                        TileSize
                    );
                    tileCache[(x, y)] = new ImageBrush(new CroppedBitmap(tileSheet, rect));
                }
            }
        }

        public void PlaceBuildingOnMap(Building building, int startRow, int startCol)
        {
            foreach (var tile in building.TileLayout)
            {
                int row = startRow + tile.Key.Row;
                int col = startCol + tile.Key.Col;
                var tileBrush = tileCache[tile.Value];
                mapCells[row, col].Background = tileBrush;
            }
        }

        private void OnCellClick(int row, int col, Building selectedBuilding)
        {

            if (map.HasSpaceForBuilding(row, col, selectedBuilding.Size))
            {
                if (map.TryPlaceBuilding(selectedBuilding, row, col))
                {
                    PlaceBuildingOnMap(selectedBuilding, row, col);
                }
                else
                {
                    MessageBox.Show("Lol2");
                }
            }
            else
            {
                MessageBox.Show("Lol");
            }
        }
    }
}