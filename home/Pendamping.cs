using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Caching;
using home.home;


namespace home
{
    public partial class Pendamping : BaseForm
    {
        public Pendamping()
        {
            InitializeComponent();
        }

        private void Pendamping_Load(object sender, EventArgs e)
        {

        }
        private void btnTambah1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Penadamping_Daftar form3 = new Penadamping_Daftar();
            form3.Show();
        }


        private void btnTambah2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Pendamping_Upadate form3 = new Pendamping_Upadate();
            form3.Show();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }


    }
}
