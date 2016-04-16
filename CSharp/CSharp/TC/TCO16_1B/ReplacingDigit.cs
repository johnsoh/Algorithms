using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TC.TCO16_1B
{
    class ReplacingDigit
    {
        public static void testc()
        {
            var a = getMaximumStockWorth(new int[] { 100, 90 }, new int[] { 0, 0, 0, 0, 2, 1, 0, 0, 0 });
        }

        public static int getMaximumStockWorth(int[] A, int[] D)
        {
            // prepare stickers
            var stickers = new List<int>();
            stickers.Add(0);
            for (var i = 1; i < 10; i++)
            {
                stickers.Add(D[i - 1]);
            }
            for (var stickerI = stickers.Count - 1; stickerI >= 0; stickerI--)
            {
                if (stickers[stickerI] == 0)
                {
                    stickers.RemoveAt(stickerI);
                }
                else
                {
                    break;
                }
            }

            // prepare prices // sort his items into grps of digits. dict -> LL
            var ordering = new Dictionary<int, List<int>>();
            foreach (var a in A)
            {
                var digitLen = lenOf(a);
                if (!ordering.ContainsKey(digitLen))
                {
                    ordering.Add(digitLen, new List<int>());
                }
                ordering[digitLen].Add(a);
            }
            var keys = ordering.Keys.ToList();
            keys.Sort();


            // for positions N to 1
            // sort the numbers based on the position. 
            // take best sticker and replace weakest position. 
            // if it makes it better, really replace. otherwise, go down to make make N lesser

            var underconsideration = new List<int>();
            var key = -1;
            while (true)
            {
                if (keys.Count == 0 || stickers.Sum() == 0) break;
                key = keys[keys.Count - 1];
                keys.RemoveAt(keys.Count - 1);

                underconsideration.AddRange(ordering[key]);
                sortBasedOnPos(underconsideration, key); // reverse sort!
                var considerationSize = underconsideration.Count;
                var toAddToConsideration = new List<int>();
                for (var ptr = considerationSize - 1; ptr >= 0; ptr--)
                {
                    var oldValue = underconsideration[ptr];
                    var nthDigit = getNthDigit(oldValue, key);
                    var nextBestSticker = stickers.Count - 1;
                    if (nthDigit < nextBestSticker)
                    {
                        var oldRemove = getNthDigit(oldValue, key) * (int)Math.Pow(10, key - 1);
                        var newAdd = nextBestSticker * (int)Math.Pow(10, key - 1);
                        underconsideration.Remove(oldValue);
                        toAddToConsideration.Add(oldValue - oldRemove + newAdd);

                        // remove the sticker as well! if cout == zero, cut it out too! 
                        stickers[stickers.Count - 1]--;
                        for (var stickerI = stickers.Count - 1; stickerI >= 0; stickerI--)
                        {
                            if (stickers[stickerI] == 0)
                            {
                                stickers.RemoveAt(stickerI);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        // break n go to next best 
                    }
                }
                underconsideration.AddRange(toAddToConsideration);
            }

            // add up untouched keys to underconsideratio before doing sum n returning 
            foreach (var k in keys)
            {
                underconsideration.AddRange(ordering[k]);
            }
            return underconsideration.Sum();

        }

        private static int getNthDigit(int oldValue, int key)
        {
            if (oldValue < (int)Math.Pow(10, key - 1))
            {
                return 0;
            }
            while (key != 1)
            {
                oldValue = oldValue / 10;
                key--;
            }
            return oldValue % 10;
        }

        // reverse sort! 
        private static void sortBasedOnPos(List<int> underconsideration, int position)
        {
            underconsideration.Sort((a, b) => getNthDigit(b, position) - getNthDigit(a, position));
        }

        private static int lenOf(int a)
        {
            var len = 1;
            while (a >= 10)
            {
                len++;
                a = a / 10;
            }
            return len;
        }
    }
}
