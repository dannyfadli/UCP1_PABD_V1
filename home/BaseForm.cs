using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home
{
    namespace home
    {
        public class BaseForm : Form
        {

            public BaseForm()
            {
                this.FormClosing += (s, e) => Application.Exit();
            }

            private void InitializeComponent()
            {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(814, 453);
            this.Name = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

            }

            private void BaseForm_Load(object sender, EventArgs e)
            {

            }
        }
    }
}
