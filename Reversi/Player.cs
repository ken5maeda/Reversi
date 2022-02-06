using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    abstract class Player
    {
        protected Color color;
        protected Board board;

        public Player(Color color, Board board)
        {
            if (color != Color.White && color != Color.Black)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.color = color;
            this.board = board;
        }
        public abstract bool PutDisk();
    }

    class Human : Player
    {
        public Human(Color color, Board board) : base(color, board)
        {
            ;
        }


        public override bool PutDisk()
        {
            if (board.IsDiskFlipping())
            {
                return false;
            }
            if(Mouse.mousePoint.X > 0 && Mouse.mousePoint.X < 640 && Mouse.mousePoint.Y > 0 && Mouse.mousePoint.Y < 640)
            {
                SquareCoordinate coordinate = new SquareCoordinate(Mouse.mousePoint.X / 80, Mouse.mousePoint.Y / 80);
                if (board.CanPutDisk(coordinate, this.color))
                {
                    board.PutDisk(coordinate, this.color);
                    Mouse.mousePoint = new Point(-1, -1);
                    return true;
                }
            }
            return false;
        }
    }

    class CPU : Player
    {
        public CPU(Color color, Board board) : base(color, board)
        {
            ;
        }

        public override bool PutDisk()
        {
            if (board.IsDiskFlipping())
            {
                return false;
            }

            if (!board.CanPutAnyDisk())
            {
                return false;
            }
            board.PutDisk(new SquareCoordinate(1, 2), base.color);
            return false;
        }

        private void CalcPutPosition()
        {
            UInt64 player, opponent;
            if (base.color == Color.Black)
            {
                (player, opponent) = board.GetDisks(0);
            }
            else
            {
                (player, opponent) = board.GetDisks(1);
            }

            CPUAlgorithm cpuAlgorithm = new CPUAlgorithm();

            UInt64 legalMoves = CPUAlgorithm.GetLegalMoves(player, opponent);
            UInt64 put;
            int max_score = -100000000;
            for(int i = 0; i < 64; i++)
            {
                UInt64 tempPut = legalMoves & (1UL << i);
                if (tempPut != 0)
                {
                    int score = cpuAlgorithm.negaAlpha(player, opponent, 5, -1000, 1000);
                    if(max_score < score)
                    {

                    }
                }
            }
        }
    }

}
