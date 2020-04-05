using System;

namespace SortedSquaredArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testArray = {-7, -3, -1, 4, 8, 12};
            int[] testCorrectAnswer = {1, 9, 16, 49, 64, 144};
            SortAndSquare.RunAllMethods(testArray, testCorrectAnswer);
        }
    }
}
