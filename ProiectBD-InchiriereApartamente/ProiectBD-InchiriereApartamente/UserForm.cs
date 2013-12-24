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
    public partial class UserForm : Form
    {
        bool ocupat;
        int ok1 = 0;
        int ok2 = 0;
        string Username = "";
        BusinessLayer b = new BusinessLayer();
        public UserForm(string username)
        {
            
            InitializeComponent();
            Username = username;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            dataGridView1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            button1.BackColor = System.Drawing.Color.Red;
            button2.BackColor = System.Drawing.Color.Red;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }

        private void gestiune_camere(object sender, EventArgs e)
        {
            if (ok1 == 0)
            {
                button1.BackColor = System.Drawing.Color.Lime;
                ok1 = 1;
                button2.BackColor = System.Drawing.Color.Red;
                ok2 = 0;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                dataGridView1.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
                ocupat = false;
                filter_changed(sender, e);
                
            }
            else 
            {
                button1.BackColor = System.Drawing.Color.Red;
                ok1 = 0;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                dataGridView1.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                comboBox4.Visible = false;
            }
            
        }

        private void gestiune_camere2(object sender, EventArgs e)
        {
             if (ok2 == 0)
            {
                button2.BackColor = System.Drawing.Color.Lime;
                ok2 = 1;
                button1.BackColor = System.Drawing.Color.Red;
                ok1 = 0;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                dataGridView1.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;

                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
                ocupat = true;
                filter_changed(sender, e);
                
               
            }
            else if(ok2==1)
            {
                button2.BackColor = System.Drawing.Color.Red;
                ok2 = 0;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                dataGridView1.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                comboBox4.Visible = false;
            }
        }

        private void filter_changed(object sender, EventArgs e)
        {
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),ocupat);
            //combobox1
            int i = comboBox1.Items.Count;
            for(int j=0;j<i;j++)
            {
                if (comboBox1.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox1.Items[j].ToString(), comboBox1.SelectedItem.ToString()) != 0)
                    {
                        comboBox1.Items.Remove(comboBox1.Items[j]);
                        j--;
                        i--;
                    }
                }
                else
                {
                    comboBox1.Items.Remove(comboBox1.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;
            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox1.Items.Count; j++)
                    if (string.Compare(comboBox1.Items[j].ToString(), dataGridView1.Rows[f].Cells[1].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox1.Items.Add(dataGridView1.Rows[f].Cells[1].Value);
            }
            //combobox2
             i = comboBox2.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox2.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox2.Items[j].ToString(), comboBox2.SelectedItem.ToString()) != 0)
                    {
                        comboBox2.Items.Remove(comboBox2.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox2.Items.Remove(comboBox2.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;

           
            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox2.Items.Count; j++)
                    if (string.Compare(comboBox2.Items[j].ToString(), dataGridView1.Rows[f].Cells[2].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox2.Items.Add(dataGridView1.Rows[f].Cells[2].Value);
            }
            //combobox3
            i = comboBox3.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox3.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox3.Items[j].ToString(), comboBox3.SelectedItem.ToString()) != 0)
                    {
                        comboBox3.Items.Remove(comboBox3.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox3.Items.Remove(comboBox3.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;


           
            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox3.Items.Count; j++)
                    if (string.Compare(comboBox3.Items[j].ToString(), dataGridView1.Rows[f].Cells[8].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox3.Items.Add(dataGridView1.Rows[f].Cells[8].Value);
            }
            //combobox4
            i = comboBox4.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox4.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox4.Items[j].ToString(), comboBox4.SelectedItem.ToString()) != 0)
                    {
                        comboBox4.Items.Remove(comboBox4.Items[j]);
                        j--;
                        i--;
                    }
                }
                else
                {
                    comboBox4.Items.Remove(comboBox4.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;



            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox4.Items.Count; j++)
                    if (string.Compare(comboBox4.Items[j].ToString(), dataGridView1.Rows[f].Cells[6].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox4.Items.Add(dataGridView1.Rows[f].Cells[6].Value);
            }

        }

        private void details(object sender, DataGridViewCellEventArgs e)
        {
            if (ocupat == false)
            {
                DataTable u = new DataTable();
                u = b.read_table("dbo.Apartamente");
                int j = u.Rows.Count;
                for (int w = 0; w < j; w++)
                {
                    if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) == Int32.Parse(u.Rows[w][1].ToString()))
                    {
                        string f = u.Rows[w][2].ToString();
                        string g = u.Rows[w][3].ToString();
                        string h = u.Rows[w][4].ToString();
                        string i = u.Rows[w][5].ToString();
                        string p = u.Rows[w][6].ToString();
                        MessageBox.Show(" NUMAR CAMERE:" + f + " \n SUPRAFATA UTILA:" + g + " \n AN CONSTRUCTIE:" + h + " \n FACILITATI:" + i + " \n PRET INCHIRIERE:" + p);
                    }
                }

            }
            else
            {
                 DataTable u = new DataTable();
                u = b.read_table("dbo.Apartamente");
                int j = u.Rows.Count;
                for (int w = 0; w < j; w++)
                {
                    if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) == Int32.Parse(u.Rows[w][1].ToString()))
                    {

                        string f = u.Rows[w][2].ToString();
                        string g = u.Rows[w][3].ToString();
                        string h = u.Rows[w][4].ToString();
                        string i = u.Rows[w][5].ToString();
                        string p = u.Rows[w][6].ToString();
                        DataTable n = new DataTable();
                        n = b.read_table("dbo.Clienti");
                        int l = n.Rows.Count;
                        for (int y = 0; y < l; y++)
                        {
                            if (Int32.Parse(u.Rows[w][0].ToString()) == Int32.Parse(n.Rows[y][1].ToString()))
                            {
                                string x = n.Rows[y][2].ToString();
                                string v = n.Rows[y][3].ToString();
                                string z = n.Rows[y][4].ToString();
                                string c = n.Rows[y][5].ToString();
                                string inn = n.Rows[y][6].ToString();
                                string outt = n.Rows[y][7].ToString();
                                MessageBox.Show(" NUMAR CAMERE:" + f + " \n SUPRAFATA UTILA:" + g + " \n AN CONSTRUCTIE:" + h + " \n FACILITATI:" + i + " \n PRET INCHIRIERE:" + p + "\n\n   INCHIRIAT DE: \n\n NUME:" + x + " \n PRENUME" + v + "\n SERIE_NUMAR_CI" + z + "\n CNP" + c + " \n DATA INCHIRIERE" + inn + " \n INCHIRIAT PANA LA:" + outt);
                            }
                        }
                    }
                }
            }
        }

       
      
    }
}
