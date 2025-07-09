namespace home
{
    partial class FstatusCs
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
            this.button4 = new System.Windows.Forms.Button();
            this.datePickerSelesai = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdRiwayat = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboIdPengaduan = new System.Windows.Forms.ComboBox();
            this.lblmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 399);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 39);
            this.button4.TabIndex = 39;
            this.button4.Text = "Kembali";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // datePickerSelesai
            // 
            this.datePickerSelesai.Location = new System.Drawing.Point(185, 216);
            this.datePickerSelesai.Name = "datePickerSelesai";
            this.datePickerSelesai.Size = new System.Drawing.Size(400, 22);
            this.datePickerSelesai.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 16);
            this.label8.TabIndex = 57;
            this.label8.Text = "id_pengaduan*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 16);
            this.label5.TabIndex = 56;
            this.label5.Text = "tanggal_perubahan*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 55;
            this.label4.Text = "id_riwayat*";
            // 
            // txtIdRiwayat
            // 
            this.txtIdRiwayat.Location = new System.Drawing.Point(185, 50);
            this.txtIdRiwayat.Name = "txtIdRiwayat";
            this.txtIdRiwayat.Size = new System.Drawing.Size(400, 22);
            this.txtIdRiwayat.TabIndex = 53;
            this.txtIdRiwayat.TextChanged += new System.EventHandler(this.txtIdRiwayat_TextChanged);
            this.txtIdRiwayat.Enter += new System.EventHandler(this.txtIdRiwayat_Enter);
            this.txtIdRiwayat.Leave += new System.EventHandler(this.txtIdRiwayat_Leave);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Masuk",
            "Diproses",
            "Selesai"});
            this.comboBoxStatus.Location = new System.Drawing.Point(185, 154);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(400, 24);
            this.comboBoxStatus.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 59;
            this.label1.Text = "status_baru*";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 39);
            this.button1.TabIndex = 61;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // comboIdPengaduan
            // 
            this.comboIdPengaduan.FormattingEnabled = true;
            this.comboIdPengaduan.Location = new System.Drawing.Point(185, 99);
            this.comboIdPengaduan.Name = "comboIdPengaduan";
            this.comboIdPengaduan.Size = new System.Drawing.Size(400, 24);
            this.comboIdPengaduan.TabIndex = 62;
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Location = new System.Drawing.Point(182, 259);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(64, 16);
            this.lblmsg.TabIndex = 63;
            this.lblmsg.Text = "Massage";
            // 
            // FstatusCs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.comboIdPengaduan);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datePickerSelesai);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIdRiwayat);
            this.Controls.Add(this.button4);
            this.Name = "FstatusCs";
            this.Text = "FstatusCs";
            this.Load += new System.EventHandler(this.FstatusCs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DateTimePicker datePickerSelesai;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIdRiwayat;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboIdPengaduan;
        private System.Windows.Forms.Label lblmsg;
    }
}