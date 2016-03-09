using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharp
{
    class ProgramGCJ
    {
        //static void Main(string[] args) { Console.Read(); }

        public static void Parse()
        {
            var file = Properties.Resources.TextFile1.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            for (var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                // var intArray = file[ptr++].Split(' ').Select(int.Parse).ToArray();
                var res = Solve();
                Debug.WriteLine("Case #{0}: {1}", caseNum, res);
            }
        }

        public static string Solve()
        {
            return null;
        }
    }
}
