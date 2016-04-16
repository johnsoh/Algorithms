using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.GCJ
{
    class CodeJam2016_1A
    {
        static void parseSmallInput()
        {
            var file = Properties.Resources.aInBig.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            var lines = string.Empty;
            for (var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                var word = file[ptr++];
                lines += string.Format("Case #{0}: {1}", caseNum, solve(word) + "\n");
            }

            System.IO.File.WriteAllText(@"C:\git\CSharpTester\CSharpTester\Resources\aOutBig.txt", lines);
            Console.Read();
        }

        static string solve(string word)
        {
            var n = word.Length;
            var ans = word[0] + "";
            var best = ans[0];
            for (var i = 1; i < n; i++)
            {
                var thisChar = word[i];
                if (thisChar >= best)
                {
                    ans = thisChar + ans;
                    best = thisChar;
                }
                else
                {
                    ans = ans + thisChar;
                }
            }
            return ans;
        }
    }
}
}
