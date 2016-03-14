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
    public partial class SwearJar : Form
    {
        int caretposition = 0;
        public bool content = false;
        public string apologystring = "I am sorry ";
        public int apologyamount = 15;
        public SwearJar()
        {
            InitializeComponent();
            
        }

        private void SwearJar_Load(object sender, EventArgs e)
        {
            this.Show();

            richTextBox1.SelectionColor = Color.DarkGray;
            for (int i = 0; i <= apologyamount; i++)
            {
                richTextBox1.AppendText(apologystring);
            }
            this.TopMost = true;
         }
 
        private void SwearJar_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] apology = apologystring.ToCharArray();
            if (e.KeyChar == apology[caretposition % apology.Length])
            {
                //richTextBox1.Text += e.KeyChar;
                caretposition++;
            }
            if (caretposition >= (apologystring.Length * apologyamount))
                content = true;



            richTextBox1.Clear();
            richTextBox1.SelectionColor = Color.DarkCyan;
            for (int i=0; i <= apologystring.Length * apologyamount; i++)
            {
                if (i == caretposition)
                    richTextBox1.SelectionColor = Color.DarkGray;
                richTextBox1.AppendText(apology[i % apology.Length].ToString());
            }

        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] apology = apologystring.ToCharArray();
            if (e.KeyChar == apology[caretposition % apology.Length])
            {
                richTextBox1.Text += e.KeyChar;
                caretposition++;
            }
            if (caretposition >= (apologystring.Length * apologyamount))
                content = true; 
        }

        private void label1_Click(object sender, EventArgs e)
        {
            caretposition+=9;
        }
    }
}
