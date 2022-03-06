using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class Game
    {
        Board board;
        Player[] players;
        int turn;

        public Game()
        {
            board = new Board();
            players = new Player[2];
            players[0] = new Human(DiskColor.Black, board);
            players[1] = new CPU(DiskColor.White, board);
            turn = 0;
        }

        public async Task StartAsync()
        {
            await Task.Run(() => Start());
        }

        public void Start()
        {
            Graphics.SetText("黒:" + players[0].CountDisk() + "  白:" + players[1].CountDisk());
            while (true)
            {
                if (players[turn].PutDisk())
                {
                    Graphics.SetText("黒:" + players[0].CountDisk() + "  白:" + players[1].CountDisk());
                    NextTurn();
                    if (IsGameEnd())
                    {
                        break;
                    }
                }
            }
            Graphics.AddLog("黒:" + players[0].CountDisk() + "   白:" + players[1].CountDisk());
        }

        public void Init()
        {
            board = new Board();
            players = new Player[2];
            if(Option.turn == 0)
            {
                players[0] = new CPU(DiskColor.Black, board);
                players[1] = new Human(DiskColor.White, board);
            }
            else if(Option.turn == 1)
            {
                players[0] = new Human(DiskColor.Black, board);
                players[1] = new CPU(DiskColor.White, board);
            }
            else
            {
                players[0] = new Human(DiskColor.Black, board);
                players[1] = new Human(DiskColor.White, board);
            }
            turn = 0;
        }

        private void NextTurn()
        {
            turn = (turn + 1) % 2;
        }
        private bool IsGameEnd()
        {
            if (players[turn].CanPut())
            {
                return false;
            }
            NextTurn();
            if (players[turn].CanPut())
            {
                players[(turn + 1) % 2].Pass();
                return false;
            }
            return true;
        }

        public void Update()
        {
            board.Update();
        }

        public void Print(System.Drawing.Graphics g)
        {
            board.Print(g);
        }
    }
}
