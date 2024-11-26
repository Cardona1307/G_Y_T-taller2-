namespace Puzzle.PipePuzzle
{
    public static class VectorFunctions
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
    }
}