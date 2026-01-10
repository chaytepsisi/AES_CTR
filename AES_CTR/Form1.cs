using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES_CTR
{
    public partial class Form1 : Form
    {
        //Sabitler
        public static int BLOCK_BYTE_SIZE = 16; // 128 bits
        public static int BLOCK_SIZE = 128; // 128 bits
        public static byte[] ZERO_IV = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

        //Değişkenler
        int KeyBitSize;
        byte[] Key;
        byte[] IV;
        int KeyStreamSize;
        int KeyStreamBitSize;
        string fileName;
        private readonly Stopwatch timer;

        /// <summary>
        ///  AES Key          : 7E 24 06 78 17 FA E0 D7 43 D6 CE 1F 32 53 91 63
        ///  Counter Block (1): 00 6C B6 DB C0 54 3B 59 DA 48 D9 0B 00 00 00 01
        ///  Key Stream    (1): 51 05 A3 05 12 8F 74 DE 71 04 4B E5 82 D7 DD 87
        ///  Counter Block(2) : 00 6C B6 DB C0 54 3B 59 DA 48 D9 0B 00 00 00 02
        ///  Key Stream(2)    : FB 3F 0C EF 52 CF 41 DF E4 FF 2A C4 8D 5C A0 37
        /// </summary>

        public Form1()
        {
            InitializeComponent();
            timer = new Stopwatch();

            KeySizeCbx.Items.Add("128");
            KeySizeCbx.Items.Add("192");
            KeySizeCbx.Items.Add("256");
            KeySizeCbx.SelectedIndex = 0;

            //KeyTbx.Text = "7E24067817FAE0D743D6CE1F32539163";
            //IVTbx.Text = "006CB6DBC0543B59DA48D90B00000001";
            Key_BinRbtn.Checked = true;
            IV_BinRbtn.Checked = true;
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            string keyString = KeyTbx.Text;
            string ivString = IVTbx.Text;

            KeyBitSize = int.Parse(KeySizeCbx.SelectedItem.ToString());
            int keyRadix = Key_BinRbtn.Checked ? 2 : 16;
            Key = StringToByteArray(keyString, keyRadix);

            int ivRadix = IV_BinRbtn.Checked ? 2 : 16;
            IV = StringToByteArray(ivString, ivRadix);

            try
            {
                KeyStreamBitSize = int.Parse(KeyStreamSizeTbx.Text);
                KeyStreamSize = KeyStreamBitSize / 8;
            }
            catch
            {
                MessageBox.Show("Çıktı boyutunu kontrol edin.");
                return;
            }
            if(fileName==null)
            {
                MessageBox.Show("Lütfen çıktı dosyasını seçin.");
                return;
            }

            timer.Restart();
            EncryptionBgw.RunWorkerAsync();
        }

        byte[] StringToByteArray(string input, int radix)
        {
            if (radix == 2)
            {
                return OtherFunctions.BinaryStringToByteArray(input);
            }
            else if (radix == 16)
            {
                return OtherFunctions.HexStringToByteArray(input);
            }
            else return null;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd = new SaveFileDialog())
            {
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                   fileName= sfd.FileName;
                    textBox1.Text = fileName;
                }
            }
        }

        private void EncryptionBgw_DoWork(object sender, DoWorkEventArgs e)
        {
            AesManaged aes;
            try
            {
                aes = new AesManaged
                {
                    KeySize = KeyBitSize,
                    Key = Key,
                    BlockSize = BLOCK_BYTE_SIZE * 8,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.Zeros,
                    IV = ZERO_IV
                };
            }
            catch 
            {
                MessageBox.Show("Anahtar boyutu geçersiz");
                return;
            }
            ICryptoTransform encryptor = aes.CreateEncryptor();

            var newSeed = new byte[IV.Length];
            Array.Copy(IV, newSeed, IV.Length);

            int limit = (int)Math.Ceiling(KeyStreamSize * 1.0 / BLOCK_BYTE_SIZE);
            byte[] output;
            StreamWriter writer = new StreamWriter(fileName);
            int outputSize = KeyStreamBitSize;  

            for (int i = 0; i < limit; i++)
            {
                output = encryptor.TransformFinalBlock(newSeed, 0, newSeed.Length);
                newSeed = OtherFunctions.IncrementArray(newSeed);
                EncryptionBgw.ReportProgress((int)((i + 1) * 100.0 / limit));
                if(outputSize >= 128)
                    writer.Write(OtherFunctions.ByteArrayToBinaryString(output));
                else
                    writer.Write(OtherFunctions.ByteArrayToBinaryString(output).Substring(0, outputSize));
                outputSize -= 128;
            }
            writer.Flush(); 
            writer.Close();
        }

        private void EncryptionBgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;  
        }

        private void EncryptionBgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();
            progressBar1.Value = 100;
            TimerLabel.Text = "Süre: " + timer.ElapsedMilliseconds/1000.0 + " s";
            MessageBox.Show("İşlem tamamlandı.");

        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            if (fileName != null && fileName != "")
            {
                FileInfo fi = new FileInfo(fileName);
                Process.Start(fi.DirectoryName);
            }
        }


        void CheckKey()
        {
            if (KeyTbx.Text.Length != 0)
                KeyLengthLabel.Text = "(" + KeyTbx.Text.Length + ")";

            else
            {
                KeyLengthLabel.Text = "( - )";
                KeyTbx.BackColor = Color.White;
                return;
            }

            if (Key_HexRbtn.Checked)
            {
                if (!(KeyTbx.Text.Length == 32 || KeyTbx.Text.Length == 48 || KeyTbx.Text.Length == 64))
                    KeyLengthLabel.BackColor = Color.Red;
                else KeyLengthLabel.BackColor = Color.Green;

                //If contains non hex characters
                if (System.Text.RegularExpressions.Regex.IsMatch(KeyTbx.Text, @"[^0-9a-fA-F]"))
                    KeyTbx.BackColor = Color.Red;
                else KeyTbx.BackColor = Color.White;
            }
            else
            {
                if (!(KeyTbx.Text.Length == 128 || KeyTbx.Text.Length == 192 || KeyTbx.Text.Length == 256))
                    KeyLengthLabel.BackColor = Color.Red;
                else KeyLengthLabel.BackColor = Color.Green;

                //If contains non binary characters
                if (System.Text.RegularExpressions.Regex.IsMatch(KeyTbx.Text, @"[^01]"))
                    KeyTbx.BackColor = Color.Red;
                else KeyTbx.BackColor = Color.White;
            }
        }

       void CheckIV()
        {
            if (IVTbx.Text.Length != 0)
                IVLengthLabel.Text = "(" + IVTbx.Text.Length + ")";
            else
            {
                IVLengthLabel.Text = "( - )";
                IVTbx.BackColor = Color.White;
                return;
            }

            if (IV_HexRbtn.Checked)
            {
                if (IVTbx.Text.Length != 32)
                    IVLengthLabel.BackColor = Color.Red;
                else IVLengthLabel.BackColor = Color.Green;

                //If contains non hex characters
                if (System.Text.RegularExpressions.Regex.IsMatch(IVTbx.Text, @"[^0-9a-fA-F]"))
                    IVTbx.BackColor = Color.Red;
                else IVTbx.BackColor = Color.White;
            }
            else
            {
                if (IVTbx.Text.Length != 128)
                    IVLengthLabel.BackColor = Color.Red;
                else IVLengthLabel.BackColor = Color.Green;

                //If contains non binary characters
                if (System.Text.RegularExpressions.Regex.IsMatch(IVTbx.Text, @"[^01]"))
                    IVTbx.BackColor = Color.Red;
                else IVTbx.BackColor = Color.White;
            }
        }

        private void Key_HexRbtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckKey();
        }

        private void KeyTbx_TextChanged(object sender, EventArgs e)
        {
            CheckKey();
        }

        private void IVTbx_TextChanged(object sender, EventArgs e)
        {
            //if (IVTbx.Text.Length != 0)
            //    IVLengthLabel.Text = "(" + IVTbx.Text.Length + ")";
            //else IVLengthLabel.Text = "( - )";
            CheckIV();
        }

        private void IV_BinRbtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckIV();
        }
    }
}