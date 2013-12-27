using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProiectBD_InchiriereApartamente;
namespace ProiectBD_InchiriereApartamente
{
    public partial class UserForm : Form
    {
     
        bool ocupat;
        int ok1 = 0;
        int ok2 = 0;
        string Username = "";
        int option=0;
        BusinessLayer b = new BusinessLayer();
        public UserForm(string username)
        {
            
            InitializeComponent();
            dataGridView1.ReadOnly = true;
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
            button1.Visible = false;
            button2.Visible = false;
            comboBox5.Items.Add("Apartamente");
            comboBox5.Items.Add("Adrese");
            comboBox5.Items.Add("Clienti");
            label1.Visible = false;
            comboBox5.Text = "Apartamente";
            label1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            //
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;

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
                comboBox6.Visible = true;
                comboBox7.Visible = true;
                comboBox8.Visible = true;
                comboBox9.Visible = true;
                comboBox10.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
                ocupat = false;
                BusinessLayer.start= 0;
                filter_changed(sender, e);
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
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
                comboBox6.Visible = false;
                comboBox7.Visible = false;
                comboBox8.Visible = false;
                comboBox9.Visible = false;
                comboBox10.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
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
                comboBox6.Visible = true;
                comboBox7.Visible = true;
                comboBox8.Visible = true;
                comboBox9.Visible = true;
                comboBox10.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
                ocupat = true;
               BusinessLayer.start = 0;
                filter_changed(sender, e);

                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
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
                comboBox6.Visible = false;
                comboBox7.Visible = false;
                comboBox8.Visible = false;
                comboBox9.Visible = false;
                comboBox10.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
            }
        }

