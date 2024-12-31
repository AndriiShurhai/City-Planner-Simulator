using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CityPlannerSimulator.Models
{
    public class Map
    {
        private Building[,] buildings;
        private (int Row, int Col)[,] buildingOrigins;
        public int Money { get; private set; }
        public int Rows { get; }
        public int Cols { get; }
        public Map(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            buildings = new Building[rows, cols];
            Money = 10000;
            buildingOrigins = new (int Row, int Col)[rows, cols];
        }

        public bool HasAdjacentRoad(int row, int col)
        {
            return CheckAdjacent(row, col, b => b is Road); 
        }

        public bool IsNearIndustrial(int row, int col, int radius = 5)
        {
            return CheckArea(row, col, radius, b => b is IndustrialBuilding); 
        }

        public bool IsNearResidential(int row, int col, int radius = 3)
        {
            return CheckArea(row, col, radius, b => b is ResidentialBuilding);
        }

        public bool isFirstRoad()
        {
            return true;
        }

        public bool CheckAdjacent(int row, int col, Func<Building, bool> predicate)
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dy[i];
                int newCol = col + dx[i];

                if (IsValidPosition(newRow, newCol) && buildings[newRow, newCol] != null &&
                    predicate(buildings[newRow, newCol]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckArea(int row, int col, int radius, Func<Building, bool> predicate)
        {
            for (int r = row - radius; r <= row + radius; r++)
            {
                for (int c = col - radius; c <= col + radius; c++)
                {
                    if (IsValidPosition(r, c) && buildings[r, c] != null &&
                        predicate(buildings[r, c]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Cols;
        }
        public Building GetBuilding(int row, int col)
        {
            return IsValidPosition(row, col) ? buildings[row, col] : null;
        }

        public bool HasSpaceForBuilding(int row, int col, (int Width, int Height) size)
        {
            for (int r = row; r <= row + size.Height; r++)
            {
                for (int c = col; c <= col + size.Width; c++)
                {
                    if (!IsValidPosition(r, c) || buildings[r, c] != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool TryPlaceBuilding(Building building, int row, int col)
        {
            if (!building.CanPlace(this, row, col))
            {
                return false;
            }

            foreach(var tile in building.TileLayout)
            {
                int tileRow = row + tile.Key.Row;
                int tileCol = col + tile.Key.Col;

                buildings[tileRow, tileCol] = building; 
                buildingOrigins[tileRow, tileCol] = (row, col);
            }
           
            Money -= building.Cost;

            return true;
        }
    }
}
