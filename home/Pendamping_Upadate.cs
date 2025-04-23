using home.home;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace home
{
    public partial class Pendamping_Upadate : BaseForm
    {
        string connectionString = "Data Source=LAPTOP-CUMP4OII\\DANNY;Initial Catalog=layananPengaduan;Integrated Security=True";

        public Pendamping_Upadate()
        {
            InitializeComponent();
        }

        public void PendampingUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Pendamping";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void UpdatePendamping()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Pendamping SET nama = @nama, no_hp = @no_hp, email = @email 
                                 WHERE id_pendamping = @id_pendamping";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_pendamping", txtIdPendamping.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data pendamping berhasil diperbarui!");
                }
            }
        }

        private void DeletePendamping(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["id_pendamping"].Value.ToString();
                DialogResult result = MessageBox.Show("Yakin ingin menghapus pendamping ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Pendamping WHERE id_pendamping = @id_pendamping";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id_pendamping", id);
                            int affected = cmd.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Data pendamping berhasil dihapus!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data pendamping tidak ditemukan.");
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtIdPendamping.Text = row.Cells["id_pendamping"].Value.ToString();
                txtNama.Text = row.Cells["nama"].Value.ToString();
                txtNoHp.Text = row.Cells["no_hp"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value?.ToString();

                txtIdPendamping.ReadOnly = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtIdPendamping.Text == "" || txtNama.Text == "" || txtNoHp.Text == "")
            {
                MessageBox.Show("Harap isi semua data pendamping terlebih dahulu!");
                return;
            }

            try
            {
                UpdatePendamping();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan: " + ex.Message);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.Show();
        }
    }
}
