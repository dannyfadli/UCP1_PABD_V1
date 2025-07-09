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
using System.Windows.Forms.VisualStyles;

namespace home
{

    public partial class mhspngd_daftarcs : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        public mhspngd_daftarcs()
        {
            InitializeComponent();
        }

        private void mhspngd_daftarcs_Load(object sender, EventArgs e)
        {
            // Load data ke ComboBox NIM
            LoadComboBoxData();

            // Set default status pengaduan
            comboBoxStatus.SelectedItem = "Masuk";
            datePickerSelesai.ShowCheckBox = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ambil data dari form
            string idPengaduan = txtIdPengaduan.Text.Trim();
            string nim = ((KeyValuePair<string, string>)comboNIM.SelectedItem).Key;
            string deskripsi = txtDeskripsi.Text.Trim();
            string bukti = txtBukti.Text.Trim(); // bisa nama file / path file
            DateTime tanggalPengaduan = datePickerPengaduan.Value;
            DateTime? tanggalSelesai = datePickerSelesai.Checked ? datePickerSelesai.Value : (DateTime?)null;
            string statusPengaduan = comboBoxStatus.SelectedItem?.ToString() ?? "Masuk";

            // Validasi input wajib
            if (string.IsNullOrWhiteSpace(idPengaduan) ||
                string.IsNullOrWhiteSpace(nim) ||
                string.IsNullOrWhiteSpace(deskripsi))
            {
                lblmsg.Text = "ID Pengaduan, NIM, dan Deskripsi wajib diisi!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_CreatePengaduan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@nim", nim);
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@bukti", string.IsNullOrEmpty(bukti) ? (object)DBNull.Value : bukti);
                        cmd.Parameters.AddWithValue("@tanggal_pengaduan", tanggalPengaduan);
                        cmd.Parameters.AddWithValue("@tanggal_Perkiraan_selesai",
                        tanggalSelesai.HasValue ? (object)tanggalSelesai.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@status_pengaduan", statusPengaduan);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblmsg.Text = "Data pengaduan berhasil disimpan!";
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    string errorMessage = ex.Message;

                    // Tangani pesan error dari stored procedure
                    if (errorMessage.Contains("ID pengaduan sudah terdaftar"))
                        lblmsg.Text = "ID pengaduan tersebut sudah digunakan.";
                    else if (errorMessage.Contains("Format ID pengaduan tidak valid"))
                        lblmsg.Text = "Format ID pengaduan tidak valid! Contoh: G0001.";
                    else if (errorMessage.Contains("NIM mahasiswa tidak ditemukan"))
                        lblmsg.Text = "NIM tidak terdaftar!";
                    else if (errorMessage.Contains("Status pengaduan tidak valid"))
                        lblmsg.Text = "Status pengaduan hanya boleh: Masuk, Diproses, atau Selesai.";
                    else if (errorMessage.Contains("Tanggal selesai tidak boleh sebelum"))
                        lblmsg.Text = "Tanggal selesai tidak boleh sebelum tanggal pengaduan.";
                    else
                        lblmsg.Text = "Terjadi kesalahan saat menyimpan data. Cek kembali input!";
                }
            }
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string queryMahasiswa = "SELECT nim, nama FROM Mahasiswa";
                    using (SqlCommand cmd = new SqlCommand(queryMahasiswa, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, string> mahasiswaDict = new Dictionary<string, string>();
                        while (reader.Read())
                        {
                            string nim = reader["nim"].ToString();
                            string nama = reader["nama"].ToString();
                            mahasiswaDict[nim] = $"{nim} - {nama}";
                        }

                        comboNIM.DataSource = new BindingSource(mahasiswaDict, null);
                        comboNIM.DisplayMember = "Value"; // yang ditampilkan
                        comboNIM.ValueMember = "Key";     // yang diambil nilainya (NIM)
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data Mahasiswa: " + ex.Message);
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            mhspngd form = new mhspngd();
            form.Show();
        }

        private void txtIdPengaduan_Enter(object sender, EventArgs e)
        {
            if (txtIdPengaduan.Text == "Contoh: G0001")
            {
                txtIdPengaduan.Text = "";
                txtIdPengaduan.ForeColor = Color.Black;
            }
        }

        private void txtIdPengaduan_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPengaduan.Text))
            {
                txtIdPengaduan.Text = "Contoh: G0001";
                txtIdPengaduan.ForeColor = Color.Gray;
            }
        }

        

        private void comboNIM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
