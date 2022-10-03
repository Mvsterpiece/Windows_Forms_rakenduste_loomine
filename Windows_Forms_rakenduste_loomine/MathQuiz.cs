using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms_rakenduste_loomine
{
    public class MathQuiz : MinuVorm
    {

        TableLayoutPanel table;
        string text;
        string title;
        Label label;
        Label label2;
        Label l;
        Label timeLabel;
        Button start;
        NumericUpDown numb;


        string[] tehed = new string[4] { "+", "-", "*", "/" };
        public MathQuiz(string title)
        {
            Text = "Math Quiz";
            Size = new Size(500, 400);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill
            };
            table = new TableLayoutPanel
            {

                Dock = DockStyle.Fill,
                ColumnStyles = { new ColumnStyle(SizeType.Percent, 85), new ColumnStyle(SizeType.Percent, 15) },
                RowStyles = { new RowStyle(SizeType.Percent, 90), new RowStyle(SizeType.Percent, 10) },
                BorderStyle = BorderStyle.Fixed3D,
                AutoSize = false,


            };

            Button button = new Button { Text = "Math Quiz", Location = new Point(200, 50), BackColor = Color.Blue };
            Label label = new Label { Text = "Time Left", AutoSize = true, };
            label.Location = new Point(15, 15);
            Label label2 = new Label { BorderStyle = BorderStyle.FixedSingle, AutoSize = false, };
            label2.Location = new Point(80, 12);
            Button start = new Button { Text = "Start", Location = new Point(200, 12), };
            start.Click += Start_Click;


            string[] names = { "rowNum", "rowSign", "rowNums", "rowEquals", "RowResult" };
            string[] text = { "?", "+", "-", "*", "/", "=" };
            for (int j = 1; j < 5; j++)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (i == 5)
                    {
                        numb = new NumericUpDown
                        {
                            Width = 100,
                            Name = "sum" + j,
                        };
                        table.Controls.Add(numb);
                        table.SetCellPosition(numb, new TableLayoutPanelCellPosition(i - 1, j));
                    }
                    else
                    {
                        var lblText = text[0];
                        if (names[i - 1] == "rowSign") lblText = text[j];
                        else if (names[i - 1] == "rowEquals") lblText = text.Last();
                        l = new Label
                        {
                            Text = lblText,
                            AutoSize = false,
                            Size = new Size(60, 50),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Name = names[i - 1] + j,
                        };
                        table.Controls.Add(l);
                        table.SetCellPosition(l, new TableLayoutPanelCellPosition(i - 1, j));
                    }
                }
            }


            this.Controls.Add(start);
            this.Controls.Add(label);
            this.Controls.Add(label2);
            this.Controls.Add(table);
        }
        public void Start_Click(object sender, EventArgs e)
        {
            Label l;
            Button b = (sender as Button);
            b.Enabled = false;
            Random rnd = new Random();
            for (int j = 1; j < 5; j++)
            {
                for (int i = 0; i < 3; i += 2)
                {
                    if (j == 2)
                    {
                        int a = 1, c = 2;
                        while (a < c)
                        {
                            a = rnd.Next(1, 12);
                            c = rnd.Next(1, 12);
                        }
                        l = (Label)table.GetControlFromPosition(i, j);
                        l.Text = a.ToString();

                        l = (Label)table.GetControlFromPosition(i + 2, j);
                        l.Text = c.ToString();
                        break;
                    }
                    else if (j == 4)
                    {
                        int a, c;
                        a = rnd.Next(1, 12);
                        c = rnd.Next(1, 12);

                        l = (Label)table.GetControlFromPosition(i, j);
                        l.Text = (a * c).ToString();

                        l = (Label)table.GetControlFromPosition(i + 2, j);
                        l.Text = c.ToString();
                        break;
                    }
                    else
                    {
                        l = (Label)table.GetControlFromPosition(i, j);
                        l.Text = rnd.Next(0, 101).ToString();
                    }
                }
            }
        }
        int tik = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            tik++;
            timeLabel.Text = tik.ToString();
            timeLabel.Location = new Point(300, 300);
            this.Controls.Add(timeLabel);
        }


    }
}
