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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartRiwayat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelJudul = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // chartRiwayat
            // 
            this.chartRiwayat.Anchor = System.Windows.Forms.AnchorStyles.None;
            chartArea1.Name = "MainArea";
            this.chartRiwayat.ChartAreas.Add(chartArea1);
            legend1.Name = "Status";
            this.chartRiwayat.Legends.Add(legend1);
            this.chartRiwayat.Location = new System.Drawing.Point(55, 248);
            this.chartRiwayat.Name = "chartRiwayat";
            series1.ChartArea = "MainArea";
            series1.Legend = "Status";
            series1.Name = "Status Pengaduan";
            this.chartRiwayat.Series.Add(series1);
            this.chartRiwayat.Size = new System.Drawing.Size(1731, 599);
            this.chartRiwayat.TabIndex = 1;
            title1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Grafik Jumlah Status Pengaduan";
            this.chartRiwayat.Titles.Add(title1);
            this.chartRiwayat.Click += new System.EventHandler(this.chartRiwayat_Click);
            // 
            // labelJudul
            // 
            this.labelJudul.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelJudul.AutoSize = true;
            this.labelJudul.Font = new System.Drawing.Font("Showcard Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJudul.ForeColor = System.Drawing.SystemColors.Desktop;
            this.labelJudul.Location = new System.Drawing.Point(612, 49);
            this.labelJudul.Name = "labelJudul";
            this.labelJudul.Size = new System.Drawing.Size(657, 50);
            this.labelJudul.TabIndex = 0;
            this.labelJudul.Text = "DASHBOARD STATUS PENGADUAN";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Semua",
            "Masuk",
            "Diproses",
            "Selesai"});
            this.comboBox2.Location = new System.Drawing.Point(749, 146);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(335, 37);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(55, 853);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 66);
            this.button1.TabIndex = 3;
            this.button1.Text = "Previous";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // FormChartStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1838, 1055);
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