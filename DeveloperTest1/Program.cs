using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            string haystack = "vnk2435kvabco8awkh125kjneytbcd12qjhb4acd123xmnbqwnw4t";
            string needle = "abcd1234";
            int threshold = 3;
            var results = Enumerable.Range(0, 1 << needle.Length)
            .Select(e => string.Join(string.Empty, Enumerable.Range(0, needle.Length).Select(b => (e & (1 << b)) == 0 ? (char?)null : needle[b])));
            List<string> values = new List<string>();
            foreach (var result in results)
            {
                if (result.ToString().Length >= threshold && needle.Contains(result.ToString()))
                {
                    values.Add(result);
                }
            }
            
             SearchString(haystack, values,needle.Length);

        }


       
        public static void SearchString(string str, List<string> values , int l)
        {
	        List<int> retVal = new List<int>();
                    foreach (string pat in values)
                    {
                        int m = pat.Length;
                        int n = str.Length;

                        int[] badChar = new int[256];

                        BadCharHeuristic(pat, m, ref badChar);

                        int s = 0;
                        while (s <= (n - m))
                        {
                            int j = m - 1;

                            while (j >= 0 && pat[j] == str[s + j])
                                --j;

                            if (j < 0)
                            {
                                retVal.Add(s);
                                Console.WriteLine("sequence of length = " + pat.Length + " found at haystack offset " + s + ", needle offset " + (l - pat.Length).ToString());
                                s += (s + m < n) ? m - badChar[str[s + m]] : 1;
                            }
                            else
                            {
                                s += Math.Max(1, j - badChar[str[s + j]]);
                            }
                        }
                    }
        }

        private static void BadCharHeuristic(string str, int size, ref int[] badChar)
        {
            int i;

            for (i = 0; i < 256; i++)
                badChar[i] = -1;

            for (i = 0; i < size; i++)
                badChar[(int)str[i]] = i;
        }
       
    }

}

