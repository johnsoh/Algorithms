using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class Test
    {
        #region new input accessors
        public static int Int
        {
            get
            {
                return int.Parse(file[++pointer]);
            }
        }

        public static double Double
        {
            get
            {
                return double.Parse(file[++pointer]);
            }
        }

        public static string String
        {
            get
            {
                return file[++pointer].Replace("\"", "");
            }
        }

        public static int[] IntArray
        {
            get
            {
                return nextArray().Select(x => int.Parse(x)).ToArray();
            }
        }

        public static double[] DoubleArray
        {
            get
            {
                return nextArray().Select(x => double.Parse(x)).ToArray();
            }
        }

        public static string[] StringArray
        {
            get
            {
                return nextArray().Select(x => x.Replace("\"", "")).ToArray();
            }
        }

        #endregion

        #region getNext


        public static int nextInt()
        {
            return int.Parse(file[++pointer]);
        }

        public static double nextDouble()
        {
            return double.Parse(file[++pointer]);
        }

        public static string nextLine()
        {
            return file[++pointer];
        }

        public static string[] nextArray()
        {
            return file[++pointer].Replace("{", "").Replace("}","").Split(',');
        }

        public static int[] nextIntArray()
        {
            return nextArray().Select(x => int.Parse(x)).ToArray();
        }

        public static double[] nextDoubleArray()
        {
            return nextArray().Select(x => double.Parse(x)).ToArray();
        }

        public static string[] nextStringArray()
        {
            return nextArray().Select(x => x.Replace("\"", "")).ToArray();
        }

        #endregion


        #region getNext cousins

        public static void nextAnswer()
        {
            // removes 'Returns: ' from next input
            file[pointer] = file[pointer].Replace("Returns: ", "");
        }

        public static bool hasNext()
        {
            return pointer < file.Length;
        }
        #endregion


        #region administrative
        static int pointer = 0;
        static string[] file;

        public static void Init(int textFileChoice = 1)
        {
            pointer = 0;
            string filePath;

            // http://stackoverflow.com/questions/9406474/referencing-text-file-visual-studio-c-sharp
            switch (textFileChoice)
            {
                case 2:
                    filePath = Properties.Resources.TextFile2;
                    break;
                case 3:
                    filePath = Properties.Resources.TextFile3;
                    break;
                default:
                    filePath = Properties.Resources.TextFile1;
                    break;
            }

            file = filePath.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
        }
        #endregion
    }
}
