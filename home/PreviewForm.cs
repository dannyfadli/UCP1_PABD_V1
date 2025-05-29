using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home
{
    public partial class PreviewForm : Form
    {
        public PreviewForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
             private string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        // Konstruktor menerima DataTable dan menampilkan data di DataGridView
        public PreviewForm(DataTable data)
        {
            InitializeComponent();
            // Menetapkan data source DataGridView ke DataTable yang diterima
            dgvPreview.DataSource = data;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            // Opsional: Sesuaikan DataGridView jika perlu
            dgvPreview.AutoResizeColumns(); // Menyesuaikan ukuran kolom
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Menanyakan kepada pengguna jika mereka ingin mengimpor data
            DialogResult result = MessageBox.Show("Apakah Anda ingin mengimpor data ini ke database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Mengimpor data dari DataGridView ke database
                ImportDataToDatabase();
            }



        }

        private bool ValidateRow(DataRow row)
        {
            string nim = row["nim"].ToString();

            // Validasi NIM (misalnya, harus berjumlah 11 karakter)
            if (nim.Length != 11)
            {
                MessageBox.Show("nim harus terdiri dari 11 karakter", "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Jika perlu, tambahkan validasi lain sesuai dengan kebutuhan (misalnya pola tertentu untuk NIM)
            return true;
        }

        private void ImportDataToDatabase()
        {
            try
            {
                DataTable dt = (DataTable)dgvPreview.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    // Validasi setiap baris
                    if (!ValidateRow(row))
                        continue;

                    string query = @"INSERT INTO Mahasiswa 
                             (nim, nama, jenis_kelamin, fakultas, prodi, no_hp, email) 
                             VALUES 
                             (@nim, @nama, @jenis_kelamin, @fakultas, @prodi, @no_hp, @email)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nim", row["nim"]);
                            cmd.Parameters.AddWithValue("@nama", row["nama"]);
                            cmd.Parameters.AddWithValue("@jenis_kelamin", row["jenis_kelamin"]);
                            cmd.Parameters.AddWithValue("@fakultas", row["fakultas"]);
                            cmd.Parameters.AddWithValue("@prodi", row["prodi"]);
                            cmd.Parameters.AddWithValue("@no_hp", row["no_hp"]);
                            cmd.Parameters.AddWithValue("@email", row["email"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Data berhasil diimpor ke database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

