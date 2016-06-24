using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        public static string GetSubString(string input1,string input2)
    {
        //Write code here
        // Create char to count mapping for fast lookup
        // as some characters may appear more than once
        var charToCount = new Dictionary<char, int>();
        foreach (var c in input2)
        {
            if (!charToCount.ContainsKey(c))
                charToCount.Add(c, 0);
            charToCount[c]++;
        }

        var unmatchesCount = input2.Length;
        int minWindowLength = input1.Length + 1, minWindowStart = -1;
        int currWindowStart = 0, currWindowEnd = 0;
        for (; currWindowEnd < input1.Length; currWindowEnd++)
        {
            var c = input1[currWindowEnd];
            // Skip chars that are not in P
            if (!charToCount.ContainsKey(c))
                continue;
            // Reduce unmatched characters count
            charToCount[c]--;
            if (charToCount[c] >= 0)
                // But do this only while count is positive
                // as count may go negative which means
                // that there are more than required characters
                unmatchesCount--;

            // No complete match, so continue searching
            if (unmatchesCount > 0)
                continue;

            // Decrease window as much as possible by removing
            // either chars that are not in T or those that
            // are in T but there are too many of them
            c = input1[currWindowStart];
            var contains = charToCount.ContainsKey(c);
            while (!contains || charToCount[c] < 0)
            {
                if (contains)
                    // Return character to P
                    charToCount[c]++;

                c = input1[++currWindowStart];
                contains = charToCount.ContainsKey(c);
            }

            if (minWindowLength > currWindowEnd - currWindowStart + 1)
            {
                minWindowLength = currWindowEnd - currWindowStart + 1;
                minWindowStart = currWindowStart;
            }

            // Remove last char from window - it is definitely in a
            // window because we stopped at it during decrease phase
            charToCount[c]++;
            unmatchesCount++;
            currWindowStart++;
        }

        return minWindowStart > -1 ?
               input1.Substring(minWindowStart, minWindowLength) :
               String.Empty;

    }
        static void Main(string[] args)
        {
            Console.WriteLine
                (GetSubString("My Name is Fran","rim"));
            Console.ReadLine();
        }
    }

}
