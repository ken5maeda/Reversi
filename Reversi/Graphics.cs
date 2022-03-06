using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Reversi
{
    static internal class Graphics
    {
        static Label label;
        static TextBox textBox;
        static public void Init(Label lbl, TextBox tbx)
        {
            label = lbl;
            textBox = tbx;
        }

        static public void SetText(string text)
        {
            label.Text = text;
        }

        static public void AddLog(string text)
        {
            //textBox.AppendText(text + "\r\n");
            textBox.AppendText(text);
        }
    }
}
