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
using System.Diagnostics;
using System.Net.Sockets;
//using System.Net.WebSockets;
using WebSocket4Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Cryptography;

//for commands just do like normal but true the command parameter.
namespace chat

{
   
    public partial class Form1 : Form

    {

        bool remove = false;

        public string wordschecker = "";
        public bool buttonpressed;
        public static string username = "";
        public static string password = "";
        public string colorpickermode;
        string backcolorhex = "#7796A9";
        string buttoncolorhex = "#BBBBBB";
        string buttonoutlinehex = "#BBBBBB";
        string textcolorhex = "#DBEAF9";
        string secondarytextcolorhex = "#000000";
        public Color backcolor;
        Color buttoncolor;
        Color buttonoutline;
        Color textcolor;
        Color secondarytextcolor;
        int scrollvalue = 29;
        int commandcount = 0;
        int topstorage;
        int leftstorage;

        Object textholder;
        string lastMessage = "";
        bool shownotifications = true;
        bool focused = true;
        string displayname = "";
        bool goingup = false;
        bool goingdown = false;
        bool displayingmessage = false;
        bool changingname = false;
        bool firsttime = true;
        bool spying = false;
        bool logocolor = false;
        bool blinking = false;
        bool setbackmeta = false;
        bool greentexting = false;
        bool running = true;
        bool disptime = true;
        bool logging = false;
        bool newmessage = false;
        bool disguisemode = false;
        bool saylogin = true; //used to not say you've joined when it reconnects every 29 minutes to help heroku
        bool muted = false;
        bool gettinghistory = true;
        bool recieveingfile = false;
        bool sendingfile = false;
        public bool asriel = false;

        bool listing = false;
        string listresponses = "";

        string filepath = "";
        bool gameinsteadoffly = true;
        public string[] lastmessages = new string[2]; 

        Timer notificationtimer = new Timer();
        Timer flyaround = new Timer();
        Timer swearjartimer = new Timer();
        int flyaroundx = 0;
        bool flyarounddir = true;//true=right.

        string replacementword = "";
        object clipboardholder;
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        WebSocket thesocket = new WebSocket("ws://felixchat.herokuapp.com");
        string raw;
        long initialtime;
        System.Threading.Thread guithread;
        Form2 ad;
        SwearJar swearjar;
        bool swearjarContent = false;

        char[][] swears = new char[][] {
            new char[] {'f', 'u', 'c', 'k' },
            new char[] {'s', 'h', 'i', 't' },
            new char[] {'d', 'a', 'm', 'n' }/*,
            new char[] {'s', 'w', 'e', 'r' }*/

        };
        char[] last4 = new char[4];

        // ClientWebSocket cws = new ClientWebSocket();

        public Form1()

        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            thesocket.Opened += new EventHandler(thesocket_Opened);
            thesocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(ErrorHandler);

            EventHandler<MessageReceivedEventArgs> eventHandler = new EventHandler<MessageReceivedEventArgs>(Message_Recieved);
            thesocket.MessageReceived += eventHandler;
            initialtime = System.DateTime.Now.Ticks;
            //thesocket.Closed += new EventHandler<ClosedEventArgs>(ErrorEventHandler);
            //EventHandler<ErrorEventArgs> errorHandler = new EventHandler<ErrorEventArgs>(ErrorEventHandler);
            //thesocket.Error += errorHandler;
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;


        }

       

        private void ErrorHandler(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            Console.WriteLine("ERRRRROROORORORORO");
            DoChat();

        }
        

        [DllImport("user32")]
        public static extern void LockWorkStation();
        private void Message_Recieved(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message_Recieved");
            if (e.Message.Split('\u0007').Length == 3 && e.Message[0] == '/')//unless... commando!
            {

                if (e.Message.Split('\u0007')[0].Split(' ')[1] == username || e.Message.Split('\u0007')[0].Split(' ')[1] == "*") 
                {
                    string commandsender = e.Message.Split('\u0007')[1];
                    string commandtext = e.Message.Split('\u0007')[0];
                    
                    switch (commandtext.Split(' ')[0])//this is where it reacts to commands that were sent.
                    {
                        case "/kick":
                            getKicked();
                            break;
                        case "/mute":
                            getMuted();
                            break;
                        case "/top":
                            forceTop();
                            break;
                        case "/lock":
                            LockWorkStation();
                            break;
                        case "/notepadify":
                            this.Invoke((MethodInvoker)delegate {
                                toggleDisguiseMode();//runs on UI thread
                            });
                            break;
                        case "/swear":
                            this.Invoke((MethodInvoker)delegate {
                                blockSwear();//runs on UI thread
                            });
                            break;
                        case "/unswear":
                            swearjarContent = true;
                            break;
                        case "/list":
                            sendMessage("/here " + commandsender, username, username, true);
                            listresponses = "";
                            break;
                        case "/here":
                            //how /here works: someone does /list, and they all respond with a /here directed at the name of the 
                            // /list user. then the /list user gets the sender of that comand, and adds it to the who's here.
                            //you should only use /list *, never a name.
                            listresponses += commandsender;
                            raw += commandsender + " \u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007";
                            parse_Messages(commandsender + " \u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007");
                            Console.WriteLine(commandsender);
                            break;
                    }
                }
            }
            else if (e.Message.Split('\u0007').Length != 5)//fileo
            {
                if (sendingfile)
                    return;
                
                string[] things = e.Message.Split('\u0007');
                byte[] file = Convert.FromBase64String(things[1]);
                File.WriteAllBytes(filepath + things[0], file);
                recieveingfile = false;
                raw += things[0] + " recieved!\u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007";
                parse_Messages(things[0] + " recieved!\u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007");

            }/*private void Message_Recieved(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message_Recieved");
           
            if (e.Message.Split('\u0007').Length != 5)
            {
                recieveingfile = true;
                string[] things = e.Message.Split('\u0007');
                //0 is section number, 1 is file name, 2 is file data.
                //Console.WriteLine(e.Message);
                if (things[0] != "f")
                {
                    Console.WriteLine("hola");
                    byte[] f = Convert.FromBase64String(things[2]);
                    File.WriteAllBytes(filepath + things[1] + "." + things[0], f);
                    parts++;
                }
                else {
                    if (parts != 0)
                    {
                        for (int i = 0; i < parts; i++)
                        {
                            using (var stream = new FileStream(filepath + things[1], FileMode.Append))
                            {
                                Console.WriteLine("parting");
                                //move from section to file
                                stream.Write(File.ReadAllBytes(filepath + things[1] + '.' + i), 0, 300000);
                            }
                        }
                    }
                    using (var stream = new FileStream(filepath + things[1], FileMode.Append))
                    {//this bit appends the last bit on. if there wasnt enough to split, it just puts down the whole file.
                        Console.WriteLine("gparted");
                        stream.Write(Convert.FromBase64String(things[2]), 0, Convert.FromBase64String(things[2]).Length);


                        raw += things[1] + " recieved!\u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007";
                        parse_Messages(things[1] + " recieved!\u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007");
                    }
                    parts = 0;
                    recieveingfile = false;
                }                
                //byte[] file = Convert.FromBase64String(things[2]);
                //File.WriteAllBytes(filepath + things[1], file);
                //recieveingfile = false;
            }
            else
            {
                gettinghistory = false;
                raw += e.Message;
                parse_Messages(e.Message);
                if (e.Message.Split('\u0007').Length > 80)
                    gettinghistory = true;
            }
        }*/

           /* else if (e.Message == "\u001C")
            {
                recieveingfile = true;
            }*/
            else
            {
                gettinghistory = false;
                raw += e.Message;
                parse_Messages(e.Message);
                if (e.Message.Split('\u0007').Length > 80)
                    gettinghistory = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guithread = System.Threading.Thread.CurrentThread;
            richTextBox1.Clear();
            richTextBox1.AppendText("Please enter username");
            maskedTextBox1.Hide();


            textBox1.Select();

            notifyIcon1.Visible = true;
            this.Text = Path.GetFullPath(@"New folder")/* + " - Notepad ++"*/;
            this.Icon = new System.Drawing.Icon(Path.GetFullPath(@"content/earth.ico"));
            backcolor = System.Drawing.ColorTranslator.FromHtml(backcolorhex);
            buttoncolor = System.Drawing.ColorTranslator.FromHtml(buttoncolorhex);
            buttonoutline = System.Drawing.ColorTranslator.FromHtml(buttoncolorhex);
            textcolor = System.Drawing.ColorTranslator.FromHtml(textcolorhex);
            secondarytextcolor = System.Drawing.ColorTranslator.FromHtml(secondarytextcolorhex);

            //serverurl = System.IO.File.ReadAllText(@"serverurl.txt");


            LoadSettings();
            this.BackColor = backcolor;
            button1.BackColor = buttoncolor;
            button1.FlatAppearance.BorderColor = buttonoutline;
            buttonpressed = false;

            richTextBox1.BackColor = backcolor;




        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username == "" && textBox1.Text != "")
            {
                displayname = textBox1.Text;
                username = textBox1.Text;
                textBox1.Text = "";
                maskedTextBox1.Show();
                textBox1.Hide();
                maskedTextBox1.Focus();
                maskedTextBox1.Clear();
                Stage2_auth();
            }
            else if (password == "" && maskedTextBox1.Text != "")
            {

                password = maskedTextBox1.Text;
                maskedTextBox1.Text = "";
                maskedTextBox1.Hide();

                textBox1.Show();
                if (authenticate())
                {
                    DoChat();
                    textBox1.Focus();
                }
                else
                {
                    //listBox1.Items.Clear();
                    //listBox1.Items.Add("That is the wrong answer!");
                    richTextBox1.Clear();
                    richTextBox1.Text = "THAT IS THE WRONGGGGG ASNWERRRRRRRRRR.";
                    
                    this.Close();
                }
            }
            else
            {//SEND MESSAGE BLOCK


                if (!(displayname == ""))
                {


                    if (!(textBox1.Text == ""))

                    {
                        if (!(displayingmessage))
                        {
                            if (!isACommand(textBox1.Text))
                            {
                                // Console.WriteLine("message=" + textBox1.Text + "&user=" + displayname);



                                /*      HttpWebRequest sendmessagerequest = (HttpWebRequest)WebRequest.Create(serverurl);sendmessagerequest.KeepAlive = false;sendmessagerequest.Method = "POST";sendmessagerequest.UserAgent = "chat program";byte[] byteArray = Encoding.ASCII.GetBytes("message=" + textBox1.Text + "&user=" + username + "&displayname=" + displayname);sendmessagerequest.ContentType = "application/x-www-form-urlencoded";sendmessagerequest.ContentLength = byteArray.Length;Stream dataStream = sendmessagerequest.GetRequestStream();Console.WriteLine("Version");dataStream.Write(byteArray, 0, byteArray.Length);dataStream.Close();dataStream.Dispose();WebResponse response = sendmessagerequest.GetResponse();response.Close();textBox1.Text = "";parse_Messages(null, null);*/

                                if (greentexting)
                                {
                                    textBox1.Text = ">" + textBox1.Text;
                                }
                                sendMessage(textBox1.Text);

                                // Console.WriteLine("message=" + textBox1.Text + "&user=" + displayname);
                            }
                        }
                        else
                        {


                            if (changingname)
                            {
                                displayname = textBox1.Text;
                                textBox1.Text = "";
                                displayingmessage = false;
                                changingname = false;
                                richTextBox1.AppendText("\r\n\r\n Name change accepted.\r\n");
                                richTextBox1.Clear();
                                parse_Messages(raw);

                            }



                        }

                    }
                }
                else
                {
                    textBox1.Text = "Please your name in the bottom right before saying anything!";
                }
                textBox1.Text = "";

            }

        }
        private void Stage2_auth()
        {
            //listBox1.Items.Clear();
            //listBox1.Items.Add("Please enter password");
            richTextBox1.Clear();
            richTextBox1.AppendText("Please enter password");
        }


