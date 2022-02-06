using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Reversi
{
    internal class BitBoard
    {
        private UInt64[] disks;
        private UInt64 legalMoves;
        private UInt64 waitFlipping;

        public BitBoard(UInt64 black, UInt64 white)
        {
            this.disks = new  UInt64[2];
            this.disks[0] = black;
            this.disks[1] = white;
			this.waitFlipping = 0;
			UpdateLegalMoves(0);
		}

		public (UInt64 player, UInt64 opponent) GetDisks(int myColor)
		{
			if (myColor == 0)
			{
				return (disks[0], disks[1]);
			}
			else
			{
				return (disks[1], disks[0]);
			}
		}

		public bool IsWaitFlipping(int index)
        {
			return (waitFlipping & (1UL << index)) != 0;
		}

		public void CompleteFlip(int index)
		{
			waitFlipping ^= 1UL << index;
		}


		public void UpdateLegalMoves(int color)
        {
			UInt64 blank = ~(disks[0] | disks[1]);

			UInt64 player, opponent;
			if (color == 0)
            {
				player = disks[0];
				opponent = disks[1];
			}
            else
            {
				player = disks[1];
				opponent = disks[0];
			}

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

			this.legalMoves = legalMoves;
		}

        public bool CanPutDisk(SquareCoordinate coordinate)
        {
            int index = coordinate.ToIndex();
            return (this.legalMoves & (1UL << index)) != 0;
        }

        public bool CanPutAnyDisk()
        {
            return legalMoves != 0;
        }

		public void PutDisk(SquareCoordinate coordinate, int color)
        {
			UInt64 player, opponent;
			if (color == 0)
			{
				player = disks[0];
				opponent = disks[1];
			}
			else
			{
				player = disks[1];
				opponent = disks[0];
			}

			// 左側方向の処理
			UInt64 put = 1UL << coordinate.ToIndex();
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


			player ^= (put | flips);
			opponent ^= flips;

			disks[0] = player;
			disks[1] = opponent;

			if (color == 0)
			{
				disks[0] = player;
				disks[1] = opponent;
			}
			else
			{
				disks[1] = player;
				disks[0] = opponent;
			}

			this.waitFlipping = flips;
		}
    }
}
