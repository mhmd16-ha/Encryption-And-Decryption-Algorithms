using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Encryption_And_Decryption_Algorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      static  bool Cipher = false;
        static bool RSA = true;

        //Caesar Cipher 
        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);

        }
        //Choose e
        public static double get_e(double a, double h)
        {
            double temp;
            while (true)
            {
                temp = a % h;
                if (temp == 0)
                    return h;
                a = h;
                h = temp;
            }
        }
       public static double c;
       public static double d;
       public static double n;
     




        private void Encryptr_Click(object sender, EventArgs e)
        {
            if (Cipher == true)
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
            }else if (RSA == true)
            {
                if (textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("please Enter the Keys");
                }
                else { 
                double p =double.Parse( textBox5.Text);
                double q = double.Parse( textBox4.Text);
                 n = p * q;
                double _e = 2;
                double phi = (p - 1) * (q - 1);
                while (_e < phi)
                {                  
                    if (get_e(_e, phi) == 1)
                        break;
                    else
                        _e++;
                }
                int k = 2; 
                  d = (1 + (k * phi)) / _e;
                var msg = textBox2.Text;
                textBox1.Text = String.Format("{0:F6}", msg);
                c = Math.Pow(double.Parse( msg), _e);
                c = c % n;
                textBox1.Text= String.Format("{0:F6}", c);
            }
            }

        }
        
        private void Decrypte_Click(object sender, EventArgs e)
        {
            if (Cipher == true) { 
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
            }else if (RSA == true)
            {

                double m = Math.Pow(c, d);
                m = m % n;
                textBox2.Text= String.Format("{0:F6}", m);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           int currentMyComboBoxIndex = comboBox1.FindStringExact(comboBox1.Text);
            if (currentMyComboBoxIndex == 0)
            {
                RSA = true;
                Cipher = false;

            }
            else if(currentMyComboBoxIndex == 1)
            {
                RSA = false;
                Cipher = true;
            }
        }
    }
}
