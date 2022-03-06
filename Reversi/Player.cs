using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    abstract class Player
    {
        protected DiskColor color;
        protected Board board;

        public Player(DiskColor color, Board board)
        {
            this.color = color;
            this.board = board;
        }

        public bool CanPut()
        {
            return this.board.CanPutAnyDisk(this.color);
        }

        public int CountDisk()
        {
            return board.CountDisks(color);
        }

        public void Pass()
        {
            Graphics.AddLog(color.ToJapaneseString() + ":パス");
        }

        protected void AddLog(SquareCoordinate putPosition)
        {
            if(this.color == DiskColor.White)
            {
                Graphics.AddLog(color.ToJapaneseString() + ":" + putPosition.ToString() + "\r\n");
            }
            else
            {
                Graphics.AddLog(color.ToJapaneseString() + ":" + putPosition.ToString() + "     ");
            }
        }

        public abstract bool PutDisk();
    }

    class Human : Player
    {
        public Human(DiskColor color, Board board) : base(color, board)
        {
            ;
        }


        public override bool PutDisk()
        {
            if (board.IsDiskFlipping())
            {
                return false;
            }
            if (!board.CanPutAnyDisk(this.color))
            {
                return false;
            }
            if (Mouse.mousePoint.X > 0 && Mouse.mousePoint.X < 640 && Mouse.mousePoint.Y > 0 && Mouse.mousePoint.Y < 640)
            {
                SquareCoordinate coordinate = new SquareCoordinate(Mouse.mousePoint.X / 80, Mouse.mousePoint.Y / 80);
                if (board.CanPutDisk(coordinate, this.color))
                {
                    AddLog(coordinate);
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
        public CPU(DiskColor color, Board board) : base(color, board)
        {
            ;
        }

        public override bool PutDisk()
        {
            if (board.IsDiskFlipping())
            {
                return false;
            }

            if (!board.CanPutAnyDisk(this.color))
            {
                return false;
            }

            int turn = board.CountDisks();

            CPUAlgorithm cpuAlgorithm = new CPUAlgorithm();
            UInt64 player, opponent;
            (player, opponent) = board.GetDisks(color.ToInt());
            UInt64 put = cpuAlgorithm.negaMax(player, opponent, 7, turn);
            board.PutDisk(BBoard.Index2Coordinate(put), base.color);
            AddLog(BBoard.Index2Coordinate(put));
            return true;
        }
    }

}
