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
        public CommercialBuilding() : base("Shop", 200, (6, 6))
        {
            TileLayout.Add((0, 0), (18, 4));
            TileLayout.Add((0, 1), (19, 4));
            TileLayout.Add((0, 2), (20, 4));
            TileLayout.Add((0, 3), (18, 4));
            TileLayout.Add((0, 4), (19, 4));
            TileLayout.Add((0, 5), (20, 4));

            TileLayout.Add((1, 0), (18, 5));
            TileLayout.Add((1, 1), (19, 5));
            TileLayout.Add((1, 2), (20, 5));
            TileLayout.Add((1, 3), (18, 5));
            TileLayout.Add((1, 4), (19, 5));
            TileLayout.Add((1, 5), (20, 5));

            TileLayout.Add((2, 0), (18, 6));
            TileLayout.Add((2, 1), (19, 6));
            TileLayout.Add((2, 2), (20, 6));
            TileLayout.Add((2, 3), (18, 6));
            TileLayout.Add((2, 4), (19, 6));
            TileLayout.Add((2, 5), (20, 6));

            TileLayout.Add((3, 0), (5, 7));
            TileLayout.Add((3, 1), (6, 7));
            TileLayout.Add((3, 2), (7, 7));
            TileLayout.Add((3, 3), (5, 7));
            TileLayout.Add((3, 4), (6, 7));
            TileLayout.Add((3, 5), (7, 7));

            TileLayout.Add((4, 0), (20, 10));
            TileLayout.Add((4, 1), (20, 10));
            TileLayout.Add((4, 2), (20, 10));
            TileLayout.Add((4, 3), (20, 10));
            TileLayout.Add((4, 4), (20, 10));
            TileLayout.Add((4, 5), (20, 10));

            TileLayout.Add((5, 0), (5, 7));
            TileLayout.Add((5, 1), (6, 7));
            TileLayout.Add((5, 2), (19, 10));
            TileLayout.Add((5, 3), (19, 10));
            TileLayout.Add((5, 4), (6, 7));
            TileLayout.Add((5, 5), (8, 7));

        }

        public override bool CanPlace(Map map, int row, int col)
        {
            return //
                !map.IsNearIndustrial(row, col) &&
                map.HasSpaceForBuilding(row, col, Size);
        }
    }

public class IndustrialBuilding : Building
    {
        public IndustrialBuilding() : base("Factory", 300, (3, 7))
        {
            TileLayout.Add((0, 0), (15, 3));
            TileLayout.Add((0, 1), (16, 4));
            TileLayout.Add((0, 2), (17, 3));

            TileLayout.Add((1, 0), (15, 5));
            TileLayout.Add((1, 1), (16, 5));
            TileLayout.Add((1, 2), (17, 5));

            TileLayout.Add((2, 0), (15, 6));
            TileLayout.Add((2, 1), (16, 6));
            TileLayout.Add((2, 2), (17, 6));

            TileLayout.Add((3, 0), (10, 7));
            TileLayout.Add((3, 1), (11, 7));
            TileLayout.Add((3, 2), (12, 7));

            TileLayout.Add((4, 0), (10, 8));
            TileLayout.Add((4, 1), (11, 8));
            TileLayout.Add((4, 2), (12, 8));
        }
        public override bool CanPlace(Map map, int row, int col)
        {
            return //
                map.HasSpaceForBuilding(row, col, Size);
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
