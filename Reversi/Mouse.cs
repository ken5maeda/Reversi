using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class Mouse
    {
        public static Point mousePoint;

        public static void SetMousePoint(Point point)
        {
            mousePoint = point;
        }
    }
}
