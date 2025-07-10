namespace home
{
    partial class FormChartStatus
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartRiwayat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelJudul = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // chartRiwayat
            // 
            chartArea6.Name = "MainArea";
            this.chartRiwayat.ChartAreas.Add(chartArea6);
            legend6.Name = "Status";
            this.chartRiwayat.Legends.Add(legend6);
            this.chartRiwayat.Location = new System.Drawing.Point(164, 119);
            this.chartRiwayat.Name = "chartRiwayat";
            series6.ChartArea = "MainArea";
            series6.Legend = "Status";
            series6.Name = "Status Pengaduan";
            this.chartRiwayat.Series.Add(series6);
            this.chartRiwayat.Size = new System.Drawing.Size(702, 366);
            this.chartRiwayat.TabIndex = 1;
            title6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title6.Name = "Title1";
            title6.Text = "Grafik Jumlah Status Pengaduan";
            this.chartRiwayat.Titles.Add(title6);
            this.chartRiwayat.Click += new System.EventHandler(this.chartRiwayat_Click);
            // 
            // labelJudul
            // 
            this.labelJudul.AutoSize = true;
            this.labelJudul.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelJudul.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelJudul.Location = new System.Drawing.Point(213, 9);
            this.labelJudul.Name = "labelJudul";
            this.labelJudul.Size = new System.Drawing.Size(466, 37);
            this.labelJudul.TabIndex = 0;
            this.labelJudul.Text = "DASHBOARD STATUS PENGADUAN";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Semua",
            "Masuk",
            "Diproses",
            "Selesai"});
            this.comboBox2.Location = new System.Drawing.Point(531, 89);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(335, 24);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(458, 511);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Previous";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // FormChartStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 701);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.labelJudul);
            this.Controls.Add(this.chartRiwayat);
            this.Name = "FormChartStatus";
            this.Text = "Grafik Status Pengaduan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormChartStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
       

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartRiwayat;
        private System.Windows.Forms.Label labelJudul;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
    }
}