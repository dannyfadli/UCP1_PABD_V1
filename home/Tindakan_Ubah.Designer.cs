namespace home
{
    partial class Tindakan_Ubah
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStatusTindakan = new System.Windows.Forms.ComboBox();
            this.dtpTanggalTindakan = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.txtIdTindakan = new System.Windows.Forms.TextBox();
            this.txtIdPengaduan = new System.Windows.Forms.TextBox();
            this.txtIdPendamping = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.lblmsg = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 44);
            this.button1.TabIndex = 87;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(543, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(395, 304);
            this.dataGridView1.TabIndex = 86;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 396);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 44);
            this.button2.TabIndex = 85;
            this.button2.Text = "Kembali";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 80;
            // 
            // cmbStatusTindakan
            // 
            this.cmbStatusTindakan.FormattingEnabled = true;
            this.cmbStatusTindakan.Location = new System.Drawing.Point(168, 293);
            this.cmbStatusTindakan.Name = "cmbStatusTindakan";
            this.cmbStatusTindakan.Size = new System.Drawing.Size(353, 24);
            this.cmbStatusTindakan.TabIndex = 98;
            // 
            // dtpTanggalTindakan
            // 
            this.dtpTanggalTindakan.Location = new System.Drawing.Point(167, 237);
            this.dtpTanggalTindakan.Name = "dtpTanggalTindakan";
            this.dtpTanggalTindakan.Size = new System.Drawing.Size(353, 22);
            this.dtpTanggalTindakan.TabIndex = 96;
            this.dtpTanggalTindakan.ValueChanged += new System.EventHandler(this.dtpTanggalTindakan_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 95;
            this.label7.Text = "Tanggal_tindakan*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 94;
            this.label6.Text = "Deskripsi*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 16);
            this.label5.TabIndex = 93;
            this.label5.Text = "Id_pendamping*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 92;
            this.label4.Text = "Status Tindakan*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 91;
            this.label3.Text = "Id_Pengaduan*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 90;
            this.label2.Text = "Id_Tindakan*";
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Location = new System.Drawing.Point(168, 187);
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(353, 22);
            this.txtDeskripsi.TabIndex = 89;
            // 
            // txtIdTindakan
            // 
            this.txtIdTindakan.Location = new System.Drawing.Point(168, 49);
            this.txtIdTindakan.Name = "txtIdTindakan";
            this.txtIdTindakan.Size = new System.Drawing.Size(353, 22);
            this.txtIdTindakan.TabIndex = 88;
            // 
            // txtIdPengaduan
            // 
            this.txtIdPengaduan.Location = new System.Drawing.Point(168, 99);
            this.txtIdPengaduan.Name = "txtIdPengaduan";
            this.txtIdPengaduan.Size = new System.Drawing.Size(353, 22);
            this.txtIdPengaduan.TabIndex = 99;
            // 
            // txtIdPendamping
            // 
            this.txtIdPendamping.Location = new System.Drawing.Point(168, 146);
            this.txtIdPendamping.Name = "txtIdPendamping";
            this.txtIdPendamping.Size = new System.Drawing.Size(353, 22);
            this.txtIdPendamping.TabIndex = 100;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(426, 396);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 44);
            this.button3.TabIndex = 101;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DeleteTindakan);
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Location = new System.Drawing.Point(562, 366);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(64, 16);
            this.lblmsg.TabIndex = 102;
            this.lblmsg.Text = "Message";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(622, 394);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(102, 44);
            this.btn_Refresh.TabIndex = 103;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.BtnRefresh);
            // 
            // Tindakan_Ubah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 450);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtIdPendamping);
            this.Controls.Add(this.txtIdPengaduan);
            this.Controls.Add(this.cmbStatusTindakan);
            this.Controls.Add(this.dtpTanggalTindakan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.txtIdTindakan);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Name = "Tindakan_Ubah";
            this.Text = "Tindakan_Ubah";
            this.Load += new System.EventHandler(this.TindakanUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatusTindakan;
        private System.Windows.Forms.DateTimePicker dtpTanggalTindakan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.TextBox txtIdTindakan;
        private System.Windows.Forms.TextBox txtIdPengaduan;
        private System.Windows.Forms.TextBox txtIdPendamping;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Button btn_Refresh;
    }
}