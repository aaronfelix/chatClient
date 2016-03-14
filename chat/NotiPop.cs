using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat
{
    public partial class NotiPop : Form
    {

        int iterations = 0;
        Timer timer = new Timer();
        Timer timer2 = new Timer();
        Timer timer3 = new Timer();
        Timer timer4 = new Timer();
        Timer timer5 = new Timer();
        public Form1 Creator;

        public NotiPop(string message)
        {
            InitializeComponent();
            this.Cursor = Cursors.Hand;
            label1.Text = message;
            this.ShowInTaskbar = false;
           
        }

        private void NotiPop_Load(object sender, EventArgs e)
        {
            Console.WriteLine(" NOTIPOP");
            this.Left = Screen.PrimaryScreen.Bounds.Width - 250;
            this.Top = Screen.PrimaryScreen.Bounds.Height - 10;
            int topper = Screen.PrimaryScreen.Bounds.Height - 10;
            
            
            timer.Interval = 1;
            timer.Tick += new EventHandler(Display);
            timer.Start();

        }





        private void Display(object sender, EventArgs e) {

            if (iterations <= 50)
            {
                Top--;
                iterations++;
                this.Opacity += .02;
            }
            else
            {
                timer.Stop();
                timer2.Interval = 1;
                timer2.Tick += new EventHandler(Display2);
                timer2.Start();
            }
        }


        private void Display2(object sender, EventArgs e)
        {

            if (iterations <= 85)
            {
                Top--;
                iterations++;
            }
            else
            {
                timer2.Stop();

                timer3.Interval = 1000;
                timer3.Tick += new EventHandler(Display3);
                timer3.Start();
            }
        }


        private void Display3(object sender, EventArgs e)
        {
            timer3.Stop();
            iterations = 0;
            timer4.Interval = 5;
            timer4.Tick += new EventHandler(Display4);
            timer4.Start();
            
        }
        private void Display4(object sender, EventArgs e)
        {

            if (iterations <= 135)
            {
                Top++;
                iterations++;
            }
            else
            {
                timer4.Stop();

                timer5.Interval = 1;
                timer5.Tick += new EventHandler(Display5);
                timer5.Start();
            }
        
        }



        private void Display5(object sender, EventArgs e)
        {

            if (iterations <= 170)
            {
                Top++;
                iterations++;

                this.Opacity -= .02;
            }
            else
            {
                timer5.Stop();

                this.Close();
                this.Dispose();
            }
        }


        private void NotiPop_MouseClick(object sender, MouseEventArgs e)
        {
            Creator.Focus();
            //Creator.Activate();
            Creator.BringToFront();
            //Creator.WindowState = FormWindowState.Minimized;
            //Creator.Show();
            //Creator.WindowState = FormWindowState.Normal;
            this.Close();
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            NotiPop_MouseClick(sender, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NotiPop_MouseClick(sender, null);
        }
    }
}
