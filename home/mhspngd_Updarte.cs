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
using System.Runtime.Caching;


namespace home
{
    public partial class mhspngd_Updarte : BaseForm
    {

        //string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        Koneksi kn = new Koneksi();
        string strKonek = "";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _Policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) // Cache akan kedaluwarsa setelah 10 menit
        };
        private const string CacheKey = "PengaduanData";
        public mhspngd_Updarte()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        public void PengaduanUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
            dtpTglSelesai.ShowCheckBox = true;
            textBox1.TextChanged += SearchDataPengaduan_TextChanged;

            EnsureIndexesPengaduan();
           
        }

        private void SearchDataPengaduan_TextChanged(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);

            string kw = textBox1.Text.Trim();
            SearchDataPengaduan(string.IsNullOrEmpty(kw) ? null : kw);
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
                    using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
                            
                            _cache.Remove(CacheKey); // Hapus cache setelah penghapusan
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
            if (_cache.Contains(CacheKey))
            {
                dataGridView1.DataSource = _cache.Get(CacheKey) as DataTable;
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                string query = "SELECT * FROM Pengaduan";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) // cache 10 menit
                };

                _cache.Add(CacheKey, dt, policy);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPengaduan.Text) || string.IsNullOrWhiteSpace(txtNim.Text))
            {
                MessageBox.Show("Pilih data pengaduan yang mau diubah dulu ya~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
                        else if (!(reader["tanggal_Perkiraan_selesai"] is DBNull) && dtpTglSelesai.Value.Date != Convert.ToDateTime(reader["tanggal_Perkiraan_selesai"]).Date)
                            isChanged = true;
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
                    cmd.Parameters.AddWithValue("@tanggal_Perkiraan_selesai", dtpTglSelesai.Value.Date);
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

        private void EnsureIndexesPengaduan()
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();

                var indexScript = @"
        -- Indeks untuk mempercepat filter berdasarkan nim
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'idx_Pengaduan_NIM'
              AND object_id = OBJECT_ID('dbo.Pengaduan')
        )
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Pengaduan_NIM
            ON dbo.Pengaduan(nim);
            PRINT 'Created idx_Pengaduan_NIM';
        END
        ELSE
            PRINT 'idx_Pengaduan_NIM sudah ada.';

        -- Indeks untuk mempercepat filter berdasarkan status_pengaduan
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'idx_Pengaduan_Status'
              AND object_id = OBJECT_ID('dbo.Pengaduan')
        )
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Pengaduan_Status
            ON dbo.Pengaduan(status_pengaduan);
            PRINT 'Created idx_Pengaduan_Status';
        END
        ELSE
            PRINT 'idx_Pengaduan_Status sudah ada.';

        -- Indeks gabungan untuk pencarian berdasarkan tanggal dan status
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'idx_Pengaduan_Tanggal_Status'
              AND object_id = OBJECT_ID('dbo.Pengaduan')
        )
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Pengaduan_Tanggal_Status
            ON dbo.Pengaduan(tanggal_pengaduan, status_pengaduan);
            PRINT 'Created idx_Pengaduan_Tanggal_Status';
        END
        ELSE
            PRINT 'idx_Pengaduan_Tanggal_Status sudah ada.';
        ";

                using (var cmd = new SqlCommand(indexScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnalyzeQuery(string sqlQuery, Dictionary<string, object> parameters = null)
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO");
                conn.Open();

                var wrapped = $@"
        SET STATISTICS IO ON;
        SET STATISTICS TIME ON;
        {sqlQuery}
        SET STATISTICS IO OFF;
        SET STATISTICS TIME OFF;";

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


        private void SearchDataPengaduan(string keyword)
        {

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SearchPengaduan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tangani keyword yang kosong atau null
                        if (string.IsNullOrWhiteSpace(keyword))
                            cmd.Parameters.AddWithValue("@keyword", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@keyword", keyword);

                        DataTable dt = new DataTable();
                        new SqlDataAdapter(cmd).Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal melakukan pencarian pengaduan: " + ex.Message,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string nim = txtNim.Text.Trim();
            string status = cmbStatus.SelectedItem?.ToString();

            string sqlQuery = "SELECT * FROM Pengaduan WHERE nim = @nim AND status_pengaduan = @status";

            var parameters = new Dictionary<string, object>
            {
                { "@nim", nim },
                { "@status", status }

            };

            AnalyzeQuery(sqlQuery, parameters);
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
                dtpTglSelesai.Value = row.Cells["tanggal_Perkiraan_selesai"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["tanggal_Perkiraan_selesai"].Value);
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

        private void BtnRefresh(object sender, EventArgs e)
        {
            LoadData();
            _cache.Remove(CacheKey);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
