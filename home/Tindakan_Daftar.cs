using home.home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            string idTindakan = txtIdTindakan.Text.Trim();
            string idPengaduan = ((KeyValuePair<string, string>)comboPengaduan.SelectedItem).Key;
            string idPendamping = ((KeyValuePair<string, string>)comboPendamping.SelectedItem).Key;
            string deskripsi = txtDeskripsi.Text.Trim();
            DateTime tanggalTindakan = datePickerTindakan.Value.Date;
            string statusTindakan = comboStatusTindakan.SelectedItem?.ToString();

            // Validasi field wajib
            if (string.IsNullOrWhiteSpace(idTindakan) || string.IsNullOrWhiteSpace(idPengaduan) ||
                string.IsNullOrWhiteSpace(deskripsi) || string.IsNullOrWhiteSpace(statusTindakan))
            {
                lblmsg.Text = "Semua field wajib diisi!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_AddTindakan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_tindakan", idTindakan);
                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@id_pendamping",
                            string.IsNullOrWhiteSpace(idPendamping) ? DBNull.Value : (object)idPendamping);
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@tanggal_tindakan", tanggalTindakan);
                        cmd.Parameters.AddWithValue("@status_tindakan", statusTindakan);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblmsg.Text = "Data tindakan berhasil disimpan!";
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();
                    string error = ex.Message;

                    // Mapping error SQL ke UI
                    if (error.Contains("ID tindakan sudah digunakan"))
                        lblmsg.Text = "ID Tindakan sudah terdaftar.";
                    else if (error.Contains("Format ID tindakan tidak valid"))
                        lblmsg.Text = "Gunakan format ID: T0001, T0002, ...";
                    else if (error.Contains("ID pengaduan tidak ditemukan"))
                        lblmsg.Text = "ID Pengaduan tidak ditemukan.";
                    else if (error.Contains("ID pendamping tidak ditemukan"))
                        lblmsg.Text = "ID Pendamping tidak ditemukan.";
                    else if (error.Contains("Tanggal tindakan tidak boleh sebelum tanggal pengaduan"))
                        lblmsg.Text = "Tanggal tindakan tidak boleh lebih awal dari tanggal pengaduan.";
                    else if (error.Contains("Status tindakan tidak valid"))
                        lblmsg.Text = "Status hanya boleh: Direncanakan, Dilaksanakan, atau Ditunda.";
                    else if (error.Contains("wajib diisi"))
                        lblmsg.Text = "Semua data wajib diisi (kecuali pendamping).";
                    else
                        lblmsg.Text = "Gagal menyimpan data. Silakan cek kembali.";
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Tindakan form = new Tindakan();
            form.Show();
        }


        private void txtIdTindakan_Enter(object sender, EventArgs e)
        {
            if (txtIdTindakan.Text == "Contoh: T0001")
            {
                txtIdTindakan.Text = "";
                txtIdTindakan.ForeColor = Color.Black;
            }
        }

        private void txtIdTindakan_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdTindakan.Text))
            {
                txtIdTindakan.Text = "Contoh: T0001";
                txtIdTindakan.ForeColor = Color.Gray;
            }
        }


    }
}
