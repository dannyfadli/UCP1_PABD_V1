﻿using home.home;
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
    public partial class Form4 : Form
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public Form4()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ambil data dari inputan form
            string nim = txtNIM.Text;
            string nama = txtNama.Text;
            string jenis_kelamin = comboBoxJK.SelectedItem.ToString();
            string fakultas = comboBoxFakultas.SelectedItem?.ToString();
            string prodi = comboBoxProdi.SelectedItem?.ToString();
            string no_hp = txtNoHP.Text;
            string email = txtEmail.Text;

   

            string query = "INSERT INTO Mahasiswa (NIM, Nama, jenis_kelamin, Fakultas, Prodi, no_hp, Email) " +
                           "VALUES (@NIM, @Nama, @JenisKelamin, @Fakultas, @Prodi, @NoHP, @Email)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Tambahkan parameter untuk mencegah SQL Injection
                        cmd.Parameters.AddWithValue("@NIM", nim);
                        cmd.Parameters.AddWithValue("@Nama", nama);
                        cmd.Parameters.AddWithValue("@JenisKelamin", jenis_kelamin);
                        cmd.Parameters.AddWithValue("@Fakultas", fakultas);
                        cmd.Parameters.AddWithValue("@Prodi", prodi);
                        cmd.Parameters.AddWithValue("@NoHP", no_hp);
                        cmd.Parameters.AddWithValue("@Email", email);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
