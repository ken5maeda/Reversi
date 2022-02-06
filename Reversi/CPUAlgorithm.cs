using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class CPUAlgorithm
    {
		int nodeNum, leafNum;
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

		UInt64 putDisc(UInt64 player, UInt64 opponent, UInt64 put)
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

		private int CountDisks(UInt64 bit)
        {
			bit = (bit & 0x5555555555555555) + (bit >> 1 & 0x5555555555555555);
			bit = (bit & 0x3333333333333333) + (bit >> 2 & 0x3333333333333333);
			bit = (bit & 0x0f0f0f0f0f0f0f0f) + (bit >> 4 & 0x0f0f0f0f0f0f0f0f);
			bit = (bit & 0x00ff00ff00ff00ff) + (bit >> 8 & 0x00ff00ff00ff00ff);
			bit = (bit & 0x0000ffff0000ffff) + (bit >> 16 & 0x0000ffff0000ffff);
			bit = (bit & 0x00000000ffffffff) + (bit >> 32 & 0x00000000ffffffff);

			return (int)bit;
		}

		int eval(UInt64 playerDiscs, UInt64 opponentDiscs)
		{
			int eval = 0;

			//eval += corner[playerDiscs & 0x8100000000000081];
			//eval -= corner[opponentDiscs & 0x8100000000000081];
			eval += CountDisks(playerDiscs & 0x8100000000000081) * 20;
			eval -= CountDisks(opponentDiscs & 0x8100000000000081) * 20;
			eval += CountDisks(playerDiscs);
			//eval -= eva(opponentDiscs);
			UInt64 legalMoves = GetLegalMoves(playerDiscs, opponentDiscs);
			//eval += countDiscs(legalMoves);
			eval += CountDisks(legalMoves) * 5;
			legalMoves = GetLegalMoves(opponentDiscs, playerDiscs);
			//eval -= countDiscs(legalMoves);
			eval -= CountDisks(legalMoves) * 5;
			return eval;
		}

		public int negaAlpha(UInt64 playerDiscs, UInt64 opponentDiscs, int depth, int alpha, int beta)
		{
			nodeNum++;
			if (depth == 0)
			{
				leafNum++;
				return eval(playerDiscs, opponentDiscs);
			}

			UInt64 legalMoves = GetLegalMoves(playerDiscs, opponentDiscs);

			if (legalMoves != 0)
			{
				legalMoves = GetLegalMoves(opponentDiscs, playerDiscs);
				if (legalMoves != 0)
				{
					leafNum++;
					return eval(playerDiscs, opponentDiscs);
				}
				return -negaAlpha(opponentDiscs, playerDiscs, depth, -beta, -alpha);
			}

			int score;
			UInt64 put = 1;
			///*
			for (int i = 0; i < 64; i++)
			{
				if ((put & legalMoves) != 0)
				{
					UInt64 flips = putDisc(playerDiscs, opponentDiscs, put);
					score = -negaAlpha(opponentDiscs ^ flips, playerDiscs ^ (put | flips), depth - 1, -beta, -alpha);

					if (score >= beta)
					{
						return score;
					}
					if (alpha < score)
					{
						alpha = score;
					}
				}
				put <<= 1;
			}
			return alpha;

		}
	}
}
