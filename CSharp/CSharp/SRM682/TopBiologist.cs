using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//42.12 left 
namespace CSharp.SRM682
{
    class TopBiologist
    {

        public static String findShortestNewSequence(String sequence)
        {
            // try for each len 
            
            for(var i = 1; i < 6; i++)
            {
                var list = new List<string>();
                GenerateList(list, string.Empty, i);
                foreach (string s in list)
                {
                    if (!sequence.Contains(s))
                        return s;
                }
            }
            return "AAAAAA";
        }

        public static void GenerateList(List<String> list, string s, int len)
        {
            if(len == 0)
            {
                list.Add(s);
                return;
            }

            len--;
            GenerateList(list, s + 'A', len);
            GenerateList(list, s + 'T', len);
            GenerateList(list, s + 'G', len);
            GenerateList(list, s + 'C', len);
        }

        public static void test()
        {
            var a = findShortestNewSequence("");
            var a1 = findShortestNewSequence("AGACGACGGAGAACGA");
            var a2 = findShortestNewSequence("A");
            var a3 = findShortestNewSequence("AAGATACACCGGCTTCGTG");
            var list = new List<string>();
            GenerateList(list, string.Empty, 5);
            string a4 = "";
            foreach(string s in list)
            {
                a4 += s;
            }
            var a5 = findShortestNewSequence(a4);
            Console.ReadKey();

        }
    }


}
