using home.home;
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
    public partial class Form2 : BaseForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.Hide();
            mhspngd form = new mhspngd();
            form.Show();
        }

        private void btnTindakan_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Tindakan form = new Tindakan();
            form.Show();
        }

        private void btnstatusn_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Form7 form = new Form7();
            form.Show();
        }

            private void btnPendamping_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            Pendamping form3 = new Pendamping();
            form3.Show();
        }

        private void btnstatusg_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            FstatusCs form3 = new FstatusCs();
            form3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide(); // Kalau mau sembunyikan Form2, bisa juga dihapus kalau nggak perlu
            FormLaporan form3 = new FormLaporan();
            form3.Show();
        }
    }
}
