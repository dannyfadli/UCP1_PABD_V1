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
using Microsoft.Reporting.WinForms;

namespace home
{
    public partial class FormLaporan : Form
    {
        public FormLaporan()
        {
            InitializeComponent();
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            SetupReportViewer();
        }

        private void SetupReportViewer()
        {
            // Connection string to your database
            string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

            // SQL query to retrieve the required data from the database

            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // Use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_GetPengaduanMahasiswa", conn);
                da.Fill(dt);
            }

            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // "DataSet1" harus sama dengan nama dataset di file RDLC

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            // Ganti path ini dengan lokasi sebenarnya file RDLC di proyek kamu
            reportViewer1.LocalReport.ReportPath = @"F:\Pabd\home\home\Report1.rdlc";

            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide(); // Menyembunyikan Form4
            Form2 form2 = new Form2(); // Membuat instance form sebelumnya
            form2.Show(); // Menampilkan form sebelumnya
        }



        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
