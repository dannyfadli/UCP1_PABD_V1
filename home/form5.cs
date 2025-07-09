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
            EnsureIndexesRiwayatStatusPengaduan();
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


        private void AnalyzeQuery(string sqlQuery)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO");
                conn.Open();

                var wrapped = $@"
            SET STATISTICS IO ON;
            SET STATISTICS TIME ON;
            {sqlQuery};
            SET STATISTICS IO OFF;
            SET STATISTICS TIME OFF;";

                using (var cmd = new SqlCommand(wrapped, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void AnalyzeRiwayatStatusQuery(string sqlQuery, Dictionary<string, object> parameters = null)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                // Menampilkan hasil STATISTICS IO dan TIME lewat MessageBox
                conn.InfoMessage += (s, e) =>
                {
                    MessageBox.Show(e.Message, "STATISTICS INFO");
                };

                conn.Open();

                string wrapped = $@"
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

{sqlQuery}

SET STATISTICS IO OFF;
SET STATISTICS TIME OFF;
";

                using (var cmd = new SqlCommand(wrapped, conn))
                {
                    // Tambahkan parameter jika ada
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            // Contoh: Kamu bisa mengganti query ini sesuai kebutuhan analisis
            string query = @"
SELECT * 
FROM RiwayatStatusPengaduan 
WHERE id_pengaduan = @id_pengaduan AND status_baru = @status_baru";

            var parameters = new Dictionary<string, object>
    {
        { "@id_pengaduan", "P01" },
        { "@status_baru", "Diproses" }
    };

            try
            {
                AnalyzeRiwayatStatusQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menganalisis query:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void EnsureIndexesRiwayatStatusPengaduan()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var indexScript = @"
                -- Indeks gabungan untuk pencarian berdasarkan id_pengaduan dan status_baru
                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Riwayat_Pengaduan_Status'
                      AND object_id = OBJECT_ID('dbo.RiwayatStatusPengaduan')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Riwayat_Pengaduan_Status
                    ON dbo.RiwayatStatusPengaduan(id_pengaduan, status_baru);
                    PRINT 'Created idx_Riwayat_Pengaduan_Status';
                END
                ELSE
                    PRINT 'idx_Riwayat_Pengaduan_Status sudah ada.';

                -- Indeks untuk kolom tanggal_perubahan
                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Riwayat_Tanggal'
                      AND object_id = OBJECT_ID('dbo.RiwayatStatusPengaduan')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Riwayat_Tanggal
                    ON dbo.RiwayatStatusPengaduan(tanggal_perubahan);
                    PRINT 'Created idx_Riwayat_Tanggal';
                END
                ELSE
                    PRINT 'idx_Riwayat_Tanggal sudah ada.';

                -- Indeks untuk kolom status_baru
                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Riwayat_StatusBaru'
                      AND object_id = OBJECT_ID('dbo.RiwayatStatusPengaduan')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Riwayat_StatusBaru
                    ON dbo.RiwayatStatusPengaduan(status_baru);
                    PRINT 'Created idx_Riwayat_StatusBaru';
                END
                ELSE
                    PRINT 'idx_Riwayat_StatusBaru sudah ada.';

                -- Indeks gabungan untuk id_pengaduan, status_baru, dan tanggal_perubahan
                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Riwayat_Multi'
                      AND object_id = OBJECT_ID('dbo.RiwayatStatusPengaduan')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Riwayat_Multi
                    ON dbo.RiwayatStatusPengaduan(id_pengaduan, status_baru, tanggal_perubahan);
                    PRINT 'Created idx_Riwayat_Multi';
                END
                ELSE
                    PRINT 'idx_Riwayat_Multi sudah ada.';
                ";

                using (var cmd = new SqlCommand(indexScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Tangkap input
            string idRiwayat = txtIdRiwayat.Text.Trim();
            string statusBaru = comboBoxStatus.SelectedItem?.ToString() ?? "";
            DateTime tanggalBaru = datePickerSelesai.Value.Date;

            // 2. Validasi sebelum mulai transaksi
            if (string.IsNullOrEmpty(idRiwayat))
            {
                lblmsg.Text = "Pilih data riwayat yang ingin diubah dulu ya~";
                return;
            }
            if (string.IsNullOrEmpty(statusBaru))
            {
                lblmsg.Text = "Status tidak boleh kosong!";
                return;
            }
            if (tanggalBaru > DateTime.Today.AddDays(30))
            {
                lblmsg.Text = "Tanggal perubahan maksimal hanya sampai 30 hari ke depan!";
                return;
            }

            // 3. Lakukan update di dalam transaksi
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // 4. Call SP
                        using (var cmd = new SqlCommand("sp_UpdateStatusAndTanggalRiwayat", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_riwayat", idRiwayat);
                            cmd.Parameters.AddWithValue("@status_baru", statusBaru);
                            cmd.Parameters.AddWithValue("@tanggal_perubahan", tanggalBaru);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                // SP tidak menemukan record untuk di-update
                                lblmsg.Text = "Update gagal: ID riwayat tidak ditemukan.";
                                tx.Rollback();
                                return;
                            }
                        }

                        // 5. Commit jika berhasil
                        tx.Commit();
                        lblmsg.Text = "Status dan tanggal berhasil diperbarui.";
                        LoadData();
                    }
                    catch (SqlException ex)
                    {
                        tx.Rollback();
                        int code = ex.Errors[0].Number;
                        switch (code)
                        {
                            case 53021:
                                lblmsg.Text = "Format ID riwayat tidak valid! Gunakan pola R01, R02, …";
                                break;
                            case 53022:
                                lblmsg.Text = "Status tidak valid! Pilih: Masuk, Diproses, atau Selesai.";
                                break;
                            case 53023:
                                lblmsg.Text = "ID riwayat tidak ditemukan.";
                                break;
                            case 53025:
                                lblmsg.Text = "Tanggal perubahan maksimal hanya sampai 30 hari ke depan!";
                                break;
                            default:
                                lblmsg.Text = "Terjadi kesalahan saat memperbarui data.";
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        lblmsg.Text = "Terjadi kesalahan tak terduga. Coba lagi nanti.";
                    }
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedCell = dataGridView1.SelectedRows[0].Cells["id_riwayat"].Value;

                if (selectedCell == null || string.IsNullOrWhiteSpace(selectedCell.ToString()))
                {
                    MessageBox.Show("ID tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string id = selectedCell.ToString().Trim();

                // Validasi format ID: harus 5 karakter dan seperti R0001, R0002, dst.
                if (!System.Text.RegularExpressions.Regex.IsMatch(id, @"^R\d{4}$"))
                {
                    MessageBox.Show("Format ID riwayat tidak valid. Gunakan format R0001, R0002, dst.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                                cmd.Parameters.Add("@id_riwayat", SqlDbType.Char, 5).Value = id;

                                cmd.ExecuteNonQuery();

                                transaction.Commit();
                                lblmsg.Text = "Data berhasil dihapus.";
                                LoadData();
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
