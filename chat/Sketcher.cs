using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat
{
    public partial class Sketcher : Form
    {
        bool drawing = false;
        Graphics g;
        Graphics l2g;
        Pen p = new Pen(Color.OliveDrab);
        Brush b = Brushes.OliveDrab;
        Timer painttimer = new Timer();
        int oldx;
        int oldy;
        bool hasold;
        string mode = "free";
        Point originalposition;


        Bitmap testbmp;
        Bitmap layer2;
        Form1 parent;

        int it = 0;
        List<Point> checkaround = new List<Point> { };
        Color fillcolor;
        //static string imagepath = @"progdraw";
        //Image image = Image.FromFile(imagepath );
       // Bitmap image;
        public Sketcher(Form1 creator)
        {
            InitializeComponent();
            parent = creator;
            //if (!File.Exists(imagepath))
                //File.Create(imagepath);

            //image = (Bitmap)Bitmap.FromFile(imagepath);


            testbmp = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(testbmp);

            layer2 = new Bitmap(this.Width, this.Height);
            l2g = Graphics.FromImage(layer2);


            p.Width = 3;

            painttimer.Interval = 1;
            painttimer.Tick += Painttimer_Tick;
            
        }

        

        private void Sketcher_Load(object sender, EventArgs e)
        {
            pictureBox2.Location = pictureBox1.Location;
            pictureBox2.Size = pictureBox1.Size;

            pictureBox1.Image = testbmp;
            //this.Size = image.Size;
            pictureBox1.Controls.Add(pictureBox2);
            pictureBox2.Image = layer2;
            pictureBox2.BackColor = Color.Transparent;




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            originalposition = new Point(Cursor.Position.X  - this.Left, Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight);
            painttimer.Start();
            hasold = false;
            if (mode == "fill")
            {
                fillcolor = testbmp.GetPixel(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight);
                checkaround.Clear();
                checkaround.Add(new Point(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight));
                oldx = checkaround[0].X;
                oldy = checkaround[0].Y;
            }
            Console.WriteLine("andbutso");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void Painttimer_Tick(object sender, EventArgs e)
        {
            switch (mode) {
                case "free":
                    if (hasold)
                    {
                        //g.FillRectangle(b, new Rectangle(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top, 3, 3));
                        g.DrawLine(p, new Point(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight), new Point(oldx, oldy));
                        oldy = Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight;
                        oldx = Cursor.Position.X - this.Left;
                        pictureBox1.Refresh();
                    }
                    else {
                        g.FillRectangle(b, new Rectangle(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight, 3, 3));
                        hasold = true;
                        oldy = Cursor.Position.Y - this.Top - SystemInformation.CaptionHeight;
                        oldx = Cursor.Position.X - this.Left;
                    }
                    break;
                case "circle":
                    l2g.Clear(Color.Transparent);



                    l2g.DrawEllipse(p, originalposition.X, originalposition.Y, Cursor.Position.X - originalposition.X - this.Left, Cursor.Position.Y - originalposition.Y - Top - SystemInformation.CaptionHeight);
                    pictureBox2.Refresh();
                    pictureBox2.Focus();
                    break;
                case "line":
                    l2g.Clear(Color.Transparent);

                    //Console.WriteLine(originalposition.X + ", " + Cursor.Position.X);
                    l2g.DrawLine(p, originalposition.X, originalposition.Y, Cursor.Position.X - this.Left, Cursor.Position.Y - Top - SystemInformation.CaptionHeight);
                    pictureBox2.Refresh();
                    pictureBox2.Focus();
                    break;


               case "square":
                    l2g.Clear(Color.Transparent);

                    //Console.WriteLine(originalposition.X + ", " + Cursor.Position.X);
                    l2g.DrawRectangle(p, originalposition.X, originalposition.Y, Cursor.Position.X - this.Left - originalposition.X, Cursor.Position.Y - Top - SystemInformation.CaptionHeight - originalposition.Y);
                    pictureBox2.Refresh();
                    pictureBox2.Focus();
                    break;

               case "solidsquare":
                    l2g.Clear(Color.Transparent);

                    //Console.WriteLine(originalposition.X + ", " + Cursor.Position.X);
                    l2g.FillRectangle(b, originalposition.X, originalposition.Y, Cursor.Position.X - this.Left - originalposition.X, Cursor.Position.Y - Top - SystemInformation.CaptionHeight - originalposition.Y);
                    pictureBox2.Refresh();
                    pictureBox2.Focus();
                    break;

              case "solidcircle":
                    l2g.Clear(Color.Transparent);

                    l2g.FillEllipse(b, originalposition.X, originalposition.Y, Cursor.Position.X - originalposition.X - this.Left, Cursor.Position.Y - originalposition.Y - Top - SystemInformation.CaptionHeight);
                    pictureBox2.Refresh();
                    pictureBox2.Focus();
                    break;

                case "fill":

                    Point[] checkaroundarray = checkaround.ToArray();
                    checkaround.Clear();
                    foreach (Point p in checkaroundarray)//toarray so it doesnt modify it during the foreach
                    {
                        testbmp.SetPixel(p.X, p.Y, button1.BackColor);
                        if (p.X > 1 && p.Y > 1 && p.X < testbmp.Width -1 && p.Y < testbmp.Height -1)
                        {
                            if (p.X != 0 && testbmp.GetPixel(p.X - 1, p.Y) == fillcolor)
                            {
                                checkaround.Add(new Point(p.X - 1, p.Y));
                                testbmp.SetPixel(p.X - 1, p.Y, button1.BackColor);
                            }
                            if (p.X != testbmp.Width && testbmp.GetPixel(p.X + 1, p.Y) == fillcolor)
                            {
                                checkaround.Add(new Point(p.X + 1, p.Y));
                                testbmp.SetPixel(p.X + 1, p.Y, button1.BackColor);
                            }
                            if (p.Y != testbmp.Height && testbmp.GetPixel(p.X, p.Y - 1) == fillcolor)
                            {
                                checkaround.Add(new Point(p.X, p.Y - 1));
                                testbmp.SetPixel(p.X, p.Y - 1, button1.BackColor);
                            }
                            if (p.Y != 0 && testbmp.GetPixel(p.X, p.Y + 1) == fillcolor)
                            {
                                checkaround.Add(new Point(p.X, p.Y + 1));
                                testbmp.SetPixel(p.X, p.Y + 1, button1.BackColor);
                            }
                        }
                        it++;
                       
                        checkaround.Remove(p);
                    }
                    
                    break;

            }


        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            painttimer.Stop();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            checkaround.Clear();
            painttimer.Stop();
            switch (mode)
            {
                case "circle":
                    g.DrawEllipse(p, originalposition.X, originalposition.Y, Cursor.Position.X - originalposition.X - this.Left, Cursor.Position.Y - originalposition.Y - Top - SystemInformation.CaptionHeight);
                    break;
                case "line":
                    g.DrawLine(p, originalposition.X, originalposition.Y, Cursor.Position.X - this.Left, Cursor.Position.Y - Top - SystemInformation.CaptionHeight);
                    break;
                case "square":
                    g.DrawRectangle(p, originalposition.X, originalposition.Y, Cursor.Position.X - this.Left - originalposition.X, Cursor.Position.Y - Top - SystemInformation.CaptionHeight - originalposition.Y);
                    break;
                case "solidsquare":
                    g.FillRectangle(b, originalposition.X, originalposition.Y, Cursor.Position.X - this.Left - originalposition.X, Cursor.Position.Y - Top - SystemInformation.CaptionHeight - originalposition.Y);
                    break;
                case "solidcircle":
                    g.FillEllipse(b, originalposition.X, originalposition.Y, Cursor.Position.X - originalposition.X - this.Left, Cursor.Position.Y - originalposition.Y - Top - SystemInformation.CaptionHeight);
                    break;                    
            }

            l2g.Clear(Color.Transparent);
            pictureBox2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
            button2.BackColor = colorDialog1.Color;
            button3.BackColor = colorDialog1.Color;
            button5.BackColor = colorDialog1.Color;
            button6.BackColor = colorDialog1.Color;
            button7.BackColor = colorDialog1.Color;
            button8.BackColor = colorDialog1.Color;
            button9.BackColor = colorDialog1.Color;
            button10.BackColor = colorDialog1.Color;
            
            p.Color = colorDialog1.Color;
            b = new SolidBrush(colorDialog1.Color);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.WhiteSmoke);
            pictureBox1.Refresh();
        }


            public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();



        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {


       
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mode = "circle";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mode = "free";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mode = "line";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mode = "square";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mode = "solidsquare";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mode = "solidcircle";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap sketch = new Bitmap(pictureBox1.Image);
            byte[] sketcharray;
            using (var ms = new MemoryStream())
            {
                sketch.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                sketcharray = ms.ToArray();
            }
            sketch.Dispose();
            string sketch64 = Convert.ToBase64String(sketcharray);
            //parent.sendMessage(sketch64);//basically it sends it like normal and when it is incoming the client sees if it is the right length and starts with the right thing and interperts it like so
            //we are simply sending it like a file for now.
            string rightnow = DateTime.Now.ToString().Replace(':', '.').Replace('/','-');

            parent.sendFile("sketch" + rightnow+".png", sketch64);
            this.Close();
            this.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mode = "fill";
        }
    }
}
