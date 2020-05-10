using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    public partial class login : Form
    {
        loading load = new loading();
        public login()
        {
            InitializeComponent();
            this.Hide();
            load.ShowDialog();
            this.Show();

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            lbClose.BackColor = Color.Red;
        }

        private void lbClose_MouseLeave(object sender, EventArgs e)
        {
            lbClose.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void pictureFB_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).Size = new Size(50, 50);
        }

        private void pictureFB_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Size = new Size(40, 40);
        }

        private void tbUserName_Enter(object sender, EventArgs e)
        {
            if(tbUserName.Text== "User name")
            {
                tbUserName.Text = "";
                tbUserName.ForeColor = Color.WhiteSmoke;
            }
        }

        private void tbUserName_Leave(object sender, EventArgs e)
        {
            if (tbUserName.Text == "")
            {
                tbUserName.Text = "User name";
                tbUserName.ForeColor = Color.Gray;
            }
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {

        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {

        }

        private void btnLogin_MouseHover(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(100, 45, 99, 221);
            btnLogin.ForeColor = Color.WhiteSmoke;
           
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.CadetBlue;
            btnLogin.ForeColor = Color.FromArgb(100,0, 0, 64);
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Trim().ToLower() == "admin" && tbPassword.Text.Trim().ToLower() == "admin") ;
            FormMain formMain = new FormMain();
            this.Hide();
            formMain.ShowDialog();
            tbPassword.Text = "";
            tbUserName.Text = "User name"; tbUserName.ForeColor = Color.Gray;
            this.Show();
        }
    }
}
