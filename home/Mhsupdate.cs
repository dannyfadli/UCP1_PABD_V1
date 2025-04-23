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
    public partial class Mhsupdate : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        public Mhsupdate()
        {
            InitializeComponent();
            
        }

        public void Mhsupdate_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void UpdateMahasiswa()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Mahasiswa SET Nama = @Nama, jenis_kelamin = @jenis_kelamin, Fakultas = @Fakultas, Prodi = @Prodi, no_hp = @no_hp, Email = @Email WHERE Nim = @Nim";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nim", txtNim.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@jenis_kelamin", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Fakultas", txtFakultas.Text);
                    cmd.Parameters.AddWithValue("@Prodi", txtProdi.Text);
                    cmd.Parameters.AddWithValue("@no_hp", txtNoHP.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diperbarui!");
                }
            }
        }


        private void DeleteMahasiswa(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string nim = dataGridView1.SelectedRows[0].Cells["nim"].Value.ToString();

                DialogResult result = MessageBox.Show("Yakin ingin menghapus pelanggan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Mahasiswa WHERE nim = @nim";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@nim", nim);
                            int affected = cmd.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Data Mahasiswa berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data Mahasiswa tidak ditemukan!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error saat menghapus Mahasiswa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Tekan kolom kosong paling kiri untuk memilih baris yang ingin dihapus!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Mahasiswa"; // ganti Mahasiswa dengan nama tabelmu
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtNim.Text == "" || txtNama.Text == "" || txtFakultas.Text == "" || txtProdi.Text == "")
            {
                MessageBox.Show("Pilih data yang mau diubah dulu dong~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Mahasiswa SET Nama = @Nama, jenis_kelamin = @jenis_kelamin, Fakultas = @Fakultas, Prodi = @Prodi, no_hp = @no_hp, Email = @Email WHERE Nim = @Nim";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nim", txtNim.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@jenis_kelamin", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Fakultas", txtFakultas.Text);
                    cmd.Parameters.AddWithValue("@Prodi", txtProdi.Text);
                    cmd.Parameters.AddWithValue("@no_hp", txtNoHP.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data maintenance berhasil di-update, nice~", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Gagal update, data ga ketemu atau belum diubah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtNim.Text = row.Cells["nim"].Value.ToString();
                txtNama.Text = row.Cells["nama"].Value.ToString();
                comboBox1.SelectedItem = row.Cells["jenis_kelamin"].Value;
                txtFakultas.Text = row.Cells["fakultas"].Value.ToString();
                txtProdi.Text = row.Cells["prodi"].Value.ToString();
                txtNoHP.Text = row.Cells["no_hp"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();

                txtNim.ReadOnly = true; // Set NIM menjadi read-only agar tidak bisa diubah
            }
        }
    }
}
