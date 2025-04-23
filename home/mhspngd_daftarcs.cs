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
            // Ambil data dari inputan form
            string idPengaduan = txtIdPengaduan.Text; // contoh TextBox
            string nim = ((KeyValuePair<string, string>)comboNIM.SelectedItem).Key;
            string deskripsi = txtDeskripsi.Text;
            string bukti = txtBukti.Text; // bisa path file atau nama file
            DateTime tanggalPengaduan = datePickerPengaduan.Value;
            DateTime? tanggalSelesai = datePickerSelesai.Checked ? datePickerSelesai.Value : (DateTime?)null;
            string statusPengaduan = comboBoxStatus.SelectedItem?.ToString() ?? "Masuk";

            string query = "INSERT INTO Pengaduan (id_pengaduan, nim, deskripsi, bukti, tanggal_pengaduan, tanggal_selesai, status_pengaduan) " +
                           "VALUES (@IdPengaduan, @NIM, @Deskripsi, @Bukti, @TanggalPengaduan, @TanggalSelesai, @StatusPengaduan)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdPengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@NIM", nim);
                        cmd.Parameters.AddWithValue("@Deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@Bukti", bukti);
                        cmd.Parameters.AddWithValue("@TanggalPengaduan", tanggalPengaduan);

                        if (tanggalSelesai.HasValue)
                            cmd.Parameters.AddWithValue("@TanggalSelesai", tanggalSelesai.Value);
                        else
                            cmd.Parameters.AddWithValue("@TanggalSelesai", DBNull.Value);

                        cmd.Parameters.AddWithValue("@StatusPengaduan", statusPengaduan);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pengaduan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
