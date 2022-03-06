using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Square
    {

    }
    internal class Board
    {
        private Disk[] disks;
        private BitBoard bitBoard;

        public Board()
        {
            disks = new Disk[64];
            bitBoard = new BitBoard(0x0000000810000000, 0x0000001008000000);
            SetDisk(new SquareCoordinate(3, 3), DiskColor.White);
            SetDisk(new SquareCoordinate(4, 4), DiskColor.White);
            SetDisk(new SquareCoordinate(3, 4), DiskColor.Black);
            SetDisk(new SquareCoordinate(4, 3), DiskColor.Black);

        }

        public int CountDisks(DiskColor color)
        {
            return bitBoard.CountDisks(color);
        }

        public int CountDisks()
        {
            int count = 0;
            count += bitBoard.CountDisks(DiskColor.Black);
            count += bitBoard.CountDisks(DiskColor.White);
            return count;
        }


        public (UInt64 player, UInt64 opponent) GetDisks(int myColor)
        {
            return bitBoard.GetDisks(myColor);
        }
        

        public void Update()
        {
            FlipDisk();
            UpdateDisksState();
        }

        public void UpdateDisksState()
        {
            foreach (Disk disk in disks)
            {
                disk?.Update();
            }
        }

        public bool IsDiskFlipping()
        {
            bool flag = false;
            foreach (Disk disk in disks)
            {
                if(disk?.IsFlipping() ?? false)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public void FlipDisk()
        {
            for(int i = 1; i < 63; i++)
            {
                if (bitBoard.IsWaitFlipping(i))
                {
                    if (disks[i + 1]?.IsFlipNext() ?? false)
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (disks[i - 1]?.IsFlipNext() ?? false)
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (i > 7 && (disks[i - 8]?.IsFlipNext() ?? false))
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (i < 56 && (disks[i + 8]?.IsFlipNext() ?? false))
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (i > 6 && (disks[i - 7]?.IsFlipNext() ?? false))
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (i > 8 && (disks[i - 9]?.IsFlipNext() ?? false))
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (i < 57 && (disks[i + 7]?.IsFlipNext() ?? false))
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                    else if (i < 55 && (disks[i + 9]?.IsFlipNext() ?? false))
                    {
                        disks[i].Flip();
                        bitBoard.CompleteFlip(i);
                    }
                }
            }
        }

        public bool CanPutDisk(SquareCoordinate point, DiskColor color)
        {
            bitBoard.UpdateLegalMoves(color);
            return bitBoard.CanPutDisk(point);
        }

        public bool CanPutAnyDisk(DiskColor color)
        {
            bitBoard.UpdateLegalMoves(color);
            return bitBoard.CanPutAnyDisk();
        }

        private void SetDisk(SquareCoordinate point, DiskColor color)
        {
            Disk disk = new Disk(point, color);
            disks[point.ToIndex()] = disk;
        }

        public void PutDisk(SquareCoordinate point, DiskColor color)
        {
            bitBoard.PutDisk(point, (int)color);
            Disk disk = new Disk(point, color);
            disks[point.ToIndex()] = disk;

            int index = point.ToIndex();
            if (index > 0 && bitBoard.IsWaitFlipping(index - 1))
            {
                disks[index - 1].Flip();
                bitBoard.CompleteFlip(index - 1);
            }
            if (index < 63 && bitBoard.IsWaitFlipping(index + 1))
            {
                disks[index + 1].Flip();
                bitBoard.CompleteFlip(index + 1);
            }
            if (index > 6 && bitBoard.IsWaitFlipping(index - 7))
            {
                disks[index - 7].Flip();
                bitBoard.CompleteFlip(index - 7);
            }
            if (index > 7 && bitBoard.IsWaitFlipping(index - 8))
            {
                disks[index - 8].Flip();
                bitBoard.CompleteFlip(index - 8);
            }
            if (index > 8 && bitBoard.IsWaitFlipping(index - 9))
            {
                disks[index - 9].Flip();
                bitBoard.CompleteFlip(index - 9);
            }
            if (index < 55 && bitBoard.IsWaitFlipping(index + 9))
            {
                disks[index + 9].Flip();
                bitBoard.CompleteFlip(index + 9);
            }
            if (index < 56 && bitBoard.IsWaitFlipping(index + 8))
            {
                disks[index + 8].Flip();
                bitBoard.CompleteFlip(index + 8);
            }
            if (index < 57 && bitBoard.IsWaitFlipping(index + 7))
            {
                disks[index + 7].Flip();
                bitBoard.CompleteFlip(index + 7);
            }

        }

        public void Print(System.Drawing.Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Green);
            g.FillRectangle(brush, 0, 0, 640, 640);
            brush.Dispose();
            Pen pen = new Pen(Color.Black, 2);
            for(int i = 0; i < 9; i++)
            {
                g.DrawLine(pen, 0, i * 80, 640, i * 80);
                g.DrawLine(pen, i * 80, 0, i * 80, 640);
            }
            pen.Dispose();
            foreach (var disk in this.disks)
            {
                disk?.Draw(g);
            }
        }
    }
}
