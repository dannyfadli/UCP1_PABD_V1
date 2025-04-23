using home.home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace home
{
    public partial class mhspngd_Updarte : BaseForm
    {

        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public mhspngd_Updarte()
        {
            InitializeComponent();
        }

        public void PengaduanUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
            dtpTglSelesai.ShowCheckBox = true;
        }

        private void UpdatePengaduan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Pengaduan SET nim = @nim, deskripsi = @deskripsi, bukti = @bukti, 
                             tanggal_pengaduan = @tanggal_pengaduan, tanggal_selesai = @tanggal_selesai, 
                             status_pengaduan = @status_pengaduan WHERE id_pengaduan = @id_pengaduan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_pengaduan", txtIdPengaduan.Text);
                    cmd.Parameters.AddWithValue("@nim", txtNim.Text);
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text);
                    cmd.Parameters.AddWithValue("@bukti", txtBukti.Text);
                    cmd.Parameters.AddWithValue("@tanggal_pengaduan", dtpTglPengaduan.Value);
                    cmd.Parameters.AddWithValue("@tanggal_selesai", dtpTglSelesai.Value);
                    cmd.Parameters.AddWithValue("@status_pengaduan", cmbStatus.SelectedItem.ToString());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data pengaduan berhasil diperbarui!");
                }
            }
        }

        private void DeletePengaduan(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["id_pengaduan"].Value.ToString();
                DialogResult result = MessageBox.Show("Yakin ingin menghapus pengaduan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Pengaduan WHERE id_pengaduan = @id_pengaduan";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id_pengaduan", id);
                            int affected = cmd.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Data pengaduan berhasil dihapus!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data pengaduan tidak ditemukan.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Pengaduan";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtIdPengaduan.Text == "" || txtNim.Text == "" || txtDeskripsi.Text == "")
            {
                MessageBox.Show("Harap isi semua data pengaduan terlebih dahulu!");
                return;
            }

            try
            {
                UpdatePengaduan();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtIdPengaduan.Text = row.Cells["id_pengaduan"].Value.ToString();
                txtNim.Text = row.Cells["nim"].Value.ToString();
                txtDeskripsi.Text = row.Cells["deskripsi"].Value.ToString();
                txtBukti.Text = row.Cells["bukti"].Value?.ToString();
                dtpTglPengaduan.Value = Convert.ToDateTime(row.Cells["tanggal_pengaduan"].Value);
                dtpTglSelesai.Value = row.Cells["tanggal_selesai"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["tanggal_selesai"].Value);
                cmbStatus.SelectedItem = row.Cells["status_pengaduan"].Value.ToString();

                txtIdPengaduan.ReadOnly = true;
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.Show();
        }
    }
}
