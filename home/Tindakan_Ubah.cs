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
    public partial class Tindakan_Ubah : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public Tindakan_Ubah()
        {
            InitializeComponent();
        }

        public void TindakanUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbStatusTindakan.Items.AddRange(new string[] { "Direncanakan", "Dilaksanakan", "Ditunda" });
            cmbStatusTindakan.SelectedItem = "Direncanakan";
        }

        private void UpdateTindakan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Tindakan SET 
                                id_pengaduan = @id_pengaduan,
                                id_pendamping = @id_pendamping,
                                deskripsi = @deskripsi,
                                tanggal_tindakan = @tanggal_tindakan,
                                status_tindakan = @status_tindakan 
                             WHERE id_tindakan = @id_tindakan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_tindakan", txtIdTindakan.Text);
                    cmd.Parameters.AddWithValue("@id_pengaduan", txtIdPengaduan.Text);
                    cmd.Parameters.AddWithValue("@id_pendamping", string.IsNullOrWhiteSpace(txtIdPendamping.Text) ? DBNull.Value : (object)txtIdPendamping.Text);
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text);
                    cmd.Parameters.AddWithValue("@tanggal_tindakan", dtpTanggalTindakan.Value);
                    cmd.Parameters.AddWithValue("@status_tindakan", cmbStatusTindakan.SelectedItem.ToString());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data tindakan berhasil diperbarui!");
                }
            }
        }

        private void DeleteTindakan(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["id_tindakan"].Value.ToString();
                DialogResult result = MessageBox.Show("Yakin ingin menghapus tindakan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Tindakan WHERE id_tindakan = @id_tindakan";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id_tindakan", id);
                            int affected = cmd.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Data tindakan berhasil dihapus!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data tindakan tidak ditemukan.");
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
                string query = "SELECT * FROM Tindakan";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtIdTindakan.Text == "" || txtIdPengaduan.Text == "" || txtDeskripsi.Text == "")
            {
                MessageBox.Show("Harap isi semua data tindakan terlebih dahulu!");
                return;
            }

            try
            {
                UpdateTindakan();
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
                txtIdTindakan.Text = row.Cells["id_tindakan"].Value.ToString();
                txtIdPengaduan.Text = row.Cells["id_pengaduan"].Value.ToString();
                txtIdPendamping.Text = row.Cells["id_pendamping"].Value?.ToString();
                txtDeskripsi.Text = row.Cells["deskripsi"].Value.ToString();
                dtpTanggalTindakan.Value = Convert.ToDateTime(row.Cells["tanggal_tindakan"].Value);
                cmbStatusTindakan.SelectedItem = row.Cells["status_tindakan"].Value.ToString();

                txtIdTindakan.ReadOnly = true;
                txtIdPengaduan.ReadOnly = true;
                txtIdPendamping.ReadOnly = true;
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tindakan form = new Tindakan();
            form.Show();
        }
    }

}
