using home.home;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace home
{
    public partial class FstatusCs : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public FstatusCs()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string idRiwayat = txtIdRiwayat.Text;
            string idPengaduan = txtIdPengaduan.Text;
            string statusBaru = cbStatusBaru.SelectedItem?.ToString();

            string query = "INSERT INTO RiwayatStatusPengaduan (id_riwayat, id_pengaduan, status_baru) " +
                           "VALUES (@IdRiwayat, @IdPengaduan, @StatusBaru)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdRiwayat", idRiwayat);
                        cmd.Parameters.AddWithValue("@IdPengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@StatusBaru", statusBaru);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Riwayat status berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Ganti dengan form utama Riwayat jika ada
            Form2 form = new Form2();
            form.Show();
        }

        private void RiwayatStatusForm_Load(object sender, EventArgs e)
        {
            // Inisialisasi ComboBox status
            cbStatusBaru.Items.Add("Masuk");
            cbStatusBaru.Items.Add("Diproses");
            cbStatusBaru.Items.Add("Selesai");
        }
    }
}
