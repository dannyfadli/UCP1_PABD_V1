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
    public partial class form5 : Form
    {

        private string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM RiwayatStatusPengaduan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
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
                try
                {
                    conn.Open();

                    // Ambil data lama berdasarkan id_riwayat
                    SqlCommand selectCmd = new SqlCommand("sp_GetRiwayatStatusById", conn);
                    selectCmd.CommandType = CommandType.StoredProcedure;
                    selectCmd.Parameters.AddWithValue("@id_riwayat", txtIdRiwayat.Text.Trim());

                    SqlDataReader reader = selectCmd.ExecuteReader();
                    bool isChanged = false;

                    if (reader.Read())
                    {
                        if (!comboIdPengaduan.Text.Equals(reader["id_pengaduan"].ToString())) isChanged = true;
                        else if (!comboBoxStatus.SelectedItem.ToString().Equals(reader["status_baru"].ToString())) isChanged = true;
                        else if (datePickerSelesai.Value != Convert.ToDateTime(reader["tanggal_perubahan"])) isChanged = true;

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
                        MessageBox.Show("Data riwayat status tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lanjut update
                    SqlCommand cmd = new SqlCommand("sp_UpdateRiwayatStatusPengaduan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_riwayat", txtIdRiwayat.Text.Trim());
                    cmd.Parameters.AddWithValue("@id_pengaduan", comboIdPengaduan.Text.Trim());
                    cmd.Parameters.AddWithValue("@status_baru", comboBoxStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@tanggal_perubahan", datePickerSelesai.Value);

                    SqlParameter returnValue = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data riwayar status  berhasil di-update, nice~", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat update riwayat status:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["id_riwayat"].Value.ToString();
                DialogResult result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = new SqlCommand("sp_DeleteRiwayatStatusPengaduan", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_riwayat", txtIdRiwayat.Text.Trim());

                            cmd.ExecuteNonQuery();

                            // Kalau tidak ada exception, berarti berhasil
                            MessageBox.Show("Data berhasil dihapus.");
                            LoadData();
                        }
                        catch (SqlException ex)
                        {
                            // Tangkap pesan dari RAISERROR di SQL Server
                            MessageBox.Show("Gagal menghapus data: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
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
    }
}
