using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PokeApiNet;

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
        private PokeApiClient pokeApiClient = null;
        private Random random;

        public QuizForm()
        {
            InitializeComponent();

            pokeApiClient = new PokeApiClient();
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

        private async void SetUpQuestion()
        {
            this.Cursor = Cursors.WaitCursor;

            /*
             * the API is coming back with over 1,100 Pokemon.
             * NamedApiResourceList<Pokemon> somePokemon = await pokeApiClient.GetNamedResourcePageAsync<Pokemon>();
             * int pokemonCount = somePokemon.Count;
             */
            int pokemonCount = 905;

            int r1 = random.Next(1, pokemonCount + 1);
            Pokemon p1 = await pokeApiClient.GetResourceAsync<Pokemon>(r1);

            int r2 = r1;
            while (r2 == r1)
            {
                r2 = random.Next(1, pokemonCount + 1);
            }
            Pokemon p2 = await pokeApiClient.GetResourceAsync<Pokemon>(r2);

            int r3 = r2;
            while (r3 == r2 || r3 == r1)
            {
                r3 = random.Next(1, pokemonCount + 1);
            }
            Pokemon p3 = await pokeApiClient.GetResourceAsync<Pokemon>(r3);

            this.rightAnswer = random.Next(0, 3);
            this.rightPokemon = rightAnswer == 0 ? p1 : rightAnswer == 1 ? p2 : p3;

            int qt = random.Next(0, 6);
            switch (qt)
            {
                case 0:
                    lblQuestion.Text = $"Which Pokemon has weight = {rightPokemon.Weight} hectograms?";
                    break;
                case 1:
                    lblQuestion.Text = $"Which Pokemon has height = {rightPokemon.Height} decimetres?";
                    break;
                case 2:
                    lblQuestion.Text = $"Which Pokemon has species = {rightPokemon.Species.Name}?";
                    break;
                case 3:
                    StringBuilder buf = new StringBuilder();
                    for (int i = 0; i < rightPokemon.Abilities.Count; i++)
                    {
                        buf.Append(rightPokemon.Abilities[i].Ability.Name);
                        if (i < rightPokemon.Abilities.Count - 1)
                            buf.Append(", ");
                    }
                    lblQuestion.Text = $"Which Pokemon has abilit(ies) = {buf}?";
                    break;
                case 4:
                    buf = new StringBuilder();
                    for (int i = 0; i < rightPokemon.Types.Count; i++)
                    {
                        buf.Append(rightPokemon.Types[i].Type.Name);
                        if (i < rightPokemon.Types.Count - 1)
                            buf.Append(", ");
                    }
                    lblQuestion.Text = $"Which Pokemon has type(s) = {buf}?";
                    break;
                case 5:
                    lblQuestion.Text = "Which Pokemon is this?";
                    break;
            }

            groupBox1.Text = p1.Name;
            groupBox2.Text = p2.Name;
            groupBox3.Text = p3.Name;

            Bitmap bitmap1 = GetFrontDefaultBitmap(p1);
            Bitmap bitmap2 = GetFrontDefaultBitmap(p2);
            Bitmap bitmap3 = GetFrontDefaultBitmap(p3);

            if (qt == 5)
            {
                silhouetteForm = new SilhouetteForm();
                silhouetteForm.OriginalBitmap = rightAnswer == 0 ? bitmap1 : rightAnswer == 1 ? bitmap2 : bitmap3;
                silhouetteForm.Show();

                pictureBox1.Image = pictureBox2.Image = pictureBox3.Image = null;
            }
            else
            {
                pictureBox1.Image = bitmap1.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, ThumbnailCallback, IntPtr.Zero);
                pictureBox2.Image = bitmap2.GetThumbnailImage(pictureBox2.Width, pictureBox2.Height, ThumbnailCallback, IntPtr.Zero);
                pictureBox3.Image = bitmap3.GetThumbnailImage(pictureBox3.Width, pictureBox3.Height, ThumbnailCallback, IntPtr.Zero);
            }

            this.Cursor = Cursors.Default;
        }

        private Bitmap GetFrontDefaultBitmap(Pokemon p)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(p.Sprites.FrontDefault);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        private void cmdP1_Click(object sender, EventArgs e)
        {
            HandleAnswer(0);
        }

        private void cmdP2_Click(object sender, EventArgs e)
        {
            HandleAnswer(1);
        }

        private void cmdP3_Click(object sender, EventArgs e)
        {
            HandleAnswer(2);
        }

        private void HandleAnswer(int clicked)
        {
            if (silhouetteForm != null)
            {
                silhouetteForm.ShowOriginalBitmap();
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
                lblLastAnswerStatus.Text = $"INCORRECT! ({rightPokemon.Name})";
            }
            lblScore.Text = $"{score}/{currentQuestion}";

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
    }
}
