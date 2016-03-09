using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CSharp.Utilities
{
    public class Template
    {
        public static void solve()
        {
            ////////////////
            // Getting text file as resource
            // http://stackoverflow.com/questions/9406474/referencing-text-file-visual-studio-c-sharp
            ////////////////

            var file = Properties.Resources.TextFile1.Split(new string[] {System.Environment.NewLine}, StringSplitOptions.None);
            file.ToList().ForEach(Console.WriteLine);
            var k = "2 2 33 asd";
        }

        // top coder
        public static void coder()
        {
            Test t = new Test();
            Test.Init(1);
            while(Test.hasNext())
            {
                var res = Math.Max(Test.nextInt(), Test.nextInt());
                Test.nextAnswer();
                var ans = Test.nextInt();
                
            }

        }
    }
}
