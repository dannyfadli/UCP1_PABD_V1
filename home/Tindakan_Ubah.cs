using home.home;
using NPOI.SS.Formula.Functions;
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

        /* private void UpdateTindakan()
         {
             using (SqlConnection conn = new SqlConnection(connectionString))
             {

                 using (SqlCommand cmd = new SqlCommand("sp_UpdateTindakan", conn))
                 {
                     cmd.CommandType = CommandType.StoredProcedure;
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
         }*/

       
private void DeleteTindakan(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["id_tindakan"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Yakin ingin menghapus tindakan dengan ID: {id}?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlTransaction transaction = null;

                        try
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            using (SqlCommand cmd = new SqlCommand("sp_DeleteTindakan", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@id_tindakan", id);

                                int affected = cmd.ExecuteNonQuery();

                                if (affected > 0)
                                {
                                    transaction.Commit();
                                    lblmsg.Text = "Data tindakan berhasil dihapus!";
                                    // Optional: Tampilkan popup
                                    // MessageBox.Show("Data tindakan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Data tindakan tidak ditemukan atau sudah dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction?.Rollback();

                            if (ex.Message.Contains("tidak ditemukan"))
                            {
                                MessageBox.Show("Data tindakan tidak ditemukan atau sudah dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Gagal menghapus tindakan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string query = "SELECT * FROM Tindakan";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdTindakan.Text) || string.IsNullOrWhiteSpace(txtIdPengaduan.Text))
            {
                MessageBox.Show("Pilih data tindakan yang ingin diubah dulu ya~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Ambil data lama
                    SqlCommand selectCmd = new SqlCommand("sp_GetTindakanById", conn, transaction);
                    selectCmd.CommandType = CommandType.StoredProcedure;
                    selectCmd.Parameters.AddWithValue("@id_tindakan", txtIdTindakan.Text.Trim());

                    SqlDataReader reader = selectCmd.ExecuteReader();

                    bool isChanged = false;

                    if (reader.Read())
                    {
                        if (!txtIdPengaduan.Text.Equals(reader["id_pengaduan"].ToString())) isChanged = true;
                        else if (!txtIdPendamping.Text.Equals(reader["id_pendamping"] == DBNull.Value ? "" : reader["id_pendamping"].ToString())) isChanged = true;
                        else if (!txtDeskripsi.Text.Equals(reader["deskripsi"].ToString())) isChanged = true;
                        else if (dtpTanggalTindakan.Value.Date != Convert.ToDateTime(reader["tanggal_tindakan"]).Date) isChanged = true;
                        else if (!cmbStatusTindakan.SelectedItem?.ToString().Equals(reader["status_tindakan"].ToString()) ?? false) isChanged = true;

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
                        MessageBox.Show("Data tindakan tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lanjut update jika ada perubahan
                    SqlCommand cmd = new SqlCommand("sp_UpdateTindakan", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_tindakan", txtIdTindakan.Text.Trim());
                    cmd.Parameters.AddWithValue("@id_pengaduan", txtIdPengaduan.Text.Trim());
                    cmd.Parameters.AddWithValue("@id_pendamping", string.IsNullOrWhiteSpace(txtIdPendamping.Text) ? (object)DBNull.Value : txtIdPendamping.Text.Trim());
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal_tindakan", dtpTanggalTindakan.Value.Date);
                    cmd.Parameters.AddWithValue("@status_tindakan", cmbStatusTindakan.SelectedItem?.ToString() ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                   lblmsg.Text = "Data tindakan berhasil diperbarui!";
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    string msg = ex.Message;

                    if (msg.Contains("tidak ditemukan"))
                    {
                        lblmsg.Text = "Tindakan dengan ID tersebut tidak ditemukan.";
                    }
                    else if (msg.Contains("Format tanggal tidak valid"))
                    {
                        lblmsg.Text = "Format tanggal tidak valid!";
                    }
                    else if (msg.Contains("ID pengaduan wajib diisi"))
                    {
                        lblmsg.Text = "ID pengaduan wajib diisi!";
                    }
                    else
                    {
                        lblmsg.Text = "Gagal meng-update data. Periksa kembali input yang dimasukkan.";
                    }
                }
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

        private void dtpTanggalTindakan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
