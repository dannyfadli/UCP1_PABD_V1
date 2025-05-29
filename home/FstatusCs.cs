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
using System.Windows.Forms.VisualStyles;

namespace home
{
    public partial class FstatusCs : Form
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        public FstatusCs()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (comboIdPengaduan.SelectedItem == null || comboBoxStatus.SelectedItem == null || string.IsNullOrWhiteSpace(txtIdRiwayat.Text))
            {
                MessageBox.Show("Pastikan semua field telah diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idPengaduan = ((KeyValuePair<string, string>)comboIdPengaduan.SelectedItem).Key;
            DateTime tanggalPerubahan = datePickerSelesai.Value;
            string statusPengaduan = comboBoxStatus.SelectedItem.ToString();
            string idRiwayat = txtIdRiwayat.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertRiwayatStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@id_riwayat", idRiwayat);
                        cmd.Parameters.AddWithValue("@status_baru", statusPengaduan);
                        cmd.Parameters.AddWithValue("@tanggal_perubahan", tanggalPerubahan);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Form2 form = new Form2();
            form.Show();
        }

        private void FstatusCs_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            { 
               
                try
                {
                        conn.Open();
                    string query = "select id_pengaduan, nim from Pengaduan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, string> idPengaduanList = new Dictionary<string, string>();
                        while (reader.Read())
                        {
                            string idPengaduan = reader["id_pengaduan"].ToString();
                            string nim = reader["nim"].ToString();
                            idPengaduanList[idPengaduan] = $"{idPengaduan} - {nim}";
                        }
                        comboIdPengaduan.DataSource = new BindingSource(idPengaduanList, null);
                        comboIdPengaduan.DisplayMember = "Value";
                        comboIdPengaduan.ValueMember = "Key";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                

                }
            }
        }
    }
}
