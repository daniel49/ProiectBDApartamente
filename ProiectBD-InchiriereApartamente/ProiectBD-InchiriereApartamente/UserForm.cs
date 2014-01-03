using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

//using ProiectBD_InchiriereApartamente;

namespace ProiectBD_InchiriereApartamente
{
    public partial class UserForm : Form
    {
        DataView dvClienti = new DataView();
        int ClientSelectat = -1;
        bool ocupat;    //false - camere libere , true - camere ocupate
        int ok1 = 0;    //ok1 - 1-camere libere verde 
        int ok2 = 0;    //ok2 - 1-camere ocuoate
        string Username = "";
        int option=0;      // 0 - adrese 1 - apartamente 2 - clienti
        BusinessLayer b = new BusinessLayer();
        public UserForm(string username)
        {
            
            InitializeComponent();

            comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
            label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;


            button4.Visible = false;
            dataGridView1.ReadOnly = true;
            Username = username;

            label13.Visible = false;
            button3.Visible = false;
            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = false;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = false;

            //ascund tot ce tine de cautare(ramane decat butoanele de alegere apartamente ocupate sau libere)

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
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
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

            DataTable dt = new DataTable();
            dt = b.read_table("Clienti");
            dt.TableName = "Clienti";
            dvClienti.Table = dt;
        }


        private void gestiune_camere(object sender, EventArgs e)    //camere libere - button click event
        {
            ClientSelectat = -1;
            if (ok1 == 0)
            {
                button4.Visible = true;
                button1.BackColor = System.Drawing.Color.Purple;
                ok1 = 1;
                button2.BackColor = System.Drawing.Color.Transparent;
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
                //reseteaza filtrele cand trece de la apartamente libere la ocupate
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
                ocupat = false;                 // sa stiu cand sunt in camere libere si cand in camere ocupate
                b.start= 0;         //nu este aplicat nici un filtru momentan
                filter_changed(sender, e);
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
            }
            else 
            {
                button1.BackColor = System.Drawing.Color.Transparent;
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

        private void gestiune_camere2(object sender, EventArgs e)   //camere ocupate
        {
            ClientSelectat = -1;
             if (ok2 == 0)
            {
                button4.Visible = true;
                button2.BackColor = System.Drawing.Color.Purple;
                ok2 = 1;
                button1.BackColor = System.Drawing.Color.Transparent;
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
                b.start = 0; //nu este aplicat nici un filtru momentan
                filter_changed(sender, e);

                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
            }
            else if(ok2==1)
            {
                button2.BackColor = System.Drawing.Color.Transparent;
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

        private void filter_changed(object sender, EventArgs e)     //SelectedIndexChanged pt ComboBoxurile de filtrare
        {
            
           b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(),ocupat,option,b.start,option);
            //combobox1
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, 1,b.start,0);
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
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(),comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, 0,b.start,0);
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

            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(), comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, option,b.start,1);
        }

        private void details(object sender, DataGridViewCellEventArgs e)    //apasa pe o celula si incarca clientii / etc
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

        private void sursa_date(object sender, EventArgs e)     //alege sursa de date
        {
            DataTable tabel_ales=new DataTable();

            if (option == 2)    //trec de la sursa de date Clienti la oricare alta
            {
                label2.Visible = false;
                dataGridView1.Visible = false;
                if (ok1 == 1 && ok2 == 0)
                    ok1 = 0;
                if (ok2 == 1 && ok1 == 0)
                    ok2 = 0;
            }

            if (string.Compare(comboBox5.Text.ToString(), "Apartamente") == 0)
            {
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;

                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare client
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = false;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = false;
                }

                ClientSelectat = -1;
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

                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;


                ClientSelectat = -1;
                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare client
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = false;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = false;
                }
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
                button1.BackColor = Color.Transparent;
                button2.BackColor = Color.Transparent;

                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = true;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = true;

