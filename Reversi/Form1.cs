using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        Game game;
        public Form1()
        {
            game = new Game();
            timer = new System.Windows.Forms.Timer()
            {
                Interval = 2,
                Enabled = true,
            };

            timer.Tick += new EventHandler(timer_Tick);

            this.DoubleBuffered = true;  // ダブルバッファリング
            this.BackColor = SystemColors.Window;
            InitializeComponent();

            this.MouseClick += new MouseEventHandler(Form1_MouseClick);

            Task t =  game.StartAsync();
            //t.Wait();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();  // 再描画を促す
            this.game.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            game.Print(e.Graphics);
        }

        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Mouse.SetMousePoint(e.Location);
        }

    }


}