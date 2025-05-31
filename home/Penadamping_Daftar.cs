using home.home;
using NPOI.SS.Formula.Functions;
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

namespace home
{
    public partial class Penadamping_Daftar : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public Penadamping_Daftar()
        {
            InitializeComponent();
        }

    
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ambil data dari inputan form
            string idPendamping = txtIdPendamping.Text;
            string nama = txtNama.Text;
            string noHp = txtNoHP.Text;
            string email = txtEmail.Text;

            // Validasi input
            if (string.IsNullOrWhiteSpace(idPendamping) || string.IsNullOrWhiteSpace(nama) ||
                string.IsNullOrWhiteSpace(noHp) || string.IsNullOrWhiteSpace(email))
            {
                lblmsg.Text = "Semua field harus diisi!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertPendamping", conn))
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_pendamping", idPendamping);
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@no_hp", noHp);
                        cmd.Parameters.AddWithValue("@email", email);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblmsg.Text = "Pendamping berhasil ditambahkan!";
                    // Atau jika ingin pakai MessageBox:
                    // MessageBox.Show("Pendamping berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();
                    string errorMessage = ex.Message;

                    // Deteksi error umum dari stored procedure
                    if (errorMessage.Contains("Pendamping dengan ID tersebut sudah terdaftar"))
                    {
                        lblmsg.Text = "Pendamping dengan ID tersebut sudah terdaftar!";
                    }
                    else if (errorMessage.Contains("Format nomor HP tidak valid"))
                    {
                        lblmsg.Text = "Format nomor HP tidak valid!";
                    }
                    else if (errorMessage.Contains("Format email tidak valid"))
                    {
                        lblmsg.Text = "Format email tidak valid!";
                    }
                    else
                    {
                        lblmsg.Text = "Gagal menyimpan data. Pastikan data yang dimasukkan sudah benar.";
                    }
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Ganti form berikut sesuai kebutuhan
            Pendamping form = new Pendamping();
            form.Show();
        }

        private void Penadamping_Daftar_Load(object sender, EventArgs e)
        {

        }
    }
}
