using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
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
        bool hasError = false;

        ///Test vectors from RFC 3686
        ///https://www.rfc-editor.org/rfc/rfc3686#section-6
        ////////////////////////////////////////////////////////////////////////
        ///  Test Vector #2: Encrypting 32 octets using AES-CTR with 128-bit key,
        ///  
        ///  AES Key          : 7E 24 06 78 17 FA E0 D7 43 D6 CE 1F 32 53 91 63
        ///  Counter          : 00 6C B6 DB C0 54 3B 59 DA 48 D9 0B 00 00 00 01
        ///  Key Stream(1)    : 51 05 A3 05 12 8F 74 DE 71 04 4B E5 82 D7 DD 87
        ///  Key Stream(2)    : FB 3F 0C EF 52 CF 41 DF E4 FF 2A C4 8D 5C A0 37
        /// 
        ////////////////////////////////////////////////////////////////////////
        ///  Test Vector #6: Encrypting 48 octets using AES-CTR with 192-bit key
        ///  
        ///  AES Key          : 02 BF 39 1E E8 EC B1 59 B9 59 61 7B 09 65 27 9B F5 9B 60 A7 86 D3 E0 FE
        ///  Counter          : 00 07 BD FD 5C BD 60 27 8D CC 09 12 00 00 00 01
        ///  Key Stream(1)    : 96 88 3D C6 5A 59 74 28 5C 02 77 DA D1 FA E9 57
        ///  Key Stream(2)    : C2 99 AE 86 D2 84 73 9F 5D 2F D2 0A 7A 32 3F 97
        ///  Key Stream(3)    : 8B CF 2B 16 39 99 B2 26 15 B4 9C D4 FE 57 39 98
        ///  
        ////////////////////////////////////////////////////////////////////////
        ///Test Vector #9: Encrypting 48 octets using AES-CTR with 256-bit key
        ///AES Key          : FF 7A 61 7C E6 91 48 E4 F1 72 6E 2F 43 58 1D E2 AA 62 D9 F8 05 53 2E DF F1 EE D6 87 FB 54 15 3D
        ///Counter          : 00 1C C5 B7 51 A5 1D 70 A1 C1 11 48 00 00 00 01
        ///Key stream(1)    : EB 6D 50 81 19 0E BD F0 C6 7C 9E 4D 26 C7 41 A5
        ///Key stream(2)    : A4 16 CD 95 71 7C EB 10 EC 95 DA AE 9F CB 19 00
        ///Key stream(3)    : 3E E1 C4 9B C6 B9 CA 21 3F 6E E2 71 D0 A9 33 39
        ////////////////////////////////////////////////////////////////////////


        public Form1()
        {
            InitializeComponent();
            timer = new Stopwatch();

            KeySizeCbx.Items.Add("128");
            KeySizeCbx.Items.Add("192");
            KeySizeCbx.Items.Add("256");
            KeySizeCbx.SelectedIndex = 0;

            Key_BinRbtn.Checked = true;
            IV_BinRbtn.Checked = true;
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            EncryptButton.Text = "Durdur";
            if (EncryptionBgw.IsBusy)
            {
                EncryptionBgw.CancelAsync();
                return;
            }

            string keyString = KeyTbx.Text;
            string ivString = IVTbx.Text;
            hasError = false;
            KeyBitSize = int.Parse(KeySizeCbx.SelectedItem.ToString());
            int keyRadix = Key_BinRbtn.Checked ? 2 : 16;
            try
            {
                Key = OtherFunctions.StringToByteArray(keyString, keyRadix);
            }
            catch
            {
                return;
            }

            int ivRadix = IV_BinRbtn.Checked ? 2 : 16;
            try
            {
                IV = OtherFunctions.StringToByteArray(ivString, ivRadix);
            }
            catch
            {
                return;
            }

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
            if (fileName == null)
            {
                MessageBox.Show("Lütfen çıktı dosyasını seçin.");
                return;
            }
            progressBar1.Value = 0;
            timer.Restart();
            EncryptionBgw.RunWorkerAsync();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    fileName = sfd.FileName;
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
                hasError = true;
                return;
            }
            ICryptoTransform encryptor = aes.CreateEncryptor();

            var newSeed = new byte[IV.Length];
            Array.Copy(IV, newSeed, IV.Length);

            int limit = (int)Math.Ceiling(KeyStreamSize * 1.0 / BLOCK_BYTE_SIZE);
            if (limit == 0)
                limit = 1;
            byte[] output;
            StreamWriter writer = new StreamWriter(fileName);
            int outputSize = KeyStreamBitSize;
            int prevPercent = 0;
            for (int i = 0; i < limit; i++)
            {
                if (EncryptionBgw.CancellationPending)
                {
                    e.Cancel = true;
                    writer.Flush();
                    writer.Close();
                    hasError = true;
                    MessageBox.Show("İşlem iptal edildi.");
                    return;
                }
                output = encryptor.TransformFinalBlock(newSeed, 0, newSeed.Length);
                newSeed = OtherFunctions.IncrementArray(newSeed);
                int percent = (int)((i + 1) * 100.0 / limit);
                if (prevPercent < percent)
                {
                    prevPercent = percent;
                    EncryptionBgw.ReportProgress(percent);
                }
                if (outputSize >= 128)
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
            EncryptButton.Text = "Oluştur";
            progressBar1.Value = 100;
            TimerLabel.Text = "Süre: " + timer.ElapsedMilliseconds / 1000.0 + " s";
            if (!hasError)
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
            CheckIV();
        }

        private void IV_BinRbtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckIV();
        }

        private void KeyTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteFromClipboard(KeyTbx);
                e.SuppressKeyPress = true;
            }
        }

        void PasteFromClipboard(TextBox tbx)
        {
            string selectedText = Clipboard.GetText();
            selectedText = selectedText.Replace(" ", "").Replace("-", "").Replace("\n", "").Replace("\r", "");
            tbx.Text = selectedText;
        }

        private void IVTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteFromClipboard(IVTbx);
                e.SuppressKeyPress = true;
            }
        }
    }
}