        private void filter_changed(object sender, EventArgs e)
        {
            
           b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(),ocupat,option,BusinessLayer.start,option);
            //combobox1
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, 1,BusinessLayer.start,0);
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
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, 0,BusinessLayer.start,0);
            //comboBox6
            i = comboBox6.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox6.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox6.Items[j].ToString(), comboBox6.SelectedItem.ToString()) != 0)
                    {
                        comboBox6.Items.Remove(comboBox6.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox6.Items.Remove(comboBox6.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;



            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox6.Items.Count; j++)
                    if (string.Compare(comboBox6.Items[j].ToString(), dataGridView1.Rows[f].Cells[2].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox6.Items.Add(dataGridView1.Rows[f].Cells[2].Value);
            }
            //comboBox7
            i = comboBox7.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox7.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox7.Items[j].ToString(), comboBox7.SelectedItem.ToString()) != 0)
                    {
                        comboBox7.Items.Remove(comboBox7.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox7.Items.Remove(comboBox7.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;



            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox7.Items.Count; j++)
                    if (string.Compare(comboBox7.Items[j].ToString(), dataGridView1.Rows[f].Cells[3].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox7.Items.Add(dataGridView1.Rows[f].Cells[3].Value);
            }

            //comboBox8
            i = comboBox8.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox8.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox8.Items[j].ToString(), comboBox8.SelectedItem.ToString()) != 0)
                    {
                        comboBox8.Items.Remove(comboBox8.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox8.Items.Remove(comboBox8.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;



            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox8.Items.Count; j++)
                    if (string.Compare(comboBox8.Items[j].ToString(), dataGridView1.Rows[f].Cells[4].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox8.Items.Add(dataGridView1.Rows[f].Cells[4].Value);
            }
            //combobox9
            i = comboBox9.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox9.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox9.Items[j].ToString(), comboBox9.SelectedItem.ToString()) != 0)
                    {
                        comboBox9.Items.Remove(comboBox9.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox9.Items.Remove(comboBox9.Items[j]);
                    j--; i--;
                }

            }
            
                int q = 1;
                for (int j = 0; j < comboBox9.Items.Count; j++)
                    if (string.Compare(comboBox9.Items[j].ToString(), "<") == 0)
                        q = 0;
                if (q == 1)
                    comboBox9.Items.Add("<");
            
           
                 q = 1;
                for (int j = 0; j < comboBox9.Items.Count; j++)
                    if (string.Compare(comboBox9.Items[j].ToString(), ">") == 0)
                        q = 0;
                if (q == 1)
                    comboBox9.Items.Add(">");
            
           
                 q = 1;
                for (int j = 0; j < comboBox9.Items.Count; j++)
                    if (string.Compare(comboBox9.Items[j].ToString(), "=") == 0)
                        q = 0;
                if (q == 1)
                    comboBox9.Items.Add("=");
      

            //comboBox10
            i = comboBox10.Items.Count;
            for (int j = 0; j < i; j++)
            {
                if (comboBox10.SelectedIndex > -1)
                {
                    if (string.Compare(comboBox10.Items[j].ToString(), comboBox10.SelectedItem.ToString()) != 0)
                    {
                        comboBox10.Items.Remove(comboBox10.Items[j]);
                        j--; i--;
                    }
                }
                else
                {
                    comboBox10.Items.Remove(comboBox10.Items[j]);
                    j--; i--;
                }

            }


            i = dataGridView1.Rows.Count;



            for (int f = 0; f < i; f++)
            {
                int a = 1;
                for (int j = 0; j < comboBox10.Items.Count; j++)
                    if (string.Compare(comboBox10.Items[j].ToString(), dataGridView1.Rows[f].Cells[6].Value.ToString()) == 0)
                        a = 0;
                if (a == 1)
                    comboBox10.Items.Add(dataGridView1.Rows[f].Cells[6].Value);
            }

            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(), comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, option,BusinessLayer.start,1);
        }

        private void details(object sender, DataGridViewCellEventArgs e)
        {
            if (option == 1)
            {
                if (ocupat == false)
                {
                    DataTable u = new DataTable();
                    u = b.read_table("dbo.Apartamente");
                    int j = u.Rows.Count;
                    DataTable y = new DataTable();
                    DataView p = new DataView(u);
                    for (int w = 0; w < j; w++)
                    {
                        if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) == Int32.Parse(u.Rows[w][1].ToString()))
                        {

                            p.RowFilter = string.Format("Convert(ID_ADRESA,'System.Int32') = Convert('{0}','System.Int32')", Int32.Parse(u.Rows[w][1].ToString()));
                        }
                    }
                    dataGridView2.DataSource = p;
                    dataGridView2.Visible = true;
                    dataGridView3.Visible = false;
                }
                else
                {
                    DataTable u = new DataTable();
                    u = b.read_table("dbo.Apartamente");
                    int j = u.Rows.Count;
                    DataTable s = new DataTable();
                    for (int w = 0; w < j; w++)
                    {
                        if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) == Int32.Parse(u.Rows[w][1].ToString()))
                        {


                            DataTable n = new DataTable();
                            n = b.read_table("dbo.Clienti");
                            int l = n.Rows.Count;

                            DataView p = new DataView(u);
                            p.RowFilter = string.Format("Convert(ID_ADRESA,'System.Int32') = Convert('{0}','System.Int32')", Int32.Parse(u.Rows[w][1].ToString()));
                            dataGridView2.DataSource = p;
                            l = Int32.Parse(u.Rows[w][0].ToString());
                            DataView r = new DataView(n);
                            r.RowFilter = string.Format("Convert(ID_APARTAMENT,'System.Int32') = Convert('{0}','System.Int32')", l);
                            dataGridView3.DataSource = r;
                            dataGridView2.Visible = true;
                            dataGridView3.Visible = true;
                        }
                    }

                }
            }
            if (option == 0)
            {
                if (ocupat == false)
                {
                    DataTable u = new DataTable();
                    u = b.read_table("dbo.Adresa");
                    int j = u.Rows.Count;
                    DataTable y = new DataTable();
                    DataView p = new DataView(u);
                    for (int w = 0; w < j; w++)
                    {
                        if (Int32.Parse(dataGridView1.SelectedCells[1].Value.ToString()) == Int32.Parse(u.Rows[w][0].ToString()))
                        {

                            p.RowFilter = string.Format("Convert(ID_ADRESA,'System.Int32') = Convert('{0}','System.Int32')", Int32.Parse(u.Rows[w][0].ToString()));
                        }
                    }
                    dataGridView2.DataSource = p;
                    dataGridView2.Visible = true;
                    dataGridView3.Visible = false;
                }
                else
                {
                    DataTable u = new DataTable();
                    u = b.read_table("dbo.Adresa");
                    int j = u.Rows.Count;
                    DataTable s = new DataTable();
                    for (int w = 0; w < j; w++)
                    {
                        if (Int32.Parse(dataGridView1.SelectedCells[1].Value.ToString()) == Int32.Parse(u.Rows[w][0].ToString()))
                        {


                            DataTable n = new DataTable();
                            n = b.read_table("dbo.Clienti");
                            int l = n.Rows.Count;

                            DataView p = new DataView(u);
                            p.RowFilter = string.Format("Convert(ID_ADRESA,'System.Int32') = Convert('{0}','System.Int32')", Int32.Parse(u.Rows[w][0].ToString()));
                            dataGridView2.DataSource = p;
                            l = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                            DataView r = new DataView(n);
                            r.RowFilter = string.Format("Convert(ID_APARTAMENT,'System.Int32') = Convert('{0}','System.Int32')", l);
                            dataGridView3.DataSource = r;
                            dataGridView2.Visible = true;
                            dataGridView3.Visible = true;
                        }
                    }

                }
            }
        }

        private void sursa_date(object sender, EventArgs e)
        {
            DataTable tabel_ales=new DataTable();

            if (string.Compare(comboBox5.Text.ToString(), "Apartamente") == 0)
            {
                option = 0;
                label1.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                filter_changed(sender, e);
               // b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, option,BusinessLayer.start,0);
            }

            if (string.Compare(comboBox5.Text.ToString(), "Adrese") == 0)
            {
                option = 1;
                label1.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                filter_changed(sender, e);
               // b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, option,BusinessLayer.start,0);
            }
            if (string.Compare(comboBox5.Text.ToString(), "Clienti") == 0)
            {
                option = 3;
                 DataTable n = new DataTable();
                 n = b.read_table("dbo.Clienti");
                 dataGridView1.DataSource = n;
                 dataGridView1.Visible = true;
                 label1.Visible = false;
                 button1.Visible = false;
                 button2.Visible = false;
                 comboBox6.Visible = false;
                 comboBox7.Visible = false;
                 comboBox8.Visible = false;
                 comboBox9.Visible = false;
                 comboBox10.Visible = false;
                 //
                 label9.Visible = false;
                 label10.Visible = false;
                 label11.Visible = false;
                 label12.Visible = false;
                 dataGridView2.Visible = false;
                 dataGridView3.Visible = false;
                 label2.Visible = false;
                 label3.Visible = false;
                 label4.Visible = false;
                 label5.Visible = false;
                 label6.Visible = false;
                 //dataGridView1.Visible = false;
                 comboBox1.Visible = false;
                 comboBox2.Visible = false;
                 comboBox3.Visible = false;
                 comboBox4.Visible = false;
                 button1.Visible = false;
                 button2.Visible = false;
                 label1.Visible = false;
            }
        }

       
      
    }
}
