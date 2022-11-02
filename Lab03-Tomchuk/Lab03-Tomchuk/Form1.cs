using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_Tomchuk
{
    public partial class Lab03_Tomchuk : Form
    {
        public Lab03_Tomchuk()
        {
            InitializeComponent();
        }
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        void myShowToolTip(TextBox tB, byte[] arr)
        {
            //string hexValues = BitConverter.ToString(arr);
            //toolTip_HEX.SetToolTip(tB, hexValues);
            string bbb = string.Join(" ", arr.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
            toolTip_HEX.SetToolTip(tB, bbb);
        }
        byte[] myXOR(byte[] arr_text, byte[] arr_key)
        {

            int len_text = arr_text.Length;
            int len_key = arr_key.Length;
            byte[] arr_cipher = new byte[len_text];
            for (int i = 0; i < len_text; i++)
            {
                byte p = arr_text[i];
                byte k = arr_key[i % len_key]; // mod
                byte c = (byte)(p ^ k); // XOR
                arr_cipher[i] = c;
            }
            return arr_cipher;
        }

        private void nullString()
        {
            textBox_C_IN.Text = textBox_P_IN.Text;
            textBox_P_OUT.Text = textBox_P_IN.Text;
            textBox_C_OUT.Text = textBox_P_IN.Text;
        }

        string myCipher(TextBox tb_text, TextBox tb_Key, TextBox tb_cipher, string cipher = "")
        {
            string text = tb_text.Text;
            byte[] arr_text;
            if (cipher == "")
                arr_text = Encoding.ASCII.GetBytes(text);
            else
                arr_text = Encoding.ASCII.GetBytes(cipher);
            myShowToolTip(tb_text, arr_text);

            string key = tb_Key.Text;
            
            byte[] arr_key = Encoding.ASCII.GetBytes(key);
            /*
            Console.WriteLine(key);
            Console.WriteLine(arr_key);
            Console.WriteLine(Convert.FromBase64String(key));
            Console.WriteLine(Encoding.UTF8.GetString(arr_key));
            */
            myShowToolTip(tb_Key, arr_key);

            byte[] arr_cipher = myXOR(arr_text, arr_key);

            cipher = Encoding.ASCII.GetString(arr_cipher);
            tb_cipher.Text = cipher;
            myShowToolTip(tb_cipher, arr_cipher);

            return cipher;
        }

        private void button_XOR_Click(object sender, EventArgs e)
        {
            /*
            if(string.IsNullOrEmpty(textBox_KEY_IN.Text))
            {
                MessageBox.Show("Ви забули ввести ключ");
                textBox_KEY_IN.Focus();
            }
            */
            if (textBox_KEY_IN.Text == "")
                nullString();
            else
            {
                string cipher = myCipher(textBox_P_IN, textBox_KEY_IN, textBox_C_IN);
                textBox_P_OUT.Text = textBox_C_IN.Text;
                textBox_KEY_OUT.Text = textBox_KEY_IN.Text;
                myCipher(textBox_P_OUT, textBox_KEY_OUT, textBox_C_OUT, cipher);
            }
            

        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_P_IN.Text = "" ;
            textBox_KEY_IN.Text = "";
            textBox_C_IN.Text = "";
            textBox_P_OUT.Text = "";
            textBox_KEY_OUT.Text = "" ;
            textBox_C_OUT.Text = "";
        }
    }
}
