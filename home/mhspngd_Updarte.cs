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

        /*private void UpdatePengaduan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_UpdatePengaduan", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_pengaduan", txtIdPengaduan.Text.Trim());
                    cmd.Parameters.AddWithValue("@nim", txtNim.Text.Trim());
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                    cmd.Parameters.AddWithValue("@bukti", txtBukti.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal_pengaduan", dtpTglPengaduan.Value.Date);
                    cmd.Parameters.AddWithValue("@tanggal_selesai", dtpTglSelesai.Value.Date);
                    cmd.Parameters.AddWithValue("@status_pengaduan", cmbStatus.SelectedItem?.ToString());

                    // Parameter untuk menangkap RETURN VALUE dari stored procedure
                    SqlParameter returnValue = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int result = (int)returnValue.Value;

                    if (result > 0)
                    {
                        MessageBox.Show("Data pengaduan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada perubahan data. Mungkin data yang diinput sama seperti sebelumnya.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan saat update pengaduan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

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
                        SqlTransaction transaction = null;
                        try
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();
                            SqlCommand cmd = new SqlCommand("sp_DeletePengaduan", conn, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_pengaduan", id);

                            // Eksekusi langsung tanpa cek rowsAffected
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                            lblmsg.Text = "Data Pengaduan berhasil dihapus!";
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            transaction?.Rollback();
                            /*essageBox.Show("Error saat menghapus Mahasiswa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                            if (ex.Message.Contains("tidak ditemukan"))
                            {
                                MessageBox.Show("Data Pengaduan tidak ditemukan atau sudah dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Gagal menghapus Pengaduan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
                string query = "SELECT * FROM Pengaduan";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPengaduan.Text) || string.IsNullOrWhiteSpace(txtNim.Text))
            {
                MessageBox.Show("Pilih data pengaduan yang mau diubah dulu ya~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Ambil data lama untuk cek perubahan
                    SqlCommand selectCmd = new SqlCommand("sp_GetPengaduanById", conn, transaction);
                    selectCmd.CommandType = CommandType.StoredProcedure;
                    selectCmd.Parameters.AddWithValue("@id_pengaduan", txtIdPengaduan.Text.Trim());

                    SqlDataReader reader = selectCmd.ExecuteReader();

                    bool isChanged = false;

                    if (reader.Read())
                    {
                        if (!txtNim.Text.Equals(reader["nim"].ToString())) isChanged = true;
                        else if (!txtDeskripsi.Text.Equals(reader["deskripsi"].ToString())) isChanged = true;
                        else if (!txtBukti.Text.Equals(reader["bukti"].ToString())) isChanged = true;
                        else if (dtpTglPengaduan.Value.Date != Convert.ToDateTime(reader["tanggal_pengaduan"]).Date) isChanged = true;
                        else if (dtpTglSelesai.Value.Date != Convert.ToDateTime(reader["tanggal_selesai"]).Date) isChanged = true;
                        else if (!cmbStatus.SelectedItem.ToString().Equals(reader["status_pengaduan"].ToString())) isChanged = true;

                        reader.Close();

                        if (!isChanged)
                        {
                            MessageBox.Show("Tidak ada perubahan data yang dilakukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Data pengaduan tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    // Jika ada perubahan, lakukan update
                    SqlCommand cmd = new SqlCommand("sp_UpdatePengaduan", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_pengaduan", txtIdPengaduan.Text.Trim());
                    cmd.Parameters.AddWithValue("@nim", txtNim.Text.Trim());
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                    cmd.Parameters.AddWithValue("@bukti", txtBukti.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal_pengaduan", dtpTglPengaduan.Value.Date);
                    cmd.Parameters.AddWithValue("@tanggal_selesai", dtpTglSelesai.Value.Date);
                    cmd.Parameters.AddWithValue("@status_pengaduan", cmbStatus.SelectedItem?.ToString());

                    SqlParameter returnValue = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    cmd.ExecuteNonQuery();

                    int result = (int)returnValue.Value;

                    transaction.Commit();

                    if (result > 0)
                    {
                        lblmsg.Text = "Data pengaduan berhasil diperbarui!";
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada perubahan data. Mungkin data yang diinput sama seperti sebelumnya.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    string errorMessage = ex.Message;

                    if (errorMessage.Contains("tidak ditemukan"))
                    {
                        lblmsg.Text = "Data pengaduan tidak ditemukan.";
                    }
                    else if (errorMessage.Contains("Format tanggal tidak valid"))
                    {
                        lblmsg.Text = "Format tanggal tidak valid! Pastikan tanggal pengaduan dan tanggal selesai sudah benar.";
                    }
                    else if (errorMessage.Contains("NIM tidak boleh kosong"))
                    {
                        lblmsg.Text = "NIM tidak boleh kosong!";
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan saat update pengaduan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                dtpTglPengaduan.Enabled = false;
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            mhspngd form = new mhspngd();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDeskripsi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
