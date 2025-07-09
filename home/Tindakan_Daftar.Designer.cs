namespace home
{
    partial class Tindakan_Daftar
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.txtIdTindakan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datePickerTindakan = new System.Windows.Forms.DateTimePicker();
            this.comboPengaduan = new System.Windows.Forms.ComboBox();
            this.comboStatusTindakan = new System.Windows.Forms.ComboBox();
            this.comboPendamping = new System.Windows.Forms.ComboBox();
            this.lblmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(1662, 757);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 57);
            this.button3.TabIndex = 51;
            this.button3.Text = "Submit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(48, 757);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 57);
            this.button2.TabIndex = 50;
            this.button2.Text = "Kembali";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(628, 492);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 25);
            this.label7.TabIndex = 48;
            this.label7.Text = "Tanggal_tindakan*";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(628, 442);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 25);
            this.label6.TabIndex = 47;
            this.label6.Text = "Deskripsi*";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(628, 398);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 25);
            this.label5.TabIndex = 46;
            this.label5.Text = "Id_pendamping*";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(628, 542);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 25);
            this.label4.TabIndex = 45;
            this.label4.Text = "Status Tindakan*";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(628, 348);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 25);
            this.label3.TabIndex = 44;
            this.label3.Text = "Id_Pengaduan*";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(628, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 25);
            this.label2.TabIndex = 43;
            this.label2.Text = "Id_Tindakan*";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDeskripsi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeskripsi.Location = new System.Drawing.Point(893, 431);
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(400, 30);
            this.txtDeskripsi.TabIndex = 40;
            // 
            // txtIdTindakan
            // 
            this.txtIdTindakan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIdTindakan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdTindakan.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtIdTindakan.Location = new System.Drawing.Point(893, 296);
            this.txtIdTindakan.Name = "txtIdTindakan";
            this.txtIdTindakan.Size = new System.Drawing.Size(400, 30);
            this.txtIdTindakan.TabIndex = 36;
            this.txtIdTindakan.Enter += new System.EventHandler(this.txtIdTindakan_Enter);
            this.txtIdTindakan.Leave += new System.EventHandler(this.txtIdTindakan_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(786, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 50);
            this.label1.TabIndex = 35;
            this.label1.Text = "Data Tindakan";
            // 
            // datePickerTindakan
            // 
            this.datePickerTindakan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datePickerTindakan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerTindakan.Location = new System.Drawing.Point(893, 487);
            this.datePickerTindakan.Name = "datePickerTindakan";
            this.datePickerTindakan.Size = new System.Drawing.Size(400, 30);
            this.datePickerTindakan.TabIndex = 52;
            // 
            // comboPengaduan
            // 
            this.comboPengaduan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboPengaduan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPengaduan.FormattingEnabled = true;
            this.comboPengaduan.Location = new System.Drawing.Point(893, 340);
            this.comboPengaduan.Name = "comboPengaduan";
            this.comboPengaduan.Size = new System.Drawing.Size(400, 33);
            this.comboPengaduan.TabIndex = 53;
            // 
            // comboStatusTindakan
            // 
            this.comboStatusTindakan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboStatusTindakan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboStatusTindakan.FormattingEnabled = true;
            this.comboStatusTindakan.Location = new System.Drawing.Point(893, 537);
            this.comboStatusTindakan.Name = "comboStatusTindakan";
            this.comboStatusTindakan.Size = new System.Drawing.Size(400, 33);
            this.comboStatusTindakan.TabIndex = 54;
            // 
            // comboPendamping
            // 
            this.comboPendamping.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboPendamping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPendamping.FormattingEnabled = true;
            this.comboPendamping.Location = new System.Drawing.Point(893, 390);
            this.comboPendamping.Name = "comboPendamping";
            this.comboPendamping.Size = new System.Drawing.Size(400, 33);
            this.comboPendamping.TabIndex = 55;
            // 
            // lblmsg
            // 
            this.lblmsg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.Location = new System.Drawing.Point(888, 585);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(99, 25);
            this.lblmsg.TabIndex = 56;
            this.lblmsg.Text = "message";
            this.lblmsg.Click += new System.EventHandler(this.lblmsg_Click);
            // 
            // Tindakan_Daftar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1837, 860);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.comboPendamping);
            this.Controls.Add(this.comboStatusTindakan);
            this.Controls.Add(this.comboPengaduan);
            this.Controls.Add(this.datePickerTindakan);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.txtIdTindakan);
            this.Controls.Add(this.label1);
            this.Name = "Tindakan_Daftar";
            this.Text = "Tindakan_Daftar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mhspngd_daftarcs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.TextBox txtIdTindakan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickerTindakan;
        private System.Windows.Forms.ComboBox comboPengaduan;
        private System.Windows.Forms.ComboBox comboStatusTindakan;
        private System.Windows.Forms.ComboBox comboPendamping;
        private System.Windows.Forms.Label lblmsg;
    }
}