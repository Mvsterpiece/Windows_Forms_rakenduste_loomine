using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms_rakenduste_loomine
{
    public class MatchingGame : MinuVorm
    {
        string title;
        Random rnd = new Random();
        TableLayoutPanel table;
        Label firstClicked = null;
        Label secondClicked = null;
        Label lblTimer;
        Timer timer1 = new Timer { Interval = 750 };
        private Button music;
        int counter = 1;
        List<string> icons = new List<string>() //väärtuste loend, mis ilmuvad hiljem
        {
            "m", "m", "k", "k", "N", "N", "t", "t",
            "Z", "Z", "J", "J", "E", "E", "S", "S"
        };
        public MatchingGame(string title)
        {
            CenterToScreen();
            timer1.Tick += Tick;
            Text = "Matching game";
            ClientSize = new Size(550, 550);
            table = new TableLayoutPanel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
                RowCount = 5,
                ColumnCount = 5
            };

            this.Controls.Add(table);
            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                for (int j = 0; j < 4; j++)
                {

                    Label lbl = new Label
                    {
                        BackColor = Color.LightCyan,
                        Size = new Size(100, 100),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", 48, FontStyle.Bold),
                    };


                    table.Controls.Add(lbl, i, j);
                };

            }
            foreach (Control control in table.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = rnd.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
                iconLabel.ForeColor = iconLabel.BackColor;
                iconLabel.Click += Click;
            }

            lblTimer = new Label //sildi loomine, milles kuvatakse taimerit, on sildil algselt enne alusta nupu vajutamist tekst "--:--:--"
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Italic, GraphicsUnit.Point, 200),
                Name = "lblAnswer",
                Size = new Size(50, 15),
                TabIndex = 5,
                Text = "--:--:--",
            };
            table.Controls.Add(lblTimer, 1, 4);

            music = new Button //taimeri nupu loomine, vajutamisel kuvatakse näited ja algab aja lugemine
            {
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 200),
                Location = new Point(290, 40),
                Name = "button1",
                Size = new Size(100, 35),
                TabIndex = 7,
                Text = "Muusika",
                UseVisualStyleBackColor = true,
            };
            music.Click += new EventHandler(MusicStart);
            table.Controls.Add(music, 0, 4);
        }

        private void MusicStart(object sender, EventArgs e) //funktsioon mis alustab muusika nuppude vajude
        {
            using (var muusika = new SoundPlayer(@"..\..\ashot.wav"))
            {
                muusika.Play();
            }
        }

        private void Click(object sender, EventArgs e) //ikooni kuvamiseks
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                timer1.Start();
            }
        }
        private void Tick(object sender, EventArgs e) //taimeri funktsioon
        {
            timer1.Start();
            if (counter > 0)
            {
                counter = counter + 1;
                lblTimer.Text = counter + " sammu";
            }
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked.ForeColor = firstClicked.ForeColor;
                secondClicked.ForeColor = secondClicked.ForeColor;
            }
            else
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;
            }
            firstClicked = null;
            secondClicked = null;
            timer1.Stop();
            Kontroll();
        }
        private void Kontroll() //kontrolli funktsioon
        {
            foreach (Control control in table.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            using (var muusika = new SoundPlayer(@"..\..\end.wav"))
            {
                muusika.Play();
                Close();
            }
            var vastus = MessageBox.Show("Õnnitleme, olete kõik leidnud!", "Lõpp", MessageBoxButtons.YesNo);
            if (vastus == DialogResult.Yes)
            {
                this.Close();
                MatchingGame el = new MatchingGame("Matching Game");
                el.ShowDialog();
            }
            else if (vastus == DialogResult.No)
            {
                MessageBox.Show("Ok, bye");
                Close();
            }
        }
    }
}
