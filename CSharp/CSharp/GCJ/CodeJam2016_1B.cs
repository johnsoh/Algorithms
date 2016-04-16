using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.GCJ
{
    class CodeJam2016_1B
    {
        static void parseSmallInput()
        {
            var file = Properties.Resources.bIn.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            var lines = string.Empty;
            for (var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                var strips = int.Parse(file[ptr++]);
                var papers = new List<List<int>>();
                for (var i = 0; i < strips * 2 - 1; i++)
                {
                    var strip = file[ptr++];
                    var list = strip.Split(' ').Select(x => int.Parse(x)).ToList();
                    papers.Add(list);
                }
                lines += string.Format("Case #{0}: {1}", caseNum, solve(papers) + "\n");
            }

            System.IO.File.WriteAllText(@"C:\git\CSharpTester\CSharpTester\Resources\bOut.txt", lines);
            Console.Read();
        }

        // note: gurantee match so we will have sth if we iterate through the sorted papers
        private static string solve(List<List<int>> papers)
        {
            // sort papers. pick top 2 and add them to ColList and RowList

            // for steps 1 to n
            // peek at top 2. 
            // if top 2's [step] matches, match 1 to col, match 1 to row. 
            // otherwise we have found our missing element. put either col or row back, match the other 

            var n = papers[0].Count;
            papers.Sort((list1, list2) =>
            {
                //var n = list1.Count;
                for (var i = 0; i < n; i++)
                {
                    if (list1[i] == list2[i]) continue;
                    return list1[i] - list2[i];
                }
                return 0;
            });

            var missingIndex = -1;
            for (var i = 0; i < papers.Count;)
            {
                if (i + 1 == papers.Count || papers[i][i / 2] != papers[i + 1][i / 2])
                {
                    // we found our missing index
                    missingIndex = i / 2;
                    break;
                }
                i = i + 2;
            }

            bool missingIndexIsRow = false;
            var colList = new List<List<int>>();
            var rowList = new List<List<int>>();
            var missingPaperPlaceHolder = new List<int>();
            for (var i = 0; i < n; i++) missingPaperPlaceHolder.Add(-1);

            for (var step = 0; step < papers.Count;)
            {
                var temp = papers[step];
                List<int> temp2;
                var thisStepHasMissing = step / 2 == missingIndex;

                if (thisStepHasMissing)
                {
                    var listTemp = new List<int>();
                    for (var i = 0; i < n; i++) listTemp.Add(-1);
                    //temp2 = missingPaperPlaceHolder;
                    papers.Insert(step + 1, missingPaperPlaceHolder); // to keep the steps even
                    missingIndexIsRow = !isRow(temp, step / 2, colList, rowList); // should be opposite from temp1
                    temp2 = missingPaperPlaceHolder;
                }
                else
                {
                    temp2 = papers[step + 1];
                }

                if (isRow(temp, step / 2, colList, rowList))
                {
                    rowList.Add(temp);
                    colList.Add(temp2);
                }
                else
                {
                    colList.Add(temp);
                    rowList.Add(temp2);
                }

                step += 2;
            }

            // generate the missing based on the missing's index AND col/row status
            var solution = new List<int>();

            if (missingIndexIsRow)
            {
                for (var i = 0; i < n; i++) solution.Add(colList[i][missingIndex]);
                //for (var i = missingIndex + 1; i < n; i++) solution.Add(rowList[missingIndex][i]);
            }
            else
            {
                for (var i = 0; i < n; i++) solution.Add(rowList[i][missingIndex]);
                //for (var i = missingIndex + 1; i < n; i++) solution.Add(colList[missingIndex][i]);
            }
            return string.Join(" ", solution);
        }

        /*
        NEED TO FIGURE OUT HOW TO READ ROW VS COLUMN COMPARISIOMN!!! 
            */
        private static bool isRow(List<int> temp, int step, List<List<int>> colList, List<List<int>> rowList)
        {
            if (step == 0) return true;
            var n = temp.Count;
            for (var i = 0; i < step; i++)
            {
                if (colList[i][step] == -1) break; // we dont consider this
                if (colList[i][step] != temp[i]) return false;
            }
            // we cannot see beyond step
            /*for(var i = step; i < n; i++)
            {
                if (rowList[step - 1][i] == -1) break; // we dont consider this
                if (rowList[step - 1][i] != temp[i]) return false;
            }*/
            // need to add another check for is col. if this is true, we truen false 
            var isCol = true;
            for (var i = 0; i < step; i++)
            {
                if (rowList[i][step] == -1) break; // we dont consider this
                if (rowList[i][step] != temp[i]) isCol = false;
            }
            if (isCol) return false;
            return true;
        }
    }
}
}
