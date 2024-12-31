using CityPlannerSimulator.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CityPlannerSimulator
{
    public partial class MainWindow : Window
    {
        private const int MapRows = 20;
        private const int MapColumns = 20;
        private const int TileSize = 8;
        private const int TileSpacing = 1;
        private readonly Border[,] mapCells;
        private Map map = new Map(MapRows, MapColumns);

        public MainWindow()
        {
            InitializeComponent();
            mapCells = new Border[MapRows, MapColumns];
            InitializeGrid();
            InitializeMap();
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
                    cell.MouseLeftButtonDown += (s, e) => OnCellClick(capturedRow, capturedCol);
                }
            }
        }

        private void OnCellClick(int row, int col)
        {
            mapCells[row, col].Background = GetTile(20, 12);
        }
    }
}