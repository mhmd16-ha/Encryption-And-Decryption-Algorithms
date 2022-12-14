using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption_And_Decryption_Algorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);

        }


        private void Encryptr_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("please Enter the Key");
            }
            else
            {
                var text = textBox2.Text;
                var key = int.Parse(textBox3.Text);
                string output = string.Empty;
                foreach (char ch in text)
                    output += cipher(ch, key);

                textBox1.Text = output;
            }

        }
        private void Decrypte_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("please Enter the Key");
            }
            else
            {
                var text = textBox1.Text;
                var key = int.Parse(textBox3.Text);
                string output = string.Empty;
                foreach (char ch in text)
                    output += cipher(ch, 26 - key);

                textBox2.Text = output;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";


        }
    }
}
