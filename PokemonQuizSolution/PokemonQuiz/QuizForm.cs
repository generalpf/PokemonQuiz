using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PokemonCommon;

namespace PokemonQuiz
{
    public partial class QuizForm : Form
    {
        private const int NUM_ROUNDS = 5;

        private bool quizRunning = false;
        private int currentQuestion;
        private int rightAnswer;
        private int score;
        private Pokemon rightPokemon = null;
        private SilhouetteForm silhouetteForm = null;
        
        private IPokemonDataLayer pokemonDataLayer;
        private Random random;

        public QuizForm()
        {
            InitializeComponent();

            pokemonDataLayer = new Db4oDataLayer();  // WebScraper();
            random = new Random();
        }

        private void cmdStartQuiz_Click(object sender, EventArgs e)
        {
            if (!quizRunning)
            {
                quizRunning = true;
                currentQuestion = 1;
                score = 0;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Step = 100 / NUM_ROUNDS;
                EnableQuizButtons();

                SetUpQuestion();
            }
        }

        private void SetUpQuestion()
        {
            this.Cursor = Cursors.WaitCursor;

            int r1 = random.Next(1, pokemonDataLayer.GetNumberOfPokemon());
            Pokemon p1 = pokemonDataLayer.GetPokemonByNumber(r1);

            int r2 = r1;
            while (r2 == r1)
            {
                r2 = random.Next(1, pokemonDataLayer.GetNumberOfPokemon());
            }
            Pokemon p2 = pokemonDataLayer.GetPokemonByNumber(r2);

            int r3 = r2;
            while (r3 == r2 || r3 == r1)
            {
                r3 = random.Next(1, pokemonDataLayer.GetNumberOfPokemon());
            }
            Pokemon p3 = pokemonDataLayer.GetPokemonByNumber(r3);

            this.rightAnswer = random.Next(1, 3);
            this.rightPokemon = rightAnswer == 1 ? p1 : rightAnswer == 2 ? p2 : p3;

            int qt = random.Next(0, 6);
            switch (qt)
            {
                case 0:
                    lblQuestion.Text = "Which Pokemon has weight = " + rightPokemon.Weight + "?";
                    break;
                /*case 1:
                    lblQuestion.Text = "Which Pokemon has height = " + rightPokemon.Height + "?";
                    break;*/
                case 2:
                    lblQuestion.Text = "Which Pokemon has species = " + rightPokemon.Species + "?";
                    break;
                case 3:
                    StringBuilder buf = new StringBuilder();
                    for (int i = 0; i < rightPokemon.Abilities.Length; i++)
                    {
                        buf.Append(rightPokemon.Abilities[i]);
                        if (i < rightPokemon.Abilities.Length - 1)
                            buf.Append(", ");
                    }
                    lblQuestion.Text = "Which Pokemon has abilit(ies) = " + buf.ToString() + "?";
                    break;
                case 4:
                    buf = new StringBuilder();
                    for (int i = 0; i < rightPokemon.Types.Length; i++)
                    {
                        buf.Append(rightPokemon.Types[i]);
                        if (i < rightPokemon.Types.Length - 1)
                            buf.Append(", ");
                    }
                    lblQuestion.Text = "Which Pokemon has type(s) = " + buf.ToString() + "?";
                    break;
                case 1:
                case 5:
                    lblQuestion.Text = "Which Pokemon is this?";
                    break;
            }
            
            groupBox1.Text = p1.Name;
            groupBox2.Text = p2.Name;
            groupBox3.Text = p3.Name;

            if (qt == 1 || qt == 5)
            {
                Bitmap silhouette = new Bitmap(Bitmap.FromStream(new MemoryStream(rightPokemon.Picture)));
                Silhouettify(silhouette);
                silhouetteForm = new SilhouetteForm();
                silhouetteForm.Silhouette = silhouette;
                silhouetteForm.Show();

                pictureBox1.Image = pictureBox2.Image = pictureBox3.Image = null;
            }
            else
            {
                pictureBox1.Image = new Bitmap(Bitmap.FromStream(new MemoryStream(p1.Picture))).GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, ThumbnailCallback, IntPtr.Zero);
                pictureBox2.Image = new Bitmap(Bitmap.FromStream(new MemoryStream(p2.Picture))).GetThumbnailImage(pictureBox2.Width, pictureBox2.Height, ThumbnailCallback, IntPtr.Zero);
                pictureBox3.Image = new Bitmap(Bitmap.FromStream(new MemoryStream(p3.Picture))).GetThumbnailImage(pictureBox3.Width, pictureBox3.Height, ThumbnailCallback, IntPtr.Zero);
            }

            this.Cursor = Cursors.Default;
        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        private void cmdP1_Click(object sender, EventArgs e)
        {
            HandleAnswer(1);
        }

        private void cmdP2_Click(object sender, EventArgs e)
        {
            HandleAnswer(2);
        }

        private void cmdP3_Click(object sender, EventArgs e)
        {
            HandleAnswer(3);
        }

        private void HandleAnswer(int clicked)
        {
            if (silhouetteForm != null)
            {
                silhouetteForm.Silhouette = new Bitmap(Bitmap.FromStream(new MemoryStream(rightPokemon.Picture)));
                Thread.Sleep(2000);

                silhouetteForm.Close();
                silhouetteForm = null;
            }

            toolStripProgressBar1.PerformStep();

            if (clicked == this.rightAnswer)
            {
                ++score;
                lblLastAnswerStatus.Text = "CORRECT!";
            }
            else
            {
                lblLastAnswerStatus.Text = "INCORRECT! (" + rightPokemon.Name + ")";
            }
            lblScore.Text = score + "/" + currentQuestion;

            if (++currentQuestion <= NUM_ROUNDS)
            {
                SetUpQuestion();
            }
            else
            {
                quizRunning = false;
                DisableQuizButtons();
            }
        }

        private void EnableQuizButtons()
        {
            cmdP1.Enabled = cmdP2.Enabled = cmdP3.Enabled = true;
            cmdStartQuiz.Enabled = false;
        }

        private void DisableQuizButtons()
        {
            cmdP1.Enabled = cmdP2.Enabled = cmdP3.Enabled = false;
            cmdStartQuiz.Enabled = true;
        }

        private void Silhouettify(Bitmap bitmap)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    if (color.R != 255 || color.G != 255 || color.B != 255)
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }
        }
    }
}
