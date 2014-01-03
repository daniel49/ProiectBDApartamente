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

namespace ProiectBD_InchiriereApartamente
{
    public partial class AdministratorForm : Form
    {
        DataView dvSursa = new DataView();
        int RandSelectat = -1;
        bool ocupat;    //false - camere libere , true - camere ocupate
        int ok1 = 0;    //ok1 - 1-camere libere verde 
        int ok2 = 0;    //ok2 - 1-camere ocuoate
        string Username = "";
        int option = 0;      // 0 - adrese 1 - apartamente 2 - clienti
        BusinessLayer b = new BusinessLayer();
        public AdministratorForm(string username)
        {
            InitializeComponent();

            comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
            label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;


            button4.Visible = false;
            dataGridView1.ReadOnly = true;
            Username = username;

            label13.Visible = false;
            button3.Visible = false;
            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible =false;

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
            comboBox5.Items.Add("Roluri");
            comboBox5.Items.Add("Audit");
            comboBox5.Items.Add("Conturi");
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

        private void gestiune_camere(object sender, EventArgs e)    //camere libere - button click event
        {
            RandSelectat = -1;
            if (ok1 == 0)
            {
                button4.Visible = true;
                button1.BackColor = System.Drawing.Color.Maroon;
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
                b.start = 0;         //nu este aplicat nici un filtru momentan
                filter_changed(sender, e);
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
            }
            else
            {
                button4.Visible = false;
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
            RandSelectat = -1;
            if (ok2 == 0)
            {
                button4.Visible = true;
                button2.BackColor = System.Drawing.Color.Maroon;
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
            else if (ok2 == 1)
            {
                button4.Visible = false;
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

            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(), comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, option, b.start, option);
            //combobox1
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(), comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, 1, b.start, 0);
            int i = comboBox1.Items.Count;
            for (int j = 0; j < i; j++)
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
            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(), comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, 0, b.start, 0);
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

            b.filter_datagridview(dataGridView1, comboBox1.Text.ToString(), comboBox2.Text.ToString(), comboBox3.Text.ToString(), comboBox4.Text.ToString(), comboBox6.Text.ToString(), comboBox7.Text.ToString(), comboBox8.Text.ToString(), comboBox9.Text.ToString(), comboBox10.Text.ToString(), ocupat, option, b.start, 1);
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
            DataTable tabel_ales = new DataTable();

            if (option == 2 || option == 3 || option == 4 || option == 5)    //trec de la sursa de date Clienti,Roluri,Audit sau Conturi la oricare alta
            {
                button4.Visible = false; // butonul Sterge filtrele
                label2.Visible = false; //label Filtrati dupa...
                dataGridView1.Visible = false;  //ascundem primul datagridview
                if (ok1 == 1 && ok2 == 0)   
                    ok1 = 0;
                if (ok2 == 1 && ok1 == 0)
                    ok2 = 0;
                button1.BackColor = Color.Transparent;  //punem culoarea la butoane,ca si cum nu ar fi selectat nimic
                button2.BackColor = Color.Transparent;
            }
            if (string.Compare(comboBox5.Text.ToString(), "Roluri") == 0)
            {
                tabel_ales = b.read_table("Roluri");    //preluam sursa de date din baza de date
                tabel_ales.TableName = "Roluri";
                dvSursa.Table = tabel_ales;
                dataGridView1.Visible = true;   //facem vibil locul unde afisam datele despre roluri
                dataGridView1.DataSource = dvSursa;

                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare/modificare 
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible = false;
                }
                //ascundem celelalte 2 datagridview-uri , nu avem nevoie de ele
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                label1.Visible = button1.Visible = button2.Visible = false;

                //ascundem campurile de filtrare si label-urile de la Apartamente/Adrese
                comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox6.Visible = comboBox7.Visible = comboBox8.Visible = comboBox9.Visible = comboBox10.Visible = false;
                label3.Visible = label4.Visible = label5.Visible = label6.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible =

                //ascundem capurile de filtrare si label-urile de la Clienti/Audit/Conturi
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;

                label2.Visible = false;//ascundem label-ul de Filtrati dupa...
                button4.Visible = false;//ascundem butonul de Sterge filtrele

                label1.Visible = button1.Visible = button2.Visible = false;     //ascundem eticheta Apartamente... si butoanele Libere si Ocupate

                RandSelectat = -1;
                option = 5;
            }
            if (string.Compare(comboBox5.Text.ToString(), "Audit") == 0)
            {

                tabel_ales = b.read_table("Audit"); //incarcam sursa de date din baza de date
                tabel_ales.TableName = "Audit";
                dvSursa.Table = tabel_ales;
                dataGridView1.Visible = true;   //facem vizibil locul unde punem datele despre Audit
                dataGridView1.DataSource = dvSursa;

                dataGridView2.Visible = false;  //ascundem celelalte datagridview-uri ,nu avem nevoie de ele
                dataGridView3.Visible = false;

                //redenumim o parte din etichetele de la filtrele de la clienti
                label23.Text = "Tip operatie";
                label22.Text = "Tabel implicat";
                label21.Text = "Nume cont";
                label20.Text = "Data si timp";

                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare/modificare 
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible = false;
                }
                //ascund campurile de filtrare si etichetele de la apartamente/adrese
                comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox6.Visible = comboBox7.Visible = comboBox8.Visible = comboBox9.Visible = comboBox10.Visible = false;
                label3.Visible = label4.Visible = label5.Visible = label6.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = false;

                //ascund o parte din etichetele si campurile de filtrare de la clienti..
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = true; comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = true; label24.Visible = label25.Visible = label26.Visible = false;

                label2.Visible = true;  //facem vizibil eticheta Filtrati dupa...
                button4.Visible = true; // facem vizibi butonul de Sterge Filtrele

                label1.Visible = button1.Visible = button2.Visible = false;     //ascundem eticheta Apartamente... si butoanele Libere si Ocupate

                RandSelectat = -1;
                option = 3;
            }
            if (string.Compare(comboBox5.Text.ToString(), "Conturi") == 0)
            {
               label13.Text = "Intoduceti datele contului";

                tabel_ales = b.GetTablesWithRelationships("AfiseazaConturi", null);
                tabel_ales.TableName = "Conturi";
                dvSursa.Table = tabel_ales;
                dataGridView1.Visible = true;
                dataGridView1.DataSource = dvSursa;

                dataGridView2.Visible = false;  //ascundem celelalte datagridview-uri ,nu avem nevoie de ele
                dataGridView3.Visible = false;

                //redenumim o parte din etichetele de filtrare de la clienti
                label22.Text = "Nume user";
                label23.Text = "Nume rol";

                //ascundem o parte din etichetele si butoanele  de la filtrare clienti
                comboBox13.Visible = comboBox14.Visible = true; comboBox11.Visible = comboBox12.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label22.Visible = label23.Visible = true; label21.Visible = label20.Visible = label24.Visible = label25.Visible = label26.Visible = false;

                //ascundem campurile de filtrare si etichetele de la apartamente/adrese
                 comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox6.Visible = comboBox7.Visible = comboBox8.Visible = comboBox9.Visible = comboBox10.Visible = false;
                 label3.Visible = label4.Visible = label5.Visible = label6.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = false;

                 if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare/modificare 
                 {
                     label13.Visible = false;
                     button3.Visible = false;
                     richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
                     label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible = false;
                 }

                label1.Visible = button1.Visible = button2.Visible = false;     //ascundem eticheta Apartamente... si butoanele Libere si Ocupate

                label2.Visible = true;  //facem vizibil eticheta Filtrati dupa...
                button4.Visible = true; // facem vizibi butonul de Sterge Filtrele

                option = 4;
                RandSelectat = -1;

            }
            if (string.Compare(comboBox5.Text.ToString(), "Apartamente") == 0)
            {
                label13.Text = "Intoduceti datele apartamentului";

                //ascundem datagridview-urile de care nu avem nevoie momentan
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;

                //ascundem campurile de filtrare si etichetele de la filtrare clienti,conturi,etc
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;

                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare/modificare 
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible = false;
                }

