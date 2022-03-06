using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class BBoard
    {
		static public UInt64 GetLegalMoves(UInt64 player, UInt64 opponent)
		{
			UInt64 blank = ~(player | opponent);

			// 左側方向の処理
			UInt64 o = opponent & 0x7e7e7e7e7e7e7e7e;
			UInt64 t = o & (player << 1);
			t |= o & (t << 1);
			t |= o & (t << 1);
			t |= o & (t << 1);
			t |= o & (t << 1);
			t |= o & (t << 1);
			UInt64 legalMoves = blank & (t << 1);

			// 右方向の処理
			t = o & (player >> 1);
			t |= o & (t >> 1);
			t |= o & (t >> 1);
			t |= o & (t >> 1);
			t |= o & (t >> 1);
			t |= o & (t >> 1);
			legalMoves |= blank & (t >> 1);

			// 上方向の処理
			o = opponent & 0x00ffffffffffff00;
			t = o & (player << 8);
			t |= o & (t << 8);
			t |= o & (t << 8);
			t |= o & (t << 8);
			t |= o & (t << 8);
			t |= o & (t << 8);
			legalMoves |= blank & (t << 8);

			// 下方向の処理
			t = o & (player >> 8);
			t |= o & (t >> 8);
			t |= o & (t >> 8);
			t |= o & (t >> 8);
			t |= o & (t >> 8);
			t |= o & (t >> 8);
			legalMoves |= blank & (t >> 8);

			// 左上方向の処理
			o = opponent & 0x007e7e7e7e7e7e00;
			t = o & (player << 9);
			t |= o & (t << 9);
			t |= o & (t << 9);
			t |= o & (t << 9);
			t |= o & (t << 9);
			t |= o & (t << 9);
			legalMoves |= blank & (t << 9);

			// 右上方向の処理
			t = o & (player << 7);
			t |= o & (t << 7);
			t |= o & (t << 7);
			t |= o & (t << 7);
			t |= o & (t << 7);
			t |= o & (t << 7);
			legalMoves |= blank & (t << 7);

			// 左下方向の処理
			t = o & (player >> 7);
			t |= o & (t >> 7);
			t |= o & (t >> 7);
			t |= o & (t >> 7);
			t |= o & (t >> 7);
			t |= o & (t >> 7);
			legalMoves |= blank & (t >> 7);

			// 右下方向の処理
			t = o & (player >> 9);
			t |= o & (t >> 9);
			t |= o & (t >> 9);
			t |= o & (t >> 9);
			t |= o & (t >> 9);
			t |= o & (t >> 9);
			legalMoves |= blank & (t >> 9);

			return legalMoves;
		}

		static public UInt64 PutDisk(UInt64 player, UInt64 opponent, UInt64 put)
		{
			// 左側方向の処理
			UInt64 o = opponent & 0x7e7e7e7e7e7e7e7e;
			UInt64 flips = 0, rec = 0, t = put << 1;
			while ((t & o) != 0)
			{
				rec |= t;
				t <<= 1;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 右方向の処理
			t = put >> 1;
			while ((t & o) != 0)
			{
				rec |= t;
				t >>= 1;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 上方向の処理
			o = opponent & 0x00ffffffffffff00;
			t = put << 8;
			while ((t & o) != 0)
			{
				rec |= t;
				t <<= 8;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 下方向の処理
			t = put >> 8;
			while ((t & o) != 0)
			{
				rec |= t;
				t >>= 8;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 左上方向の処理
			o = opponent & 0x007e7e7e7e7e7e00;
			t = put << 9;
			while ((t & o) != 0)
			{
				rec |= t;
				t <<= 9;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 右上方向の処理
			t = put << 7;
			while ((t & o) != 0)
			{
				rec |= t;
				t <<= 7;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 左下方向の処理
			t = put >> 7;
			while ((t & o) != 0)
			{
				rec |= t;
				t >>= 7;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}
			rec = 0;

			// 右下方向の処理
			t = put >> 9;
			while ((t & o) != 0)
			{
				rec |= t;
				t >>= 9;
			}
			if ((t & player) != 0)
			{
				flips |= rec;
			}

			return flips;
		}

		static public int CountDisks(UInt64 bit)
		{
			bit = (bit & 0x5555555555555555) + (bit >> 1 & 0x5555555555555555);
			bit = (bit & 0x3333333333333333) + (bit >> 2 & 0x3333333333333333);
			bit = (bit & 0x0f0f0f0f0f0f0f0f) + (bit >> 4 & 0x0f0f0f0f0f0f0f0f);
			bit = (bit & 0x00ff00ff00ff00ff) + (bit >> 8 & 0x00ff00ff00ff00ff);
			bit = (bit & 0x0000ffff0000ffff) + (bit >> 16 & 0x0000ffff0000ffff);
			bit = (bit & 0x00000000ffffffff) + (bit >> 32 & 0x00000000ffffffff);

			return (int)bit;
		}

		static public SquareCoordinate Index2Coordinate(UInt64 disk)
		{
			int i = 0;
			while ((disk & 1UL << i) == 0)
			{
				i++;
			}
			return new SquareCoordinate(i % 8, i / 8);

		}
	}
}
