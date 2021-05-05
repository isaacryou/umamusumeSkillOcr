using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmamusumeSkillOCR
{
    public partial class bitmapPreview : Form
    {
        public bitmapPreview(Bitmap gameBitmap)
        {
            InitializeComponent();

            populateInterface(gameBitmap);
        }

        private void populateInterface(Bitmap gameBitmap)
        {
            this.Icon = Properties.Resources.UmamusumeSkillIcon;

            this.Width = gameBitmap.Width + 40;
            this.Height = gameBitmap.Height + 64;

            pictureBox1.Width = gameBitmap.Width;
            pictureBox1.Height = gameBitmap.Height;

            pictureBox1.Image = (Image)gameBitmap;
        }

        private void bitmapPreview_Load(object sender, EventArgs e)
        {
            
        }
    }
}