                button4.Visible = true;
                label2.Visible = true;
                ClientSelectat = -1;
                option = 2;
                // DataTable n = new DataTable();
                // n = b.read_table("dbo.Clienti");
                 dataGridView1.DataSource = dvClienti;
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ajutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"
        Apasati pe (Apartamente....) 'LIBERE' pentru a vizualiza apartamentele libere,sau adresa apartamentelor libere,altfel pe 'OCUPATE'.
        Alegeti Sursa de Date din casuta dreapta-sus.
        Filtrarea se face in timp real,de indata ce se introduce text in casutele de filtrare.Apasati pe Sterge filtrele pentru a inlatura filtrele 
dinaintea apasarii butonului de stergere a filtrelor.
        Puteti sterge decat clienti,celelalte tabele putand fi decat vizualizate.
        Pentru a sterge un client selectati-l printr-un click din tabel,apoi din bara de meniu,apasati 'Sterge Client'.Pentru a introduce un client
apasati pe 'Adauga Client' din bara de meniu.Casutele care apar trebuie completate.Apoi dati click pe butonul 'Adauga client'.

    OBS : toate campurile trebuiesc completate iar data inregistrarii clientului se va genera automat.

    !!! : Daca va aflati in Apartamente sau Adrese puteti da click pe o celula din primul tabel pentru a vedea detaliile despre acel rand
pe care ati dat click
");
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)                //putem selecta un client decat daca ne aflam in tabelul clienti
              if(comboBox5.Text == "Clienti")
                    ClientSelectat = e.RowIndex;
        }

        private void stergeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text != "Clienti")
            {
                MessageBox.Show("Intai selectati tabelul Clienti", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ClientSelectat < 0)
            {
                MessageBox.Show("Intai selectati un client", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlParameter[] spp = new SqlParameter[2];
            spp[0] = new SqlParameter();
            spp[1] = new SqlParameter();

            DataGridViewRow dgvr = dataGridView1.Rows[ClientSelectat];

            spp[0].ParameterName = "@ID_CLIENT";
            spp[0].SqlDbType = SqlDbType.Int;
            spp[0].Value = Convert.ToInt32(dgvr.Cells["ID_CLIENT"].Value);
            spp[0].Direction = ParameterDirection.Input;

            spp[1].ParameterName = "@username_context";
            spp[1].SqlDbType = SqlDbType.VarChar;
            spp[1].Value = Username;
            spp[1].Direction = ParameterDirection.Input;

            b.DataBaseOperation("StergeClient", spp);
            MessageBox.Show("Clientul selectat a fost sters");
            ClientSelectat = -1;

            DataTable n = new DataTable();
            n = b.read_table("dbo.Clienti");
            n.TableName = "Clienti";
            dvClienti.Table = n;
            dataGridView1.DataSource = dvClienti;

            button4.Visible = true;
            label13.Visible = false;
            button3.Visible = false;
            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = false;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = false;

            richTextBox1.Text = richTextBox2.Text = richTextBox3.Text = richTextBox4.Text = richTextBox5.Text = richTextBox6.Text = "";
        }

        private void adaugaClientToolStripMenuItem_Click(object sender, EventArgs e)    //butonul de adaugare client din meniu
        {
            if (comboBox5.Text != "Clienti")
            {
                MessageBox.Show("Intai selectati tabelul Clienti", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            button3.Text = "Adauga Client";
            button4.Visible = false;
            label13.Visible = true;
            button3.Visible = true;
            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = true;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = true;

            label2.Visible = false;
            comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
            label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;


        }
        private void modificaClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text != "Clienti")
            {
                MessageBox.Show("Intai selectati tabelul Clienti", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            button3.Text = "Modifica Client";
            button4.Visible = false;
            label13.Visible = true;
            button3.Visible = true;
            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = true;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = true;

            label2.Visible = false;
            comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
            label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;

        }
        private void button3_Click(object sender, EventArgs e)  //butonul de adaugare client sau de modificare client
        {
            if (button3.Text == "Adauga Client")
            {
                if (richTextBox2.Text == "" || richTextBox3.Text == "" || richTextBox4.Text == "" || richTextBox5.Text == "" || richTextBox6.Text == "")
                {
                    MessageBox.Show("Camp lasat necompletat,incercati din nou", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string Data_curenta = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                SqlParameter[] spp = new SqlParameter[8];
                for (int i = 0; i < 8; i++)
                    spp[i] = new SqlParameter();
                if (richTextBox1.Text != "")
                {
                    spp[0].ParameterName = "@ID_APARTAMENT";
                    spp[0].SqlDbType = SqlDbType.Int;
                    spp[0].Value = Convert.ToInt32(richTextBox1.Text);
                    spp[0].Direction = ParameterDirection.Input;
                }
                else if (richTextBox1.Text == "")
                {
                    spp[0].ParameterName = "@ID_APARTAMENT";
                    spp[0].SqlDbType = SqlDbType.Int;
                    spp[0].Value = DBNull.Value;
                    spp[0].Direction = ParameterDirection.Input;
                }
                spp[1].ParameterName = "@NUME";
                spp[1].SqlDbType = SqlDbType.VarChar;
                spp[1].Value = richTextBox2.Text;
                spp[1].Direction = ParameterDirection.Input;

                spp[2].ParameterName = "@PRENUME";
                spp[2].SqlDbType = SqlDbType.VarChar;
                spp[2].Value = richTextBox3.Text;
                spp[2].Direction = ParameterDirection.Input;

                spp[3].ParameterName = "@SERIE_NUMAR_CI";
                spp[3].SqlDbType = SqlDbType.VarChar;
                spp[3].Value = richTextBox4.Text;
                spp[3].Direction = ParameterDirection.Input;

                spp[4].ParameterName = "@CNP";
                spp[4].SqlDbType = SqlDbType.VarChar;
                spp[4].Value = richTextBox5.Text;
                spp[4].Direction = ParameterDirection.Input;

                spp[5].ParameterName = "@DATA_INCHIRIERE";
                spp[5].SqlDbType = SqlDbType.VarChar;
                spp[5].Value = Data_curenta;
                spp[5].Direction = ParameterDirection.Input;

                spp[6].ParameterName = "@DATA_INCHEIERE";
                spp[6].SqlDbType = SqlDbType.VarChar;
                spp[6].Value = richTextBox6.Text;
                spp[6].Direction = ParameterDirection.Input;

                spp[7].ParameterName = "@username_context";
                spp[7].SqlDbType = SqlDbType.VarChar;
                spp[7].Value = Username;
                spp[7].Direction = ParameterDirection.Input;

                try
                {
                    b.DataBaseOperation("AdaugaClient", spp);
                    MessageBox.Show("Clientul a fost adaugat");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Operatia nu a putut fi facuta!Id-ul apartamenului este unul valid ?", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (button3.Text == "Modifica Client")
            {
                if (ClientSelectat < 0)
                {
                    MessageBox.Show("Intai selectati un client", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id_client=0,id_apartament=0;
                string nume = "", prenume = "", serie_numar_ci = "", cnp = "", data_inchiriere = "", data_incheiere = "";
                DataTable dt;
                dt = dvClienti.ToTable();
                dt.TableName = "Clienti";
                id_client = Convert.ToInt32(dt.Rows[ClientSelectat]["ID_CLIENT"]);
                b.GetClientFields(id_client,ref id_apartament,ref nume,ref prenume,ref serie_numar_ci,ref cnp,ref data_inchiriere,ref data_incheiere);

                SqlParameter[] spp = new SqlParameter[9];
                for (int i = 0; i < 9; i++)
                    spp[i] = new SqlParameter();

                spp[0].ParameterName = "@ID_CLIENT";
                spp[0].SqlDbType = SqlDbType.Int;
                spp[0].Value = id_client;
                spp[0].Direction = ParameterDirection.Input;
                if (richTextBox1.Text != "")
                {
                    spp[1].ParameterName = "@ID_APARTAMENT";
                    spp[1].SqlDbType = SqlDbType.Int;
                    spp[1].Value = Convert.ToInt32(richTextBox1.Text);
                    spp[1].Direction = ParameterDirection.Input;
                }
                else if (richTextBox1.Text == "0")
                {
                    spp[1].ParameterName = "@ID_APARTAMENT";
                    spp[1].SqlDbType = SqlDbType.Int;
                    spp[1].Value = DBNull.Value;
                    spp[1].Direction = ParameterDirection.Input;
                }
                else if (richTextBox1.Text == "")
                {
                    if (id_apartament == -1)
                    {
                        spp[1].ParameterName = "@ID_APARTAMENT";
                        spp[1].SqlDbType = SqlDbType.Int;
                        spp[1].Value = DBNull.Value;
                        spp[1].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        spp[1].ParameterName = "@ID_APARTAMENT";
                        spp[1].SqlDbType = SqlDbType.Int;
                        spp[1].Value =id_apartament;
                        spp[1].Direction = ParameterDirection.Input;
                    }
                }
                if (richTextBox2.Text == "")
                {
                    spp[2].ParameterName = "@NUME";
                    spp[2].SqlDbType = SqlDbType.VarChar;
                    spp[2].Value = nume;
                    spp[2].Direction = ParameterDirection.Input;
                }
                else if (richTextBox2.Text != "")
                {
                    spp[2].ParameterName = "@NUME";
                    spp[2].SqlDbType = SqlDbType.VarChar;
                    spp[2].Value = richTextBox2.Text;
                    spp[2].Direction = ParameterDirection.Input;
                }
                if (richTextBox3.Text == "")
                {
                    spp[3].ParameterName = "@PRENUME";
                    spp[3].SqlDbType = SqlDbType.VarChar;
                    spp[3].Value = prenume;
                    spp[3].Direction = ParameterDirection.Input;
                }
                else if (richTextBox3.Text != "")
                {
                    spp[3].ParameterName = "@PRENUME";
                    spp[3].SqlDbType = SqlDbType.VarChar;
                    spp[3].Value = richTextBox3.Text;
                    spp[3].Direction = ParameterDirection.Input;
                }
                if (richTextBox4.Text == "")
                {
                    spp[4].ParameterName = "@SERIE_NUMAR_CI";
                    spp[4].SqlDbType = SqlDbType.VarChar;
                    spp[4].Value = serie_numar_ci;
                    spp[4].Direction = ParameterDirection.Input;
                }
                else if (richTextBox4.Text != "")
                {
                    spp[4].ParameterName = "@SERIE_NUMAR_CI";
                    spp[4].SqlDbType = SqlDbType.VarChar;
                    spp[4].Value = richTextBox4.Text;
                    spp[4].Direction = ParameterDirection.Input;
                }
                if (richTextBox5.Text == "")
                {
                    spp[5].ParameterName = "@CNP";
                    spp[5].SqlDbType = SqlDbType.VarChar;
                    spp[5].Value = cnp;
                    spp[5].Direction = ParameterDirection.Input;
                }
                else if (richTextBox5.Text != "")
                {
                    spp[5].ParameterName = "@CNP";
                    spp[5].SqlDbType = SqlDbType.VarChar;
                    spp[5].Value = richTextBox5.Text;
                    spp[5].Direction = ParameterDirection.Input;
                }
                spp[6].ParameterName = "@DATA_INCHIRIERE";
                spp[6].SqlDbType = SqlDbType.VarChar;
                spp[6].Value = data_inchiriere;
                spp[6].Direction = ParameterDirection.Input;

                if (richTextBox6.Text == "")
                {
                    spp[7].ParameterName = "@DATA_INCHEIERE";
                    spp[7].SqlDbType = SqlDbType.VarChar;
                    spp[7].Value = data_incheiere;
                    spp[7].Direction = ParameterDirection.Input;
                }
                else if (richTextBox6.Text != "")
                {
                    spp[7].ParameterName = "@DATA_INCHEIERE";
                    spp[7].SqlDbType = SqlDbType.VarChar;
                    spp[7].Value = richTextBox6.Text;
                    spp[7].Direction = ParameterDirection.Input;
                }
                spp[8].ParameterName = "@username_context";
                spp[8].SqlDbType = SqlDbType.VarChar;
                spp[8].Value = Username;
                spp[8].Direction = ParameterDirection.Input;

                try
                {
                   b.DataBaseOperation("ModificaClient", spp);
                    MessageBox.Show("Clientul a fost modificat");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Operatia nu a putut fi facuta!Id-ul apartamenului este unul valid ?", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ClientSelectat = -1;
            }

            DataTable n = new DataTable();
            n = b.read_table("dbo.Clienti");
            n.TableName = "Clienti";
            dvClienti.Table = n;
            dataGridView1.DataSource = dvClienti;

            richTextBox1.Text = richTextBox2.Text = richTextBox3.Text = richTextBox4.Text = richTextBox5.Text = richTextBox6.Text = "";

            button4.Visible = true;
            label2.Visible = true;
            comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = true;
            label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = true;


            label13.Visible = false;
            button3.Visible = false;

            
            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = false;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox6.Text = comboBox7.Text = comboBox8.Text = comboBox10.Text = "";
            comboBox1.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text = "";

            comboBox11.Text = comboBox12.Text = comboBox13.Text = comboBox14.Text = comboBox15.Text = comboBox16.Text = comboBox17.Text = "";

            // DE COMPLETAT
            dvClienti.RowFilter = "";
        }

        private void SearchingForEntries(object sender, EventArgs e)
        {
                if (comboBox17.Text == "" && comboBox16.Text == "" && comboBox15.Text == "" && comboBox14.Text == "" && comboBox13.Text == "" && comboBox12.Text == "" && comboBox11.Text == "")
                    dvClienti.RowFilter = "";
                else if (comboBox14.Text == "")
                {
                    dvClienti.RowFilter = "";
                   dvClienti.RowFilter = String.Format("NUME LIKE '{0}*' AND PRENUME LIKE '{1}*' AND SERIE_NUMAR_CI LIKE '{2}*' AND CNP LIKE '{3}*' AND DATA_INCHIRIERE LIKE '{4}*' AND DATA_INCHEIERE LIKE '{5}*'", comboBox13.Text, comboBox12.Text, comboBox11.Text, comboBox17.Text, comboBox16.Text, comboBox15.Text);
                    
                }
                else
                     dvClienti.RowFilter = String.Format("Convert(ID_APARTAMENT,'System.String') LIKE '{0}*' AND NUME LIKE '{1}*' AND PRENUME LIKE '{2}*' AND SERIE_NUMAR_CI LIKE '{3}*' AND CNP LIKE '{4}*' AND DATA_INCHIRIERE LIKE '{5}*' AND DATA_INCHEIERE LIKE '{6}*'", comboBox14.Text, comboBox13.Text, comboBox12.Text, comboBox11.Text, comboBox17.Text, comboBox16.Text, comboBox15.Text);
                   
        }
 
        private void comboBox14_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox14.Items.Clear();
            if (comboBox14.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[1]);
                    if (String.Compare(comboBox14.Text, 0, str, 0, comboBox14.Text.Length, false) == 0)
                        comboBox14.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox14.Select(comboBox14.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }
        private void comboBox13_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox13.Items.Clear();
            if (comboBox13.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[2]);
                    if (String.Compare(comboBox13.Text, 0, str, 0, comboBox13.Text.Length, false) == 0)
                        comboBox13.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox13.Select(comboBox13.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }
        private void comboBox12_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox12.Items.Clear();
            if (comboBox12.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[3]);
                    if (String.Compare(comboBox12.Text, 0, str, 0, comboBox12.Text.Length, false) == 0)
                        comboBox12.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox12.Select(comboBox12.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }
        private void comboBox11_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox11.Items.Clear();
            if (comboBox11.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[4]);
                    if (String.Compare(comboBox11.Text, 0, str, 0, comboBox11.Text.Length, false) == 0)
                        comboBox11.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox11.Select(comboBox11.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }
        private void comboBox17_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox17.Items.Clear();
            if (comboBox17.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[5]);
                    if (String.Compare(comboBox17.Text, 0, str, 0, comboBox17.Text.Length, false) == 0)
                        comboBox17.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox17.Select(comboBox17.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }
        private void comboBox16_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox16.Items.Clear();
            if (comboBox16.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[6]);
                    if (String.Compare(comboBox16.Text, 0, str, 0, comboBox16.Text.Length, false) == 0)
                        comboBox16.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox16.Select(comboBox16.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }
        private void comboBox15_TextUpdate(object sender, EventArgs e)
        {
            // Sterg toate optiunile pe care userul le poate alege atunci cand apasa pe sagetuta combobox-ului
            // pentru a le putea reface dupa fiecare modificare a textului din combobox.
            if (comboBox5.Text != "Clienti")
                return;
            comboBox15.Items.Clear();
            if (comboBox15.Text != "")
            {
                // Formez optiunile combobox-ului prin parcurgerea dataview-ului.
                foreach (DataRowView drv in dvClienti)
                {
                    string str = "";
                    str = Convert.ToString(drv[7]);
                    if (String.Compare(comboBox15.Text, 0, str, 0, comboBox15.Text.Length, false) == 0)
                        comboBox15.Items.Add(str);
                }
            }
            // Pozitionez caret-ul la finalul textului.
            comboBox15.Select(comboBox15.Text.Length, 0);
            this.SearchingForEntries(sender, e);
        }

        private void Generic_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SearchingForEntries(sender, e);
        }

        private void inEXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Clienti")
            {
                b.ExportToExcel(dataGridView1, "Clienti");
                MessageBox.Show("Lista cu clientii a fost exportata cu succes in Excel");
            }
            else if (comboBox5.Text == "Apartamente")
            {
                if (ok1 == 1 && ok2 == 0)
                {
                    b.ExportToExcel(dataGridView1, "ApartamenteLibere");
                    MessageBox.Show("Lista cu apartamentele libere a fost exportata cu succes in Excel");
                }
                else if (ok1 == 0 && ok2 == 1)
                {
                    b.ExportToExcel(dataGridView1, "ApartamenteOcupate");
                    MessageBox.Show("Lista cu apartamentele ocupate a fost exportata cu succes in Excel");
                }
                else MessageBox.Show("Selectati tipul de apartamente : Libere sau Ocupate");
            }
            else if (comboBox5.Text == "Adrese")
            {
                b.ExportToExcel(dataGridView1, "Adrese");
                MessageBox.Show("Lista cu adresele apartamentelor a fost exportata cu succes in Excel");
            }
        }

        private void inPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Clienti")
            {
                b.ExportToPDF(dataGridView1, "Clienti");
                MessageBox.Show("Lista cu clientii a fost exportata cu succes in PDF");
            }
            else if (comboBox5.Text == "Apartamente")
            {
                if (ok1 == 1 && ok2 == 0)
                {
                    b.ExportToPDF(dataGridView1, "ApartamenteLibere");
                    MessageBox.Show("Lista cu apartamentele libere a fost exportata cu succes in PDF");
                }
                else if (ok1 == 0 && ok2 == 1)
                {
                    b.ExportToPDF(dataGridView1, "ApartamenteOcupate");
                    MessageBox.Show("Lista cu apartamentele ocupate a fost exportata cu succes in PDF");
                }
                else MessageBox.Show("Selectati tipul de apartamente : Libere sau Ocupate");
            }
            else if (comboBox5.Text == "Adrese")
            {
                b.ExportToPDF(dataGridView1, "Adrese");
                MessageBox.Show("Lista cu adresele apartamentelor a fost exportata cu succes in PDF");
            }
        }
    }
}
