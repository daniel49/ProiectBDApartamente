using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProiectBD_InchiriereApartamente
{
    class BusinessLayer
    {
      static  int[] filter_adress = new int[200];
      static  int inc_add;
      static int[] filter_apartament = new int[200];
       static int inc_app;
        public static int start;
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

        public void filter_datagridview(DataGridView m, string f, string g, string h, string i, string h1, string h2, string h3, string h4, string h5, bool oc, int option, int start,int rsd)
        {
            if (option == 1)
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
                        if (Int32.Parse(t.Rows[k][0].ToString()) == Int32.Parse(u.Rows[l][1].ToString()))
                        {
                            o[a] = new int();
                            o[a] = Int32.Parse(t.Rows[k][1].ToString());
                            a++;
                        }
                }
                if (oc == false)
                {
                    for (int q = 0; q < j; q++)
                    {
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(y.Rows[q][0].ToString()) == o[n])
                                y.Rows[q].Delete();
                    }
                }
                else
                {
                    for (int q = 0; q < j; q++)
                    {
                        bool ok = false;
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(y.Rows[q][0].ToString()) == o[n])
                                ok = true;
                        if (ok == false)
                            y.Rows[q].Delete();
                    }
                }
                 if (Math.Max(inc_app, inc_add) >= 0)
                {
                    if (BusinessLayer.start == 1 && Math.Min(inc_add, inc_app) == 0)
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
                        BusinessLayer.start = 1;

                        
                        for (int k = 0; k < inc_add; k++)
                        {
                            bool decise = false;
                            for (int l = 0; l < inc_app; l++)
                                if (filter_adress[k] == filter_apartament[l])
                                    decise = true;
                            if (decise == false)
                            {
                                int p = y.Rows.Count;
                                for (int aa = 0; aa < p; aa++)
                                {
                                    try
                                    {
                                        if (Int32.Parse(y.Rows[aa][0].ToString()) == filter_adress[k])
                                        {

                                            y.Rows[aa].Delete();
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
                            for (int aa = 0; aa < pp; aa++)
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
                        if (Int32.Parse(t.Rows[k][0].ToString()) == Int32.Parse(u.Rows[l][1].ToString()))
                        {
                            o[a] = new int();
                            o[a] = Int32.Parse(t.Rows[k][0].ToString());
                            a++;
                        }
                }
                if (oc == false)
                {
                    for (int q = 0; q < j; q++)
                    {
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(t.Rows[q][0].ToString()) == o[n])
                                t.Rows[q].Delete();

                    }
                }
                else
                {
                    for (int q = 0; q < j; q++)
                    {
                        bool ok = false;
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(t.Rows[q][0].ToString()) == o[n])
                                ok = true;
                        if (ok == false)
                            t.Rows[q].Delete();

                    }
                }
                if (Math.Max(inc_app, inc_add) >= 0)
                {
                    if (BusinessLayer.start == 1 && Math.Min(inc_add, inc_app) == 0)
                    {

                        DataView filter = new DataView(t);
                        if (h5 != "")
                        {
                            if (h4 == ">")
                                filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*' AND   Convert(PRET_INCHIRIERE,'System.Int32') > Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            if (h4 == "<")
                                filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') < Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            else if (h4 == "=")
                                filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') = Convert('{3}','System.Int32')", h1, h2, h3, h5);
                        }
                        else
                        {
                            filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*'", h1, h2, h3);
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
                        BusinessLayer.start = 1;
                        
                        for (int k = 0; k < inc_app; k++)
                        {
                            bool decise = false;
                            for (int l = 0; l < inc_add; l++)
                                if (filter_apartament[k] == filter_adress[l])
                                    decise = true;
                            if (decise == false)
                            {

                                int p = t.Rows.Count;
                                for (int aa = 0; aa < p; aa++)
                                {
                                    try
                                    {
                                        if (Int32.Parse(t.Rows[aa][1].ToString()) == filter_apartament[k])
                                        {

                                            t.Rows[aa].Delete();
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
                            int pp = t.Rows.Count;
                            for (int aa = 0; aa < pp; aa++)
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
                                filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*' AND   Convert(PRET_INCHIRIERE,'System.Int32') > Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            if (h4 == "<")
                                filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') < Convert('{3}','System.Int32')", h1, h2, h3, h5);
                            else if (h4 == "=")
                                filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*' AND Convert(PRET_INCHIRIERE,'System.Int32') = Convert('{3}','System.Int32')", h1, h2, h3, h5);
                        }
                        else
                        {
                            filter.RowFilter = string.Format("NUMAR_CAMERE LIKE '{0}*' AND SUPRAFATA_UTILA LIKE '{1}*' AND AN_CONSTRUCTIE LIKE '{2}*'", h1, h2, h3);
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
