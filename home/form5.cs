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
using System.Runtime.Caching;
using System.Collections;


namespace home
{
    public partial class form5 : Form
    {

        private string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _Policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) // Cache akan kedaluwarsa setelah 10 menit
        };
        private const string CacheKey = "RiwayatStatusPengaduanCache";
        public form5()
        {
            InitializeComponent();
        }

        private void form5_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Cek apakah data ada di cache
                if (_cache.Contains(CacheKey))
                {
                    dataGridView1.DataSource = (DataTable)_cache.Get(CacheKey);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM RiwayatStatusPengaduan";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                        // Simpan data ke cache
                        _cache.Set(CacheKey, dataTable, _Policy);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtIdRiwayat.Text = row.Cells["id_riwayat"].Value.ToString();
                comboIdPengaduan.Text = row.Cells["id_pengaduan"].Value.ToString();
                comboBoxStatus.SelectedItem = row.Cells["status_baru"].Value.ToString();
                datePickerSelesai.Value = Convert.ToDateTime(row.Cells["tanggal_perubahan"].Value);

                txtIdRiwayat.ReadOnly = true; 
                comboIdPengaduan.ReadOnly = true;
            }
        }

        /* private void UpdateRiwayatStatus()
         {
             using (SqlConnection conn = new SqlConnection(connectionString))
             {
                  using (SqlCommand cmd = new SqlCommand("sp_UpdateRiwayatStatusPengaduan", conn))
                 {

                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@id_riwayat", txtIdRiwayat.Text);
                     cmd.Parameters.AddWithValue("@id_pengaduan", comboIdPengaduan.Text);
                     cmd.Parameters.AddWithValue("@status_baru", comboBoxStatus.SelectedItem.ToString());
                     cmd.Parameters.AddWithValue("@tanggal_perubahan", datePickerSelesai.Value);

                     conn.Open();
                     cmd.ExecuteNonQuery();
                     MessageBox.Show("Data riwayatstatus berhasil diperbarui!");
                 }
             }
         }*/



       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdRiwayat.Text))
            {
                MessageBox.Show("Pilih data riwayat yang ingin diubah dulu ya~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Ambil data lama berdasarkan id_riwayat
                    SqlCommand selectCmd = new SqlCommand("sp_GetRiwayatStatusById", conn, transaction);
                    selectCmd.CommandType = CommandType.StoredProcedure;
                    selectCmd.Parameters.AddWithValue("@id_riwayat", txtIdRiwayat.Text.Trim());

                    SqlDataReader reader = selectCmd.ExecuteReader();
                    bool isChanged = false;

                    if (reader.Read())
                    {
                        string oldIdPengaduan = reader["id_pengaduan"]?.ToString() ?? "";
                        string oldStatusBaru = reader["status_baru"]?.ToString() ?? "";
                        DateTime oldTanggalPerubahan = Convert.ToDateTime(reader["tanggal_perubahan"]);

                        reader.Close();

                        if (!comboIdPengaduan.Text.Trim().Equals(oldIdPengaduan)) isChanged = true;
                        else if (!(comboBoxStatus.SelectedItem?.ToString() ?? "").Equals(oldStatusBaru)) isChanged = true;
                        else if (datePickerSelesai.Value.Date != oldTanggalPerubahan.Date) isChanged = true;

                        if (!isChanged)
                        {
                            MessageBox.Show("Tidak ada perubahan data yang dilakukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Data riwayat status tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lanjut update jika ada perubahan
                    SqlCommand cmd = new SqlCommand("sp_UpdateRiwayatStatusPengaduan", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_riwayat", txtIdRiwayat.Text.Trim());
                    cmd.Parameters.AddWithValue("@id_pengaduan", comboIdPengaduan.Text.Trim());
                    cmd.Parameters.AddWithValue("@status_baru", comboBoxStatus.SelectedItem?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tanggal_perubahan", datePickerSelesai.Value.Date);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                   lblmsg.Text = "Data riwayat status berhasil diperbarui!";
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    string msg = ex.Message;

                    if (msg.Contains("tidak ditemukan"))
                    {
                        lblmsg.Text = "Riwayat status dengan ID tersebut tidak ditemukan.";
                    }
                    else if (msg.Contains("Tanggal tidak valid"))
                    {
                        lblmsg.Text = "Tanggal perubahan tidak valid!";
                    }
                    else if (msg.Contains("Status baru wajib diisi"))
                    {
                        lblmsg.Text = "Status baru wajib diisi!";
                    }
                    else
                    {
                        lblmsg.Text = "Gagal meng-update data. Periksa kembali input yang dimasukkan.";
                    }
                }
            }
        }

      
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["id_riwayat"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Yakin ingin menghapus data dengan ID: {id}?",
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

                            using (SqlCommand cmd = new SqlCommand("sp_DeleteRiwayatStatusPengaduan", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@id_riwayat", id);

                                int affected = cmd.ExecuteNonQuery();

                                if (affected > 0)
                                {
                                    transaction.Commit();
                                    lblmsg.Text = "Data berhasil dihapus.";
                                    // Optional: MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Data tidak ditemukan atau sudah dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            transaction?.Rollback();
                            MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            transaction?.Rollback();
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Tekan kolom kosong paling kiri untuk memilih baris yang ingin dihapus!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form1 = new Form2();
            form1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnRefresh(object sender, EventArgs e)
        {
            LoadData();
            _cache.Remove(CacheKey);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
