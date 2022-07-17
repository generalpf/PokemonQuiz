using System;
using System.Drawing;
using System.Windows.Forms;

namespace PokemonQuiz
{
    public partial class SilhouetteForm : Form
    {
        public SilhouetteForm()
        {
            InitializeComponent();
        }

        private Bitmap originalBitmap;

        public Bitmap OriginalBitmap
        {
            get
            {
                return originalBitmap;
            }
            set
            {
                originalBitmap = value;
                pictureBox1.Image = Silhouettify(value).GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, ThumbnailCallback, IntPtr.Zero);
            }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        private Bitmap Silhouettify(Bitmap bitmap)
        {
            Bitmap newBitmap = bitmap;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = newBitmap.GetPixel(x, y);
                    if (color.A != 0 && (color.R != 255 || color.G != 255 || color.B != 255))
                    {
                        newBitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return newBitmap;
        }

        public void ShowOriginalBitmap()
        {
            pictureBox1.Image = originalBitmap.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, ThumbnailCallback, IntPtr.Zero);
        }
    }
}
