using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ResidentialBuilding() : base("House", 100, (2, 2))
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
        }

        public override bool CanPlace(Map map, int row, int col)
        {
            return map.HasAdjacentRoad(row, col) &&
                map.HasSpaceForBuilding(row, col, Size);
        }
    }
}
