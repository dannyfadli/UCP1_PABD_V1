namespace home
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNoHP = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxJK = new System.Windows.Forms.ComboBox();
            this.comboBoxProdi = new System.Windows.Forms.ComboBox();
            this.comboBoxFakultas = new System.Windows.Forms.ComboBox();
            this.lblmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(686, 396);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 44);
            this.button3.TabIndex = 34;
            this.button3.Text = "Submit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 396);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 44);
            this.button2.TabIndex = 33;
            this.button2.Text = "Pevieos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 331);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 32;
            this.label8.Text = "Email*";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 31;
            this.label7.Text = "no-hp*";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "Prodi*";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Fakultas*";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Jenis-Kelamin*";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Nama*";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Nim*";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(170, 325);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 22);
            this.txtEmail.TabIndex = 25;
            this.txtEmail.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // txtNoHP
            // 
            this.txtNoHP.Location = new System.Drawing.Point(170, 280);
            this.txtNoHP.Name = "txtNoHP";
            this.txtNoHP.Size = new System.Drawing.Size(400, 22);
            this.txtNoHP.TabIndex = 24;
            this.txtNoHP.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(170, 84);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(400, 22);
            this.txtNama.TabIndex = 20;
            this.txtNama.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtNIM
            // 
            this.txtNIM.Location = new System.Drawing.Point(170, 43);
            this.txtNIM.Name = "txtNIM";
            this.txtNIM.Size = new System.Drawing.Size(400, 22);
            this.txtNIM.TabIndex = 19;
            this.txtNIM.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "=========DataMahasiswa=========";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBoxJK
            // 
            this.comboBoxJK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJK.FormattingEnabled = true;
            this.comboBoxJK.Items.AddRange(new object[] {
            "L",
            "P"});
            this.comboBoxJK.Location = new System.Drawing.Point(170, 127);
            this.comboBoxJK.Name = "comboBoxJK";
            this.comboBoxJK.Size = new System.Drawing.Size(400, 24);
            this.comboBoxJK.TabIndex = 35;
            // 
            // comboBoxProdi
            // 
            this.comboBoxProdi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProdi.FormattingEnabled = true;
            this.comboBoxProdi.Items.AddRange(new object[] {
            "Teknik Mesin",
            "Teknik Informatika",
            "Teknik Sipil",
            "Teknik Elektro",
            "Akuntansi",
            "Manajemen",
            "Ilmu Hukum",
            "Ilmu Komunikasi",
            "Hubungan Internasional",
            "Ilmu Pemerintahan",
            "Pendidikan Agama Islam",
            "Komunikasi dan Penyiaran Islam",
            "Ekonomi Syariah",
            "Pendidikan Bahasa Inggris",
            "Pendidikan Bahasa Arab",
            "Pendidikan Bahasa Jepang",
            "Kedokteran",
            "Kedokteran Gigi",
            "Ilmu Keperawatan",
            "Farmasi",
            "Agroteknologi",
            "Agribisnis",
            "Teknologi Rekayasa Otomotif",
            "Teknik Elektro-medis"});
            this.comboBoxProdi.Location = new System.Drawing.Point(170, 222);
            this.comboBoxProdi.Name = "comboBoxProdi";
            this.comboBoxProdi.Size = new System.Drawing.Size(400, 24);
            this.comboBoxProdi.TabIndex = 36;
            this.comboBoxProdi.SelectedIndexChanged += new System.EventHandler(this.comboBoxProdi_SelectedIndexChanged);
            // 
            // comboBoxFakultas
            // 
            this.comboBoxFakultas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFakultas.FormattingEnabled = true;
            this.comboBoxFakultas.Items.AddRange(new object[] {
            "Teknik",
            "Ekonomi dan Bisnis",
            "Hukum",
            "Ilmu Sosial dan Ilmu Politik",
            "Agama Islam",
            "Pendidikan Bahasa",
            "Kedokteran dan Ilmu Kesehatan",
            "Pertanian",
            "Vokasi"});
            this.comboBoxFakultas.Location = new System.Drawing.Point(170, 178);
            this.comboBoxFakultas.Name = "comboBoxFakultas";
            this.comboBoxFakultas.Size = new System.Drawing.Size(400, 24);
            this.comboBoxFakultas.TabIndex = 37;
            this.comboBoxFakultas.SelectedIndexChanged += new System.EventHandler(this.comboBoxFakultas_SelectedIndexChanged_1);
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Location = new System.Drawing.Point(167, 370);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(64, 16);
            this.lblmsg.TabIndex = 38;
            this.lblmsg.Text = "Message";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.comboBoxFakultas);
            this.Controls.Add(this.comboBoxProdi);
            this.Controls.Add(this.comboBoxJK);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNoHP);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNIM);
            this.Controls.Add(this.label1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNoHP;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtNIM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxJK;
        private System.Windows.Forms.ComboBox comboBoxProdi;
        private System.Windows.Forms.ComboBox comboBoxFakultas;
        private System.Windows.Forms.Label lblmsg;
    }
}