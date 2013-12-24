using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ProiectBD_InchiriereApartamente
{
    public partial class SchimbaUsernameSauParolaForm : Form
    {
        public enum UserType : int
        {
            Administrator = 1,
            User
        }

        BusinessLayer bl = new BusinessLayer();

        public SchimbaUsernameSauParolaForm()
        {
            InitializeComponent();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\tIntroduceti username-ul si parola veche.Daca vreti sa schimbati numai parola contului lasati casuta cu noul username goala.Introduceti noua parola si apasati 'Schimba'.\n\n\tDaca vreti sa schimbati numai username-ul,lasati campul pentru noua parola gol,introduceti noul username si apasati 'Schimba'");
        }

        private void btnSchimba_Click(object sender, EventArgs e)
        {
            int ut = 0;
            int account_id = 0;

            if (rtbUsernameVechi.Text == "" || tbParolaVeche.Text == "" || (tbParolaNoua.Text == "" && tbRepParolaNoua.Text == "" && rtbUsernameNou.Text == ""))
            {
                if (MessageBox.Show("Casuta necompletata,incercati inca odata", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    this.Close();
                return;
            }
            //Verific daca exista acest cont
            bl.CheckIfValidLogin(rtbUsernameVechi.Text, tbParolaVeche.Text, ref ut);
            UserType userType = (UserType)ut;
            if (userType != UserType.User && userType != UserType.Administrator)
            {
                MessageBox.Show("Acest cont nu exista!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {//exista acel user,continuam
                string account_username = "";
                string account_password = "";
                if (rtbUsernameNou.Text != "")
                {
                    //Verific ca noul cont sa nu existe deja
                    bool ifexists = false;
                    ifexists = bl.CheckIfUsernameTaken(rtbUsernameNou.Text);
                    if (ifexists == true)
                    {
                        MessageBox.Show("Acest username este luat,incercati din nou!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    account_username = rtbUsernameNou.Text;
                }
                else account_username = rtbUsernameVechi.Text;
                if (tbParolaNoua.Text != "")
                {
                    if (tbParolaNoua.Text != tbRepParolaNoua.Text)
                    {
                        MessageBox.Show("Parola noua introdusa gresit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    account_password = tbParolaNoua.Text;
                }
                else account_password = tbParolaVeche.Text;
                int affectedRows;
                SqlParameter[] spc = new SqlParameter[5];
                for (int i = 0; i < spc.Length; i++)
                    spc[i] = new SqlParameter();
                string newpassword = bl.Encrypt(account_password);

                account_id = 0;
                int role_id = 0;

                bl.RetrieveAccountDetails(rtbUsernameVechi.Text, ref account_id, ref role_id);

                spc[0].ParameterName = "@ID_CONT";
                spc[0].SqlDbType = SqlDbType.Int;
                spc[0].Value = account_id;
                spc[0].Direction = ParameterDirection.Input;
                spc[1].ParameterName = "@ID_ROL";
                spc[1].SqlDbType = SqlDbType.Int;
                spc[1].Value = role_id;
                spc[1].Direction = ParameterDirection.Input;
                spc[2].ParameterName = "@NUME_USER";
                spc[2].SqlDbType = SqlDbType.VarChar;
                spc[2].Value = account_username;
                spc[2].Direction = ParameterDirection.Input;
                spc[3].ParameterName = "@PAROLA";
                spc[3].SqlDbType = SqlDbType.VarChar;
                spc[3].Value = newpassword;
                spc[3].Direction = ParameterDirection.Input;
                spc[4].ParameterName = "@username_context";
                spc[4].SqlDbType = SqlDbType.VarChar;
                spc[4].Value = account_username;
                spc[4].Direction = ParameterDirection.Input;

                affectedRows = bl.DataBaseOperation("ModificaCont", spc);
                if (MessageBox.Show("Cont modificat cu succes","",MessageBoxButtons.OK,MessageBoxIcon.Information) == DialogResult.OK)
                    this.Close();
            }
        }

        private void SchimbaUsernameSauParolaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
