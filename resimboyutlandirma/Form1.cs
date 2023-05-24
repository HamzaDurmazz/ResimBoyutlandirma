using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace resimboyutlandirma
{
    public partial class Form1 : Form
    {
        private string selectedImagePath;

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları (*.jpg; *.png; *.gif; *.bmp)|*.jpg; *.png; *.gif; *.bmp|Tüm Dosyalar (*.*)|*.*"; // //*.jpg; *.png)|*.jpg; *.png
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;
                DisplayImage();
            }
        }

        private void DisplayImage()
        {
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                Image image = Image.FromFile(selectedImagePath);
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Lütfen önce bir resim dosyası seçin.");
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            int newWidth, newHeight;

            if (int.TryParse(textBox1.Text, out newWidth) && int.TryParse(textBox2.Text, out newHeight))
            {
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    Image image = Image.FromFile(selectedImagePath);
                    Bitmap resizedImage = ResizeImage(image, newWidth, newHeight);
                    pictureBox1.Image = resizedImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    image.Dispose();
                }
                else
                {
                    MessageBox.Show("Lütfen önce bir resim dosyası seçin.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir genişlik ve yükseklik değeri girin.");
            }


        }
        private Bitmap ResizeImage(Image image, int newWidth, int newHeight)
        {

            //Orjinal Kod

            //Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            //using (Graphics graphic = Graphics.FromImage(resizedImage))
            //{
            //    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            //}
            //return resizedImage;

            #region ILKDENEME
            //int originalWidth = image.Width;
            //int originalHeight = image.Height;

            //float aspectRatio = (float)originalWidth / originalHeight;

            //if (newWidth == 0)
            //{
            //    newWidth = (int)(newHeight * aspectRatio);
            //}
            //else if (newHeight == 0)
            //{
            //    newHeight = (int)(newWidth / aspectRatio);
            //}

            //Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            //using (Graphics graphic = Graphics.FromImage(resizedImage))
            //{
            //    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            //}
            //return resizedImage; 
            #endregion

            int originalWidth = image.Width;
            int originalHeight = image.Height;

            float aspectRatio = (float)originalWidth / originalHeight;

            int targetWidth = newWidth;
            int targetHeight = newHeight;

            if (newWidth == 0)
            {
                targetWidth = (int)(newHeight * aspectRatio);
            }
            else if (newHeight == 0)
            {
                targetHeight = (int)(newWidth / aspectRatio);
            }
            else
            {
                float targetAspectRatio = (float)newWidth / newHeight;

                if (targetAspectRatio > aspectRatio)
                {
                    targetWidth = (int)(newHeight * aspectRatio);
                }
                else
                {
                    targetHeight = (int)(newWidth / aspectRatio);
                }
            }

            Bitmap resizedImage = new Bitmap(targetWidth, targetHeight);
            using (Graphics graphic = Graphics.FromImage(resizedImage))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }
            return resizedImage;



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image| *.jpg | PNG Image | *.png | GIF Image | *.gif | BMP Image | *.bmp";  //JPEG Image|*.jpg|PNG Image|*.png
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    ImageFormat imageFormat = ImageFormat.Jpeg;

                   
                    string extension = Path.GetExtension(filePath);
                    if (extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        imageFormat = ImageFormat.Png;
                    }

                    pictureBox1.Image.Save(filePath, imageFormat);
                    MessageBox.Show("Resim başarıyla kaydedildi.");
                }
            }
        }
    }
}

