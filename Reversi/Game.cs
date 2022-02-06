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
            players[0] = new Human(Color.Black, board);
            players[1] = new Human(Color.White, board);
            turn = 0;
        }

        public async Task StartAsync()
        {
            await Task.Run(() => Start());
        }

        public void Start()
        {
            while (true)
            {
                if (players[turn].PutDisk())
                {
                    turn = (turn + 1) % 2;
                }
            }

        }
        public void Update()
        {
            board.Update();
        }

        public void Print(Graphics g)
        {
            board.Print(g);
        }
    }
}
