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
        //string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        Koneksi kn = new Koneksi();
        string strKonek = "";

        public Penadamping_Daftar()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ambil dan trimming data input
            string idPendamping = txtIdPendamping.Text.Trim();
            string nama = txtNama.Text.Trim();
            string noHp = txtNoHP.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Validasi input kosong
            if (string.IsNullOrWhiteSpace(idPendamping) ||
                string.IsNullOrWhiteSpace(nama) ||
                string.IsNullOrWhiteSpace(noHp) ||
                string.IsNullOrWhiteSpace(email))
            {
                lblmsg.Text = "Semua field harus diisi!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertPendamping", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_pendamping", idPendamping);
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@no_hp", noHp);
                        cmd.Parameters.AddWithValue("@email", email);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblmsg.Text = "Pendamping berhasil ditambahkan!";
                    // MessageBox.Show("Pendamping berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    // Mapping error SQL berdasarkan isi pesan
                    string msg = ex.Message;

                    if (msg.Contains("ID Pendamping sudah terdaftar"))
                        lblmsg.Text = "Pendamping dengan ID tersebut sudah terdaftar!";
                    else if (msg.Contains("Nama hanya boleh mengandung huruf"))
                        lblmsg.Text = "Nama hanya boleh terdiri dari huruf dan spasi!";
                    else if (msg.Contains("No HP harus diawali"))
                        lblmsg.Text = "Nomor HP harus diawali dengan '62' dan 11-14 karakter!";
                    else if (msg.Contains("Format email tidak valid"))
                        lblmsg.Text = "Format email tidak valid!";
                    else if (msg.Contains("Email sudah digunakan"))
                        lblmsg.Text = "Email sudah digunakan oleh pendamping lain!";
                    else if (msg.Contains("Format ID Pendamping tidak valid"))
                        lblmsg.Text = "Format ID harus D0001, D0002, dst.";
                    else
                        lblmsg.Text = "Gagal menyimpan data. Pastikan data yang dimasukkan sudah benar.";
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

        private void txtIdPendamping_Enter(object sender, EventArgs e)
        {
            if (txtIdPendamping.Text == "Contoh: D0001")
            {
                txtIdPendamping.Text = "";
                txtIdPendamping.ForeColor = Color.Black;
            }
        }

        private void txtIdPendamping_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPendamping.Text))
            {
                txtIdPendamping.Text = "Contoh: D0001";
                txtIdPendamping.ForeColor = Color.Gray;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
