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
    public partial class Form1 : BaseForm

    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtuser.Text;
            string password = txtPassword.Text;

            // Validasi sederhana (gunakan database untuk sistem yang lebih aman)
            if (user == "Admin" && password == "12345")
            {
                MessageBox.Show("Login berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form2 form2 = new Form2();
                form2.Show();


                this.Hide(); // Sembunyikan Form1 (Login)
            }
            else
            {
                MessageBox.Show("Email atau password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }


    }
}
