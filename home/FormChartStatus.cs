using home.home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace home
{
    public partial class FormChartStatus : BaseForm
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";

        public FormChartStatus()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void FormChartStatus_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            LoadStatusChart("Semua");
        }

        private void LoadStatusChart(string filterStatus)
        {
            // Reset chart
            chartRiwayat.Series.Clear();
            chartRiwayat.Titles.Clear();
            chartRiwayat.Legends.Clear();
            chartRiwayat.ChartAreas.Clear();

            // Set Chart Area
            ChartArea ca = new ChartArea("MainArea");
            ca.AxisX.Title = "Status Pengaduan";
            ca.AxisY.Title = "Jumlah";
            chartRiwayat.ChartAreas.Add(ca);

            // SQL Query
            //string connStr = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
            string query;

            if (filterStatus == "Semua")
            {
                query = @"
            SELECT status_baru, COUNT(*) AS Jumlah
            FROM RiwayatStatusPengaduan
            GROUP BY status_baru;";
            }
            else
            {
                query = @"
            SELECT status_baru, COUNT(*) AS Jumlah
            FROM RiwayatStatusPengaduan
            WHERE status_baru = @status
            GROUP BY status_baru;";
            }

            // Ambil data
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (filterStatus != "Semua")
                {
                    cmd.Parameters.AddWithValue("@status", filterStatus);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            // Siapkan series grafik
            Series series = new Series("Status Pengaduan")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            // Jika filter "Semua", tampilkan semua status bahkan jika tidak ada di data
            if (filterStatus == "Semua")
            {
                // Inisialisasi status dan default jumlah 0
                Dictionary<string, int> dataStatus = new Dictionary<string, int>()
        {
            { "Masuk", 0 },
            { "Diproses", 0 },
            { "Selesai", 0 }
        };

                // Isi data dari hasil query (jika ada)
                foreach (DataRow row in dt.Rows)
                {
                    string status = row["status_baru"].ToString();
                    int jumlah = Convert.ToInt32(row["Jumlah"]);

                    if (dataStatus.ContainsKey(status))
                    {
                        dataStatus[status] = jumlah;
                    }
                }

                // Tambahkan ke grafik
                foreach (var item in dataStatus)
                {
                    int idx = series.Points.AddXY(item.Key, item.Value);

                    switch (item.Key)
                    {
                        case "Masuk":
                            series.Points[idx].Color = System.Drawing.Color.SteelBlue;
                            break;
                        case "Diproses":
                            series.Points[idx].Color = System.Drawing.Color.Orange;
                            break;
                        case "Selesai":
                            series.Points[idx].Color = System.Drawing.Color.ForestGreen;
                            break;
                    }
                }
            }
            else
            {
                // Hanya satu status (filter)
                if (dt.Rows.Count > 0)
                {
                    string status = dt.Rows[0]["status_baru"].ToString();
                    int jumlah = Convert.ToInt32(dt.Rows[0]["Jumlah"]);
                    int idx = series.Points.AddXY(status, jumlah);

                    switch (status)
                    {
                        case "Masuk":
                            series.Points[idx].Color = System.Drawing.Color.SteelBlue;
                            break;
                        case "Diproses":
                            series.Points[idx].Color = System.Drawing.Color.Orange;
                            break;
                        case "Selesai":
                            series.Points[idx].Color = System.Drawing.Color.ForestGreen;
                            break;
                    }
                }
                else
                {
                    // Tidak ada data → tampilkan batang 0
                    int idx = series.Points.AddXY(filterStatus, 0);

                    switch (filterStatus)
                    {
                        case "Masuk":
                            series.Points[idx].Color = System.Drawing.Color.SteelBlue;
                            break;
                        case "Diproses":
                            series.Points[idx].Color = System.Drawing.Color.Orange;
                            break;
                        case "Selesai":
                            series.Points[idx].Color = System.Drawing.Color.ForestGreen;
                            break;
                        default:
                            series.Points[idx].Color = System.Drawing.Color.Gray;
                            break;
                    }
                }
            }

            // Tambahkan ke chart
            chartRiwayat.Series.Add(series);
            chartRiwayat.Titles.Add("Grafik Status Pengaduan");
            chartRiwayat.Legends.Add(new Legend("Status"));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chartRiwayat_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBox2.SelectedItem.ToString();
            LoadStatusChart(selected);
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();

        }

    }
}
