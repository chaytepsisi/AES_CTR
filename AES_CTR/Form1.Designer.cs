namespace AES_CTR
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EncryptButton = new System.Windows.Forms.Button();
            this.KeySizeCbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KeyTbx = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.KeyLengthLabel = new System.Windows.Forms.Label();
            this.Key_HexRbtn = new System.Windows.Forms.RadioButton();
            this.Key_BinRbtn = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.IVLengthLabel = new System.Windows.Forms.Label();
            this.IV_HexRbtn = new System.Windows.Forms.RadioButton();
            this.IV_BinRbtn = new System.Windows.Forms.RadioButton();
            this.IVTbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.KeyStreamSizeTbx = new System.Windows.Forms.TextBox();
            //this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new ProgressBarEx();
            this.EncryptionBgw = new System.ComponentModel.BackgroundWorker();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(599, 117);
            this.EncryptButton.Margin = new System.Windows.Forms.Padding(4);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(100, 32);
            this.EncryptButton.TabIndex = 0;
            this.EncryptButton.Text = "Oluştur";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // KeySizeCbx
            // 
            this.KeySizeCbx.FormattingEnabled = true;
            this.KeySizeCbx.Location = new System.Drawing.Point(131, 7);
            this.KeySizeCbx.Margin = new System.Windows.Forms.Padding(4);
            this.KeySizeCbx.Name = "KeySizeCbx";
            this.KeySizeCbx.Size = new System.Drawing.Size(112, 24);
            this.KeySizeCbx.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Anahtar Uzunluğu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Anahtar : ";
            // 
            // KeyTbx
            // 
            this.KeyTbx.Location = new System.Drawing.Point(70, 13);
            this.KeyTbx.Margin = new System.Windows.Forms.Padding(4);
            this.KeyTbx.Name = "KeyTbx";
            this.KeyTbx.Size = new System.Drawing.Size(356, 22);
            this.KeyTbx.TabIndex = 6;
            this.KeyTbx.TextChanged += new System.EventHandler(this.KeyTbx_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(456, 117);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 32);
            this.button2.TabIndex = 12;
            this.button2.Text = "Dosya Seç";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.KeyLengthLabel);
            this.groupBox1.Controls.Add(this.Key_HexRbtn);
            this.groupBox1.Controls.Add(this.Key_BinRbtn);
            this.groupBox1.Controls.Add(this.KeyTbx);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(691, 41);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // KeyLengthLabel
            // 
            this.KeyLengthLabel.AutoSize = true;
            this.KeyLengthLabel.Location = new System.Drawing.Point(640, 16);
            this.KeyLengthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.KeyLengthLabel.Name = "KeyLengthLabel";
            this.KeyLengthLabel.Size = new System.Drawing.Size(25, 16);
            this.KeyLengthLabel.TabIndex = 7;
            this.KeyLengthLabel.Text = "( - )";
            // 
            // Key_HexRbtn
            // 
            this.Key_HexRbtn.AutoSize = true;
            this.Key_HexRbtn.Location = new System.Drawing.Point(515, 14);
            this.Key_HexRbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Key_HexRbtn.Name = "Key_HexRbtn";
            this.Key_HexRbtn.Size = new System.Drawing.Size(105, 20);
            this.Key_HexRbtn.TabIndex = 1;
            this.Key_HexRbtn.TabStop = true;
            this.Key_HexRbtn.Text = "Hexadecimal";
            this.Key_HexRbtn.UseVisualStyleBackColor = true;
            this.Key_HexRbtn.CheckedChanged += new System.EventHandler(this.Key_HexRbtn_CheckedChanged);
            // 
            // Key_BinRbtn
            // 
            this.Key_BinRbtn.AutoSize = true;
            this.Key_BinRbtn.Location = new System.Drawing.Point(435, 14);
            this.Key_BinRbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Key_BinRbtn.Name = "Key_BinRbtn";
            this.Key_BinRbtn.Size = new System.Drawing.Size(63, 20);
            this.Key_BinRbtn.TabIndex = 0;
            this.Key_BinRbtn.TabStop = true;
            this.Key_BinRbtn.Text = "Binary";
            this.Key_BinRbtn.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.IVLengthLabel);
            this.groupBox3.Controls.Add(this.IV_HexRbtn);
            this.groupBox3.Controls.Add(this.IV_BinRbtn);
            this.groupBox3.Controls.Add(this.IVTbx);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(8, 72);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(691, 41);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // IVLengthLabel
            // 
            this.IVLengthLabel.AutoSize = true;
            this.IVLengthLabel.Location = new System.Drawing.Point(640, 16);
            this.IVLengthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IVLengthLabel.Name = "IVLengthLabel";
            this.IVLengthLabel.Size = new System.Drawing.Size(25, 16);
            this.IVLengthLabel.TabIndex = 9;
            this.IVLengthLabel.Text = "( - )";
            // 
            // IV_HexRbtn
            // 
            this.IV_HexRbtn.AutoSize = true;
            this.IV_HexRbtn.Location = new System.Drawing.Point(515, 14);
            this.IV_HexRbtn.Margin = new System.Windows.Forms.Padding(4);
            this.IV_HexRbtn.Name = "IV_HexRbtn";
            this.IV_HexRbtn.Size = new System.Drawing.Size(105, 20);
            this.IV_HexRbtn.TabIndex = 1;
            this.IV_HexRbtn.TabStop = true;
            this.IV_HexRbtn.Text = "Hexadecimal";
            this.IV_HexRbtn.UseVisualStyleBackColor = true;
            // 
            // IV_BinRbtn
            // 
            this.IV_BinRbtn.AutoSize = true;
            this.IV_BinRbtn.Location = new System.Drawing.Point(435, 14);
            this.IV_BinRbtn.Margin = new System.Windows.Forms.Padding(4);
            this.IV_BinRbtn.Name = "IV_BinRbtn";
            this.IV_BinRbtn.Size = new System.Drawing.Size(63, 20);
            this.IV_BinRbtn.TabIndex = 0;
            this.IV_BinRbtn.TabStop = true;
            this.IV_BinRbtn.Text = "Binary";
            this.IV_BinRbtn.UseVisualStyleBackColor = true;
            this.IV_BinRbtn.CheckedChanged += new System.EventHandler(this.IV_BinRbtn_CheckedChanged);
            // 
            // IVTbx
            // 
            this.IVTbx.Location = new System.Drawing.Point(70, 13);
            this.IVTbx.Margin = new System.Windows.Forms.Padding(4);
            this.IVTbx.Name = "IVTbx";
            this.IVTbx.Size = new System.Drawing.Size(356, 22);
            this.IVTbx.TabIndex = 6;
            this.IVTbx.TextChanged += new System.EventHandler(this.IVTbx_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "IV : ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(78, 121);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(356, 22);
            this.textBox1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 125);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Dosya :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(284, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Çıktı Bit Uzunluğu:";
            // 
            // KeyStreamSizeTbx
            // 
            this.KeyStreamSizeTbx.Location = new System.Drawing.Point(396, 7);
            this.KeyStreamSizeTbx.Margin = new System.Windows.Forms.Padding(4);
            this.KeyStreamSizeTbx.Name = "KeyStreamSizeTbx";
            this.KeyStreamSizeTbx.Size = new System.Drawing.Size(99, 22);
            this.KeyStreamSizeTbx.TabIndex = 7;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 157);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(691, 28);
            this.progressBar1.TabIndex = 17;
            // 
            // EncryptionBgw
            // 
            this.EncryptionBgw.WorkerReportsProgress = true;
            this.EncryptionBgw.WorkerSupportsCancellation = true;
            this.EncryptionBgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.EncryptionBgw_DoWork);
            this.EncryptionBgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.EncryptionBgw_ProgressChanged);
            this.EncryptionBgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.EncryptionBgw_RunWorkerCompleted);
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.Location = new System.Drawing.Point(599, 193);
            this.OpenFolderButton.Margin = new System.Windows.Forms.Padding(4);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(100, 32);
            this.OpenFolderButton.TabIndex = 18;
            this.OpenFolderButton.Text = "Klasörü Aç";
            this.OpenFolderButton.UseVisualStyleBackColor = true;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Location = new System.Drawing.Point(9, 201);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(41, 16);
            this.TimerLabel.TabIndex = 19;
            this.TimerLabel.Text = "Süre: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 229);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.OpenFolderButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.KeyStreamSizeTbx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeySizeCbx);
            this.Controls.Add(this.EncryptButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "AES-CRT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.ComboBox KeySizeCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KeyTbx;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Key_HexRbtn;
        private System.Windows.Forms.RadioButton Key_BinRbtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton IV_HexRbtn;
        private System.Windows.Forms.RadioButton IV_BinRbtn;
        private System.Windows.Forms.TextBox IVTbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label KeyLengthLabel;
        private System.Windows.Forms.Label IVLengthLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox KeyStreamSizeTbx;
        //private System.Windows.Forms.ProgressBar progressBar1;
        private ProgressBarEx progressBar1;
        private System.ComponentModel.BackgroundWorker EncryptionBgw;
        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.Label TimerLabel;
    }
}

