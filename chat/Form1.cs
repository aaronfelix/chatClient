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
    public partial class Form1 : Form

    {
        public string wordschecker = "";
        public bool buttonpressed;
        public string username = "";
        public string password = "";
        public string colorpickermode;
        string backcolorhex = "#7796A9";
        string buttoncolorhex = "#BBBBBB";
        string buttonoutlinehex = "#BBBBBB";
        string textcolorhex = "#DBEAF9";
        Color backcolor;
        Color buttoncolor;
        Color buttonoutline;
        Color textcolor;
        string serverurl = null;
        
        public Form1()
        
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("initializing");
            listBox1.Items.Add("Please enter username");
            textBox1.Select();

            backcolor = System.Drawing.ColorTranslator.FromHtml(backcolorhex);
            buttoncolor = System.Drawing.ColorTranslator.FromHtml(buttoncolorhex);
            buttonoutline = System.Drawing.ColorTranslator.FromHtml(buttoncolorhex);
            textcolor = System.Drawing.ColorTranslator.FromHtml(textcolorhex);

            serverurl = System.IO.File.ReadAllText(@"serverurl.txt");





            listBox1.BackColor = backcolor;
            this.BackColor = backcolor;
            listBox2.BackColor = backcolor;
            button1.BackColor = buttoncolor;
            button1.FlatAppearance.BorderColor = buttonoutline;
            buttonpressed = false;
          

            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username == "" && textBox1.Text != "") {
                username = textBox1.Text;
                textBox1.Text = "";
                Stage2_auth();
            }
            else if (password == "" && textBox1.Text != "")
            {
                password = textBox1.Text;
                textBox1.Text = "";
                DoChat();
            }
            else {
                /*I think it doesn't work because of this block but I have no idea why*/
                HttpWebRequest sendmessagerequest = (HttpWebRequest)WebRequest.Create(serverurl);
                sendmessagerequest.KeepAlive = false;
                sendmessagerequest.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes("message=" + textBox1.Text + "&user=" + username);
                sendmessagerequest.ContentType = "chatMessageSubmit";
                Stream dataStream = sendmessagerequest.GetRequestStream();
                Console.WriteLine(sendmessagerequest.GetRequestStream());
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                dataStream.Dispose();            
                /*probably something doesn't go away after the loop, causing overflow*/
            }
            
        }
        private void Stage2_auth() {
            listBox1.Items.Clear();
            listBox1.Items.Add("Please enter password");
        }

        private void DoChat() {
            listBox1.Items.Clear();
            listBox1.Items.Add("Connecting...");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverurl + "?user=" + username + "&pass=" + password);
            
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            /* HttpWebRequest request = (HttpWebRequest)WebRequest.Create("serverurl");
             request.Method = "GET";
             Console.WriteLine("Request fulfilled:");
            
            
             Console.WriteLine(request.GetResponse());*/
            string s;
            using (WebClient client = new WebClient())
            {
                s = client.DownloadString(serverurl);
            }

            if (s != wordschecker)
            {

                string[] words = s.Replace("\r\n", "\n").Replace("\n", " ").Replace("\r", " ").Replace("  ", " ").Split(" ".ToCharArray());
                Console.WriteLine(words.Length);
                Console.WriteLine(words[0]);
                Console.WriteLine(words[1]);
                //Console.WriteLine(words[90]);
                words[0] = "first!";


                listBox1.Items.Clear();
                for (int i = 0; i < words.Length - (words.Length % 3); i = i + 3)
                {
                    if (words[i] != " ")
                    {
                        listBox1.Items.Add(words[i + 1] + ": " + words[i]);
                    }



                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13){
                button1_Click(null, null);
            }
           
        }



        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening_1(object sender, CancelEventArgs e)
        {

        }

        private void backgroundColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorpickermode = "background";
            colorDialog1.ShowDialog();
            backcolor = colorDialog1.Color;
            listBox1.BackColor = backcolor;
            listBox2.BackColor = backcolor;
            this.BackColor = backcolor;
            
        }

        private void buttonColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorpickermode = "button";
            colorDialog1.ShowDialog();
            buttoncolor = colorDialog1.Color;
            button1.BackColor = buttoncolor;
            
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorpickermode = "text";
            colorDialog1.ShowDialog();
            textcolor = colorDialog1.Color;
            listBox1.ForeColor = textcolor;
            listBox2.ForeColor = textcolor;
        }


      
    }
}
