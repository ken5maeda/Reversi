using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class Disks
    {
        private UInt64 disks;

        public Disks(UInt64 disks)
        {
            this.disks = disks;
        }

        public void Put(int pos)
        {
            disks |= 1UL << pos;
        }

        public int CountDisks()
        {
            UInt64 bit = this.disks;

            bit = (bit & 0x5555555555555555) + (bit >> 1 & 0x5555555555555555);
            bit = (bit & 0x3333333333333333) + (bit >> 2 & 0x3333333333333333);
            bit = (bit & 0x0f0f0f0f0f0f0f0f) + (bit >> 4 & 0x0f0f0f0f0f0f0f0f);
            bit = (bit & 0x00ff00ff00ff00ff) + (bit >> 8 & 0x00ff00ff00ff00ff);
            bit = (bit & 0x0000ffff0000ffff) + (bit >> 16 & 0x0000ffff0000ffff);
            bit = (bit & 0x00000000ffffffff) + (bit >> 32 & 0x00000000ffffffff);

            return (int)bit;
        }
    }

    internal class LegalMoves
    {
        private UInt64 legalMoves;

        public LegalMoves(UInt64 legalMoves)
        {
            this.legalMoves = legalMoves;
        }

        public bool CanPut(UInt64 disk)
        {
            return (this.legalMoves & disk) != 0;
        }
    }


}
