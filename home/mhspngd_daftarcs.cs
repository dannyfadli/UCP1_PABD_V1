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

            if (string.IsNullOrWhiteSpace(idPengaduan) || string.IsNullOrWhiteSpace(nim) ||
                               string.IsNullOrWhiteSpace(deskripsi) || string.IsNullOrWhiteSpace(bukti))
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

                    using (SqlCommand cmd = new SqlCommand("sp_CreatePengaduan", conn))
                    {

                        cmd.Transaction = transaction; // Gunakan transaksi
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Tambahkan parameter untuk mencegah SQL Injection

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@nim", nim);
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@bukti", bukti);
                        cmd.Parameters.AddWithValue("@tanggal_pengaduan", tanggalPengaduan);
                        /*  conn.Open();*/
                        cmd.ExecuteNonQuery();
                        /*MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                    }
                    transaction.Commit(); // Commit transaksi jika semua berhasil
                    lblmsg.Text = "Data berhasil disimpan!"; // Tampilkan pesan sukses
                }

                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    // Tangkap pesan dari stored procedure
                    string errorMessage = ex.Message;

                    // Deteksi error karena NIM duplikat
                    if (errorMessage.Contains("ID Pengaduan tersebut sudah terdaftar"))
                    {
                        lblmsg.Text = "ID Pengaduan tersebut sudah terdaftar";
                    }
                    else if (errorMessage.Contains("Format NIM tidak valid"))
                    {
                        lblmsg.Text = "Format NIM tidak valid!";
                    }
                    else if (errorMessage.Contains("Format deskripsi tidak valid"))
                    {
                        lblmsg.Text = "Format deskripsi tidak valid!";
                    }
                    else if (errorMessage.Contains("Bukti tidak valid"))
                    {
                        lblmsg.Text = "Bukti tidak valid!";
                    }
                    else if (errorMessage.Contains("Tanggal pengaduan tidak valid"))
                    {
                        lblmsg.Text = "Tanggal pengaduan tidak valid!";
                    }
                    else
                    {
                        // Pesan umum jika tidak dikenali
                        lblmsg.Text = "Gagal menyimpan data. Pastikan data yang dimasukkan sudah benar.";
                    }

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

        private void comboNIM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
