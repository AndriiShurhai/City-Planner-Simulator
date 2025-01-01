using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CityPlannerSimulator.Models
{
    public abstract class Building
    {
        public string Name { get; }
        public (int Width, int Height) Size { get; }
        public Dictionary<(int Row, int Col), (int X, int Y)> TileLayout { get; }
        public int Cost { get; }

        protected Building(string name, int cost, (int width, int height) size)
        {
            Name = name;
            Cost = cost;
            Size = size;
            TileLayout = new Dictionary<(int Row, int Col), (int X, int Y)>();
        }

        public abstract bool CanPlace(Map map, int row, int col);
    }

    public class ResidentialBuilding : Building
    {
        public ResidentialBuilding() : base("House", 100, (4, 7))
        {
            TileLayout.Add((0, 0), (0, 4));
            TileLayout.Add((0, 1), (1, 4));
            TileLayout.Add((0, 2), (2, 4));
            TileLayout.Add((0, 3), (3, 4));

            TileLayout.Add((1, 0), (0, 5));
            TileLayout.Add((1, 1), (1, 5));
            TileLayout.Add((1, 2), (2, 5));
            TileLayout.Add((1, 3), (3, 5));

            TileLayout.Add((2, 0), (0, 6));
            TileLayout.Add((2, 1), (1, 6));
            TileLayout.Add((2, 2), (2, 6));
            TileLayout.Add((2, 3), (3, 6));

            TileLayout.Add((3, 0), (0, 7));
            TileLayout.Add((3, 1), (1, 7));
            TileLayout.Add((3, 2), (2, 7));
            TileLayout.Add((3, 3), (3, 7));

            TileLayout.Add((4, 0), (0, 8));
            TileLayout.Add((4, 1), (1, 8));
            TileLayout.Add((4, 2), (2, 8));
            TileLayout.Add((4, 3), (3, 8));

            TileLayout.Add((5, 0), (0, 9));
            TileLayout.Add((5, 1), (1, 9));
            TileLayout.Add((5, 2), (2, 9));
            TileLayout.Add((5, 3), (3, 9));

            TileLayout.Add((6, 0), (0, 10));
            TileLayout.Add((6, 1), (1, 10));
            TileLayout.Add((6, 2), (2, 10));
            TileLayout.Add((6, 3), (3, 10));
        }

        public override bool CanPlace(Map map, int row, int col)
        {
            if (!map.HasAdjacentRoad(row, col))
            {
                return true; //
            }

            if (map.IsNearIndustrial(row, col))
            {
                MessageBox.Show("Занадто близько до промислової зони");
                return false;
            }

            if (!map.HasSpaceForBuilding(row, col, Size))
            {
                MessageBox.Show("Недостатньо місця для будівлі");
                return false;
            }

            return true;
        }

    }

    public class CommercialBuilding : Building
    {
        public CommercialBuilding() : base ("Shop", 200, (2, 2))
        {
            TileLayout.Add((0, 0), (12, 3));
            TileLayout.Add((0, 1), (13, 3));
            TileLayout.Add((1, 0), (12, 4));
            TileLayout.Add((1, 1), (13, 4));
        }
        public override bool CanPlace(Map map, int row, int col)
        {
            return map.HasAdjacentRoad(row, col) &&
                !map.IsNearIndustrial(row, col) &&
                map.HasSpaceForBuilding(row, col, Size);
        }
    }

    public class IndustrialBuilding : Building
    {
        public IndustrialBuilding() : base("Factory", 300, (2, 2))
        {
            TileLayout.Add((0, 0), (12, 3));
            TileLayout.Add((0, 1), (13, 3));
            TileLayout.Add((1, 0), (12, 4));
            TileLayout.Add((1, 1), (13, 4));
        }
        public override bool CanPlace(Map map, int row, int col)
        {
            return map.HasAdjacentRoad(row, col) &&
                !map.IsNearIndustrial(row, col) &&
                map.HasSpaceForBuilding(row, col, Size) &&
                map.IsNearResidential(row, col);
        }
    }

    public class Road : Building
    {
        public Road() : base("Road", 50, (2, 2))
        {
            TileLayout.Add((0, 0), (12, 12));
        }

        public override bool CanPlace(Map map, int row, int col)
        {
            return map.HasAdjacentRoad(row, col) &&
                map.HasSpaceForBuilding(row, col, Size);
        }
    }
}
