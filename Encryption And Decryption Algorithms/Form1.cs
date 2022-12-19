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
        private static string CaesarCipher(string input, int key)
        {
            string output= string.Empty;
            for (int i = 0; i < input.Length;i++)
            {            
                char offset = char.IsUpper(input[i])?'A' : 'a';
                char ch= (char)((((input[i]+key)- offset) % 26) + offset);
              output += ch;
                
            }
          return output;
        }
        //gsd 
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
            for (int i = 0; i < key.Length; i++)
                if (!char.IsLetter(key[i]))
                    return null; 

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = encipher ? k : -k;
                    int r = input[i] - offset;
                    char ch = (char)((Mod(((r + k)), 26)) + offset);
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
        public static string AffineEncrypt(string input, int M, int K)
        {
            string output = string.Empty;           
            
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    char offset = char.IsUpper(input[i]) ? 'A' : 'a';
                    int p = input[i] - offset;
                    char ch= Convert.ToChar(((M * p + K) % 26) + offset);
                    output += ch;
                }else
                {
                    output += input[i];
                }
            }

            return output;
        }
        public static string AffineDecrypt(string input, int M, int K)
        {
            string output = string.Empty;
            int aInverse = MultiplicativeInverse(M);
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    char offset = char.IsUpper(input[i]) ? 'A' : 'a';
                    int c = input[i] - offset;
                    char ch = (char)((Mod((aInverse * (c - K)) , 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                }
            }

            return output;
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
                    string text = textBox2.Text;
                    var key = int.Parse(textBox3.Text);                
                    textBox1.Text = CaesarCipher(text, key);
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
                    
                    if (!key.All(char.IsDigit))
                    {
                        string cipherText = ViginerEncrypt(text, key);
                        textBox1.Text = cipherText;

                    }
                    else
                    {
                        MessageBox.Show("Enter the Key in Letters");
                    }
                  
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
                string text = textBox1.Text;
                var key = int.Parse(textBox3.Text);
                textBox2.Text = CaesarCipher(text, 26 - key);
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
                    if (!key.All(char.IsDigit))
                    {
                        string plainText = ViginerDecrypt(text, key);
                        textBox2.Text = plainText;
                    }
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
