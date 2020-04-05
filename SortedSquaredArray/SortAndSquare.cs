using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Khud0.Utility;

namespace SortedSquaredArray
{
    class SortAndSquare
    {
        private static List<Func<int[], int[]>> allMethods = new List<Func<int[], int[]>>()
        {
            new Func<int[], int[]>(StraightForward), 
            new Func<int[], int[]>(InsertNegative),
            new Func<int[], int[]>(MaxAtEdges)
        };

        public static void RunAllMethods(int[] inputArray, int[] correctAnswer)
        {
            int methodsCount = allMethods.Count;
            for (int i=0; i<methodsCount; i++)
            {
                int arrayLength = inputArray.Length;
                int[] newArray = new int[arrayLength];
                Array.Copy(inputArray, newArray, arrayLength);

                Stopwatcher.Start();
                int[] result = allMethods[i].Invoke(newArray);
                Stopwatcher.Stop();

                Console.WriteLine($"{allMethods[i].Method.ToString()} produced the following result:");
                int resultCount = result.Length;
                for (int r=0; r<resultCount; r++)
                {
                    Console.Write(result[r].ToString() + " ");
                }
                string resultEvaluation = result.SequenceEqual(correctAnswer) ? "correct" : "incorrect";
                Console.WriteLine($"- this output is {resultEvaluation}.\n");
            }
        }   

        /* Constraints:
         * Input array lenth: 1 <= N <= 10000 (No need to check for 0 arguments)
         * Element: -10000 <= array[i] <= 10000
         * Input array is SORTED, outpput array must be sorted
        */

        /* Example:
         * Input: [-3, -2, -1, 0, 4, 9]
         * Output: [0, 1, 4, 9, 16, 81]
         * -2 -1 0 -3 4 9
         * -1 0 -2 -3 4 9
         * 0 -1 -2 -3 4 9
        */
        
        /// <summary>
        /// Simply squares every number and then sorts the array.
        /// </summary>
        /// <returns></returns>
        public static int[] StraightForward(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            for (int i=0; i<arrayLength; i++)
            {
                inputArray[i] *= inputArray[i]; // Square
            }
            
            Array.Sort(inputArray);

            return inputArray;
        }

        /// <summary>
        /// Reduces the amount of needed actions by only sorting the negative numbers.
        /// If no number is negative, the function will just square every value in the array.
        /// </summary>
        /// <returns></returns>
        public static int[] InsertNegative(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            int maxNegativeIndex = 0;
            for (int i=0; i<arrayLength; i++)
            {
                if (inputArray[i] < 0) maxNegativeIndex = i;
                else break;
            }

            for (int i=0; i<=maxNegativeIndex; i++)
            {
                inputArray[i] = Math.Abs(inputArray[i]);
            }

            // Insert every negative number into subarray of positive numbers, because its modulo can be higher than some of the positive numbers
            for (int i=0; i<=maxNegativeIndex; i++)
            {
                if (arrayLength < 2) break;

                for (int j=0; j<arrayLength-1; j++)
                {
                    if (inputArray[j] > inputArray[j+1])
                    {
                        int temp = inputArray[j+1];
                        inputArray[j+1] = inputArray[j];
                        inputArray[j] = temp;
                    }
                }
            }

            for (int i=0; i<arrayLength; i++)
            {
                inputArray[i] *= inputArray[i]; // Square
            }

            return inputArray;
        }

        // The idea (but not the implementation) was looked up here: https://www.youtube.com/watch?v=4eWKHLSRHPY
        /// <summary>
        /// Checks the values at 2 edges, because in a sorted array: 
        /// left edge value is the highest absolute from negative numbers
        /// right edge value is the highest absolute from positive numbers
        /// </summary>
        /// <returns></returns>
        public static int[] MaxAtEdges(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            int[] outArray = new int[arrayLength];

            // Turn negative numbers into their absolute equivalents
            for (int i=0; i<arrayLength; i++)
            {
                if (inputArray[i] < 0) inputArray[i] *= -1;
                else break;
            }

            int indexLeft, indexRight, indexOutArray;
            indexRight = indexOutArray = arrayLength-1;
            indexLeft = 0;

            int leftValue, rightValue;

            while (indexOutArray != -1) // You still need to fill in outArray[0]
            {
                leftValue = inputArray[indexLeft];
                rightValue = inputArray[indexRight];
                
                if (leftValue > rightValue)
                {
                    indexLeft++;
                    outArray[indexOutArray] = leftValue*leftValue;
                } else 
                {
                    indexRight--;
                    outArray[indexOutArray] = rightValue*rightValue;
                }

                indexOutArray--;
            }

            return outArray;
        }
    }
}
