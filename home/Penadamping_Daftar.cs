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
            string idPendamping = txtIdPendamping.Text;
            string nama = txtNama.Text;
            string noHp = txtNoHP.Text;
            string email = txtEmail.Text;

            string query = "INSERT INTO Pendamping (id_pendamping, nama, no_hp, email) " +
                           "VALUES (@IdPendamping, @Nama, @NoHp, @Email)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdPendamping", idPendamping);
                        cmd.Parameters.AddWithValue("@Nama", nama);
                        cmd.Parameters.AddWithValue("@NoHp", noHp);
                        cmd.Parameters.AddWithValue("@Email", email);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Pendamping berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
