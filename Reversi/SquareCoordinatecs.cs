using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class SquareCoordinate
    {
        private int _posX;
        private int _posY;

        public int posX
        {
            get { return _posX; }
        }

        public int posY
        {
            get { return _posY; }
        }
        public SquareCoordinate(int poxX, int posY)
        {
            if ((posX < 0 || 8 < posX) || (posY < 0 || 8 < posY))
            {
                throw new ArgumentOutOfRangeException();
            }
            this._posX = poxX;
            this._posY = posY;
        }

        public int ToIndex()
        {
            int index = _posY * 8 + _posX;
            return index;
        }

        public override string ToString()
        {
            string coordinate = (char)('A' + _posY) + (_posX + 1).ToString();
            return coordinate;
        }
    }
}
