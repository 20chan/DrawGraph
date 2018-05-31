using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DrawGraph.Parser;

namespace DrawGraph
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        Func<float, float> func;

        float x_min = -30, x_gap = 0.1f;

        public Form1()
        {
            InitializeComponent();

            func = (x) => (float)Math.Pow(-1, x);
            trackBar1.Value = (int)-x_min;
            g = panel1.CreateGraphics();
            p = Pens.Blue;
            panel1.Paint += Panel1_Paint;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid();
            DrawFunction();
        }

        void DrawGrid()
        {
            g.DrawLine(Pens.Black, panel1.Width / 2, 0, panel1.Width / 2, panel1.Height);
            g.DrawLine(Pens.Black, 0, panel1.Height / 2, panel1.Width, panel1.Height / 2);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            x_min = -trackBar1.Value;
            panel1.Invalidate();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // TODO: 식 파싱
            }
        }

        void DrawFunction()
        {
            float ratio = panel1.Width / (-2 * x_min);
            float prev_x = x_min, prev_y = func(x_min);
            for (var x = x_min + x_gap; x < -x_min; x += x_gap)
            {
                var y = func(x);

                DrawLine(prev_x, prev_y, x, y);

                prev_x = x;
                prev_y = y;
            }

            void DrawLine(float x1, float y1, float x2, float y2)
                => g.DrawLine(
                    p,
                    panel1.Width / 2 + x1 * ratio,
                    panel1.Height / 2 - y1 * ratio,
                    panel1.Width / 2 + x2 * ratio,
                    panel1.Height / 2 - y2 * ratio);
        }
    }
}
