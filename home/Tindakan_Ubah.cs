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
using System.Runtime.Caching;



namespace home
{
    public partial class Tindakan_Ubah : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _Policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) // Cache akan kedaluwarsa setelah 10 menit
        };
        private const string CacheKey = "TindakanData";
        public Tindakan_Ubah()
        {
            InitializeComponent();
        }

        public void TindakanUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbStatusTindakan.Items.AddRange(new string[] { "Direncanakan", "Dilaksanakan", "Ditunda" });
            cmbStatusTindakan.SelectedItem = "Direncanakan";

            EnsureIndexesTindakan();
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
            // Pastikan user memilih satu baris
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Klik kotak paling kiri untuk memilih baris yang ingin dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ambil nilai id_tindakan dari baris yang dipilih
            string id = dataGridView1.SelectedRows[0].Cells["id_tindakan"].Value.ToString().Trim();

            // Validasi id
            if (string.IsNullOrWhiteSpace(id) || id.StartsWith("Contoh"))
            {
                MessageBox.Show("ID tindakan tidak valid atau belum dipilih.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi penghapusan
            DialogResult result = MessageBox.Show(
                $"Yakin ingin menghapus tindakan dengan ID: {id}?",
                "Konfirmasi Penghapusan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                        conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteTindakan", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_tindakan", id);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                            lblmsg.Text = "Data tindakan berhasil dihapus!";
                            LoadData();
                            if (_cache.Contains(CacheKey)) _cache.Remove(CacheKey);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        if (ex.Message.Contains("tidak ditemukan"))
                        {
                            MessageBox.Show("Data tindakan tidak ditemukan atau sudah dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Terjadi kesalahan saat menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
        }


        private void EnsureIndexesTindakan()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var indexScript = @"
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'idx_Tindakan_IdPengaduan'
              AND object_id = OBJECT_ID('dbo.Tindakan')
        )
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Tindakan_IdPengaduan
            ON dbo.Tindakan(id_pengaduan);
            PRINT 'Created idx_Tindakan_IdPengaduan';
        END
        ELSE
            PRINT 'idx_Tindakan_IdPengaduan sudah ada.';

        -- Indeks untuk kolom id_pendamping
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'idx_Tindakan_IdPendamping'
              AND object_id = OBJECT_ID('dbo.Tindakan')
        )
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Tindakan_IdPendamping
            ON dbo.Tindakan(id_pendamping);
            PRINT 'Created idx_Tindakan_IdPendamping';
        END
        ELSE
            PRINT 'idx_Tindakan_IdPendamping sudah ada.';

        -- Indeks untuk kolom status_tindakan
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'idx_Tindakan_Status'
              AND object_id = OBJECT_ID('dbo.Tindakan')
        )
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Tindakan_Status
            ON dbo.Tindakan(status_tindakan);
            PRINT 'Created idx_Tindakan_Status';
        END
        ELSE
            PRINT 'idx_Tindakan_Status sudah ada.';
        ";

                using (var cmd = new SqlCommand(indexScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnalyzeQueryWithParams(string sqlQuery, Dictionary<string, object> parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (s, e) =>
                {
                    MessageBox.Show(e.Message, "STATISTICS INFO");
                };

                conn.Open();

                var wrapped = $@"
            SET STATISTICS IO ON;
            SET STATISTICS TIME ON;
            {sqlQuery};
            SET STATISTICS IO OFF;
            SET STATISTICS TIME OFF;";

                using (var cmd = new SqlCommand(wrapped, conn))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnAnalyzeTindakan_Click(object sender, EventArgs e)
        {
            string idPendamping = txtIdPendamping.Text.Trim();

            string sqlQuery = @"
        SELECT id_tindakan, id_pengaduan, deskripsi, tanggal_tindakan, status_tindakan
        FROM dbo.Tindakan
        WHERE id_pendamping = @idPendamping";

            var parameters = new Dictionary<string, object>
             {
                 { "@idPendamping", idPendamping }
             };

            AnalyzeQueryWithParams(sqlQuery, parameters);
        }



        private void LoadData()
        {
            // Cek apakah data sudah ada di cache
            if (_cache.Contains(CacheKey))
            {
                dataGridView1.DataSource = _cache.Get(CacheKey) as DataTable;
                return;
            }

            // Jika tidak ada di cache, ambil dari database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tindakan";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Set data ke DataGridView
                dataGridView1.DataSource = dt;

                // Simpan data ke cache
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10)
                };

                _cache.Set(CacheKey, dt, policy); // gunakan Set agar replace jika sudah ada
            }
        }






        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string idTindakan = txtIdTindakan.Text.Trim();
            string idPengaduan = txtIdPengaduan.Text.Trim();
            string idPendamping = txtIdPendamping.Text.Trim();
            string deskripsi = txtDeskripsi.Text.Trim();
            DateTime tanggalTindakan = dtpTanggalTindakan.Value.Date;
            string statusTindakan = cmbStatusTindakan.SelectedItem?.ToString();

            // Validasi awal
            if (string.IsNullOrWhiteSpace(idTindakan) || string.IsNullOrWhiteSpace(idPengaduan))
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
                    bool isChanged = false;
                    using (SqlCommand selectCmd = new SqlCommand("sp_GetTindakanById", conn, transaction))
                    {
                        selectCmd.CommandType = CommandType.StoredProcedure;
                        selectCmd.Parameters.AddWithValue("@id_tindakan", idTindakan);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!idPengaduan.Equals(reader["id_pengaduan"].ToString()))
                                    isChanged = true;
                                else if (!idPendamping.Equals(reader["id_pendamping"] == DBNull.Value ? "" : reader["id_pendamping"].ToString()))
                                    isChanged = true;
                                else if (!deskripsi.Equals(reader["deskripsi"].ToString()))
                                    isChanged = true;
                                else if (tanggalTindakan != Convert.ToDateTime(reader["tanggal_tindakan"]).Date)
                                    isChanged = true;
                                else if (!(statusTindakan?.Equals(reader["status_tindakan"].ToString()) ?? false))
                                    isChanged = true;
                            }
                            else
                            {
                                MessageBox.Show("Data tindakan tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    if (!isChanged)
                    {
                        MessageBox.Show("Tidak ada perubahan data yang dilakukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Update data
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateTindakan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_tindakan", idTindakan);
                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@id_pendamping", string.IsNullOrWhiteSpace(idPendamping) ? DBNull.Value : (object)idPendamping);
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@tanggal_tindakan", tanggalTindakan);
                        cmd.Parameters.AddWithValue("@status_tindakan", statusTindakan ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    _cache?.Remove(CacheKey); // Opsional: hanya jika cache dipakai

                    lblmsg.Text = "Data tindakan berhasil diperbarui!";
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    string msg = ex.Message;

                    if (msg.Contains("tidak ditemukan"))
                        lblmsg.Text = "Tindakan dengan ID tersebut tidak ditemukan.";
                    else if (msg.Contains("tanggal tidak boleh sebelum"))
                        lblmsg.Text = "Tanggal tindakan tidak boleh lebih awal dari tanggal pengaduan.";
                    else if (msg.Contains("wajib diisi"))
                        lblmsg.Text = "Data penting belum diisi!";
                    else if (msg.Contains("format ID"))
                        lblmsg.Text = "Format ID tindakan harus T0001, T0002, ...";
                    else if (msg.Contains("status tindakan tidak valid"))
                        lblmsg.Text = "Status hanya boleh Direncanakan, Dilaksanakan, atau Ditunda.";
                    else
                        lblmsg.Text = "Gagal meng-update data. Periksa kembali input yang dimasukkan.";
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

        private void BtnRefresh(object sender, EventArgs e)
        {
            LoadData();
            _cache.Remove(CacheKey);
        }

        private void dtpTanggalTindakan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbStatusTindakan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
