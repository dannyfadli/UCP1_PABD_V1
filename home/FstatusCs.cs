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
using System.Windows.Forms.VisualStyles;

namespace home
{
    public partial class FstatusCs : BaseForm
    {
        //string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        Koneksi kn = new Koneksi();
        string strKonek = "";

        public FstatusCs()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }


        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (comboIdPengaduan.SelectedItem == null ||
                comboBoxStatus.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtIdRiwayat.Text))
            {
                lblmsg.Text = "Pastikan semua field telah diisi.";
                return;
            }

            // Ambil data input dari form
            string idRiwayat = txtIdRiwayat.Text.Trim();
            var selectedItem = (KeyValuePair<string, string>)comboIdPengaduan.SelectedItem;
            string idPengaduan = selectedItem.Key;
            string statusPengaduan = comboBoxStatus.SelectedItem.ToString();
            DateTime tanggalPerubahan = datePickerSelesai.Value;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertRiwayatStatus", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_riwayat", idRiwayat);
                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@status_baru", statusPengaduan);
                        cmd.Parameters.AddWithValue("@tanggal_perubahan", tanggalPerubahan);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblmsg.Text = "Riwayat status berhasil ditambahkan!";
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    string msg = ex.Message;

                    if (msg.Contains("ID Riwayat sudah digunakan"))
                        lblmsg.Text = "Riwayat dengan ID tersebut sudah ada!";
                    else if (msg.Contains("Format ID Riwayat tidak valid"))
                        lblmsg.Text = "Format ID Riwayat tidak valid!";
                    else if (msg.Contains("Status hanya boleh"))
                        lblmsg.Text = "Status hanya boleh: Masuk, Diproses, atau Selesai.";
                    else if (msg.Contains("ID Pengaduan tidak ditemukan"))
                        lblmsg.Text = "ID Pengaduan tidak ditemukan!";
                    else if (msg.Contains("Tanggal perubahan tidak boleh"))
                        lblmsg.Text = "Tanggal perubahan tidak boleh di masa depan!";
                    else
                        lblmsg.Text = "Terjadi kesalahan saat menyimpan data.";
                }
            }
        }


        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }

        private void FstatusCs_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            { 
               
                try
                {
                        conn.Open();
                    string query = "select id_pengaduan, nim from Pengaduan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, string> idPengaduanList = new Dictionary<string, string>();
                        while (reader.Read())
                        {
                            string idPengaduan = reader["id_pengaduan"].ToString();
                            string nim = reader["nim"].ToString();
                            idPengaduanList[idPengaduan] = $"{idPengaduan} - {nim}";
                        }
                        comboIdPengaduan.DataSource = new BindingSource(idPengaduanList, null);
                        comboIdPengaduan.DisplayMember = "Value";
                        comboIdPengaduan.ValueMember = "Key";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                }
            }
        }
        private void txtIdRiwayat_Enter(object sender, EventArgs e)
        {
            if (txtIdRiwayat.Text == "Contoh: R0001")
            {
                txtIdRiwayat.Text = "";
                txtIdRiwayat.ForeColor = Color.Black;
            }
        }

        private void txtIdRiwayat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdRiwayat.Text))
            {
                txtIdRiwayat.Text = "Contoh: R0001";
                txtIdRiwayat.ForeColor = Color.Gray;
            }
        }


        private void txtIdRiwayat_TextChanged(object sender, EventArgs e)
        {
        
     

        }
    }

}
