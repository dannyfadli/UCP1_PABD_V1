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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace home
{
    public partial class Form4 : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public Form4()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ambil data dari inputan form
           /* string nim = txtNIM.Text;
            string nama = txtNama.Text;
            string jenis_kelamin = comboBoxJK.SelectedItem.ToString();
            string fakultas = comboBoxFakultas.SelectedItem?.ToString();
            string prodi = comboBoxProdi.SelectedItem?.ToString();
            string no_hp = txtNoHP.Text;
            string email = txtEmail.Text;*/


            if (string.IsNullOrWhiteSpace(txtNIM.Text) ||
        string.IsNullOrWhiteSpace(txtNama.Text) ||
        comboBoxJK.SelectedItem == null ||
        comboBoxFakultas.SelectedItem == null ||
        comboBoxProdi.SelectedItem == null ||
        string.IsNullOrWhiteSpace(txtNoHP.Text) ||
        string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblmsg.Text = "Semua field harus diisi!";
                return;
            }

            string nim = txtNIM.Text.Trim();
            string nama = txtNama.Text.Trim();
            string jenis_kelamin = comboBoxJK.SelectedItem.ToString().Trim();
            string fakultas = comboBoxFakultas.SelectedItem.ToString().Trim();
            string prodi = comboBoxProdi.SelectedItem.ToString().Trim();
            string no_hp = txtNoHP.Text.Trim();
            string email = txtEmail.Text.Trim();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try {

                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_RegisterMahasiswa", conn))
                    {

                        cmd.Transaction = transaction; // Gunakan transaksi
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Tambahkan parameter untuk mencegah SQL Injection
                        cmd.Parameters.AddWithValue("@nim", nim);
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin);
                        cmd.Parameters.AddWithValue("@fakultas", fakultas);
                        cmd.Parameters.AddWithValue("@prodi", prodi);
                        cmd.Parameters.AddWithValue("@no_hp", no_hp);
                        cmd.Parameters.AddWithValue("@email", email);

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

                    string errorMessage = ex.Message;

                    if (errorMessage.Contains("sudah terdaftar"))
                        lblmsg.Text = "Mahasiswa dengan NIM tersebut sudah terdaftar!";
                    else if (errorMessage.Contains("Format nomor HP tidak valid"))
                        lblmsg.Text = "Format nomor HP tidak valid! Harus dimulai dengan 62 dan 11-14 digit.";
                    else if (errorMessage.Contains("Format email tidak valid"))
                        lblmsg.Text = "Format email tidak valid!";
                    else if (errorMessage.Contains("Jenis kelamin harus L atau P"))
                        lblmsg.Text = "Jenis kelamin harus L atau P!";
                    else if (errorMessage.Contains("wajib diisi"))
                        lblmsg.Text = "NIM, nama, fakultas, dan prodi wajib diisi!";
                    else
                        lblmsg.Text = "Gagal menyimpan data. Periksa kembali input Anda.";
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblmsg.Text = "Terjadi error: " + ex.Message;
                }

            }


        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide(); // Menyembunyikan Form4
            Form3 form3 = new Form3(); // Membuat instance form sebelumnya
            form3.Show(); // Menampilkan form sebelumnya
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Hindari dobel isi
            if (comboBoxFakultas.Items.Count == 0)
            {
                comboBoxFakultas.Items.AddRange(new string[] { "Teknik", "Ekonomi dan Bisnis", "Hukum", "Ilmu Sosial dan Ilmu Politik", "Agama Islam" ,"Pendidikan Bahasa", "Kedokteran dan Ilmu Kesehatan", "Pertanian", "Vokasi" });
            }

            // Tambah event handler hanya sekali
            comboBoxFakultas.SelectedIndexChanged -= comboBoxFakultas_SelectedIndexChanged;
            comboBoxFakultas.SelectedIndexChanged += comboBoxFakultas_SelectedIndexChanged;
        }


        private void comboBoxFakultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxProdi.Items.Clear(); // Kosongkan prodi dulu

            switch (comboBoxFakultas.SelectedItem.ToString())
            {
                case "Teknik":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Teknik Mesin", "Teknik Elektro", "Teknik Sipil", "Teknologi Informasi"
            });
                    break;
                case "Ekonomi dan Bisnis":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Manajemen", "Akuntansi", "Ilmu Ekonomi"
            });
                    break;
                case "Hukum":
                    comboBoxProdi.Items.AddRange(new string[]{
                "Hukum kelas Reguler", "Hukum kelas Internasional"
            });
                    break;
                case "Ilmu Sosial dan Ilmu Politik":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Ilmu Komunikasi", "Hubungan Internasional ", "Ilmu Pemerintahan"
            });
                    break;
                case "Agama Islam":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Pendidikan Agama Islam", "Komunikasi dan Penyiaran Islam", "Ekonomi Syariah"
            });
                    break;
                case "Pendidikan Bahasa":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Pendidikan Bahasa Inggris", "Pendidikan Bahasa Arab", "Pendidikan Bahasa Jepang"
            });
                    break;
                case "Kedokteran dan Ilmu Kesehatan":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Kedokteran", "Kedokteran Gigi", "Ilmu Keperawatan", "Farmasi"
            });
                    break;
                case "Pertanian":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Agroteknologi", "Agribisnis"
            });
                    break;
                case "Vokasi":
                    comboBoxProdi.Items.AddRange(new string[] {
                "Teknologi Rekayasa Otomotif",
                "Teknik Elektro-medis"
                
            });
                    break;
                default:
                    comboBoxProdi.Items.Clear(); // Tidak ditemukan
                    break;
            }

            comboBoxProdi.SelectedIndex = 0; // Pilih default pertama
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxProdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxFakultas_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lblmsg_Click(object sender, EventArgs e)
        {

        }
    }
}
