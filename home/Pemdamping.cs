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
    public partial class Pemdamping : BaseForm
    {
        public Pemdamping()
        {
            InitializeComponent();
        }

        private void Pemdamping_Load(object sender, EventArgs e)
        {

        }

        private void btnDaftarPendamping_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Ganti form berikut sesuai kebutuhan
            Penadamping_Daftar form = new Penadamping_Daftar();
            form.Show();
        }

        private void btnUpdatePendamping_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Ganti form berikut sesuai kebutuhan
            Pendamping_Upadate form = new Pendamping_Upadate();
            form.Show();
        }
    }
}
