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

		delegate int Evaluate(UInt64 playerDiscs, UInt64 opponentDiscs);

		int eval(UInt64 playerDiscs, UInt64 opponentDiscs)
		{
			int eval = 0;

			eval += BBoard.CountDisks(playerDiscs & 0x8100000000000081) * 50;
			eval -= BBoard.CountDisks(opponentDiscs & 0x8100000000000081) * 50;
			eval += BBoard.CountDisks(playerDiscs);
			eval -= BBoard.CountDisks(opponentDiscs);
			UInt64 legalMoves = BBoard.GetLegalMoves(playerDiscs, opponentDiscs);
			eval += BBoard.CountDisks(legalMoves) * 5;
			legalMoves = BBoard.GetLegalMoves(opponentDiscs, playerDiscs);
			eval -= BBoard.CountDisks(legalMoves) * 5;
			return eval;
		}

		int evalEnd(UInt64 playerDiscs, UInt64 opponentDiscs)
		{
			int eval = 0;
			eval += BBoard.CountDisks(playerDiscs);
			eval -= BBoard.CountDisks(opponentDiscs);
			return eval;
		}

		public int innerNegaMax(UInt64 playerDiscs, UInt64 opponentDiscs, int depth, bool isBeforePass)
		{
			UInt64 legalMoves = BBoard.GetLegalMoves(playerDiscs, opponentDiscs);

			if (depth == 0 || (legalMoves == 0 && isBeforePass))
			{
				return eval(playerDiscs, opponentDiscs);
			}

			if (legalMoves == 0)
			{
				return -innerNegaMax(opponentDiscs, playerDiscs, depth - 1, true);
			}

			int max_score = -1000;
			for (int i = 0; i < 64; i++)
			{
				UInt64 put = legalMoves & (1UL << i);
				if (put != 0)
				{
					UInt64 flips = BBoard.PutDisk(playerDiscs, opponentDiscs, put);
					int score = -innerNegaMax(opponentDiscs ^ flips, playerDiscs ^ (put | flips), depth - 1, false);
					if (max_score < score)
					{
						max_score = score;
					}
				}
			}
			return max_score;
		}

		private int innerNegaAlpha(UInt64 playerDiscs, UInt64 opponentDiscs, int depth, int turn, int alpha, int beta, bool isBeforePass)
		{
			UInt64 legalMoves = BBoard.GetLegalMoves(playerDiscs, opponentDiscs);
			if (depth == 0 || (legalMoves == 0 && isBeforePass))
			{
				return eval(playerDiscs, opponentDiscs);
			}

			if (legalMoves == 0)
			{
				return -innerNegaAlpha(opponentDiscs, playerDiscs, depth - 1, turn + 1, -beta, -alpha, true);
			}
			for (int i = 0; i < 64; i++)
			{
				UInt64 put = legalMoves & (1UL << i);
				if (put != 0)
				{
					UInt64 flips = BBoard.PutDisk(playerDiscs, opponentDiscs, put);
					alpha = Math.Max(alpha, -innerNegaAlpha(opponentDiscs ^ flips, playerDiscs ^ (put | flips), depth - 1, turn + 1, -beta, -alpha, false));
					if (alpha >= beta)
					{
						break;
					}
				}
			}
			return alpha;
		}

		private int innerNegaAlphaDelegate(UInt64 playerDiscs, UInt64 opponentDiscs, int depth, int turn, int alpha, int beta, bool isBeforePass, Evaluate evaluate)
		{
			UInt64 legalMoves = BBoard.GetLegalMoves(playerDiscs, opponentDiscs);
			if (depth == 0 || (legalMoves == 0 && isBeforePass))
			{
				return evaluate(playerDiscs, opponentDiscs);
			}

			if (legalMoves == 0)
			{
				return -innerNegaAlphaDelegate(opponentDiscs, playerDiscs, depth - 1, turn + 1, -beta, -alpha, true, evaluate);
			}
			for (int i = 0; i < 64; i++)
			{
				UInt64 put = legalMoves & (1UL << i);
				if (put != 0)
				{
					UInt64 flips = BBoard.PutDisk(playerDiscs, opponentDiscs, put);
					alpha = Math.Max(alpha, -innerNegaAlphaDelegate(opponentDiscs ^ flips, playerDiscs ^ (put | flips), depth - 1, turn + 1, -beta, -alpha, false, evaluate));
					if (alpha >= beta)
					{
						break;
					}
				}
			}
			return alpha;
		}

		public UInt64 negaMax(UInt64 playerDiscs, UInt64 opponentDiscs, int depth, int turn)
		{
			UInt64 legalMoves = BBoard.GetLegalMoves(playerDiscs, opponentDiscs);
			if (legalMoves == 0)
			{
				return 0;
			}

			UInt64 put = 1;
			int max_score = -10000;
			Evaluate evaluate;
			if(turn < 48)
            {
				evaluate = eval;
			}
            else
            {
				depth = 100;
				evaluate = evalEnd;
			}
			for (int i = 0; i < 64; i++)
			{
				UInt64 tempPut = legalMoves & (1UL << i);
				if (tempPut != 0)
				{
					UInt64 flips = BBoard.PutDisk(playerDiscs, opponentDiscs, tempPut);
					//int score = innerMiniMax(opponentDiscs ^ flips, playerDiscs ^ (tempPut | flips), depth - 1, 1, false);
					//int score = -innerNegaMax(opponentDiscs ^ flips, playerDiscs ^ (tempPut | flips), depth - 1, false);
					//int score = -innerNegaAlpha(opponentDiscs ^ flips, playerDiscs ^ (tempPut | flips), depth - 1, 1, -10000, 10000, false);
					int score = -innerNegaAlphaDelegate(opponentDiscs ^ flips, playerDiscs ^ (tempPut | flips), depth - 1, 1, -10000, 10000, false , evaluate);
					if (max_score < score)
					{
						max_score = score;
						put = tempPut;
					}
				}
			}
			return put;
		}

	}
}
