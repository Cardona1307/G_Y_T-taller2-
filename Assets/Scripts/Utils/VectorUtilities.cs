using System.Collections.Generic;
using Puzzle.PipePuzzle;

namespace Utils
{
    public static class VectorUtilities
    {
        public static int[] RotateVector(int[] vector, int angle)
        {
            int[] result = new int[vector.Length];
            int n = vector.Length;
            for (int i = 0; i < n; i++)
            {
                if (angle == 90) result[(i + 1) % n] = vector[i];
                else if (angle == 180) result[(i + 2) % n] = vector[i];
                else if (angle == 270) result[(i + 3) % n] = vector[i];
                else result[i] = vector[i];
            }
            
            return result;
        }

        public static bool IsVectorActive(int[] pipeVector, int[] activationVector)
        {
            for (int i = 0; i < pipeVector.Length; i++)
            {
                if (pipeVector[i] == 1 && activationVector[i] == 1)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public static Coordinate2D[] NeighborsFromVector(Coordinate2D currentCoord, int[] exitVector, Coordinate2D grid)
        {
            List<Coordinate2D> neighbors = new List<Coordinate2D>();
            if (exitVector[0] == 1 && currentCoord.column != 0)
            {
                Coordinate2D left = new Coordinate2D
                {
                    column = currentCoord.column - 1,
                    row = currentCoord.row
                };
                neighbors.Add(left);
            }

            if (exitVector[1] == 1 && currentCoord.row != 0)
            {
                Coordinate2D up = new Coordinate2D()
                {
                    column = currentCoord.column,
                    row = currentCoord.row - 1,
                };
                neighbors.Add(up);
            }

            if (exitVector[2] == 1 && currentCoord.column != grid.column)
            {
                Coordinate2D right = new Coordinate2D()
                {
                    column = currentCoord.column + 1,
                    row = currentCoord.row,
                };
                neighbors.Add(right);
            }

            if (exitVector[3] == 1 && currentCoord.row != grid.row)
            {
                Coordinate2D down = new Coordinate2D()
                {
                    column = currentCoord.column,
                    row = currentCoord.row + 1,
                };
                neighbors.Add(down);
            }
            
            return neighbors.ToArray();
        }

        public static bool IsPipeConnected(this Pipe currentPipe, Pipe nextPipe)
        {
            int[] currentExit = currentPipe.PipeExit;
            int[] nextExit = nextPipe.PipeExit;
            
            return currentExit[0] == 1 && nextExit[2] == 1 || 
                   currentExit[1] == 1 && nextExit[3] == 1 ||
                   nextExit[0] == 1 && currentExit[2] == 1 ||
                   nextExit[1] == 1 && currentExit[3] == 1;
        }
    }
}