        private void DoChat()
        {
            //listBox1.Items.Clear();
            //listBox1.Items.Add("Connecting...");
            if (richTextBox1.Lines.Length < 3)
            {
                richTextBox1.Clear();
                richTextBox1.AppendText("Connecting...");
            }

            
            thesocket.AllowUnstrustedCertificate = true;
            thesocket.Open();

            Console.WriteLine("DoChat");
            sendMessage("am in.");
            //listBox1.Items.Add("");
            //listBox1.Items.Add("");
            //listBox1.Items.Add("PK Chat Version 1.5.2");
            if(richTextBox1.Lines.Length < 3)
                richTextBox1.AppendText("\r\nPK Chat Version 2.6.1!\r\n");
           


            //StreamReader reader = new StreamReader();
            /* s.Bind(new IPEndPoint(Dns.GetHostEntry("localhost").AddressList[0], 44555));
             s.Listen(5);
             s.BeginAccept(new AsyncCallback(CallAccept), sock);
             s.Send(System.Text.Encoding.UTF8.GetBytes("joined \u0007" + username + "\u0007" + displayname));
             */
            //using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
            //NetworkStream s = c.GetStream();
            // cws.ConnectAsync(new

            Timer timer = new Timer();
            timer.Interval = 50000;
            timer.Tick += new EventHandler(ping);
            timer.Start();
            /*if(raw != null)
                parse_Messages(raw);*/
        }



