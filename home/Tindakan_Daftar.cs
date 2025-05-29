using home.home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace home
{
    public partial class Tindakan_Daftar : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public Tindakan_Daftar()
        {
            InitializeComponent();
        }

        private void mhspngd_daftarcs_Load(object sender, EventArgs e)
        {
            LoadComboPengaduan();
            LoadComboPendamping();

            // Set data untuk ComboBox Status Tindakan
            comboStatusTindakan.Items.AddRange(new string[] { "Direncanakan", "Dilaksanakan", "Ditunda" });
            comboStatusTindakan.SelectedItem = "Direncanakan";
        }

        private void LoadComboPengaduan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_pengaduan, deskripsi FROM Pengaduan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, string> pengaduanDict = new Dictionary<string, string>();
                        while (reader.Read())
                        {
                            string id = reader["id_pengaduan"].ToString();
                            string deskripsi = reader["deskripsi"].ToString();
                            pengaduanDict[id] = $"{id} - {deskripsi.Substring(0, Math.Min(deskripsi.Length, 20))}...";
                        }

                        comboPengaduan.DataSource = new BindingSource(pengaduanDict, null);
                        comboPengaduan.DisplayMember = "Value";
                        comboPengaduan.ValueMember = "Key";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data pengaduan: " + ex.Message);
                }
            }
        }

        private void LoadComboPendamping()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id_pendamping, nama FROM Pendamping";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, string> pendampingDict = new Dictionary<string, string>();
                        pendampingDict.Add("", "- (Kosong) -"); // Optional jika pendamping bisa null

                        while (reader.Read())
                        {
                            string id = reader["id_pendamping"].ToString();
                            string nama = reader["nama"].ToString();
                            pendampingDict[id] = $"{id} - {nama}";
                        }

                        comboPendamping.DataSource = new BindingSource(pendampingDict, null);
                        comboPendamping.DisplayMember = "Value";
                        comboPendamping.ValueMember = "Key";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data pendamping: " + ex.Message);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string idTindakan = txtIdTindakan.Text;
            string idPengaduan = ((KeyValuePair<string, string>)comboPengaduan.SelectedItem).Key;
            string idPendamping = ((KeyValuePair<string, string>)comboPendamping.SelectedItem).Key;
            string deskripsi = txtDeskripsi.Text;
            DateTime tanggalTindakan = datePickerTindakan.Value;
            string statusTindakan = comboStatusTindakan.SelectedItem?.ToString();

            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AddTindakan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_tindakan", idTindakan);
                        cmd.Parameters.AddWithValue("@Id_Pengaduan", idPengaduan);

                        if (!string.IsNullOrEmpty(idPendamping))
                            cmd.Parameters.AddWithValue("@Id_Pendamping", idPendamping);
                        else
                            cmd.Parameters.AddWithValue("@Id_Pendamping", DBNull.Value);

                        cmd.Parameters.AddWithValue("@Deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@Tanggal_Tindakan", tanggalTindakan);
                        cmd.Parameters.AddWithValue("@Status_Tindakan", statusTindakan);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tindakan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Tindakan form = new Tindakan();
            form.Show();
        }
    }
}
