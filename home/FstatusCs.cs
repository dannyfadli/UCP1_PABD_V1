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
    public partial class FstatusCs : Form
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
        public FstatusCs()
        {
            InitializeComponent();
        }

        
private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Validasi input wajib
            if (comboIdPengaduan.SelectedItem == null || comboBoxStatus.SelectedItem == null || string.IsNullOrWhiteSpace(txtIdRiwayat.Text))
            {
                lblmsg.Text = "Pastikan semua field telah diisi.";
                return;
            }

            // Ambil data dari form
            string idPengaduan = ((KeyValuePair<string, string>)comboIdPengaduan.SelectedItem).Key;
            DateTime tanggalPerubahan = datePickerSelesai.Value;
            string statusPengaduan = comboBoxStatus.SelectedItem.ToString();
            string idRiwayat = txtIdRiwayat.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertRiwayatStatus", conn))
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_pengaduan", idPengaduan);
                        cmd.Parameters.AddWithValue("@id_riwayat", idRiwayat);
                        cmd.Parameters.AddWithValue("@status_baru", statusPengaduan);
                        cmd.Parameters.AddWithValue("@tanggal_perubahan", tanggalPerubahan);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblmsg.Text = "Data berhasil disimpan.";
                    // Jika ingin menggunakan MessageBox:
                    // MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();
                    string errorMessage = ex.Message;

                    // Deteksi error yang umum dari stored procedure (jika ada)
                    if (errorMessage.Contains("Riwayat dengan ID tersebut sudah ada"))
                    {
                        lblmsg.Text = "Riwayat dengan ID tersebut sudah ada!";
                    }
                    else if (errorMessage.Contains("Format ID riwayat tidak valid"))
                    {
                        lblmsg.Text = "Format ID riwayat tidak valid!";
                    }
                    else
                    {
                        lblmsg.Text = "Terjadi kesalahan saat menyimpan data.";
                    }
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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
    }
}
