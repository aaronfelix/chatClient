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
    public partial class AdminGui : Form
    {
        bool moreButtonsShown = false;
        Form1 p;
        public AdminGui(Form1 parent)
        {
            InitializeComponent();
            p = parent;
        }

        private void AdminGui_Load(object sender, EventArgs e)
        {
            //if()
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kickButton_Click(object sender, EventArgs e)
        {
            p.sendMessage("/kick " + Target.Text, "Aaron", "Aaron", true);
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            p.sendMessage("/mute " + Target.Text, "Aaron", "Aaron", true);
            Console.WriteLine("/mute " + Target.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {//this is the say button.
            p.sendMessage(Target.Text, "Server", "Official Message");
            Target.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {//this is the log button.

        }

        private void AdminGui_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void AdminGui_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.K:
                        kickButton_Click(null,null);
                        break;
                    case Keys.M:
                        muteButton_Click(null,null);
                        break;
                    case Keys.S:
                        button1_Click(null,null);
                        break;
                    //case Keys.T:
                    //    if(this.Width > 300)


                }
            }
            else
            {
                Target.Focus();
                if(e.Shift)
                    Target.Text = e.KeyData.ToString().ToUpper();
                else
                    Target.Text = e.KeyData.ToString().ToLower();
                Target.SelectionStart = 1;

            }
        }

        private void moreButtons_Click(object sender, EventArgs e)
        {
            if (!moreButtonsShown)
            {
                this.Width = 400;
                moreButtonsShown = true;
            }
            else
            {
                this.Width = 198;
                moreButtonsShown = false;
            }
        }

        private void toggleTopmostButton_Click(object sender, EventArgs e)
        {
            p.sendMessage("/top " + Target.Text, "Aaron", "Aaron", true);
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            p.sendMessage("/lock " + Target.Text, "Aaron", "Aaron", true);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            p.sendMessage("/notepadify " + Target.Text, "Aaron", "Aaron", true);
        }

        private void unswearButton_Click(object sender, EventArgs e)
        {
            p.sendMessage("/unswear " + Target.Text, "Aaron", "Aaron", true);
        }

        private void swearButton_Click(object sender, EventArgs e)
        {
            p.sendMessage("/swear " + Target.Text, "Aaron", "Aaron", true);
        }
    }
}
