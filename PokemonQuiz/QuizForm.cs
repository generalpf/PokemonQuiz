using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

            NamedApiResourceList<PokemonSpecies> somePokemon = await pokeApiClient.GetNamedResourcePageAsync<PokemonSpecies>();
            int pokemonCount = somePokemon.Count;

            int r1 = random.Next(1, pokemonCount + 1);
            int r2 = r1;
            while (r2 == r1)
            {
                r2 = random.Next(1, pokemonCount + 1);
            }
            int r3 = r2;
            while (r3 == r2 || r3 == r1)
            {
                r3 = random.Next(1, pokemonCount + 1);
            }

            int[] numbers = new[] { r1, r2, r3 };
            PokemonSpecies[] species = await Task.WhenAll(numbers.Select(n => pokeApiClient.GetResourceAsync<PokemonSpecies>(n)));
            Pokemon[] pokemon = await Task.WhenAll(species.Select(s => pokeApiClient.GetResourceAsync(s.Varieties[random.Next(0, s.Varieties.Count)].Pokemon)));

            this.rightAnswer = random.Next(0, 3);
            this.rightPokemon = pokemon[rightAnswer];

            int qt = random.Next(0, 6);
            switch (qt)
            {
                case 0:
                    lblQuestion.Text = $"Which Pokemon has weight = {rightPokemon.Weight / 10.0} kilograms?";
                    break;
                case 1:
                    lblQuestion.Text = $"Which Pokemon has height = {rightPokemon.Height / 10.0} metres?";
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

            groupBox1.Text = pokemon[0].Name;
            groupBox2.Text = pokemon[1].Name;
            groupBox3.Text = pokemon[2].Name;

            Bitmap[] bitmaps = pokemon.Select(p => GetFrontDefaultBitmap(p)).ToArray();

            if (qt == 5)
            {
                silhouetteForm = new SilhouetteForm();
                silhouetteForm.OriginalBitmap = bitmaps[rightAnswer];
                silhouetteForm.Show();

                pictureBox1.Image = pictureBox2.Image = pictureBox3.Image = null;
            }
            else
            {
                pictureBox1.Image = bitmaps[0].GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, ThumbnailCallback, IntPtr.Zero);
                pictureBox2.Image = bitmaps[1].GetThumbnailImage(pictureBox2.Width, pictureBox2.Height, ThumbnailCallback, IntPtr.Zero);
                pictureBox3.Image = bitmaps[2].GetThumbnailImage(pictureBox3.Width, pictureBox3.Height, ThumbnailCallback, IntPtr.Zero);
            }

            this.Cursor = Cursors.Default;
        }

        private Bitmap GetFrontDefaultBitmap(Pokemon p)
        {
            if (p.Sprites.FrontDefault == null)
            {
                return QuestionMarkBitmap();
            }
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
                this.Cursor = Cursors.WaitCursor;
                Thread.Sleep(2000);
                this.Cursor = Cursors.Default;

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

        private Bitmap questionMarkBitmap = null;
        private Bitmap QuestionMarkBitmap()
        {
            if (questionMarkBitmap == null)
            {
                questionMarkBitmap = new Bitmap("placeholder.bmp");
                RectangleF rectf = new RectangleF(0, 0, 100, 100);
                Graphics g = Graphics.FromImage(questionMarkBitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("?", SystemFonts.DefaultFont, Brushes.Black, rectf, format);
                g.Flush();
            }
            return questionMarkBitmap;
        }
    }
}
