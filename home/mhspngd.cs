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
    public partial class mhspngd : BaseForm
    {
        public mhspngd()
        {
            InitializeComponent();
        }

        private void mhspngd_Load(object sender, EventArgs e)
        {

        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mhspngd_daftarcs daftarForm = new mhspngd_daftarcs();
            daftarForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            mhspngd_Updarte daftarForm = new mhspngd_Updarte();
            daftarForm.Show();
        }
    }
}