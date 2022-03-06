using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    internal class Disk
    {
        private SquareCoordinate coordinate;
        private DiskColor color;
        private int flipState;

        public Disk(SquareCoordinate coordinate, DiskColor color)
        {
            this.coordinate = coordinate;
            this.color = color;
            flipState = 0;
        }

        public bool IsFlipNext()
        {
            return flipState == 10;
        }

        public bool IsFlipping()
        {
            return flipState != 0;
        }

        public void Flip()
        {
            flipState = 1;
        }

        public void Update()
        {
            if(flipState != 0)
            {
                flipState += 1;
            }
            if(flipState == 15)
            {
                ChangeColor();
            }
            if(flipState == 30)
            {
                flipState = 0;
            }
        }

        private void ChangeColor()
        {
            if(this.color == DiskColor.White)
            {
                this.color = DiskColor.Black;
            }
            else
            {
                this.color = DiskColor.White;
            }
        }

        public void Draw(System.Drawing.Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            SolidBrush brush;
            brush = new SolidBrush(color.ToColor());

            // (20, 20) から (200, 200) まで直線を描画
            int size = 80;
            Point center = new Point(this.coordinate.posX * size + size / 2, this.coordinate.posY * size + size / 2);
            Rectangle rectangle = RectangleCenter(
                center, size * 4 / 5 * Math.Abs(15 - this.flipState) / 15, size * 4 / 5);
            g.FillEllipse(brush, rectangle);
            g.DrawEllipse(pen, rectangle);   // 楕円


            brush.Dispose();
            pen.Dispose();
            //g.Dispose();
        }

        private Rectangle RectangleCenter(Point pos, int w, int h)
        {
            return new Rectangle(pos.X - w / 2, pos.Y - h / 2, w, h);
        }
    }
}
