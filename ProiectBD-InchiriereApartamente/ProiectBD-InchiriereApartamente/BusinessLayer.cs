using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ProiectBD_InchiriereApartamente
{
    class BusinessLayer
    {
        int[] filter_adress = new int[200];       //idu-rile de la adrese retinute dupa filtrarea
        int inc_add;  //nr de elemente din vectorul filter_adress
       int[] filter_apartament = new int[200];    
        int inc_app;
        public  int start;
        DataLayer dl = new DataLayer();

        public string Encrypt(string password)
        {
            int count = 0;
            byte temp1 = Convert.ToByte(0);
            byte temp2 = Convert.ToByte(0);
            string output = "";
            byte[] toBytes = new byte[password.Length];
            toBytes = Encoding.Default.GetBytes(password);
            foreach (byte bte in toBytes)
            {
                temp1 = Convert.ToByte((bte << 5) & 0xFF);
                temp2 = Convert.ToByte((bte >> 3) & 0xFF);
                toBytes[count] = (byte)Convert.ToByte(temp1 + temp2);
                // Scriu byte-ul in fisier,pe prima linie.
                count++;
                // Trec la urmatorul byte
            }
            output = Encoding.Default.GetString(toBytes);
            return output;
        }

        public string Decrypt(string password)
        {
            int count = 0;
            byte temp1 = Convert.ToByte(0);
            byte temp2 = Convert.ToByte(0);
            string output;
            byte[] toBytes = new byte[password.Length];
            toBytes = Encoding.Default.GetBytes(password);
            foreach (byte bte in toBytes)
            {
                temp1 = Convert.ToByte((bte >> 5) & 0xFF);
                temp2 = Convert.ToByte((bte << 3) & 0xFF);
                toBytes[count] = (byte)Convert.ToByte(temp1 + temp2); ;
                count++;
            }
            output = Encoding.Default.GetString(toBytes);
            return output;
        }

        public int DataBaseOperation(string spName, SqlParameter[] spc)
        {
            int affectedRows;
            affectedRows = dl.ExecuteNonQuery_StoredProcedure_CSharpTransaction(spName, spc);
            return affectedRows;
        }

        public void CheckIfValidLogin(string username, string password, ref int userType)
        {
            DataTable dt = null;
            dt = dl.ExecuteReader_GetTable("Conturi");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["NUME_USER"]) == username && Convert.ToString(dr["PAROLA"]) == this.Encrypt(password))
                {
                    userType = Convert.ToInt32(dr["ID_ROL"]);
                    break;
                }
            }
        }
        public void RetrieveAccountDetails(string username, ref int account_id, ref int role_id)
        {
            DataTable dt = dl.ExecuteReader_GetTable("Conturi");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["NUME_USER"]) == username)
                {
                    account_id = Convert.ToInt32(dr["ID_CONT"]);
                    role_id = Convert.ToInt32(dr["ID_ROL"]);
                    return;
                }
            }
        }
        public bool CheckIfUsernameTaken(string username)
        {
            DataTable dt = null;
            dt = dl.ExecuteReader_GetTable("Conturi");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["NUME_USER"]) == username)
                {
                    return true;
                }
            }
            return false;
        }
        public void ReportLoginToAudit(string username)
        {
            string date_time = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
            int affectedRows;
            SqlParameter[] spc = new SqlParameter[3];
            for (int i = 0; i < spc.Length; i++)
                spc[i] = new SqlParameter();
            spc[0].ParameterName = "@TIP_OPERATIE";
            spc[0].SqlDbType = SqlDbType.VarChar;
            spc[0].Value = "Logare";
            spc[0].Direction = ParameterDirection.Input;
            spc[1].ParameterName = "@NUME_CONT";
            spc[1].SqlDbType = SqlDbType.VarChar;
            spc[1].Value = username;
            spc[1].Direction = ParameterDirection.Input;
            spc[2].ParameterName = "@DATA_TIMP";
            spc[2].SqlDbType = SqlDbType.VarChar;
            spc[2].Value = date_time;
            spc[2].Direction = ParameterDirection.Input;
            affectedRows = dl.ExecuteNonQuery_StoredProcedure_SqlTransaction("ReportLoginToAudit", spc);
        }
        public DataTable read_table(string nume)
        {
            DataTable m = new DataTable();
            m = dl.ExecuteReader_GetTable(nume);
            return m;
        }

        //intru in functia asta de fiecare data cand se modifca textul din filtre
        public void filter_datagridview(DataGridView m, string f, string g, string h, string i, string h1, string h2, string h3, string h4, string h5, bool ocupat, int option, int start,int rsd)
        {
            if (option == 1)    //tabel adrese
            {
                DataTable y = new DataTable();
                DataTable u = new DataTable();
                DataTable t = new DataTable();
                t = read_table("dbo.Apartamente");
                u = read_table("dbo.Clienti");
                y = read_table("dbo.Adresa");
                int j = y.Rows.Count;
                int w = u.Rows.Count;
                int r = t.Rows.Count;

                //Formez lista de apartamente ocupate
                int a = 0;  //nr de id-uri comune
                int[] o = new int[200]; 
                for (int k = 0; k < r; k++) //indicele randului din apartamente
                {
                    for (int l = 0; l < w; l++) //indicele randului din Clienti
                        if (u.Rows[l][1] != DBNull.Value)
                        {
                            if (Int32.Parse(t.Rows[k][0].ToString()) == Int32.Parse(u.Rows[l][1].ToString()))
                            {
                                o[a] = new int();
                                o[a] = Int32.Parse(t.Rows[k][0].ToString());
                                a++;
                                break;
                            }
                        }
                }
                if (ocupat == false)    //afisez apartamentele libere
                {
                    //parcurg lista de apartamente ocupate,si afisez apartamentele libere
                    for (int q = 0; q < y.Rows.Count; q++)
                    {
                        for (int n = 0; n < a; n++)
                            if(y.Rows.Count>0)
                                if (Int32.Parse(y.Rows[q][0].ToString()) == o[n])
                                {
                                    y.Rows[q].Delete();
                                    y.AcceptChanges();
                                }
                    }
                }
                            //vreau sa afisez apartamentele ocupate
                else
                {
                    for (int q = 0; q < y.Rows.Count; q++)
                    {
                        bool ok = false;
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(y.Rows[q][0].ToString()) == o[n])
                                ok = true;
                        if (ok == false)    //daca id-ul este pt un apartament liber il sterg din lista
                        {
                            y.Rows[q].Delete();
                            y.AcceptChanges();
                        }
                    }
                }
                 if (Math.Max(inc_app, inc_add) >= 0)   //e degeaba
                {                                                                     //pt option = 1 : ma aflu in adrese
                    if (start == 1 && Math.Min(inc_add, inc_app) == 0)    //se afla ceva in filtre dar un filtru face sa nu corespunda nimic cu val din filtru
                    {
                        DataView filter = new DataView(y);
                        filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND SECTOR LIKE '{2}*' AND ETAJ LIKE '{3}*'", f, g, h, i);
                        m.DataSource = filter;
                        int e = m.Rows.Count;
                        int inc = 0;
                        for (int l = 0; l < 200; l++)
                            filter_adress[l] = 0;
                        for (int k = 0; k < e; k++)
                        {
                            filter_adress[inc] = new int();
                            filter_adress[inc] = Int32.Parse(m.Rows[k].Cells[0].Value.ToString());
                            inc++;
                        }
                        inc_add = inc;
                        m.DataSource = null;
                    }
                    else
                    {
                        start = 1;   

                        
                        for (int k = 0; k < inc_add; k++)
                        {
                            bool decise = false;
                            for (int l = 0; l < inc_app; l++)
                                if (filter_adress[k] == filter_apartament[l])
                                    decise = true;
                            if (decise == false)
                            {
                                int p = y.Rows.Count;
                                for (int aa = 0; aa < y.Rows.Count; aa++)
                                {
                                    try
                                    {
                                        if (Int32.Parse(y.Rows[aa][0].ToString()) == filter_adress[k])
                                        {

                                            y.Rows[aa].Delete();
                                            y.AcceptChanges();
                                            j--;
                                            aa--;
                                        }
                                        
                                            
                                    }
                                    catch (Exception b)
                                    {
                                        continue;
                                    }
                                }
                            }

                        }
                        if (rsd == 1)
                        {
                            int pp = y.Rows.Count;
                            for (int aa = 0; aa < y.Rows.Count; aa++)
                            {
                                bool sterg = false;
                                try
                                {
                                    for (int tt = 0; tt < inc_app; tt++)
                                        if (Int32.Parse(y.Rows[aa][0].ToString()) == filter_apartament[tt])
                                        {
                                            sterg = true;
                                        }
                                    if (sterg == false)
                                    {
                                        y.Rows[aa].Delete();
                                        y.AcceptChanges();
                                        j--;
                                        aa--;
                                    }
                                }
                                catch (Exception uy)
                                {
                                    continue;
                                }
                            }
                        }
                        DataView filter = new DataView(y);
                        filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND SECTOR LIKE '{2}*' AND ETAJ LIKE '{3}*'", f, g, h, i);
                        m.DataSource = filter;
                        int e = m.Rows.Count;
                        int inc = 0;
                        for (int l = 0; l < 200; l++)
                            filter_adress[l] = 0;
                        for (int k = 0; k < e; k++)
                        {
                            filter_adress[inc] = new int();
                            filter_adress[inc] = Int32.Parse(m.Rows[k].Cells[0].Value.ToString());
                            inc++;
                        }
                        inc_add = inc;
                    }
               
                    }
            }
            if (option == 0)
            {
                DataTable y = new DataTable();
                DataTable u = new DataTable();
                DataTable t = new DataTable();
                t = read_table("dbo.Apartamente");
                u = read_table("dbo.Clienti");
                y = read_table("dbo.Adresa");
                int j = y.Rows.Count;
                int w = u.Rows.Count;
                int r = t.Rows.Count;

                int a = 0;
                int[] o = new int[200];
                for (int k = 0; k < r; k++)
                {
                    for (int l = 0; l < w; l++)
                        if (u.Rows[l][1] != DBNull.Value)
                        {
                            if (Int32.Parse(t.Rows[k][0].ToString()) == Int32.Parse(u.Rows[l][1].ToString()))
                            {
                                o[a] = new int();
                                o[a] = Int32.Parse(t.Rows[k][0].ToString());
                                a++;
                                break;
                            }
                        }
                }
                if (ocupat == false)
                {
                    for (int q = 0; q < t.Rows.Count; q++)
                    {
                        for (int n = 0; n < a; n++)
                            if(t.Rows.Count>0)
                                if (Int32.Parse(t.Rows[q][0].ToString()) == o[n])
                                {
                                    t.Rows[q].Delete();
                                    t.AcceptChanges();
                                }
                    }
                }
                else
                {
                    for (int q = 0; q < t.Rows.Count; q++)
                    {
                        bool ok = false;
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(t.Rows[q][0].ToString()) == o[n])
                                ok = true;
                        if (ok == false)
                        {
                            t.Rows[q].Delete();
                            t.AcceptChanges();
                        }

                    }
                }
                if (Math.Max(inc_app, inc_add) >= 0)
                {
                    if (start == 1 && Math.Min(inc_add, inc_app) == 0)
                    {

                        DataView filter = new DataView(t);
                        if (h5 != "")
                        {
                            if (h4 == ">")
                                filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') > Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            if (h4 == "<")
                                filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') < Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            else if (h4 == "=")
                                filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') = Convert('{3}','System.Int32')", h1, h2, h3, h5);
                        }
                        else
                        {
                            filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*'", Convert.ToString(h1), Convert.ToString(h2), Convert.ToString(h3));

                        }

                        m.DataSource = filter;
                        int inc = 0;
                        int e = m.Rows.Count;
                        for (int l = 0; l < 200; l++)
                            filter_apartament[l] = 0;
                        for (int k = 0; k < e; k++)
                        {
                            filter_apartament[inc] = new int();
                            filter_apartament[inc] = Int32.Parse(m.Rows[k].Cells[1].Value.ToString());
                            inc++;
                        }
                        inc_app = inc;
                        m.DataSource = null;
                    }
                    else
                    {
                        start = 1;
                        
                        for (int k = 0; k < inc_app; k++)
                        {
                            bool decise = false;            //exista elemente comune - true , nu exista - false
                            for (int l = 0; l < inc_add; l++)
                                if (filter_apartament[k] == filter_adress[l])
                                    decise = true;
                            if (decise == false)
                            {

                                int p = t.Rows.Count;
                                for (int aa = 0; aa < t.Rows.Count; aa++)
                                {
                                    try
                                    {
                                        if (Int32.Parse(t.Rows[aa][1].ToString()) == filter_apartament[k])
                                        {

                                            t.Rows[aa].Delete();
                                            t.AcceptChanges();
                                            j--;
                                            aa--;
                                        }
                                    }
                                    catch (Exception b)
                                    {
                                        continue;
                                    }
                                }
                            }


                        }
                        if (rsd == 1)   //exista id-uri care au fost filtrate,dar nu au fost scoase din vector
                        {
                            int pp = t.Rows.Count;
                            for (int aa = 0; aa < t.Rows.Count; aa++)
                            {
                                bool sterg = false;
                                try
                                {
                                    for (int tt = 0; tt < inc_add; tt++)
                                        if (Int32.Parse(t.Rows[aa][1].ToString()) == filter_adress[tt])
                                        {
                                            sterg = true;
                                        }
                                    if (sterg == false)
                                    {
                                        t.Rows[aa].Delete();
                                        t.AcceptChanges();
                                        j--;
                                        aa--;
                                    }
                                }
                                catch (Exception uy)
                                {
                                    continue;
                                }
                            }
                        }
                      
                        DataView filter = new DataView(t);
                        if (h5 != "")
                        {
                            if (h4 == ">")
                                filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') > Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            if (h4 == "<")
                                filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') < Convert('{3}','System.Int32')", h1, h2, h3, h5);

                            else if (h4 == "=")
                                filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') = Convert('{3}','System.Int32')", h1, h2, h3, h5);
                        }
                        else
                        {
                            filter.RowFilter = string.Format("Convert(NUMAR_CAMERE,'System.String') LIKE '{0}*' AND Convert(SUPRAFATA_UTILA,'System.String') LIKE '{1}*' AND Convert(AN_CONSTRUCTIE,'System.String') LIKE '{2}*'", Convert.ToString(h1), Convert.ToString(h2), Convert.ToString(h3));
                        }

                        m.DataSource = filter;
                        int inc = 0;
                        int e = m.Rows.Count;
                        for (int l = 0; l < 200; l++)
                            filter_apartament[l] = 0;
                        for (int k = 0; k < e; k++)
                        {
                            filter_apartament[inc] = new int();
                            filter_apartament[inc] = Int32.Parse(m.Rows[k].Cells[1].Value.ToString());
                            inc++;
                        }
                        inc_app = inc;

                    }

                }
            }
        }
    }
}