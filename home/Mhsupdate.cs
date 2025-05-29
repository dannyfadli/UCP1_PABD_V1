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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Runtime.Caching;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


namespace home
{
    public partial class Mhsupdate : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _Policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) // Cache selama 10 menit
        };
        private const string CacheKey = "MahasiswaData";

        public Mhsupdate()
        {
            InitializeComponent();
            
        }

        public void Mhsupdate_Load(object sender, EventArgs e)
        {

            LoadData();

            // Hindari dobel isi
            if (comboBoxFakultas.Items.Count == 0)
            {
                comboBoxFakultas.Items.AddRange(new string[] { "Teknik", "Ekonomi dan Bisnis", "Hukum", "Ilmu Sosial dan Ilmu Politik", "Agama Islam", "Pendidikan Bahasa", "Kedokteran dan Ilmu Kesehatan", "Pertanian", "Vokasi" });
            }

            // Tambah event handler hanya sekali
            comboBoxFakultas.SelectedIndexChanged -= comboBoxFakultas_SelectedIndexChanged;
            comboBoxFakultas.SelectedIndexChanged += comboBoxFakultas_SelectedIndexChanged;

            
        }

        private void EnsureIndexs()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var indexScript = @"
                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Mahasiswa_FakultasProdi'
                      AND object_id = OBJECT_ID('dbo.Mahasiswa')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Mahasiswa_FakultasProdi
                    ON dbo.Mahasiswa(fakultas, prodi);
                    PRINT 'Created idx_Mahasiswa_FakultasProdi';
                END
                ELSE
                    PRINT 'idx_Mahasiswa_FakultasProdi sudah ada.';

                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Mahasiswa_Email_Include'
                      AND object_id = OBJECT_ID('dbo.Mahasiswa')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Mahasiswa_Email_Include
                    ON dbo.Mahasiswa(email)
                    INCLUDE(nama, no_hp);
                    PRINT 'Created idx_Mahasiswa_Email_Include';
                END
                ELSE
                    PRINT 'idx_Mahasiswa_Email_Include sudah ada.';

                IF NOT EXISTS (
                    SELECT 1 
                    FROM sys.indexes 
                    WHERE name = 'idx_Mahasiswa_JK_Fakultas'
                      AND object_id = OBJECT_ID('dbo.Mahasiswa')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Mahasiswa_JK_Fakultas
                    ON dbo.Mahasiswa(jenis_kelamin, fakultas);
                    PRINT 'Created idx_Mahasiswa_JK_Fakultas';
                END
                ELSE
                    PRINT 'idx_Mahasiswa_JK_Fakultas sudah ada.';
                ";

                using (var cmd = new SqlCommand(indexScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
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


         private void UpdateMahasiswa()
         {
             using (SqlConnection conn = new SqlConnection(connectionString))
             {


                 using (SqlCommand cmd = new SqlCommand("sp_UpdateMahasiswa", conn))
                 {
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@nim", txtNim.Text);
                     cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                     cmd.Parameters.AddWithValue("@jenis_kelamin", comboBox1.SelectedItem.ToString());
                     cmd.Parameters.AddWithValue("@Fakultas", comboBoxFakultas.Text);
                     cmd.Parameters.AddWithValue("@Prodi", comboBoxProdi.Text);
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

                DialogResult result = MessageBox.Show(
                    $"Yakin ingin menghapus mahasiswa dengan NIM: {nim}?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = new SqlCommand("sp_DeleteMahasiswa", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@nim", nim);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data Mahasiswa berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadData();
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
            DataTable dt;
            if (_cache.Contains(CacheKey))
            {
                dt = _cache.Get(CacheKey) as DataTable;
            }
            else
            {
                dt = new DataTable();
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var query = @"
                    SELECT nim, nama, email, no_hp, fakultas, prodi, jenis_kelamin
                    FROM dbo.Mahasiswa"; // Tanpa filter, tapi tetap gunakan kolom yang ada di index

                    var da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                _cache.Add(CacheKey, dt, _Policy);
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtNim.Text == "" || txtNama.Text == "")
            {
                MessageBox.Show("Pilih data yang mau diubah dulu dong~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Cek apakah ada perubahan data
                    SqlCommand selectCmd = new SqlCommand("sp_GetMahasiswaByNIM", conn);
                    selectCmd.CommandType = CommandType.StoredProcedure;
                    selectCmd.Parameters.AddWithValue("@nim", txtNim.Text);
                    SqlDataReader reader = selectCmd.ExecuteReader();

                    bool isChanged = false;

                    if (reader.Read())
                    {
                        if (!txtNama.Text.Equals(reader["nama"].ToString())) isChanged = true;
                        else if (!comboBox1.SelectedItem.ToString().Equals(reader["jenis_kelamin"].ToString())) isChanged = true;
                        else if (!comboBoxFakultas.SelectedItem.ToString().Equals(reader["fakultas"].ToString())) isChanged = true;
                        else if (!comboBoxProdi.SelectedItem.ToString().Equals(reader["prodi"].ToString())) isChanged = true;
                        else if (!txtNoHP.Text.Equals(reader["no_hp"].ToString())) isChanged = true;
                        else if (!txtEmail.Text.Equals(reader["email"].ToString())) isChanged = true;

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
                        MessageBox.Show("Data mahasiswa tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Jika ada perubahan, lanjut update
                    SqlCommand cmd = new SqlCommand("sp_UpdateMahasiswa", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nim", txtNim.Text.Trim());
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@jenis_kelamin", comboBox1.SelectedItem?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@fakultas", comboBoxFakultas.SelectedItem?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@prodi", comboBoxProdi.SelectedItem?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@no_hp", string.IsNullOrWhiteSpace(txtNoHP.Text) ? (object)DBNull.Value : txtNoHP.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data mahasiswa berhasil di-update, nice~", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

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
                comboBoxFakultas.SelectedItem = row.Cells["fakultas"].Value.ToString();
                comboBoxProdi.SelectedItem = row.Cells["prodi"].Value.ToString();
                txtNoHP.Text = row.Cells["no_hp"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();

                txtNim.ReadOnly = true; // Set NIM menjadi read-only agar tidak bisa diubah
            }
        }


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

        private void btninput(object sender, EventArgs e)
        {
            using (var openfile = new OpenFileDialog())
            {
                openfile.Filter = "Excel Files|*.xlsx;*.xlsm;*.xls";
                if (openfile.ShowDialog() == DialogResult.OK)
                    privewData(openfile.FileName);
            }
        }

        private void privewData(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    // Ambil header dari baris pertama
                    IRow headerRow = sheet.GetRow(0);
                    foreach (ICell cell in headerRow.Cells)
                        dt.Columns.Add(cell.ToString());

                    // Ambil data dari baris berikutnya
                    
                    
                        // Baris data
                        for (int i = 1; i <= sheet.LastRowNum; i++)
                        {
                            IRow dataRow = sheet.GetRow(i);
                            DataRow newRow = dt.NewRow();
                            int cellIndex = 0;
                            foreach (var cell in dataRow.Cells)
                            {
                                newRow[cellIndex++] = cell.ToString();
                            }
                            dt.Rows.Add(newRow);
                        }
                        PreviewForm previewForm = new PreviewForm(dt);
                        previewForm.ShowDialog(); 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat membaca file Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            // Ambil input fakultas dan prodi (opsional)
            string fakultas = comboBoxFakultas.SelectedItem?.ToString();
            string prodi = comboBoxProdi.SelectedItem?.ToString();

            // Bangun query analisis dengan filter fakultas dan prodi jika ada
            string heavyQuery = @"
        SELECT email, nama, no_hp 
        FROM dbo.Mahasiswa 
        WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(fakultas))
                heavyQuery += $" AND fakultas = '{fakultas}'";

            if (!string.IsNullOrWhiteSpace(prodi))
                heavyQuery += $" AND prodi = '{prodi}'";

            AnalyzeQuery(heavyQuery);
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnRefresh(object sender, EventArgs e)
        {
            LoadData();
            _cache.Remove(CacheKey);
        }

    }
}
