using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        Game game;
        Task task;

        public Form1()
        {
            InitializeComponent();

            InitializeOriginalComponent();

            this.newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            this.saveGameToolStripMenuItem.Click += saveGameToolStripMenuItem_Click;
            this.turnToolStripRadioButtonMenuItem1.Click += turnToolStripRadioButtonMenuItem1_Click;
            this.turnToolStripRadioButtonMenuItem2.Click += turnToolStripRadioButtonMenuItem2_Click;
            this.turnToolStripRadioButtonMenuItem3.Click += turnToolStripRadioButtonMenuItem3_Click;

            game = new Game();
            timer = new System.Windows.Forms.Timer()
            {
                Interval = 1,
                Enabled = true,
            };

            timer.Tick += new EventHandler(timer_Tick);
            pictureBox1.Paint += pictureBox1_Paint;

            this.DoubleBuffered = true;  // ダブルバッファリング
            this.BackColor = SystemColors.Window;

            pictureBox1.MouseClick += new MouseEventHandler(Form1_MouseClick);
            Graphics.Init(label1, textBox1);
            this.task =  game.StartAsync();
            //game.Print();
            //t.Wait();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();  // 再描画を促す
            this.game.Update();
            Mouse.ResetMousePoint();
            //game.Print(pictureBox1.CreateGraphics());
            pictureBox1.Refresh();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    game.Print(e.Graphics);
        //}

        protected void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Print(e.Graphics);
        }

        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Mouse.SetMousePoint(e.Location);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            game.Init();
            this.task = game.StartAsync();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            saveFileDialog1.FileName = dt.ToString("yyyyMMddHHmmss") + ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    sw.WriteLine(textBox1.Text);
                }
            }
        }

        private void GameStart()
        {
            Task t = game.StartAsync();
            game = new Game();
        }

        private void turnToolStripRadioButtonMenuItem1_Click(object sender, EventArgs e)
        {
            Option.turn = 0;
        }

        private void turnToolStripRadioButtonMenuItem2_Click(object sender, EventArgs e)
        {
            Option.turn = 1;
        }

        private void turnToolStripRadioButtonMenuItem3_Click(object sender, EventArgs e)
        {
            Option.turn = 2;
        }
    }


}