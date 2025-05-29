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

        /*private void UpdatePendamping()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
  ;

                using (SqlCommand cmd = new SqlCommand("sp_UpdatePendamping", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pendamping", txtIdPendamping.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data pendamping berhasil diperbarui!");
                }
            }
        }*/

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
                            SqlCommand cmd = new SqlCommand("sp_DeletePendamping", conn);
                            cmd.CommandType = CommandType.StoredProcedure;      
                            cmd.Parameters.AddWithValue("@id_pendamping", id);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data Pendamping berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadData();
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
            if (string.IsNullOrWhiteSpace(txtIdPendamping.Text) || string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Pilih data pendamping yang ingin diubah dulu ya~", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Ambil data lama berdasarkan ID
                    SqlCommand selectCmd = new SqlCommand("sp_GetPendampingById", conn);
                    selectCmd.CommandType = CommandType.StoredProcedure;
                    selectCmd.Parameters.AddWithValue("@id_pendamping", txtIdPendamping.Text.Trim());
                    SqlDataReader reader = selectCmd.ExecuteReader();

                    bool isChanged = false;

                    if (reader.Read())
                    {
                        if (!txtNama.Text.Equals(reader["nama"].ToString())) isChanged = true;
                        else if (!txtNoHp.Text.Equals(reader["no_hp"].ToString())) isChanged = true;
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
                        MessageBox.Show("Data pendamping tidak ditemukan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lanjut update jika ada perubahan
                    SqlCommand cmd = new SqlCommand("sp_UpdatePendamping", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_pendamping", txtIdPendamping.Text.Trim());
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

                    SqlParameter returnValue = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Pendamping berhasil di-update, nice~", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat update pendamping:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
