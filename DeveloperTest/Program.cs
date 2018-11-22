using System;

namespace DeveloperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 12, -34, 40, 6, -10, 56, 12, -1, -15, 10, 4 };
            int n = a.Length;
            maxSumContinuesSequence(a, n);
        }

        private static void maxSumContinuesSequence(int[] a, int n)
        {
            int max_so_far = -9999,
            max_ending_here = 0, start = 0,
            end = 0, s = 0;

            for (int i = 0; i < n; i++)
            {
                max_ending_here += a[i];

                if (max_so_far < max_ending_here)
                {
                    max_so_far = max_ending_here;
                    start = s;
                    end = i;
                }

                if (max_ending_here < 0)
                {
                    max_ending_here = 0;
                    s = i + 1;
                }
            }
            Console.WriteLine(" For vector " + string.Join(",", a) + " " + "start index is " + start + ", end index " + end + " and the sum " + max_so_far + ".");
        }
    }
}
