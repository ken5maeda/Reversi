namespace Reversi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private MenuStrip menuStrip;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem turnToolStripMenuItem;
        private ToolStripRadioButtonMenuItem turnToolStripRadioButtonMenuItem1;
        private ToolStripRadioButtonMenuItem turnToolStripRadioButtonMenuItem2;
        private ToolStripRadioButtonMenuItem turnToolStripRadioButtonMenuItem3;
        private ToolStripMenuItem cpuLevelToolStripMenuItem;
        private ToolStripRadioButtonMenuItem cpuLevelToolStripRadioButtonMenuItem1;
        private ToolStripRadioButtonMenuItem cpuLevelToolStripRadioButtonMenuItem2;

        private void InitializeOriginalComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();

            this.gameToolStripMenuItem = new ToolStripMenuItem();
            this.newGameToolStripMenuItem = new ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new ToolStripMenuItem();

            this.optionToolStripMenuItem = new ToolStripMenuItem();
            this.turnToolStripMenuItem = new ToolStripMenuItem();
            this.turnToolStripRadioButtonMenuItem1 = new ToolStripRadioButtonMenuItem();
            this.turnToolStripRadioButtonMenuItem2 = new ToolStripRadioButtonMenuItem();
            this.turnToolStripRadioButtonMenuItem3 = new ToolStripRadioButtonMenuItem();
            this.turnToolStripRadioButtonMenuItem2.Checked = true;
            this.cpuLevelToolStripMenuItem = new ToolStripMenuItem();
            this.cpuLevelToolStripRadioButtonMenuItem1 = new ToolStripRadioButtonMenuItem();
            this.cpuLevelToolStripRadioButtonMenuItem2 = new ToolStripRadioButtonMenuItem();

            gameToolStripMenuItem.Text = "対局";
            newGameToolStripMenuItem.Text = "新規対局";
            saveGameToolStripMenuItem.Text = "対局保存";

            optionToolStripMenuItem.Text = "設定";
            turnToolStripMenuItem.Text = "手番";
            turnToolStripRadioButtonMenuItem1.Text = "CPUが先手";
            turnToolStripRadioButtonMenuItem2.Text = "CPUが後手";
            turnToolStripRadioButtonMenuItem3.Text = "CPUを利用しない";
            cpuLevelToolStripMenuItem.Text = "CPUレベル";
            cpuLevelToolStripRadioButtonMenuItem1.Text = "レベル１";
            cpuLevelToolStripRadioButtonMenuItem2.Text = "レベル２";

            gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                newGameToolStripMenuItem,
                saveGameToolStripMenuItem
            });

            optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                turnToolStripMenuItem,
                cpuLevelToolStripMenuItem
            });

            turnToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                turnToolStripRadioButtonMenuItem1,
                turnToolStripRadioButtonMenuItem2,
                turnToolStripRadioButtonMenuItem3
            });

            cpuLevelToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                cpuLevelToolStripRadioButtonMenuItem1,
                cpuLevelToolStripRadioButtonMenuItem2
            });

            menuStrip.Items.AddRange(new ToolStripItem[] { 
                gameToolStripMenuItem,
                optionToolStripMenuItem
            });
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Text = "ToolStripRadioButtonMenuItem demo";
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(692, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(671, 103);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(167, 584);
            this.textBox1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 640);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 699);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private SaveFileDialog saveFileDialog1;
    }
}