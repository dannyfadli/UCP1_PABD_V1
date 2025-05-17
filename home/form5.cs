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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE RiwayatStatusPengaduan SET id_pengaduan = @id_pengaduan, status_baru = @status, tanggal_perubahan = @tanggal_selesai WHERE id_riwayat = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtIdRiwayat.Text);
                    cmd.Parameters.AddWithValue("@id_pengaduan", comboIdPengaduan.Text);
                    cmd.Parameters.AddWithValue("@status", comboBoxStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@tanggal_selesai", datePickerSelesai.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data updated successfully.");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("No data found to update.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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
                            string query = "DELETE FROM RiwayatStatusPengaduan WHERE id_riwayat = @id";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", txtIdRiwayat.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data deleted successfully.");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("No data found to delete.");
                            }
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
    }
}