                //afisam eticheta Apartamente... si butoaneel Libere , Ocupate
                label1.Visible = true;
                button1.Visible = button2.Visible = true;

                RandSelectat = -1;
                option = 0;

                filter_changed(sender, e);
            }

            if (string.Compare(comboBox5.Text.ToString(), "Adrese") == 0)
            {
                label13.Text = "Intoduceti datele adresei";

                //ascundem datagridview-urile de care nu avem nevoie momentan
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;

                //ascundem campurile de filtrare si etichetele de la filtrare clienti,conturi,etc
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = false;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = false;

                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare/modificare 
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible = false;
                }

                //afisam eticheta Apartamente... si butoaneel Libere , Ocupate
                label1.Visible = true;
                button1.Visible = button2.Visible = true;

                RandSelectat = -1;
                option = 1;

                filter_changed(sender, e);
              
            }
            if (string.Compare(comboBox5.Text.ToString(), "Clienti") == 0)
            {
                label13.Text = "Intoduceti datele despre client";

                tabel_ales = b.read_table("Clienti");
                tabel_ales.TableName = "Clienti";
                dvSursa.Table = tabel_ales;
                dataGridView1.Visible = true;
                dataGridView1.DataSource = dvSursa;

                dataGridView2.Visible = false;  //ascundem celelalte datagridview-uri ,nu avem nevoie de ele
                dataGridView3.Visible = false;

                label23.Text = "ID Apartament";
                label22.Text = "Nume";
                label21.Text = "Prenume";
                label20.Text = "Serie Numar CI";

                //ascundem o parte din etichetele si butoanele  de la filtrare clienti
                comboBox13.Visible = comboBox14.Visible = comboBox11.Visible = comboBox12.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = true;
                label22.Visible = label23.Visible = label21.Visible = label20.Visible = label24.Visible = label25.Visible = label26.Visible = true;

                //ascundem campurile de filtrare si etichetele de la apartamente/adrese
                comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox6.Visible = comboBox7.Visible = comboBox8.Visible = comboBox9.Visible = comboBox10.Visible = false;
                label3.Visible = label4.Visible = label5.Visible = label6.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = false;

                if (label13.Visible == true)    //in caz ca trece de la o sursa de date la alta,ascund campurile de adaugare/modificare 
                {
                    label13.Visible = false;
                    button3.Visible = false;
                    richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible = false ;
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible =  false;
                }

                label1.Visible = button1.Visible = button2.Visible = false;     //ascundem eticheta Apartamente... si butoanele Libere si Ocupate

                label2.Visible = true;  //facem vizibil eticheta Filtrati dupa...
                button4.Visible = true; // facem vizibi butonul de Sterge Filtrele

                option = 3;
                RandSelectat = -1;
               
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
                if (comboBox5.Text != "Roluri" && comboBox5.Text != "Audit")
                    RandSelectat = e.RowIndex;
        }

        private void stergeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text != "Clienti")
            {
                MessageBox.Show("Intai selectati tabelul Clienti", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (RandSelectat < 0)
            {
                MessageBox.Show("Intai selectati un client", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlParameter[] spp = new SqlParameter[2];
            spp[0] = new SqlParameter();
            spp[1] = new SqlParameter();

            DataGridViewRow dgvr = dataGridView1.Rows[RandSelectat];

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
            RandSelectat = -1;

            DataTable n = new DataTable();
            n = b.read_table("dbo.Clienti");
            n.TableName = "Clienti";
            dvSursa.Table = n;
            dataGridView1.DataSource = dvSursa;

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

            //modific numele etichetelor
            label14.Text = "ID Apartament"; label15.Text = "Nume"; label16.Text = "Prenume"; label17.Text = "Serie Numar CI"; label18.Text = "CNP"; label19.Text = "Data Incheiere";
            //ascund ce nu am nevoie
            label27.Visible = label28.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
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

            //modific numele etichetelor
            label14.Text = "ID Apartament"; label15.Text = "Nume"; label16.Text = "Prenume"; label17.Text = "Serie Numar CI"; label18.Text = "CNP"; label19.Text = "Data Incheiere";
            //ascund ce nu am nevoie
            label27.Visible = label28.Visible = richTextBox7.Visible = richTextBox8.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)  //butonul de adaugare sau modificare
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
                RandSelectat = -1;
                DataTable n = new DataTable();
                n = b.read_table("dbo.Clienti");
                n.TableName = "Clienti";
                dvSursa.Table = n;
                dataGridView1.DataSource = dvSursa;

                button4.Visible = true;
                label2.Visible = true;
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = true;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = true;


                label13.Visible = false;
            }
            else if (button3.Text == "Modifica Client")
            {
                if (RandSelectat < 0)
                {
                    MessageBox.Show("Intai selectati un client", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id_client = 0, id_apartament = 0;
                string nume = "", prenume = "", serie_numar_ci = "", cnp = "", data_inchiriere = "", data_incheiere = "";
                DataTable dt;
                dt = dvSursa.ToTable();
                dt.TableName = "Clienti";
                id_client = Convert.ToInt32(dt.Rows[RandSelectat]["ID_CLIENT"]);
                b.GetClientFields(id_client, ref id_apartament, ref nume, ref prenume, ref serie_numar_ci, ref cnp, ref data_inchiriere, ref data_incheiere);

                SqlParameter[] spp = new SqlParameter[9];
                for (int i = 0; i < 9; i++)
                    spp[i] = new SqlParameter();

                spp[0].ParameterName = "@ID_CLIENT";
                spp[0].SqlDbType = SqlDbType.Int;
                spp[0].Value = id_client;
                spp[0].Direction = ParameterDirection.Input;
                if (richTextBox1.Text == "0")
                {
                    spp[1].ParameterName = "@ID_APARTAMENT";
                    spp[1].SqlDbType = SqlDbType.Int;
                    spp[1].Value = DBNull.Value;
                    spp[1].Direction = ParameterDirection.Input;
                }
                else if (richTextBox1.Text != "")
                {
                    spp[1].ParameterName = "@ID_APARTAMENT";
                    spp[1].SqlDbType = SqlDbType.Int;
                    spp[1].Value = Convert.ToInt32(richTextBox1.Text);
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
                        spp[1].Value = id_apartament;
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
                RandSelectat = -1;
                DataTable n = new DataTable();
                n = b.read_table("dbo.Clienti");
                n.TableName = "Clienti";
                dvSursa.Table = n;
                dataGridView1.DataSource = dvSursa;

                button4.Visible = true;
                label2.Visible = true;
                comboBox11.Visible = comboBox12.Visible = comboBox13.Visible = comboBox14.Visible = comboBox15.Visible = comboBox16.Visible = comboBox17.Visible = true;
                label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible = label25.Visible = label26.Visible = true;


                label13.Visible = false;

            }
            else if (button3.Text == "Adauga Cont")
            {
                if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "")
                {
                    MessageBox.Show("Camp lasat necompletat,incercati din nou", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SqlParameter[] spp = new SqlParameter[4];
                for (int i = 0; i < 4; i++)
                    spp[i] = new SqlParameter();

                spp[0].ParameterName = "@ID_ROL";
                spp[0].SqlDbType = SqlDbType.Int;
                if(richTextBox1.Text == "Administrator")
                      spp[0].Value = 1;
                else if (richTextBox1.Text == "User")
                    spp[0].Value = 2;
                else
                {
                    MessageBox.Show("Nume Rol : valoare incorecta ... alegeti intre Administrator si User","Eroare",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                spp[0].Direction = ParameterDirection.Input;

                spp[1].ParameterName = "@NUME_USER";
                spp[1].SqlDbType = SqlDbType.VarChar;
                spp[1].Value = richTextBox2.Text;
                spp[1].Direction = ParameterDirection.Input;

                if (b.CheckIfUsernameTaken(richTextBox2.Text) == true)
                {
                    MessageBox.Show("Numele userului este deja luat");
                    return;
                }
                spp[2].ParameterName = "@PAROLA";
                spp[2].SqlDbType = SqlDbType.VarChar;
                spp[2].Value = b.Encrypt(richTextBox3.Text);
                spp[2].Direction = ParameterDirection.Input;

                spp[3].ParameterName = "@username_context";
                spp[3].SqlDbType = SqlDbType.VarChar;
                spp[3].Value = Username;
                spp[3].Direction = ParameterDirection.Input;

                try
                {
                    b.DataBaseOperation("AdaugaCont", spp);
                    MessageBox.Show("Contul a fost introdus in baza de date");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Operatia nu a putut fi facuta!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable n = new DataTable();
                n = b.GetTablesWithRelationships("AfiseazaConturi", null);
                n.TableName = "Conturi";
                dvSursa.Table = n;
                dataGridView1.DataSource = dvSursa;

                button4.Visible = true;
                label2.Visible = true;

                comboBox14.Visible = comboBox13.Visible =  label22.Visible = label23.Visible = true;

                label13.Visible = false;
                
            }
            else if (button3.Text == "Modifica Apartament")
            {
                int id_apartament = -1, an_constructie = -1, pret_inchiriere = -1, numar_camere = -1, suprafata_utila = -1;
                string facilitati = "";

                if (RandSelectat < 0)
                {
                    MessageBox.Show("Intai selectati un apartament", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlParameter[] spp = new SqlParameter[7];
                for (int i = 0; i < 7; i++)
                    spp[i] = new SqlParameter();

                DataGridViewRow dgvr = dataGridView1.Rows[RandSelectat];
                id_apartament = Convert.ToInt32(dgvr.Cells["ID_APARTAMENT"].Value);
                b.GetApartmentFields(id_apartament, ref numar_camere, ref suprafata_utila, ref an_constructie, ref facilitati, ref pret_inchiriere);

                spp[0].ParameterName = "@ID_APARTAMENT";
                spp[0].SqlDbType = SqlDbType.Int;
                spp[0].Value = id_apartament;
                spp[0].Direction = ParameterDirection.Input;

                spp[1].ParameterName = "@NUMAR_CAMERE";
                spp[1].SqlDbType = SqlDbType.Int;
                spp[1].Direction = ParameterDirection.Input;
                if (richTextBox1.Text == "")
                    spp[1].Value = numar_camere;
                else spp[1].Value = Convert.ToInt32(richTextBox1.Text);

                spp[2].ParameterName = "@SUPRAFATA_UTILA";
                spp[2].SqlDbType = SqlDbType.Int;
                spp[2].Direction = ParameterDirection.Input;
                if (richTextBox2.Text == "")
                    spp[2].Value = suprafata_utila;
                else spp[2].Value = Convert.ToInt32(richTextBox2.Text);

                spp[3].ParameterName = "@AN_CONSTRUCTIE";
                spp[3].SqlDbType = SqlDbType.Int;
                spp[3].Direction = ParameterDirection.Input;
                if (richTextBox3.Text == "")
                    spp[3].Value = an_constructie;
                else spp[3].Value = Convert.ToInt32(richTextBox3.Text);

                spp[4].ParameterName = "@FACILITATI";
                spp[4].SqlDbType = SqlDbType.VarChar;
                spp[4].Direction = ParameterDirection.Input;
                if (richTextBox4.Text == "")
                {
                    if (facilitati == "-1")
                        spp[4].Value = DBNull.Value;
                    else spp[4].Value = facilitati;
                }
                else if (richTextBox4.Text == "0")
                    spp[4].Value = DBNull.Value;
                else spp[4].Value = richTextBox4.Text;

                spp[5].ParameterName = "@PRET_INCHIRIERE";
                spp[5].SqlDbType = SqlDbType.Int;
                spp[5].Direction = ParameterDirection.Input;
                if (richTextBox5.Text == "")
                    spp[5].Value = pret_inchiriere;
                else spp[5].Value = Convert.ToInt32(richTextBox5.Text);

                spp[6].ParameterName = "@username_context";
                spp[6].SqlDbType = SqlDbType.VarChar;
                spp[6].Direction = ParameterDirection.Input;
                spp[6].Value = Username;

                try
                {
                    b.DataBaseOperation("ModificaApartament", spp);
                    MessageBox.Show("Apartamenul a fost modificat");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Operatia nu a putut fi realizata", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RandSelectat = -1;

                DataTable n = new DataTable();
                n = b.read_table("Apartamente");
                n.TableName = "Apartamente";
                dvSursa.Table = n;
                dataGridView1.DataSource = dvSursa;

              
            }
            else if (button3.Text == "Modifica Adresa")
            {
                int id_adresa = -1;
                string oras = "", strada = "", numar = "", bloc = "", scara = "", etaj = "", numar_apartament = "", sector = "";

                if (RandSelectat < 0)
                {
                    MessageBox.Show("Intai selectati o adresa", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlParameter[] spp = new SqlParameter[10];
                for (int i = 0; i < 10; i++)
                    spp[i] = new SqlParameter();

                DataGridViewRow dgvr = dataGridView1.Rows[RandSelectat];
                id_adresa= Convert.ToInt32(dgvr.Cells["ID_ADRESA"].Value);
                b.GetAddressFields(id_adresa, ref oras, ref strada, ref numar, ref bloc, ref scara, ref etaj, ref numar_apartament, ref sector);

                spp[0].ParameterName = "@ID_ADRESA";
                spp[0].SqlDbType = SqlDbType.Int;
                spp[0].Value = id_adresa;
                spp[0].Direction = ParameterDirection.Input;

                spp[1].ParameterName = "@ORAS";
                spp[1].SqlDbType = SqlDbType.VarChar;
                spp[1].Direction = ParameterDirection.Input;
                if (richTextBox1.Text == "")
                    spp[1].Value = oras;
                else spp[1].Value = Convert.ToString(richTextBox1.Text);

                spp[2].ParameterName = "@STRADA";
                spp[2].SqlDbType = SqlDbType.VarChar;
                spp[2].Direction = ParameterDirection.Input;
                if (richTextBox2.Text == "")
                    spp[2].Value = strada;
                else spp[2].Value = Convert.ToString(richTextBox2.Text);

                spp[3].ParameterName = "@NUMAR";
                spp[3].SqlDbType = SqlDbType.VarChar;
                spp[3].Direction = ParameterDirection.Input;
                if (richTextBox3.Text == "")
                    spp[3].Value = numar;
                else spp[3].Value = Convert.ToString(richTextBox3.Text);

                spp[4].ParameterName = "@BLOC";
                spp[4].SqlDbType = SqlDbType.VarChar;
                spp[4].Direction = ParameterDirection.Input;
                if (richTextBox4.Text == "")
                {
                    if (bloc== "-1")
                        spp[4].Value = DBNull.Value;
                    else spp[4].Value = bloc;
                }
                else if (richTextBox4.Text == "0")
                    spp[4].Value = DBNull.Value;
                else spp[4].Value = richTextBox4.Text;

                spp[5].ParameterName = "@SCARA";
                spp[5].SqlDbType = SqlDbType.VarChar;
                spp[5].Direction = ParameterDirection.Input;
                if (richTextBox5.Text == "")
                {
                    if (scara == "-1")
                        spp[5].Value = DBNull.Value;
                    else spp[5].Value = scara;
                }
                else if (richTextBox5.Text == "0")
                    spp[5].Value = DBNull.Value;
                else spp[5].Value = richTextBox5.Text;

                spp[6].ParameterName = "@ETAJ";
                spp[6].SqlDbType = SqlDbType.VarChar;
                spp[6].Direction = ParameterDirection.Input;
                if (richTextBox6.Text == "")
                {
                    if (etaj == "-1")
                        spp[6].Value = DBNull.Value;
                    else spp[6].Value = etaj;
                }
                else if (richTextBox6.Text == "0")
                    spp[6].Value = DBNull.Value;
                else spp[6].Value = richTextBox6.Text;

                spp[7].ParameterName = "@NUMAR_APARTAMENT";
                spp[7].SqlDbType = SqlDbType.VarChar;
                spp[7].Direction = ParameterDirection.Input;
                if (richTextBox7.Text == "")
                {
                    if (numar_apartament == "-1")
                        spp[7].Value = DBNull.Value;
                    else spp[7].Value = numar_apartament;
                }
                else if (richTextBox7.Text == "0")
                    spp[7].Value = DBNull.Value;
                else spp[7].Value = richTextBox7.Text;

                spp[8].ParameterName = "@SECTOR";
                spp[8].SqlDbType = SqlDbType.VarChar;
                spp[8].Direction = ParameterDirection.Input;
                if (richTextBox8.Text == "")
                {
                    if (sector == "-1")
                        spp[8].Value = DBNull.Value;
                    else spp[8].Value = sector;
                }
                else if (richTextBox8.Text == "0")
                    spp[8].Value = DBNull.Value;
                else spp[8].Value = richTextBox8.Text;

                spp[9].ParameterName = "@username_context";
                spp[9].SqlDbType = SqlDbType.VarChar;
                spp[9].Direction = ParameterDirection.Input;
                spp[9].Value = Username;
                try
                {
                    b.DataBaseOperation("ModificaAdresa", spp);
                    MessageBox.Show("Adresa apartamentului a fost modificata");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Operatia nu a putut fi realizata", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RandSelectat = -1;

                DataTable n = new DataTable();
                n = b.read_table("Adresa");
                n.TableName = "Adrese";
                dvSursa.Table = n;
                dataGridView1.DataSource = dvSursa;

            }
            richTextBox1.Text = richTextBox2.Text = richTextBox3.Text = richTextBox4.Text = richTextBox5.Text = richTextBox6.Text = richTextBox7.Text = richTextBox8.Text ="";

            button3.Visible = label13.Visible = false;

            richTextBox1.Visible = richTextBox2.Visible = richTextBox3.Visible = richTextBox4.Visible = richTextBox5.Visible = richTextBox6.Visible = richTextBox7.Visible = richTextBox8.Visible =false;
            label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible = label19.Visible = label27.Visible = label28.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)  //butonul de stergere filtre
        {
            comboBox6.Text = comboBox7.Text = comboBox8.Text = comboBox10.Text = "";
            comboBox1.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text = "";

            comboBox11.Text = comboBox12.Text = comboBox13.Text = comboBox14.Text = comboBox15.Text = comboBox16.Text = comboBox17.Text = "";

            // DE COMPLETAT
            dvSursa.RowFilter = "";
        }

        private void SearchingForEntries(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Clienti")
            {
                if (comboBox17.Text == "" && comboBox16.Text == "" && comboBox15.Text == "" && comboBox14.Text == "" && comboBox13.Text == "" && comboBox12.Text == "" && comboBox11.Text == "")
                    dvSursa.RowFilter = "";
                else if (comboBox14.Text == "")
                {
                    dvSursa.RowFilter = "";
                    dvSursa.RowFilter = String.Format("NUME LIKE '{0}*' AND PRENUME LIKE '{1}*' AND SERIE_NUMAR_CI LIKE '{2}*' AND CNP LIKE '{3}*' AND DATA_INCHIRIERE LIKE '{4}*' AND DATA_INCHEIERE LIKE '{5}*'", comboBox13.Text, comboBox12.Text, comboBox11.Text, comboBox17.Text, comboBox16.Text, comboBox15.Text);

                }
                else
                    dvSursa.RowFilter = String.Format("Convert(ID_APARTAMENT,'System.String') LIKE '{0}*' AND NUME LIKE '{1}*' AND PRENUME LIKE '{2}*' AND SERIE_NUMAR_CI LIKE '{3}*' AND CNP LIKE '{4}*' AND DATA_INCHIRIERE LIKE '{5}*' AND DATA_INCHEIERE LIKE '{6}*'", comboBox14.Text, comboBox13.Text, comboBox12.Text, comboBox11.Text, comboBox17.Text, comboBox16.Text, comboBox15.Text);
            }
            else if (comboBox5.Text == "Audit")
            {
                if (comboBox14.Text == "" && comboBox13.Text == "" && comboBox12.Text == "" && comboBox11.Text == "")
                    dvSursa.RowFilter = "";
                else if (comboBox13.Text == "")
                {
                    dvSursa.RowFilter = "";
                    dvSursa.RowFilter = String.Format("TIP_OPERATIE LIKE '{0}*' AND NUME_CONT LIKE '{1}*' AND DATA_TIMP LIKE '{2}*'", comboBox14.Text, comboBox12.Text, comboBox11.Text);
                }
                else
                    dvSursa.RowFilter = String.Format("TIP_OPERATIE LIKE '{0}*' AND Convert(TABEL_IMPLICAT,'System.String') LIKE '{1}*' AND NUME_CONT LIKE '{2}*' AND DATA_TIMP LIKE '{3}*'", comboBox14.Text, comboBox13.Text, comboBox12.Text, comboBox11.Text);
            }
            else if (comboBox5.Text == "Conturi")
            {
                dvSursa.RowFilter = String.Format("NUME_ROL LIKE '{0}*' AND NUME_USER LIKE '{1}*'", comboBox14.Text, comboBox13.Text);
            }
        }
    }
}
