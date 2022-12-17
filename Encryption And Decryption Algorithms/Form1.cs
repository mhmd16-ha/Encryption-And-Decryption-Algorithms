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
        public static double c;
        public static double d;
        public static double n;

        static bool Cipher = false;
        static bool Affine = true;
        static bool vigenere = false;
        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

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
        //gsd in RSA
        public static double gcd(double a, double h)
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
        //Viginer
        private static string ViginerCipher(string input, string key, bool encipher)
        {
            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                    return null; // Error

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = encipher ? k : -k;
                    char ch = (char)((Mod(((input[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                    ++nonAlphaCharCount;
                }
            }

            return output;
        }

        public static string ViginerEncrypt(string input, string key)
        {
            return ViginerCipher(input, key, true);
        }

        public static string ViginerDecrypt(string input, string key)
        {
            return ViginerCipher(input, key, false);
        }

        //Afine 
        public static string AffineEncrypt(string plainText, int a, int b)
        {
            string cipherText = "";

            // Put Plain Text (all capitals) into Character Array
            char[] chars = plainText.ToUpper().ToCharArray();

            // Compute e(x) = (ax + b)(mod m) for every character in the Plain Text
            foreach (char c in chars)
            {
                int x = Convert.ToInt32(c - 65);
                cipherText += Convert.ToChar(((a * x + b) % 26) + 65);
            }

            return cipherText;
        }
        public static string AffineDecrypt(string cipherText, int a, int b)
        {
            string plainText = "";

            // Get Multiplicative Inverse of a
            int aInverse = MultiplicativeInverse(a);

            // Put Cipher Text (all capitals) into Character Array
            char[] chars = cipherText.ToUpper().ToCharArray();

            // Computer d(x) = aInverse * (e(x)  b)(mod m)
            foreach (char c in chars)
            {
                int x = Convert.ToInt32(c - 65);
                if (x - b < 0) x = Convert.ToInt32(x) + 26;
                plainText += Convert.ToChar(((aInverse * (x - b)) % 26) + 65);
            }

            return plainText;
        }
        public static int MultiplicativeInverse(int a)
        {
            for (int x = 1; x < 27; x++)
            {
                if ((a * x) % 26 == 1)
                    return x;
            }

            throw new Exception("No multiplicative inverse found!");
        }
       


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
            }else if (Affine == true)
            {
                if (textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("please Enter the Keys");
                }
                else {
                    string p = textBox2.Text;
                    int m =int.Parse( textBox5.Text);
                    int k =int.Parse(textBox4.Text);
                    string c = AffineEncrypt(p, m, k);
                    textBox1.Text = c;
                }
            }
            else if (vigenere == true)
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("please enter the key");
                }
                else
                {
                    string text = textBox2.Text;
                    string key = textBox3.Text;
                    string cipherText = ViginerEncrypt(text, key);
                    textBox1.Text = cipherText;
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
            }else if (Affine == true)
            {
                if (textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("please Enter the Keys");
                }
                else
                {
                    string c = textBox1.Text;
                    int m = int.Parse(textBox5.Text);
                    int k = int.Parse(textBox4.Text);
                    string p = AffineDecrypt(c, m, k);
                    textBox2.Text = p;

                }
            }
            else if (vigenere == true)
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("please enter the key");
                }
                else { 
                string text = textBox1.Text;
                string key = textBox3.Text;
                string plainText = ViginerDecrypt(text, key);
                textBox2.Text = plainText;
                }

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
                Affine = true;
                Cipher = false;
                vigenere = false;

            }
            else if(currentMyComboBoxIndex == 1)
            {
                Affine = false;
                Cipher = true;
                vigenere = false;
            }
            else if (currentMyComboBoxIndex == 2)
            {
                Affine = false;
                Cipher = false;
                vigenere = true;
            }
        }

     
    }
}
