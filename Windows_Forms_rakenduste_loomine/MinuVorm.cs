using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace Windows_Forms_rakenduste_loomine
{
    public partial class MinuVorm : Form
    {
        TreeView puu;
        TableLayoutPanel tableLayoutPanel;
        PictureBox pictureBox;
        CheckBox checkBox;
        Button close, bgColor, clear, showPicture;
        ColorDialog colordialog;
        OpenFileDialog openfiledialog;
        FlowLayoutPanel flowlayoutpanel;

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
                this.Size = new System.Drawing.Size(1280, 500);
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

                bgColor = new Button //nupp, mis kuvab paleti
                {
                    AutoSize = true,
                    TabIndex = 1,
                    Text = "Valida tausta värvi",
                    UseVisualStyleBackColor = true,

                };
                tableLayoutPanel.Controls.Add(bgColor);
                this.bgColor.Click += new System.EventHandler(this.bgColor_Click);

                clear = new Button //nupp, mis puhastab raami ja eemaldab pildi
                {
                    AutoSize = true,
                    TabIndex = 2,
                    Text = "Kustuta",
                    UseVisualStyleBackColor = true,
                };
                tableLayoutPanel.Controls.Add(clear);
                this.clear.Click += new System.EventHandler(this.clear_Click);

                showPicture = new Button //nupp, millega saab pilti avada ja just arvutisüsteemi kaudu antakse valik, mida avada
                {
                    AutoSize = true,
                    TabIndex = 3,
                    Text = "Näita pilti",
                    UseVisualStyleBackColor = true,

                };
                tableLayoutPanel.Controls.Add(showPicture);
                this.showPicture.Click += new System.EventHandler(this.showPicture_Click);

                openfiledialog = new OpenFileDialog //funktsioon, mis avab Exploreri ja võimaldab valida arvutisse allalaaditud piltide hulgast
                {
                    RestoreDirectory = true,
                    Title = "Browse Text Files",
                    Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" + "s (*.*)|*.*",

                };
                Button[] buttons = { clear, showPicture, close, bgColor };
                flowlayoutpanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                };
                flowlayoutpanel.Controls.AddRange(buttons);
                tableLayoutPanel.Controls.Add(flowlayoutpanel, 1, 1);
                this.Controls.Add(tableLayoutPanel);
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
    }

}
