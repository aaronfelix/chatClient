using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace chat
{
    public partial class Netpeek : Form

    {
        string url;
        int intercount = 0;
        public Netpeek(string givenurl)
        {
            InitializeComponent();
            url = givenurl;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Netpeek_Load(object sender, EventArgs e)
        {
            /*string html; 
            using (WebClient client = new WebClient())
            {
               html = client.DownloadString(url);
            }
            char[] c = html.ToCharArray();
            bool inquotes = false;
            bool foundimage = false;
            string buildingurl = "";

            for (int i = 0; i < c.Length; i++) {
                if (c[i] == '"') {
                    if (!inquotes)
                    {
                        inquotes = true;
                    }
                    else
                    {
                        inquotes = false;
                        char[] buildingurlarray = buildingurl.ToCharArray();
                        string last3 = "" + buildingurlarray[buildingurlarray.Length - 3] + buildingurlarray[buildingurlarray.Length - 2] + buildingurlarray[buildingurlarray.Length - 1];
                        Console.WriteLine(last3 + "last3");
                        if (last3 == "png" || last3 == "jpg" || last3 == "peg" || last3 == "gif" || last3 == "bmp")
                        {
                            foundimage = true;
                            break;
                        }
                    }
                }else if (inquotes) {
                    buildingurl += c[i];
                }
                Console.WriteLine(i);
            }




            if (foundimage) {
                using (WebClient client = new WebClient())
                {client.DownloadFile(buildingurl.Replace("\"",""), @"temp");}
                pictureBox1.Image = Image.FromFile(@"temp");
                File.Delete(@"temp");
                this.Location = System.Windows.Forms.Cursor.Position;
                this.Show();
                while (Cursor.Position == Location) {

                }
                Console.WriteLine("this.closethis.siosepces");
                this.Close();
                this.Dispose();
            }
            else{
                System.Diagnostics.Process.Start(url);
            }
            
            *///thats for hoverzoom which i did not implement.

            using (WebClient client = new WebClient())
            { client.DownloadFile(url, @"temp"); }
            //pictureBox1.Image = Image.FromFile(@"temp");

            using (FileStream fs = new FileStream(@"temp", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                using (MemoryStream ms = new MemoryStream(buffer))
                    this.pictureBox1.Image = Image.FromStream(ms);
            }


            this.Height = pictureBox1.Image.Height;
            this.Width = pictureBox1.Image.Width;
            File.Delete(@"temp");
            this.Location = System.Windows.Forms.Cursor.Position;
            this.Left = Cursor.Position.X;
            this.Top = /*Screen.PrimaryScreen.Bounds.Height- */Cursor.Position.Y - this.Height;
            this.Show();
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            
        }
        // private void last3chars
    }
}
