using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public enum DiskColor
    {
        Black = 0,
        White,
    }

    public static class DiskColorExtension
    {
        private static Dictionary<DiskColor, string> viewStrDictionary = new Dictionary<DiskColor, string>()
        {
            {DiskColor.Black,"黒" },
            {DiskColor.White,"白" }
        };

        private static Dictionary<DiskColor, int> viewIntDictionary = new Dictionary<DiskColor, int>()
        {
            {DiskColor.Black, 0 },
            {DiskColor.White, 1 }
        };

        private static Dictionary<DiskColor, Color> viewColorDictionary = new Dictionary<DiskColor, Color>()
        {
            {DiskColor.Black, Color.Black },
            {DiskColor.White, Color.White }
        };

        public static string ToJapaneseString(this DiskColor state)       //<-　拡張メソッド
        {
            return viewStrDictionary[state];
        }

        public static int ToInt(this DiskColor state)       //<-　拡張メソッド
        {
            return viewIntDictionary[state];
        }

        public static Color ToColor(this DiskColor state)       //<-　拡張メソッド
        {
            return viewColorDictionary[state];
        }

    }
}
