using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace TestCam
{
    public partial class Form1 : Form
    {
        private Capture capture;
        private Image<Bgr, Byte> IMG;
        private Image<Bgr, Byte> IMG_Post;


        private Image<Gray, Byte> IMG_Post_Gray;
        private Image<Gray, Byte> GrayImg;

//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
        
        
        public Form1()
        {
            InitializeComponent();
        }
        
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
        private void processFrame(object sender, EventArgs e)
        {
            if (capture == null)//very important to handel excption
            {
                try
                {
                    capture = new Capture(0); //camera id you use
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            IMG = capture.QueryFrame();
            IMG_Post = IMG.CopyBlank();         
            GrayImg = IMG.Convert<Gray, Byte>();

            int Xp, Yp, x1, y1, x2, y2;
            int Max_Y, Min_Y;
            double Xc, Yc, Zc;
            int i, j;

            //Scanning frames to catch black object which is shown by Computer camera for image processing


            //1-Find Xp and Yp
            for (i=0; i < GrayImg.Width; i++)
            {
                for (j = 0; j < GrayImg.Height; j++)
                {
                    if (GrayImg[j,i].Intensity>20)
                    {
                        IMG_Post[j, i] = new Bgr(0, 0, 0);
                    }
                    else
                    {
                        IMG_Post[j, i] = new Bgr(255, 255, 255);
                    }
                }
            }

            IMG_Post_Gray = IMG_Post.Convert<Gray, Byte>();


            x1 = -1;
            for (i = 10; i < IMG_Post_Gray.Width-10; i++)
            {
                for (j = 10; j < IMG_Post_Gray.Height-10; j++)
                    if(IMG_Post_Gray[j,i].Intensity>128)
                    {
                        x1 = i;
                        break;
                    }
                    if (x1 >= 0)
                    break;
            }

            x2 = -1;
            for (i = IMG_Post_Gray.Width-10; i >= 10; i--)
            {
                for (j = 10; j < IMG_Post_Gray.Height-10; j++)
                    if (IMG_Post_Gray[j, i].Intensity > 128)
                    {
                        x2 = i;
                        break;
                    }
                if (x2 >= 0)
                    break;
            }

            y1 = -1;
            for (j = 10; i < IMG_Post_Gray.Height - 10; j++)
            {
                for (i = 10; i < IMG_Post_Gray.Width - 10; i++)
                    if (IMG_Post_Gray[j, i].Intensity > 128)
                    {
                        y1 = j;
                        break;
                    }
                if (y1 >= 0)
                    break;
            }

            y2 = -1;
            for (j = IMG_Post_Gray.Height-10; j >=10; j--)
            {
                for (i = 10; i < IMG_Post_Gray.Width - 10; i++)
                if (IMG_Post_Gray[j, i].Intensity > 128)
                {
                    y2 = j;
                        break;
                }
                if (y2 >= 0)
                    break;
            }

            Xp = (x1 + x2) / 2;
            Yp = (y1 + y2) / 2;

            textBox1.Text = Xp.ToString();
            textBox2.Text = Yp.ToString();
            //2- Find Xc and Yc

            Xc = (float)Xp / IMG_Post_Gray.Width * 30; // 
            Yc = (float)Yp / IMG_Post_Gray.Height * 20;// 

            Min_Y = 0;
            Max_Y = 20; // bunuda sen hesapla

            //3- Find Zc
            Zc = Yc * (20.0 - 30.0) / (Max_Y - Min_Y) + Min_Y;

            //4-Calculate the Inverse

            try
            {
                
                imageBox1.Image = IMG;
                imageBox2.Image = GrayImg;
                imageBox3.Image = IMG_Post;
             
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Idle += processFrame;
            button1.Enabled = false;
            button2.Enabled = true;
        }
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Idle -= processFrame;
            button1.Enabled = true;
            button2.Enabled = false;
        }    
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
//(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
        private void button3_Click(object sender, EventArgs e)
        {
            IMG.Save("D:\\Image" +  ".jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)
        //(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)(*)

    }
}