        private void parse_Messages(string s)
        {
            if (username == "" || password == "")
                return;



            if (s != wordschecker && !displayingmessage)//if the recieved message isn't blank and they arent in the middle of, for instance, changing name
            {
                
                

                string[] words = s.Replace("\r\n", "\n").Replace("\n", " ").Replace("\r", " ").Replace("  ", " ").Split("\u0007".ToCharArray());                 
                commandcount = 0;
                for (int i = 0; i < words.Length - (words.Length % 4); i = i + 4)
                {
                    if (words[i].Take(5).SequenceEqual(new char[5] { 'R', '0', 'l', 'G', 'O' }))
                    {
                        string path = @"content/sketches/" + DateTime.Now.Ticks + ".gif";
                        File.Create(path).Close();
                        File.WriteAllBytes(path, Convert.FromBase64String(words[i]));
                        this.Invoke((MethodInvoker)delegate
                        {
                            PictureBox pb = new PictureBox();
                            pb.Image = Image.FromFile(path);
                            pb.Size = Image.FromFile(path).Size;
                            pb.Parent = richTextBox1;
                            pb.Show();
                        });
                        
                    }

                    if (words[i] != " ")
                    {//  command checking here
                        if (isReceivedCommand(words[i + 1], words[i], i) && (i % 4 == 0))
                        {
                            //if its a command itll cut. it checks for /tell though:
                            //special case: /tell
                            if (words[i].Split(' ')[0] == "/tell" && words[i].Split(' ').Length > 1)
                            {
                                if (words[i].Split(' ')[1] == username || words[i + 1] == username || spying)
                                {
                                    InsertForTell(words, i);
                                }
                                continue;
                            }
                            //if it isn't /tell, isrecievedcommand sets what to replace it
                            else if (replacementword != "")
                            {
                                if (replacementword == null)
                                {
                                    replacementword = "";
                                    continue;
                                }
                                words[i] = replacementword;
                                replacementword = "";
                            }


                        }


                        

                        if (words[i] == "/fgm")//this is where it inserts emoticons
                        {
                            FGM(words, i);
                            continue;
                        }

                        if (words[i] == "/fbm")
                        {
                            FBM(words, i);
                            continue;
                        }

                        if (words[i] == "/mao")
                        {
                            LMA(words, i);
                            continue;
                        }
                        if (words[i] == "/ayy")
                        {
                            LMA(words, i);
                            continue;
                        }
                        if (words[i] == "/lmao")
                        {
                            LMA(words, i);
                            continue;
                        }
                        if (words[i] == "/sans")
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                sans(words, i);//runs on UI thread
                            });
                            continue;
                        }
                        InsertText(words, i);
                        notify();                      
                    }
                    if (logging)
                    {
                        string message = "";
                        message += (words[i + 3]);
                        message += (" (" + words[i + 1] + ") " + words[i + 2] + ": ");
                        message += (words[i] + "\r\n");
                        if (!File.Exists(@"log"))
                            File.Create(@"log");
                        File.AppendAllText(@"log", message); 
                    }
                }
                
                remove = true;
                
                     /*lastMessage = richTextBox1.Lines[richTextBox1.Lines.Count() - 1].ToString();
                     //NOTIFICATIONS, BALLOON STYLE! (REMOVED)
                     notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"content/earth.ico"));
                     notifyIcon1.Text = "new message.";
                     notifyIcon1.Visible = true;
                     notifyIcon1.BalloonTipTitle = "new message!";
                     notifyIcon1.BalloonTipText = lastMessage;

                     notifyIcon1.ShowBalloonTip(200, "New Message!", lastMessage, ToolTipIcon.Info);

                     //blink(200);

                     //listBox1.BackColor = listBox1.BackColor = Color.FromArgb(listBox1.BackColor.ToArgb() + 50);
                     richTextBox1.BackColor = Color.FromArgb(richTextBox1.BackColor.ToArgb() + 50);


                     notificationtimer.Tick += new EventHandler(blinktimer);
                     notificationtimer.Interval = 200;
                     notificationtimer.Start();*/

            }

        }

        private void notify()
        {
            this.BeginInvoke(new MethodInvoker(notifyoncorrectthread));


        }
        private void notifyoncorrectthread()
        {
            if (!noneToolStripMenuItem.Checked)
            {
                if (!notificationtimer.Enabled)
                {
                    topstorage = Top;
                    leftstorage = Left;

                    Console.WriteLine("nonchecked");


                    if (rumbleToolStripMenuItem.Checked)
                        notificationtimer.Interval = 50;
                    else
                        notificationtimer.Interval = 200;
                    notificationtimer.Tick += new EventHandler(getattention);
                    notificationtimer.Start();
                }
                
            }
            if (enablePopupsToolStripMenuItem.Checked && !(this.ContainsFocus || this.focused)) {
                NotiPop n = new NotiPop(lastMessage);
                n.Creator = this;
                n.BackColor = backcolor;
                n.ForeColor = textcolor;
                n.Show();
            }
            
        }

        private void getattention(object sender, EventArgs e)
        {
            //Console.WriteLine("ticked!");
            if (this.ContainsFocus || this.focused)
            {
                notificationtimer.Stop();
                Top = topstorage;
                Left = leftstorage;
                richTextBox1.BackColor = backcolor;
            }
            else

            if (rumbleToolStripMenuItem.Checked)
            {
                if (Top == topstorage)
                {
                    Top+=1;
                }
                else
                {
                    Top-=1;
                }

                if (Left == leftstorage + 1)
                {
                    Left-=1;
                }
                else
                {
                    Left+=1;
                }
            }
            else if (blinkToolStripMenuItem.Checked)
            {
                if (richTextBox1.BackColor == backcolor)
                {
                    richTextBox1.BackColor = Color.FromArgb(Math.Abs(backcolor.R - 50), Math.Abs(backcolor.G - 50), Math.Abs(backcolor.B - 50));
                }
                else
                {
                    richTextBox1.BackColor = backcolor;
                }
            }
            else if (hideShowToolStripMenuItem.Checked)
            {
                if (this.Opacity == 1)
                {
                    this.Opacity = .1;
                }
                else
                {
                    this.Opacity = 1;
                }
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button1_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
                
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
            richTextBox1.BackColor = backcolor;
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
            richTextBox1.ForeColor = textcolor;
            richTextBox1.Clear();
            parse_Messages(raw);
            
        }

        private void secondaryTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorpickermode = "text2";
            colorDialog1.ShowDialog();
            secondarytextcolor = colorDialog1.Color;
            //listBox1.ForeColor = textcolor;
            //listBox2.ForeColor = textcolor;
            richTextBox1.Clear();
            parse_Messages(raw);


        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void displayName_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {

            focused = true;
            notificationtimer.Stop();
            //listBox1.BackColor = backcolor;
            richTextBox1.BackColor = backcolor;
            if(!disguisemode)
                this.Icon = new System.Drawing.Icon(Path.GetFullPath(@"content/earth.ico"));

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            focused = false;

        }

        private void notificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* if (shownotifications)
             {
                 shownotifications = false;
             }
             else
             {
                 shownotifications = true;
             }*/

        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.Activate();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            last4[0] = last4[1];
            last4[1] = last4[2];
            last4[2] = last4[3];
            last4[3] = e.KeyChar;
            foreach (char[] swear in swears){
                if (swear.SequenceEqual(last4))
                    blockSwear();
            }
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {

            // listBox1.Focus();
            if (e.Delta == 120 && scrollvalue > 1)
            {
                scrollvalue--;
                /* if (!goingup) {
                     scrollvalue -= Convert.ToInt32(Math.Floor(
                         (Convert.ToDouble(Height - 90)) / 20
                         ));
                     goingup = true;
                     goingdown = false;
                 }*/
            }
            else if (scrollvalue < Convert.ToInt32(Math.Floor(
                        (Convert.ToDouble(Height - 90)) / 20
                        )) && e.Delta == -120)
            {
                scrollvalue++;
            }

            //the amount of items showing = floor((height-90)/20)

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            scrollvalue -= Convert.ToInt32(Math.Floor(
                        (Convert.ToDouble(Height - 90)) / 20
                        ));
            //scroll            listBox1.TopIndex = scrollvalue;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Enter(object sender, EventArgs e)
        {

        }

        private void displayNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayingmessage = true;
            changingname = true;
            // listBox1.Items.Clear();
            // listBox1.Items.Add("Please type your new display name into the message box");
            textholder = richTextBox1.Rtf;
            richTextBox1.Clear();
            richTextBox1.AppendText("Please type your new display name into the message box");
            textBox1.Focus();


        }

        private bool isACommand(string command)
        {
            //HERE IS WHERE THE COMMANDS SHALL BE LISTED.
            //this block stops your message from sending if you issue a purely client-side command
            //it's now slowly getting every command here and using the new command syntax. (sendmessage iscommand true.)

            if (command.Split(' ')[0] == "/ofm" && command.Split(' ').Length > 1 && username == "Aaron")
            {
                sendMessage(command.Replace("/ofm ", ""), "Server", "Official Message");
                return true;
            }
            else

           if (command.Split(' ')[0] == "/changenameto" && command.Split(' ').Length > 1)
            {
                username = command.Split(' ')[1];
                return true;
            }
            else

            if (command.Split(' ')[0] == "/vertical")
            {
                string message = command.Remove(0, 9);
                char[] messagearray = message.ToCharArray();
                string frontchar = "";
                if (messagearray[1] == '>')
                    frontchar = ">";

                /* string tosend = message + "\r\n";

                 foreach( char piece in messagearray){
                     tosend += (piece.ToString() + Environment.NewLine);
                 }
                 sendMessage(tosend);*/
                sendMessage(message.Remove(0, 1));


                for (int i = 2 + frontchar.Length; i < messagearray.Length; i++)
                {
                    sendMessage(frontchar + messagearray[i].ToString());
                }
                return true;

            } else

            if (command.Split(' ')[0] == "/dreemurr") {
                if (!asriel)
                {
                    ad = new Form2(/*this*/);
                    asriel = true;
                    ad.Show();
                    ad.TopMost = true;
                    this.TopMost = true;
                    this.Focus();
                    flyaround.Tick += Flyaround_Tick;
                    flyaround.Interval = 1;
                    flyaround.Start();

                }
                else {
                    asriel = false;
                    this.TopMost = false;
                    ad.Hide();
                    ad.Close();
                    ad.Dispose();
                    flyaround.Stop();
                }
                return true;
            } else

            if (command.Split(' ')[0] == "/spy")
            {
                if (username == "Aaron")
                {
                    if (spying)
                    {
                        spying = false;
                    }
                    else
                    {
                        spying = true;
                    }
                    return true;
                }
            }
            else

            if (command.Split(' ')[0] == "/help")
            {
                helpToolStripMenuItem_Click(null, null);
            }
            else

            if (command.Split(' ')[0] == "/meme")
            {
                // 

                switch (DateTime.Now.Millisecond.ToString().ToCharArray()[DateTime.Now.Millisecond.ToString().ToCharArray().Length - 1]) {
                    case ('1'):
                        sendMessage("                                                  ______                                                            ");
                        sendMessage("                                          _.-*'\"            \"`*-._                                        ");
                        sendMessage("                                _.-*'                                    `*-._                                  ");
                        sendMessage("                          .-'                                                        `-.                            ");
                        sendMessage("    /`-.        .-'                                    _.                            `-.                      ");
                        sendMessage("  :        `..'                                    .-'_  .                                `.                  ");
                        sendMessage("  |        .'                                  .-'_.'  \\  .                                  \\            ");
                        sendMessage("  |      /                                  .'  .*          ;                              .-'\"              ");
                        sendMessage("  :      L                                        `.          |  ;                    .-'                        ");
                        sendMessage("    \\.'  `*.                    .-*\"*-.    `.      ;  |                .'                          ");
                        sendMessage("    /            \\                '              `.    `-'    ;            .'                                ");
                        sendMessage("  :  .'\"`.    .              .-*'`*-.    \\          .            (_                                ");
                        sendMessage("  |                            .'                \\    .                          `*-.                          ");
                        sendMessage("  |.          .            /                      ;                                      `-.                      ");
                        sendMessage("  :        db            '              d$b    |                                            `-.                ");
                        sendMessage("  .      :PT;.      '              :P\"T;  :                                                  `.          ");
                        sendMessage("  :      :bd;      '                :b_d;  :                                                      \\        ");
                        sendMessage("  |      :$$;  `'                  :$$$;  |                                                        \\      ");
                        sendMessage("  |        TP                            T$P    '                                                          ;      ");
                        sendMessage("  :                                                /.-*'\"`.                                              |    ");
                        sendMessage(".sdP^T$bs.                              /'              \\                                                  ");
                        sendMessage("$$$._.$$$$b.--._            _.'      .--.      ;                                                  ");
                        sendMessage("`*$$$$$$P*'          `*--*'          '    /  \\    :                                                ");
                        sendMessage("      \\                                                .'      ;  ;      SONIC                                ");
                        sendMessage("        `.                                    _.-'        '  /                  THE                            ");
                        sendMessage("            `*-.                                            .'                          EDGEHOG              ");
                        sendMessage("                    `*-._                        _.-*'                                                          ");
                        sendMessage("                              `*=--..--=*'                                                                    ");
                        break;
                    case ('2'):
                        sendMessage("I know this is a little weird, but...");
                        System.Threading.Thread.Sleep(500);
                        sendMessage("I've decided I'm going to try being a furry. Anyone want to come with me to FurCon 2016?");
                        break;
                    case ('3'):
                        sendMessage("http://i.imgur.com/a7s8Hhd.jpg");
                        break;
                    case ('4'):
                        sendMessage("yee haw");
                        break;
                    case ('5'):
                        sendMessage("you guys heard trump said that he frequents 4chan, right?");
                        break;
                    case ('6'):
                        sendMessage("3chan>storychan");
                        break;
                    case ('7'):
                        sendMessage("dae bunny polices");
                        break;
                    case ('8'):
                        sendMessage(">implying 4chan is good");
                        break;
                    case ('9'):
                        sendMessage("Thanks, ograbme.");
                        break;
                    default:
                        sendMessage("figurative edge");
                        break;
                        
                }





                
               // textBox1.Text = "LOL FID IS GONE WE CAN MAKE FUN OF TRUMP";
                button1_Click(null, null);
                return true;
            }

            else
            if (command.Split(' ')[0] == "/clear")
            {
                richTextBox1.Text = "";
                raw = "";
                sendMessage("all cleared.");
                return true;

            }else
            if (command.Split(' ')[0] == "/>")
            {
                if (!greentexting)
                {
                    greentexting = true;
                    textBox1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#A8AA78");
                    textBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1E0D6");
                }
                else
                {
                    textBox1.ForeColor = Color.Black;
                    textBox1.BackColor = Color.White;
                    greentexting = false;
                }
                return true;
            }

            else
            if (command.Split(' ')[0] == "/log")
            {

                if (logging)
                {

                    textBox1.Text = "stlo.";
                    logging = false;
                }
                else
                {
                    textBox1.Text = "lonow";
                    logging = true;
                }
                return true;

            }
            else if(command.Split(' ')[0] == "/gui")
            {
                if (username == "Aaron")
                {
                    AdminGui ag = new AdminGui(this);
                    ag.Show();
                    return true;
                }

            }else if(command.Split(' ')[0] == "/list"){

                sendMessage("/list *", username, displayname, true);
                raw += "online: \u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007";
                parse_Messages("online: \u0007Server\u0007Official Message\u0007" + System.DateTime.Now.TimeOfDay + "\u0007");
                return true;
            }


            return false;
        }

        private void Flyaround_Tick(object sender, EventArgs e)
        {
            //true=right for flyarounddir.
            if (!gameinsteadoffly)
            {
                int amounttogo = (-1 * Math.Abs(flyaroundx * 2) / 100) + 10;


                Left = flyaroundx + Screen.PrimaryScreen.Bounds.Width / 2;
                Top = (1 * (flyaroundx * flyaroundx) / 800 + Screen.PrimaryScreen.Bounds.Height / 4);

                Console.WriteLine(amounttogo);

                if (flyaroundx < -460)
                {
                    flyaroundx = -460;
                }

                if (flyarounddir)
                {//if going right
                    flyaroundx += amounttogo;
                    if (flyaroundx > 460)
                        flyarounddir = false;
                }
                else
                {
                    flyaroundx -= amounttogo;
                    if (flyaroundx < -460)
                        flyarounddir = true;
                }
            }
            else {

               

            }

        }
        //  [DllImport("user32.dll", EntryPoint = "GetKeyboardState", SetLastError = true)]
        //  private static extern bool NativeGetKeyboardState([Out] byte[] keyStates);


        private bool authenticate()
        {
            /*for (int i = 0; i < ASCIIEncoding.Unicode.GetByteCount(username)-1;i++) {

                if (ASCIIEncoding.Unicode.GetBytes(username)[i]  == 'u')
            }*/

            var sha512 = new SHA512CryptoServiceProvider();
            string[] users = {"Aaron"  ,
                "Max"     ,
                "Ethan"   ,
                "Guest"   ,
                "Ammon"   ,
                "Anthony" ,
                "Nathan"  ,
                "Fidler"  ,
                "Clay"    ,
                "Robben"  ,
                "Westin"  ,
                "3"};
            string[] passhashes = {
                "F0-97-FD-6E-1B-15-6E-00-7E-97-85-F5-EE-7B-23-0C-9C-E9-DE-13-A7-1A-FB-D5-4E-D5-70-97-22-DF-E6-4E-44-BD-90-A5-88-D7-63-DC-93-D5-F6-6B-84-86-BB-0E-1F-C4-5F-94-AD-87-A7-C6-3B-CE-49-9B-B2-91-12-DB",
                "43-BE-18-38-80-5A-69-E4-E6-A2-53-F2-82-45-D4-37-E8-16-02-52-BE-66-EB-5D-88-77-70-35-BF-9D-9B-B2-83-91-E8-DC-55-50-23-9E-96-70-5E-C9-9E-26-4A-D6-9B-12-58-3D-3D-FF-FB-F9-C7-D2-4C-4B-69-68-7B-D8",
                "5E-87-52-DB-6E-B2-9E-89-4A-8F-A1-E7-40-E2-D6-1C-21-C4-45-26-7E-AF-EB-C0-CF-D2-9F-C2-8E-C0-C1-C7-57-D3-C7-91-81-BB-5D-70-79-2B-0A-AF-D2-F5-39-FD-0D-C1-34-B6-2A-88-02-50-63-14-E6-CD-F9-13-97-76",
                "FA-58-5D-89-C8-51-DD-33-8A-70-DC-F5-35-AA-2A-92-FE-E7-83-6D-D6-AF-F1-22-65-83-E8-8E-09-96-29-3F-16-BC-00-9C-65-28-26-E0-FC-5C-70-66-95-A0-3C-DD-CE-37-2F-13-9E-FF-4D-13-95-9D-A6-F1-F5-D3-EA-BE",
                "86-A3-E8-36-E7-46-DD-60-1D-13-A0-66-51-37-58-A1-8E-7E-67-00-E4-D1-CF-0C-84-85-A0-20-34-7E-4E-89-05-59-3D-98-D5-ED-11-B1-37-66-AC-B2-1F-1E-EE-2F-1A-01-01-19-A9-EA-83-49-B3-BA-32-CB-64-2F-CE-17",
                "D2-27-EE-85-91-93-52-C2-52-59-2D-2A-CF-19-BE-D4-75-0D-08-32-82-85-38-8B-B6-AE-59-35-2D-73-0C-11-B1-92-37-E4-D7-0F-4B-ED-E0-B2-9A-30-50-B4-09-A3-DA-EF-C9-B7-65-95-0F-1F-2D-43-43-C1-FC-A2-19-37",
                "D2-27-EE-85-91-93-52-C2-52-59-2D-2A-CF-19-BE-D4-75-0D-08-32-82-85-38-8B-B6-AE-59-35-2D-73-0C-11-B1-92-37-E4-D7-0F-4B-ED-E0-B2-9A-30-50-B4-09-A3-DA-EF-C9-B7-65-95-0F-1F-2D-43-43-C1-FC-A2-19-37",
                "2B-D1-2E-D0-25-1C-62-D8-37-96-D2-9E-45-F2-12-82-07-1F-94-E2-6B-97-D2-53-04-77-5E-11-F0-42-18-4F-02-08-89-F5-A7-E6-4C-A1-EB-10-93-26-08-59-95-4F-C5-A2-65-8E-6B-33-86-69-59-DE-08-DA-5C-65-C2-07",
                "E7-B7-DF-D7-91-FE-67-8B-E6-CA-40-E4-D4-06-4F-97-01-2F-BF-92-FF-E0-C8-D9-4E-EE-E3-D3-35-28-B2-B9-7E-90-16-AF-2B-73-DD-D2-C7-34-A7-87-53-AC-44-D2-53-AC-3A-33-88-37-27-18-DA-CB-B1-87-14-42-19-47",
                "F9-AC-F8-E0-17-C4-21-96-C7-DF-D9-2A-5C-1C-A8-49-4A-5C-70-C5-99-BD-12-81-95-F9-FA-E9-0E-A8-E0-40-09-50-D6-41-96-1A-FB-C0-7E-CE-E2-37-26-A3-7C-EA-E7-52-63-30-6A-40-01-1C-F6-14-26-F6-7D-A7-24-93",
                "B1-09-F3-BB-BC-24-4E-B8-24-41-91-7E-D0-6D-61-8B-90-08-DD-09-B3-BE-FD-1B-5E-07-39-4C-70-6A-8B-B9-80-B1-D7-78-5E-59-76-EC-04-9B-46-DF-5F-13-26-AF-5A-2E-A6-D1-03-FD-07-C9-53-85-FF-AB-0C-AC-BC-86",
                "3B-AF-BF-08-88-2A-2D-10-13-30-93-A1-B8-43-3F-50-56-3B-93-C1-4A-CD-05-B7-90-28-EB-1D-12-79-90-27-24-14-50-98-06-51-99-45-01-42-3A-66-C2-76-AE-26-C4-3B-73-9B-C6-5C-4E-16-B1-0C-3A-F6-C2-02-AE-BB" };
            if (users.Contains(username)) {
                if (passhashes[Array.IndexOf(users, username)] == BitConverter.ToString(sha512.ComputeHash(Encoding.ASCII.GetBytes(password)))) 
                {
                    if (username == "Aaron" || username == "3") {
                        logging = true;
                        username = "Aaron";
                        displayname = "Aaron";
                    }
                    return true;
                }else{
                    return false;
                }
            }
            else { return false; }

           /* switch (username)
            {
                case "Aaron":
                    if (BitConverter.ToString(sha512.ComputeHash(Encoding.ASCII.GetBytes(password))) == passhashes[1]) { logging = true; return true; }
                    else { return false; }
                    break;

                case "Max":
                    if (password == "MemesDanko2016") { return true; }
                    else { return false; }
                    break;

                case "Ethan":
                    if (password == "Solgarde") { return true; }
                    else { return false; }
                    break;
                case "Guest":
                    if (password == "12345678") { return true; }
                    else { return false; }
                    break;
                case "Ammon":
                    if (password == "sexyness") { return true; }
                    else { return false; }
                    break;
                case "Anthony":
                    if (password == "AAIT") { return true; }
                    else { return false; }
                    break;
                case "Nathan":
                    if (password == "Smallpower") { return true; }
                    else { return false; }
                    break;
                case "Fidler":
                    if (password == "Smallpower") { return true; }
                    else { return false; }
                    break;
                case "Clay":
                    if (password == "Poffenberger") { return true; }
                    else { return false; }
                    break;
                case "Robben":
                    if (password == "Mcagiz") { return true; }
                    else { return false; }
                    break;
                case "Westin":
                    if (password == "password") { return true; }
                    else { return false; }
                    break;
                case "3":
                    if (password == "3") { logging = true; username = "Aaron"; displayname = "Aaron"; return true; }
                    else { return false; }
                    break;

                default:
                    return false;
                    break;
            }*/

        }
        private bool isReceivedCommand(string sender, string str, int i)
        {//this block stops commands that people sent from showing
            //Console.WriteLine("Thing 1!");

            if (!spying)
            {
                if (str.Split(' ')[0] == "/tell" && str.Split(' ').Length > 1)
                {
                    if (i < 1)
                    {
                        commandcount++;
                    }
                    return true;
                }
            }

            if (str.Split(' ')[0] == "/len" && str.Split(' ').Length == 1)
            {
                replacementword = "( ͡° ͜ʖ ͡°)";

                return true;
            }
            if (str.Split(' ')[0] == "/kick" && str.Split(' ').Length > 1)
            {
                if (str.Split(' ')[1] == username && sender == "Aaron")
                {
                    if (!gettinghistory)
                    {
                        replacementword = "ouch!";
                        sendMessage("Has been kicked!");
                        thesocket.Close();
                        Application.Exit();
                       // if(!this.InvokeRequired)
                            this.Close();
                       /* else
                        {
                            System.Delegate(Close(),"memes");
                            Control.Invoke(this.Close());
                        }*/
                        return true;
                    }
                }
            }
            if (str.Split(' ')[0] == "/mute" && str.Split(' ').Length > 1) {
                if (str.Split(' ')[1] == username && sender == "Aaron")
                {
                    // replacementword = "Has been muted!!!";//on clients they see certain strings as others through replacementword. 
                    if (!gettinghistory)
                    {
                        if (muted)
                        {
                            muted = false;
                            sendMessage("Has been unmuted...", "Server", "Official Message");

                        }
                        else
                        {
                            sendMessage("Has been muted...", "Server", "Official Message");
                            muted = true;
                        }
                    //thesocket.Close();
                   // Application.Exit();
                    //this.Close();
                    return true;
                    }
                }
            }
            if(username == "Anthony")
            {
                Console.WriteLine("helo am antony");
                if (str.Split(' ')[0] == "/bib")
                {
                    Console.WriteLine("hiding lol");
                    replacementword = null;
                    return true;
                }

            }


            return false;

        }

        /*private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string parsingstring = listBox1.SelectedItem.ToString();
            string[] parsedbits = parsingstring.Split(' ');
            for (int i = 0; i < parsedbits.Length; i++) {
                if (parsedbits[i].Length > 8) {
                    if (parsedbits[i].Substring(0, 7) == "http://" || parsedbits[i].Substring(0,8) == "https://")
                    {
                        System.Diagnostics.Process.Start(parsedbits[i]);

                    }
                }
                Console.WriteLine(parsedbits[i]);
            }
        }*/

        private void textSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void disguiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // richTextBox1.Visible = true;
            // this.Icon = System.Drawing.Icon(Path.GetFullPath(@""))
        }
        public void sendMessage(string message, string sender = null, string senderdisp = null, bool iscommand = false)
        {
            /* HttpWebRequest sendmessagerequest = (HttpWebRequest)WebRequest.Create(serverurl);
             sendmessagerequest.KeepAlive = false;
             sendmessagerequest.Method = "POST";
             sendmessagerequest.UserAgent = "chat program";

             byte[] byteArray = Encoding.ASCII.GetBytes("message=" + message + "&user=" + username + "&displayname=" + displayname);
             sendmessagerequest.ContentType = "application/x-www-form-urlencoded";
             sendmessagerequest.ContentLength = byteArray.Length;
             Stream dataStream = sendmessagerequest.GetRequestStream();
             Console.WriteLine("Version");
             dataStream.Write(byteArray, 0, byteArray.Length);
             dataStream.Close();
             dataStream.Dispose();
             WebResponse response = sendmessagerequest.GetResponse();
             response.Close();
             textBox1.Text = "";
             //parse_Messages(null, null);*/
            if (muted) {
                if (username == "Aaron")
                {
                    Console.WriteLine("am mod lol");
                }
                else
                {
                    NotiPop n = new NotiPop("You're muted. You probably deserved it.");
                    n.Creator = this;
                    n.BackColor = backcolor;
                    n.ForeColor = textcolor;
                    n.Show();
                    return;
                }
            }
            
            try
            {
                if (iscommand) {
                    thesocket.Send(message + "\u0007" + username + "\u0007" + ".");
                   
                }
                else

                if (sender == null || senderdisp == null)//if given parameter
                {
                    //lastmessages[2] = lastmessages[1];
                    //lastmessages[1] = lastmessages[0];
                    //lastmessages[0] = message;
                    //if (!(lastmessages[0] == lastmessages[1] && lastmessages[1] == lastmessages[2]))
                    //{
                        thesocket.Send(message + "\u0007" + username + "\u0007" + displayname + "\u0007");
                        textBox1.Text = "";
                   // }
                   /* else {
                        NotiPop gtfo = new NotiPop("no spamming!");
                        gtfo.BackColor = backcolor;
                        gtfo.ForeColor = textcolor;
                        gtfo.Show();
                        System.Threading.Thread.Sleep(1000);
                    }*/
                }
                else
                {
                    thesocket.Send(message + "\u0007" + sender + "\u0007" + senderdisp + "\u0007");
                }
            }
            catch {
                try
                {
                    Console.WriteLine("going itn....");

                    if (thesocket.State == WebSocketState.Open) {
                        thesocket.Close();
                    }

                    thesocket.Open();
                    Console.WriteLine("in.....wew.");
                }
                catch {
                    //richTextBox1.Text = "The server is down!";
                }
                
            }
        }
        public void sendFile(string name, string file64)
        {
            sendMessage(username + " is sending everyone " + name, "Server", "Official Message");
            thesocket.Send(name + "\u0007" + file64);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (password != "")
            {
                sendMessage(username + " has left the room.", "Server", "Official Message");
            }
            thesocket.Close();
            SaveSettings();
            SystemEvents.SessionEnding -= SystemEvents_SessionEnding;
            

            bool removablein = false;
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {


        }
        //HERE I WILL DEFINE COMMAND FUNCTIONALITY IN FUNCTIONS.
        private void getKicked() {
            sendMessage("Has been kicked!");
            thesocket.Close();
            Application.Exit();
        }
        private void getMuted() {
            Console.WriteLine("it's a mute point");
            if (muted)
            {
                muted = false;
                sendMessage("Has been unmuted...", "Server", "Official Message");
            }
            else
            {
                sendMessage("Has been muted...", "Server", "Official Message");
                muted = true;
            }
        }
        private void getBanned() {

        }
        private void forceTop() {
            
            if (TopMost)
            {

                this.Invoke((MethodInvoker)delegate {
                    TopMost = false; //runs on UI thread
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate {
                    TopMost = true; //same here
                });
            }
        }





        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(e.X + this.Location.X, e.Y + this.Location.Y));
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {

            if(e.LinkText.Split('.')[1] == "showsketch")
            {


            }
            if (enableQuickImageViewToolStripMenuItem.Checked)
            {
                char[] arrayofurl = e.LinkText.ToCharArray();
                string last3 = "" + arrayofurl[arrayofurl.Length - 3] + arrayofurl[arrayofurl.Length - 2] + arrayofurl[arrayofurl.Length - 1];

                if (last3 == "png" || last3 == "jpg" || last3 == "peg" || last3 == "gif" || last3 == "bmp")
                {
                    Console.WriteLine("last3" + last3);
                    Netpeek np = new Netpeek(e.LinkText);
                    np.Show();

                }
                else
                {
                    System.Diagnostics.Process.Start(e.LinkText);
                }
            }
            else {
                Process.Start(e.LinkText);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        public string GetImage(string path, int width, int height)
        {
            MemoryStream stream = new MemoryStream();
            string newPath = Path.Combine(Environment.CurrentDirectory, path);
            Image img = Image.FromFile(newPath);
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            byte[] bytes = stream.ToArray();

            string str = BitConverter.ToString(bytes, 0).Replace("-", string.Empty);
            //string str = System.Text.Encoding.UTF8.GetString(bytes);

            string mpic = @"{\pict\pngblip\picw" +
                img.Width.ToString() + @"\pich" + img.Height.ToString() +
                @"\picwgoal" + width.ToString() + @"\pichgoal" + height.ToString() +
                @"\hex " + str + "}";
            return mpic;
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                button1_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
                Console.WriteLine("this");
            }

        }
        private void thesocket_Opened(object sender, EventArgs e)
        {
            if (saylogin)
            {
                sendMessage(username + " has joined!", "Server", "Official Message");
            }
            else { saylogin = true; }
        }


        private delegate void ColorSwitchCallback();

        public void ColorSwitch()
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new ColorSwitchCallback(ColorSwitch));
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {
                    richTextBox1.SelectionColor = secondarytextcolor;

                }
                else
                {
                    Console.WriteLine("Error code 0001");
                }
            }
        }

        private delegate void AppendBoxTextCallback(String s);

        public void AppendBoxText(String s)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new AppendBoxTextCallback(AppendBoxText), s);
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {
                    richTextBox1.SelectionColor = secondarytextcolor;

                }
                else
                {
                    Console.WriteLine("Error code 0002");
                }
            }
        }

        public void sans(String[] words, int i) {
            richTextBox1.SelectionColor = secondarytextcolor;
            if (disptime)
            {
                richTextBox1.AppendText(words[i + 3]);
            }
            richTextBox1.AppendText(" (" + words[i + 1] + ") " + words[i + 2] + ": ");
            richTextBox1.SelectionColor = textcolor;
            richTextBox1.AppendText(words[i] + "\r\n");
            richTextBox1.ScrollToCaret();//inserting it to replace

            richTextBox1.Select(richTextBox1.TextLength - 5, 5);
            richTextBox1.SelectedRtf = File.ReadAllText(@"content/emotes/sans.rtf").Replace(System.Environment.NewLine, "");
            richTextBox1.AppendText("");
            // commandcount++;
            richTextBox1.SelectionColor = textcolor;

        }

        private delegate void FGMCallback(String[] words, int i);

        public void FGM(String[] words, int i)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new FGMCallback(FGM), new object[] { words, i });
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {
                    richTextBox1.SelectionColor = secondarytextcolor;

                    if (disptime)
                    {
                        richTextBox1.AppendText(" " + words[i + 3]);
                    }
                    richTextBox1.AppendText(" (" + words[i + 1] + ") " + words[i + 2] + ": ");

                    richTextBox1.SelectionColor = textcolor;
                    richTextBox1.AppendText(words[i]);
                    richTextBox1.ScrollToCaret();//inserting it to replace



                    richTextBox1.Select(richTextBox1.TextLength - 5, 5);
                    richTextBox1.SelectedRtf = File.ReadAllText(@"content/emotes/happyfrog.rtf").Replace(System.Environment.NewLine, "");
                    //richTextBox1.AppendText("");
                    // commandcount++;
                    richTextBox1.SelectionColor = textcolor;

                }
                else
                {
                    Console.WriteLine("Error code 0003");
                }
            }
        }


        private delegate void FBMCallback(String[] words, int i);

        public void FBM(String[] words, int i)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new FBMCallback(FBM), new object[] { words, i });
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {
                    richTextBox1.SelectionColor = secondarytextcolor;
                    if (disptime)
                    {
                        richTextBox1.AppendText(words[i + 3]);
                    }
                    richTextBox1.AppendText(" (" + words[i + 1] + ") " + words[i + 2] + ": ");
                    richTextBox1.SelectionColor = textcolor;
                    richTextBox1.AppendText(words[i] + "\r\n");
                    richTextBox1.ScrollToCaret();//inserting it to replace

                    richTextBox1.Select(richTextBox1.TextLength - 5, 5);
                    richTextBox1.SelectedRtf = File.ReadAllText(@"content/emotes/sadfrog.rtf").Replace(System.Environment.NewLine, "");
                    richTextBox1.AppendText("");
                    // commandcount++;
                    richTextBox1.SelectionColor = textcolor;

                }
                else
                {
                    Console.WriteLine("Error code 0004");
                }
            }
        }



        private delegate void LMACallback(String[] words, int i);

        public void LMA(String[] words, int i)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new LMACallback(LMA), new object[] { words, i });
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {
                    richTextBox1.SelectionColor = secondarytextcolor;
                    if (disptime)
                    {
                        richTextBox1.AppendText(words[i + 3]);
                    }
                    richTextBox1.AppendText(" (" + words[i + 1] + ") " + words[i + 2] + ": ");
                    richTextBox1.SelectionColor = textcolor;
                    richTextBox1.AppendText(words[i] + "\r\n");
                    richTextBox1.ScrollToCaret();//inserting it to replace

                    richTextBox1.Select(richTextBox1.TextLength - 5, 5);
                    richTextBox1.SelectedRtf = File.ReadAllText(@"content/emotes/humorousalien.rtf").Replace(System.Environment.NewLine, "");
                    richTextBox1.AppendText("");
                    // commandcount++;
                    richTextBox1.SelectionColor = textcolor;

                }
                else
                {
                    Console.WriteLine("Error code 0004");
                }
            }
        }

        private delegate void InsertForTellCallback(String[] words, int i);

        public void InsertForTell(String[] words, int i)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new InsertForTellCallback(InsertForTell), new object[] { words, i });
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {
                    richTextBox1.SelectionColor = secondarytextcolor;
                    if (words[i].Split(' ')[1] == username)
                    {
                        if (disptime)
                        {
                            richTextBox1.AppendText(words[i + 3]);
                        }
                        richTextBox1.AppendText(" (" + words[i + 1] + ") " + words[i + 2] + " whispered to you!:" + words[i].Remove(0, (6 + words[i].Split(' ')[1].Length)) + "\r\n");
                        richTextBox1.SelectionColor = textcolor;
                        lastMessage = (words[i + 2] + " whispered to you!:" + words[i].Remove(0, (6 + words[i].Split(' ')[1].Length)));
                    }
                    else {
                        if (disptime)
                        {
                            richTextBox1.AppendText(words[i + 3]);
                        }
                        richTextBox1.AppendText( " You whispered to " + words[i].Split(' ')[1] + ":" + words[i].Remove(0, (6 + words[i].Split(' ')[1].Length)) + "\r\n");
                        richTextBox1.SelectionColor = textcolor;
                        lastMessage = (" You whispered to " + words[i].Split(' ')[1] + ":" + words[i].Remove(0, (6 + words[i].Split(' ')[1].Length)) + "\r\n");
                    }
                }
                else
                {
                    Console.WriteLine("Error code 0005");
                }
            }
        }

        private delegate void InsertTextCallback(String[] words, int i);

        public void InsertText(String[] words, int i)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new InsertTextCallback(InsertText), new object[] { words, i });
            }
            else
            {
                if (richTextBox1.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
                {


                    richTextBox1.SelectionColor = secondarytextcolor;
                    //                            ColorSwitch();
                    //                            AppendBoxText(words[i + 3] + " (" + words[i + 1] + ") " + words[i + 2] + ": ");
                    if (disptime)
                    {
                        richTextBox1.AppendText(words[i + 3]);
                    }
                    richTextBox1.AppendText(" (" + words[i + 1] + ") " + words[i + 2] + ": ");
                    richTextBox1.SelectionColor = textcolor;
                    richTextBox1.AppendText(words[i] + "\r\n");
                    richTextBox1.ScrollToCaret();
                    richTextBox1.AppendText("");//normal appendation

                    lastMessage = (words[i + 2] + ": " + words[i]);


                    richTextBox1.ScrollToCaret();
                }
                else
                {
                    Console.WriteLine("Error code 0006");
                }
            }
        }



        private void ping(object sender, EventArgs e)
        {
            thesocket.Send("\u0007\u0007\u0007\u0007\u0007");
            //Console.WriteLine("pingehd");
            if (initialtime + 16200000000 < DateTime.Now.Ticks) {
                saylogin = false;

                WebSocket s = new WebSocket("ws://felixchat.herokuapp.com");
                s.Open();
                s.Send("\u0007\u0007\u0007\u0007\u0007");
                s.Close();
                s.Dispose();
                Console.Write("REFRESHHHEHEDDDDDDDDD>>>>>>....");

                
                initialtime = DateTime.Now.Ticks;
            }

        }

        private void refreshConnections() {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (ghostModeToolStripMenuItem.Checked)
                {
                    ghostModeToolStripMenuItem.Checked = false;
                }
                else
                {
                    ghostModeToolStripMenuItem.Checked = true;
                }

                ghostModeToolStripMenuItem_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                toggleDisguiseMode();
            }
            else if (e.KeyCode == Keys.F4)
            {
                mountToolStripMenuItem_Click(null, null);
            }
            else if (e.Control && e.Shift) {
                if (e.KeyCode == Keys.Up)
                    Top--;
                if (e.KeyCode == Keys.Down)
                    Top++;
                if (e.KeyCode == Keys.Left)
                    Left--;
                if (e.KeyCode == Keys.Right)
                    Left++;
            }
            else if(e.KeyCode == Keys.C && e.Control)
            {
                if(textBox1.SelectionLength != 0)

                    Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void displayTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (disptime)
            {
                disptime = false;
                displayTimeToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                disptime = true;
                displayTimeToolStripMenuItem.CheckState = CheckState.Checked;
            }
            richTextBox1.Clear();
            parse_Messages(raw);
        }

        private void LoadSettings()
        {
            if (File.Exists("settings"))
            {
                try {
                    string settingsraw = File.ReadAllText("settings");
                    backcolor = System.Drawing.ColorTranslator.FromHtml(settingsraw.Split(';')[0]);
                    buttoncolor = System.Drawing.ColorTranslator.FromHtml(settingsraw.Split(';')[1]);
                    textcolor = System.Drawing.ColorTranslator.FromHtml(settingsraw.Split(';')[2]);
                    secondarytextcolor = System.Drawing.ColorTranslator.FromHtml(settingsraw.Split(';')[3]);
                    if (settingsraw.Split(';')[4] == "False") { disptime = false; } else { disptime = true; }
                    this.Height = int.Parse(settingsraw.Split(';')[5]);
                    this.Width = int.Parse(settingsraw.Split(';')[6]);
                    this.Top = Screen.PrimaryScreen.Bounds.Height - int.Parse(settingsraw.Split(';')[7]);
                    this.Left = int.Parse(settingsraw.Split(';')[8]);
                    switch (settingsraw.Split(';')[9])
                    {
                        case "1":
                            rumbleToolStripMenuItem.Checked = true;
                            noneToolStripMenuItem.Checked = false;
                            break;
                        case "2":
                            blinkToolStripMenuItem.Checked = true;
                            noneToolStripMenuItem.Checked = false;
                            break;
                        case "3":
                            hideShowToolStripMenuItem.Checked = true;
                            noneToolStripMenuItem.Checked = false;
                            break;
                        default:
                            break;
                    }
                    filepath = settingsraw.Split(';')[10];
                    if (!(Directory.Exists(filepath)))
                        filepath = "";
                    if (settingsraw.Split(';')[11] == "False") { muted = false; } else { muted = true; }

                }
                catch (IndexOutOfRangeException) {
                    richTextBox1.AppendText("\r\n outdated settings file detected. It will be patched on close.");

                }
            }
        }

        private void SaveSettings()
        {
            int notifymode = 0;

            if (rumbleToolStripMenuItem.Checked == true)
                notifymode = 1;
            if (blinkToolStripMenuItem.Checked == true)
                notifymode = 2;
            if (hideShowToolStripMenuItem.Checked == true)
                notifymode = 3;

            FileStream fs = new FileStream("settings", FileMode.Create);
            string towrite = "";
            towrite += System.Drawing.ColorTranslator.ToHtml(backcolor);
            towrite += ";";
            towrite += System.Drawing.ColorTranslator.ToHtml(buttoncolor);
            towrite += ";";
            towrite += System.Drawing.ColorTranslator.ToHtml(textcolor);
            towrite += ";";
            towrite += System.Drawing.ColorTranslator.ToHtml(secondarytextcolor);
            towrite += ";";
            towrite += disptime;
            towrite += ";";
            towrite += this.Height;
            towrite += ";";
            towrite += this.Width;
            towrite += ";";
            towrite += Screen.PrimaryScreen.Bounds.Height - this.Top;
            towrite += ";";
            towrite += this.Left;
            towrite += ";";
            towrite += notifymode;
            towrite += ";";
            towrite += filepath;
            towrite += ";";
            towrite += muted;

            fs.Write(Encoding.ASCII.GetBytes(towrite), 0, towrite.Length);
            // this.Left = 0;
            // this.Top = Screen.PrimaryScreen.Bounds.Height - 270;
        }//how the settings file is set up:

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();

        }

        private void ghostModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ghost mode activated!");
            if (ghostModeToolStripMenuItem.Checked)
            {
                this.Opacity = .50;
            }
            else
            {
                this.Opacity = 1.00;
            }
        }

        private void mountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mountToolStripMenuItem.Text == "Mount")
            {
                mountToolStripMenuItem.Text = "Dismount";
                this.FormBorderStyle = FormBorderStyle.None;
                this.Top += 30;
                this.Left += 8;
            }
            else
            {
                mountToolStripMenuItem.Text = "Mount";
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Top -= 30;
                this.Left -= 8;
            }

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rumbleToolStripMenuItem.Checked = false;
            blinkToolStripMenuItem.Checked = false;
            hideShowToolStripMenuItem.Checked = false;
        }

        private void rumbleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            blinkToolStripMenuItem.Checked = false;
            hideShowToolStripMenuItem.Checked = false;
        }

        private void blinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            rumbleToolStripMenuItem.Checked = false;
            hideShowToolStripMenuItem.Checked = false;

        }

        private void hideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            rumbleToolStripMenuItem.Checked = false;
            blinkToolStripMenuItem.Checked = false;
        }

        private void notifyMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotiPop noti = new NotiPop("Ready to deploy.");
            noti.Creator = this;
            noti.Show();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!muted)
            {
                sendingfile = true;

                string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);//returns string array with file paths.
                int i;
                ByteConverter b = new ByteConverter();
                //char[] data = new char[length];



                for (i = 0; i < s.Length; i++)
                {
                    byte[] file = File.ReadAllBytes(s[i]);
                    string file64 = Convert.ToBase64String(file);
                    sendFile(s[i].Split('\\')[s[i].Split('\\').Length - 1], file64);//that first parameter is the name
                }
                sendingfile = false;
            }    
        }

       
        /*private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!muted)
            {
                string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);//returns string array with file paths.
                int i;
                ByteConverter b = new ByteConverter();
                //char[] data = new char[length];
                for (i = 0; i < s.Length; i++)
                {
                    sendMessage(username + " is sending everyone " + s[i].Split('\\')[s[i].Split('\\').Length - 1], "Server", "Official Message");
                    byte[] file = File.ReadAllBytes(s[i]);
                    
                    for(int j = 0; j < (int)Math.Ceiling((decimal)file.Length / 300000); j++)
                    {
                        int cutoffpos = 0;
                        byte[] partition = new byte[300000];

                        for (int bytetocopy = 0 + (300000*j); bytetocopy < 300000 + (300000 * j); bytetocopy++)
                        {
                            try
                            {
                                partition[bytetocopy] = file[bytetocopy];
                            }
                            catch (IndexOutOfRangeException) {
                                cutoffpos = bytetocopy % 300000;
                                break;
                            }
                        }
                        string file64 = Convert.ToBase64String(partition);

                        if (j+1  == (int)Math.Ceiling((decimal)file.Length / 300000))
                        {
                            byte[] lastbit = new byte[cutoffpos];
                            for (int z = 0; z < cutoffpos; z++)
                                lastbit[z] = partition[z];
                            file64 = Convert.ToBase64String(lastbit);
                            thesocket.Send("f\u0007" + s[i].Split('\\')[s[i].Split('\\').Length - 1] + "\u0007" + file64);
                        }
                        else
                        {
                            //Console.WriteLine(j - 1);
                            //Console.WriteLine((int)Math.Ceiling((decimal)file.Length / 300000));
                            thesocket.Send(j + "\u0007" + s[i].Split('\\')[s[i].Split('\\').Length - 1] + "\u0007" + file64);
                        }
                    }                    
                    //that first parameter is the name
                }
            }    
*/
        private void setFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            
            filepath = folderBrowserDialog1.SelectedPath + "\\";
        }

        private void enableQuickImageViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toggleDisguiseMode()
        {
            if (disguisemode)
            {
                this.Controls.Remove(this.Controls.Find("faker", true)[0]);
                this.Icon = new System.Drawing.Icon(Path.GetFullPath(@"content/earth.ico"));
                this.Text = Path.GetFullPath(@"Page.htm") + " - Notepad ++";
                disguisemode = false;
            }
            else
            {
                this.Text = "Untitled - Notepad";
                disguisemode = true;

                this.Icon = new System.Drawing.Icon(Path.GetFullPath(@"content/notepad.ico"));

                RichTextBox faker = new RichTextBox();

                faker.Size = this.richTextBox1.Size;
                faker.Enabled = true;
                faker.Visible = true;
                faker.Name = "faker";
                faker.Parent = this;
                faker.Dock = DockStyle.Fill;
                faker.BorderStyle = BorderStyle.None;
                faker.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
                faker.Font = new System.Drawing.Font("Lucida Console", 10F, FontStyle.Regular);
                faker.Focus();
                faker.KeyDown += delegate (object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                        faker.AppendText("\r\n");
                    Console.WriteLine("key");
                    

                };

                if (lastMessage.Split(':').Length > 1)
                    faker.Text = "\r\n" + lastMessage.Split(':')[1];
                else
                    faker.Text = "\r\n<!DOCTYPE html>\r\n\r\n\r\n local bobble boi";

                //ToolStripContainer tsc = new ToolStripContainer();

                /*MenuStrip ms = new MenuStrip();
                  ms.Anchor = AnchorStyles.Top;
                  ms.Name = "fakertools";
                  ms.Enabled = true;
                  ms.Visible = true;
                  ms.Parent = this;
                  ms.Items.Add("i hope you like this notepad intimidator");


                  tsc.Dock = DockStyle.Top;
                  tsc.Parent = this;
                  tsc.Anchor = AnchorStyles.Right;
                  tsc.TopToolStripPanel.Controls.Add(ts);
                  tsc.Name = "fakertoolscontainer";
                  tsc.Enabled = true;
                  tsc.Visible = true;
                  tsc.S*/



        Controls.SetChildIndex(faker, 0);

                Console.WriteLine("nwehehe youll never catch me");

            }


            }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null && richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }


        //[DllImport("user32.dll", SetLastError = true)]
        // static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            bool removablein = false;
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                
                if (d.DriveType == DriveType.Removable)
                {
                    removablein = true;
                }
            }
            if (removablein)
            {
               
                e.Cancel = true;
                richTextBox1.Font = new Font(DefaultFont.FontFamily, 50);
                this.WindowState = FormWindowState.Maximized;
                richTextBox1.Text = "REMOVE YOUR FLASHDRIVES";
            }
        }

        private void sketchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sketcher s = new Sketcher(this);
            s.Show();
            /*Process p = Process.Start(@"C:\\Windows\\system32\\mspaint.exe");
            System.Threading.Thread.Sleep(500);
            SetParent(p.MainWindowHandle, this.Handle);*/
            /*var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\\Windows\\system32\\mspaint.exe",
                    Arguments = "command line arguments to your executable",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                    
                }
            };*/



        }

        private void blockSwear() {
            sendMessage(username + " Tried to say a swear!", "Server", "Official Message");
            swearjar = new SwearJar();
            swearjar.Show();
            swearjarContent = false;

            
            swearjartimer.Interval = 500;
            swearjartimer.Tick += new EventHandler(keepSwearJarUp);
            swearjartimer.Start();
            this.WindowState = FormWindowState.Minimized;


           
            
            //keeps it from closing unless you did the thing
                
                

            

        }

        private void keepSwearJarUp(object sender, EventArgs e) {
            if (swearjar.IsDisposed)
            {
                if (!swearjarContent) { 
                    swearjar = new SwearJar();
                    swearjar.Show(); }
            }
            if (swearjar.content || swearjarContent)
            {
                swearjarContent = true;
                swearjartimer.Stop();
                swearjar.Close();
                swearjar.Dispose();               
            }
        }

        private void runOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!runOnStartupToolStripMenuItem.Checked)
            {
                string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                using (StreamWriter writer = new StreamWriter(deskDir + "\\chat.url"))
                {
                    string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    writer.WriteLine("[InternetShortcut]");
                    writer.WriteLine("URL=file:///" + app);
                    writer.WriteLine("IconIndex=0");
                    string icon = app.Replace('\\', '/');
                    writer.WriteLine("IconFile=" + icon);
                    writer.Flush();
                    writer.Dispose();
                  }
            }
        }

        /*private void BreakBytes(int length,)*/

        //aaaaaa;bbbbbb;cccccc;dddddd;e;fff;ggg;hhh;iii;j;k;l
    }    //
         //
         //a=back color hex code
         //b=buttoncolor
         //c=textcolor
         //d=secondarytextcolor
         //e=displaytime
         //f=height
         //g=width
         //h=y: pixels from bottom
         //i=x: pixels from left
         //j:notification style. 0=none,1=rumble, 2=blink, 3=hide-show.
         //k: filepath to the file landing zone.
         //l: filepath to notepad++.

    //more than 4 bells = a ping.
    //exactly 4 bells = a message.
    //just the file split unicode character = next message is a file. (not anymore?)


}
