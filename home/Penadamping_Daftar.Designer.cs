namespace home
{
    partial class Penadamping_Daftar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNoHP = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtIdPendamping = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNoHP
            // 
            this.txtNoHP.Location = new System.Drawing.Point(170, 132);
            this.txtNoHP.Name = "txtNoHP";
            this.txtNoHP.Size = new System.Drawing.Size(400, 22);
            this.txtNoHP.TabIndex = 52;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(686, 396);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 44);
            this.button3.TabIndex = 51;
            this.button3.Text = "Submit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 396);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 44);
            this.button2.TabIndex = 50;
            this.button2.Text = "Pevieos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 49;
            this.label8.Text = "Email*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 48;
            this.label7.Text = "no-hp*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Nama*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 43;
            this.label2.Text = "IdPendamping*";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(170, 180);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 22);
            this.txtEmail.TabIndex = 39;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(170, 84);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(400, 22);
            this.txtNama.TabIndex = 38;
            // 
            // txtIdPendamping
            // 
            this.txtIdPendamping.Location = new System.Drawing.Point(170, 43);
            this.txtIdPendamping.Name = "txtIdPendamping";
            this.txtIdPendamping.Size = new System.Drawing.Size(400, 22);
            this.txtIdPendamping.TabIndex = 37;
            this.txtIdPendamping.Enter += new System.EventHandler(this.txtIdPendamping_Enter);
            this.txtIdPendamping.Leave += new System.EventHandler(this.txtIdPendamping_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "=========DataPendamping=========";
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Location = new System.Drawing.Point(178, 220);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(64, 16);
            this.lblmsg.TabIndex = 53;
            this.lblmsg.Text = "Massage";
            // 
            // Penadamping_Daftar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.txtNoHP);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtIdPendamping);
            this.Controls.Add(this.label1);
            this.Name = "Penadamping_Daftar";
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Penadamping_Daftar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtIdPendamping;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoHP;
        private System.Windows.Forms.Label lblmsg;
    }
}