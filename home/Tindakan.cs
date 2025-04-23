using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home
{
    public partial class Tindakan : Form
    {
        public Tindakan()
        {
            InitializeComponent();
        }

        private void btnTindakanDaftar_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Tindakan_Daftar form = new Tindakan_Daftar();
            form.Show();
        }

        private void btnTindakanUbah_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Tindakan_Ubah form = new Tindakan_Ubah();
            form.Show();
        }

        private void Tindakan_Load(object sender, EventArgs e)
        {

        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }
    }
}
