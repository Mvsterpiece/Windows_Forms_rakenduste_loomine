using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class MinuVorm : Form
    {
        TreeView puu;
        TableLayoutPanel tableLayoutPanel;
        PictureBox pictureBox;
        CheckBox checkBox, cngSize;
        Button close, bgColor, clear, showPicture, invtr, rotate, slideshow;
        ColorDialog colordialog;
        OpenFileDialog openfiledialog;
        FlowLayoutPanel flowlayoutpanel;
        FolderBrowserDialog slide;
        Timer timer;
        int imgNum = 1;

        public MinuVorm()
        {
            Height = 400;
            Width = 300;
            Text = "Minu oma vorm koos elementidega"; //название формы
            puu = new TreeView();
            puu.Dock = DockStyle.Right;
            puu.Location = new Point(0, 0);
            TreeNode oksad = new TreeNode("Elemendid");
            oksad.Nodes.Add(new TreeNode("Pildid"));
            oksad.Nodes.Add(new TreeNode("MangQuiz"));
            oksad.Nodes.Add(new TreeNode("MatchingGame"));

            puu.AfterSelect += Puu_AfterSelect;
            puu.Nodes.Add(oksad);
            this.Controls.Add(puu);

        }

        private void Puu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Pildid")
            {
                Text = "Pilti vaatamine";
                this.Size = new System.Drawing.Size(1280, 650);
                tableLayoutPanel = new TableLayoutPanel
                {
                    AutoSize = true,
                    ColumnCount = 2,
                    RowCount = 2,
                    Location = new System.Drawing.Point(115, 0),
                    TabIndex = 0,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                };
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
                tableLayoutPanel.ResumeLayout(false);
                this.Controls.Add(tableLayoutPanel);


                pictureBox = new System.Windows.Forms.PictureBox //luua raam, milles pilt kuvatakse
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Size = new System.Drawing.Size(300, 300),
                    TabIndex = 0,
                    TabStop = false,
                    AutoSize = false,

                };
                tableLayoutPanel.Controls.Add(pictureBox, 0, 0);
                tableLayoutPanel.SetCellPosition(pictureBox, new TableLayoutPanelCellPosition(0, 0));
                tableLayoutPanel.SetColumnSpan(pictureBox, 2);


                checkBox = new CheckBox //checkbox, mis venitab pildi kogu pildikasti raami ala pictirebox
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(150, 278),
                    TabIndex = 1,
                    UseVisualStyleBackColor = true,
                    Text = "Venita",
                    Dock = System.Windows.Forms.DockStyle.Fill,

                };
                checkBox.CheckedChanged += new System.EventHandler(CheckBox_CheckedChanged);
                tableLayoutPanel.Controls.Add(checkBox);

                cngSize = new CheckBox //checkbox, mis muudab pictureBox laius
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(150, 278),
                    TabIndex = 1,
                    UseVisualStyleBackColor = true,
                    Text = "Muuda laius",
                    Dock = System.Windows.Forms.DockStyle.Fill,

                };
                cngSize.CheckedChanged += new System.EventHandler(cngSize_Click);
                tableLayoutPanel.Controls.Add(cngSize);

                invtr = new Button //nuppu programmi sulgemiseks
                {
                    Text = "Muuda pildi invertiks",
                    TabIndex = 1,
                };
                this.invtr.Click += new System.EventHandler(this.Invert_Click);
                this.Controls.Add(invtr);

                rotate = new Button //nupp mis pöördab pildi
                {
                    Text = "Pöörda",
                    TabIndex = 1,
                };
                this.rotate.Click += new System.EventHandler(this.Rotate);
                this.Controls.Add(rotate);

                close = new Button //nuppu programmi sulgemiseks
                {
                    Text = "Suleda",
                    TabIndex = 1,
                };
                this.close.Click += new System.EventHandler(this.close_Click);
                this.Controls.Add(close);


                colordialog = new ColorDialog //funktsioon, mis väljastab paleti, mis võib asendada raami tausta
                {
                    AllowFullOpen = true,
                    AnyColor = true,
                    SolidColorOnly = false,
                    Color = Color.Red,
                };

                clear = new Button //nupp, mis puhastab raami ja eemaldab pildi
                {
                    AutoSize = true,
                    TabIndex = 2,
                    Text = "Kustuta",
                    UseVisualStyleBackColor = true,
                };
                tableLayoutPanel.Controls.Add(clear);
                this.clear.Click += new System.EventHandler(this.clear_Click);

                bgColor = new Button //nupp, mis kuvab paleti
                {
                    AutoSize = true,
                    TabIndex = 1,
                    Text = "Valida tausta värvi",
                    UseVisualStyleBackColor = true,

                };
                tableLayoutPanel.Controls.Add(bgColor);
                this.bgColor.Click += new System.EventHandler(this.bgColor_Click);

                showPicture = new Button //nupp, millega saab pilti avada ja just arvutisüsteemi kaudu antakse valik, mida avada
                {
                    AutoSize = true,
                    TabIndex = 3,
                    Text = "Näita pilti",
                    UseVisualStyleBackColor = true,

                };
                tableLayoutPanel.Controls.Add(showPicture);
                this.showPicture.Click += new System.EventHandler(this.showPicture_Click);

                slideshow = new Button
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(3, 3),
                    Size = new System.Drawing.Size(75, 23),
                    TabIndex = 5,
                    Text = "SlideShow",
                    UseVisualStyleBackColor = true,
                };
                tableLayoutPanel.Controls.Add(slideshow, 0, 3);
                slideshow.Click += SlideShowButton_Click;

                openfiledialog = new OpenFileDialog //funktsioon, mis avab Exploreri ja võimaldab valida arvutisse allalaaditud piltide hulgast
                {
                    RestoreDirectory = true,
                    Title = "Browse Text Files",
                    Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" + "s (*.*)|*.*",

                };
                Button[] buttons = {clear, showPicture, invtr, rotate, close, slideshow, bgColor };
                flowlayoutpanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                };
                flowlayoutpanel.Controls.AddRange(buttons);
                tableLayoutPanel.Controls.Add(flowlayoutpanel, 0, 1);
                this.Controls.Add(tableLayoutPanel);

                CheckBox[] checkboxs = { checkBox, cngSize };
                flowlayoutpanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                };
                flowlayoutpanel.Controls.AddRange(checkboxs);
                tableLayoutPanel.Controls.Add(flowlayoutpanel, 1, 1);
                this.Controls.Add(tableLayoutPanel);

                timer = new Timer
                {
                    Interval = 1000,
                };
                timer.Tick += Timer_Tick;
            }
            else if (e.Node.Text == "MangQuiz") //matemaatikaviktoriini mängu käivitamine eraldi aknas
            {
                MathQuiz nupp = new MathQuiz("Math Quiz");
                nupp.ShowDialog();
            }
            else if (e.Node.Text == "MatchingGame") //matemaatikaviktoriini mängu käivitamine eraldi aknas
            {
                MatchingGame el = new MatchingGame("Matching Game");
                el.ShowDialog();
            }


        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            pictureBox.ImageLocation = string.Format(slide.SelectedPath + "\\img{0}.jpg", imgNum);
            imgNum++;
            if (imgNum == 5)
                imgNum = 1;
        }

        private void SlideShowButton_Click(object sender, EventArgs e)
        {
            slide = new FolderBrowserDialog();
            slide.ShowDialog();
            timer.Enabled = true;
        }
        private void cngSize_Click(object sender, EventArgs e)
        {
            if (cngSize.Checked)
            {
                tableLayoutPanel.SetColumnSpan(pictureBox, 1);
            }
            else { tableLayoutPanel.SetColumnSpan(pictureBox, 2); }
        }
        private void close_Click(object sender, EventArgs e) //funktsioon, mis sulgeb programmi
        {
            this.Close();
        }

        private void showPicture_Click(object sender, EventArgs e) //pildi avamise funktsioon
        {
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Load(openfiledialog.FileName); //valitud fail laaditakse nime järgi
            }
        }

        private void clear_Click(object sender, EventArgs e) //eemaldab pildi picturebox
        {
            pictureBox.Image = null;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e) //funktsioon, mis venitab pildi üle kogu kaadri ala
        {
            if (checkBox.Checked)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else { pictureBox.SizeMode = PictureBoxSizeMode.Normal; }
        }

        private void bgColor_Click(object sender, EventArgs e) //funktsioon, mille abil saate valida raami taustavärvi
        {
            if (colordialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.BackColor = colordialog.Color;
            }
        }

        private void Invert_Click(object sender, EventArgs e) //funktstioon mis inverteerib pilid teise värvideks
        {
            Bitmap pic = new Bitmap(pictureBox.Image);
            for (int y = 0; (y<= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x<= (pic.Width - 1)); x++)
                {
                    Color inv = pic.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, y, inv);
                    pictureBox.Image = pic;
                }
            }
        }

        private void Rotate(System.Object sender, System.EventArgs e) //funktsioon mis pöödrub pildi
        {
            Bitmap pic = new Bitmap(pictureBox.Image);
            if (pic != null)
            {
                pic.RotateFlip(RotateFlipType.Rotate180FlipY);
                pictureBox.Image = pic;
            }
        }



    }

}
