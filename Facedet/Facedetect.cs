using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormCharpWebCam;
using Facedetect.Api;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

namespace Facedet
{
    public partial class Facedetect : MetroFramework.Forms.MetroForm
    {
        WebCam webcam;
        private Timer Timer;
        Image Image;
        public Facedetect()
        {
            InitializeComponent();
            //InitializeTimer();
        }

        private void InitializeTimer()
        {
            Timer = new System.Windows.Forms.Timer();
            Timer.Interval = 5000;
            Timer.Start();
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Stop();
            Timer.Enabled = false;
            Image=  Video.Image;
            pictureBox1.Image = Image;
            if (Image !=null)
            Helper.SaveImageCapture(Image);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref Video);
         //  webcam.Start();
           
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //webcam.Start();
            //var openDlg = new OpenFileDialog();

            //openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";

            //var result = openDlg.ShowDialog(this);

            ////if (result.)
            ////{
            ////    return;
            ////}

            //string filePath = openDlg.FileName;

            //Uri fileUri = new Uri(filePath);
            //BitmapImage bitmapSource = new BitmapImage();

            //bitmapSource.BeginInit();
            //bitmapSource.CacheOption = BitmapCacheOption.None;
            //bitmapSource.UriSource = fileUri;
            //bitmapSource.EndInit();

            //FacePhoto.Source = bitmapSource;
            this.Text = "Detecting...";
            var det = new programme();

            Image image = Image.FromFile("C:\\dev\\imagetests\\1.jpg");
            using (MemoryStream stream = new MemoryStream())
            {
                // Save image to stream.
                image.Save(stream, ImageFormat.Bmp);
                det.Detect(stream);
            }
            
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            Image = Video.Image;
            pictureBox1.Image = Image;
            webcam.Start();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            // var openDlg = new OpenFileDialog();

            // openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";



            // string filePath = openDlg.FileName;

             Uri fileUri = new Uri("C:\\dev\\imagetests\\1.Jpeg");
            // BitmapImage bitmapSource = new BitmapImage();

            // bitmapSource.BeginInit();
            // bitmapSource.CacheOption = BitmapCacheOption.None;
            // bitmapSource.UriSource = fileUri;
            // bitmapSource.EndInit();

            // FacePhoto.Source = bitmapSource;
            this.Text = "Detecting...";
            var det = new programme();

            var ms = new MemoryStream();
            Image image = Image.FromFile("C:\\dev\\imagetests\\1.jpg");
            image.Save(ms, ImageFormat.Jpeg);

            // If you're going to read from the stream, you may need to reset the position to the start
            ms.Position = 0;
            det.Detect(ms);
            // ms.Dispose();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            var det = new programme();
            var directory = "C:/Users/Athmane/Pictures/moi";
            det.AddPresons(directory);
        }
    }
}
