using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class CodeJam2015_1A
    {

        public static void SolveB()
        {
            var file = Properties.Resources.TextFile1.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            for (var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                var myPos = file[ptr++].Split(' ').Select(int.Parse).ToArray()[1];
                var n = file[ptr++].Split(' ').Select(int.Parse).ToArray()[0];
                var timings = file[ptr++].Split(' ').Select(int.Parse).ToArray();
                
                while(n > 0)
                {
                    var minPtr = 0;
                    var val = int.MaxValue;
                    
                }
            }
        }

        public static void Do()
        {
            Test t = new Test();
            Test.Init(1);
               
        }

        public static void SolveA()
        {
            var file = Properties.Resources.TextFile1.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            for(var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                ptr++;
                var list = file[ptr++].Split(' ').Select(int.Parse).ToArray();
                var constEat = Enumerable.Range(0, list.Count() - 1).Select(i => list[i] - list[i + 1]).Max();
                var y = 0; var x = 0;
                for(var i = 0; i < list.Count() - 1; i++)
                {
                    x += Math.Max(0, list[i] - list[i + 1]);
                    y += Math.Min(constEat, list[i]);
                }
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.WriteLine(string.Format("Case #{0}: {1} {2}", caseNum, x, y));
            }
        }
    }
}
