using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonQuiz
{
    public partial class SilhouetteForm : Form
    {
        public SilhouetteForm()
        {
            InitializeComponent();
        }

        public Bitmap Silhouette
        {
            get
            {
                return new Bitmap(pictureBox1.Image);
            }
            set
            {
                pictureBox1.Image = value.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, ThumbnailCallback, IntPtr.Zero);
            }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
    }
}
