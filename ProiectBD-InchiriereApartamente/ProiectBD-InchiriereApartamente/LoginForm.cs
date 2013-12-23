using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectBD_InchiriereApartamente
{
    public partial class LoginForm : Form
    {
        public enum UserType : int
        {
            Administrator = 1,
            User
        }
        UserType userType;
        BusinessLayer bl = new BusinessLayer();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnAutentificare_Click(object sender, EventArgs e)
        {
            int ut = 0;
            if (tbParola.Text == "" || rtbUsername.Text == "")
            {
                MessageBox.Show("Va rog completati toate campurile!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbParola.Text = "";
                rtbUsername.Text = "";
                return;
            }
            bl.CheckIfValidLogin(rtbUsername.Text, tbParola.Text,ref ut);
            userType = (UserType)ut;
            if (userType == UserType.User || userType == UserType.Administrator)
            {
                bl.ReportLoginToAudit(rtbUsername.Text);
            }
            if (userType == UserType.User)
            {
                UserForm frm = new UserForm(rtbUsername.Text);
                this.Visible = false;
                frm.ShowDialog();
                frm.Close();
                this.Visible = true;
                return;
            }
            if (userType == UserType.Administrator)
            {
               AdministratorForm frm = new AdministratorForm(rtbUsername.Text);
                this.Visible = false;
                frm.ShowDialog();
                frm.Close();
                this.Visible = true;
                return;
            }
            else
            {
                MessageBox.Show("Numele userului sau parola este incorecta!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbParola.Text = "";
                rtbUsername.Text = "";
            }
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Introduceti username-ul si parola pentru a va loga! \n\n Apasati pe 'Schimba username sau parola' din meniu pentru a schimba credentialele de logare!", "Ajutor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void schimbaUsernameSauParolaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SchimbaUsernameSauParolaForm susp = new SchimbaUsernameSauParolaForm();
            this.Visible = false;
            susp.ShowDialog();
            susp.Close();
            this.Visible = true;
        }
    }
}
