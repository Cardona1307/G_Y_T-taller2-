using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class Coordinate2D
    {
        public int column;
        public int row;
    }
    
    public static class MatrixUtilities
    {
        public static int Size(this Coordinate2D coordinate)
        {
            return coordinate.column * coordinate.row;
        }

        public static int GetIndexByCoordinate(Coordinate2D coord, Coordinate2D size)
        {
            if (coord.column > size.column || coord.row > size.row) return -1;
            
            int gridColumns = size.column;
            
            int coordRow = coord.row;
            int coordColumn = coord.column;
            
            return coordRow * gridColumns + coordColumn;
        }

        public static Coordinate2D GetCoordinateByIndex(int index, Coordinate2D size)
        {
            Coordinate2D coord = new Coordinate2D
            {
                column = index % size.column,
                row = index / size.column
            };
            
            return coord;
        }
    }
}