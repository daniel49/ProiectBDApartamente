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
                    for (int q = 0; q < t.Rows.Count; q++)
                    {
                        for (int n = 0; n < a; n++)
                            if(t.Rows.Count>0)
                                if (Int32.Parse(t.Rows[q][0].ToString()) == o[n])
                                {
                                    t.Rows[q].Delete();
                                    break;
                                }
                    }
                    t.AcceptChanges();
                }
                            //vreau sa afisez apartamentele ocupate
                else
                {
                    for (int q = 0; q < t.Rows.Count; q++)
                    {
                        bool ok = false;
                        for (int n = 0; n < a; n++)
                            if (Int32.Parse(t.Rows[q][0].ToString()) == o[n])
                                ok = true;
                        if (ok == false)    //daca id-ul este pt un apartament liber il sterg din lista
                        {
                            t.Rows[q].Delete();
                        }
                    }
                    t.AcceptChanges();
                }
                 if (Math.Max(inc_app, inc_add) >= 0)   //e degeaba
                {                                                                     //pt option = 1 : ma aflu in adrese
                    if (start == 1 && Math.Min(inc_add, inc_app) == 0)    //se afla ceva in filtre dar un filtru face sa nu corespunda nimic cu val din filtru
                    {
                        DataView filter = new DataView(y);
                        
                        if (h == "" && i != "")
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND Convert(ETAJ,'System.String') LIKE '{2}*'", f, g, i);
                        else if (i == "" && h != "")
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND Convert(SECTOR,'System.String') LIKE '{2}*'", f, g, h);
                        else if (h == "" && i == "")
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*'", f, g);
                        else
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND Convert(SECTOR,'System.String') LIKE '{2}*' AND Convert(ETAJ,'System.String') LIKE '{3}*'", f, g, h, i);
                        
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
                        
                        if(h=="" && i!="")
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND Convert(ETAJ,'System.String') LIKE '{2}*'", f, g, i);
                        else if (i == "" && h!="")
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND Convert(SECTOR,'System.String') LIKE '{2}*'", f, g, h);
                        else if (h=="" && i == "")
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*'", f, g);
                        else
                            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND Convert(SECTOR,'System.String') LIKE '{2}*' AND Convert(ETAJ,'System.String') LIKE '{3}*'", f, g, h, i);
                        
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
                                    break;
                                }
                    }
                    t.AcceptChanges();
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
                        }                      
                    }
                    t.AcceptChanges();
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
        








        public void GetClientFields(int id_client, ref int id_apartament, ref string nume, ref string prenume, ref string serie_numar_ci, ref string cnp, ref string data_inchiriere, ref string data_incheiere)
        {
            DataTable dt = new DataTable();
            dt = read_table("Clienti");
            dt.TableName = "Clienti";
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["ID_CLIENT"]) == id_client)
                {
                    if (dr["ID_APARTAMENT"] == DBNull.Value)
                        id_apartament = -1;
                    else id_apartament = Convert.ToInt32(dr["ID_APARTAMENT"]);
                    nume = Convert.ToString(dr["NUME"]);
                    prenume = Convert.ToString(dr["PRENUME"]);
                    serie_numar_ci = Convert.ToString(dr["SERIE_NUMAR_CI"]);
                    cnp = Convert.ToString(dr["CNP"]);
                    data_inchiriere = Convert.ToString(dr["DATA_INCHIRIERE"]);
                    data_incheiere = Convert.ToString(dr["DATA_INCHEIERE"]);
                    break;
                }
            }
        }
        public void GetApartmentFields(int id_apartament, ref int numar_camere, ref int suprafata_utila, ref int an_constructie, ref string facilitati, ref int pret_inchiriere)
        {
            DataTable dt = new DataTable();
            dt = read_table("Apartamente");
            dt.TableName = "Apartamente";

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["ID_APARTAMENT"]) == id_apartament)
                {
                    numar_camere = Convert.ToInt32(dr["NUMAR_CAMERE"]);
                    suprafata_utila = Convert.ToInt32(dr["SUPRAFATA_UTILA"]);
                    an_constructie = Convert.ToInt32(dr["AN_CONSTRUCTIE"]);
                    if (dr["FACILITATI"] == DBNull.Value)
                        facilitati = "-1";
                    else facilitati = Convert.ToString(dr["FACILITATI"]);
                    pret_inchiriere = Convert.ToInt32(dr["PRET_INCHIRIERE"]);
                    break;
                }
            }
        }
        public void GetAddressFields(int id_adresa, ref string oras, ref  string strada, ref string numar, ref string bloc, ref string scara, ref string etaj, ref string numar_apartament, ref string sector)
        {
            DataTable dt = new DataTable();
            dt = read_table("Adresa");
            dt.TableName = "Adrese";

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["ID_ADRESA"]) == id_adresa)
                {
                    oras = Convert.ToString(dr["ORAS"]);
                    strada = Convert.ToString(dr["STRADA"]);
                    numar = Convert.ToString(dr["NUMAR"]);
                    if (dr["BLOC"] == DBNull.Value)
                        bloc = "-1";
                    else bloc = Convert.ToString(dr["BLOC"]);
                    if (dr["SCARA"] == DBNull.Value)
                        scara = "-1";
                    else scara = Convert.ToString(dr["SCARA"]);
                    if (dr["ETAJ"] == DBNull.Value)
                        etaj = "-1";
                    else etaj = Convert.ToString(dr["ETAJ"]);
                    if (dr["NUMAR_APARTAMENT"] == DBNull.Value)
                        numar_apartament = "-1";
                    else numar_apartament = Convert.ToString(dr["NUMAR_APARTAMENT"]);
                    if (dr["SECTOR"] == DBNull.Value)
                        sector = "-1";
                    else sector = Convert.ToString(dr["SECTOR"]);
                    break;
                }
            }
        }
        public void ExportToExcel(DataGridView dgvInfo, string dataSource)
        {
            // Creez o aplicatie excel noua.
            Microsoft.Office.Interop.Excel._Application application = new Microsoft.Office.Interop.Excel.Application();
            // Creez un nou workbook in cadrul aplicatiei excel.
            Microsoft.Office.Interop.Excel._Workbook workbook = application.Workbooks.Add(Type.Missing);
            // Creez un worksheet nou.
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // Vedere asupra sheet-ului din spatele programului.
            application.Visible = true;
            // Preiau referinta primului sheet.Default,numele sheet-ului este Sheet1.
            // Stochez referinta in worksheet.
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // Schimb numele sheet-ului activ.
            worksheet.Name = "Exported from datagridview";
            // Formatez celulele ca fiind text.
            worksheet.Cells.NumberFormat = "@";
            worksheet.Cells.ColumnWidth = 20;
            // Stochez in celule(pe primul rand) numele coloanelor de tabel.
            for (int i = 1; i < dgvInfo.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgvInfo.Columns[i - 1].HeaderText;
                worksheet.Cells[1, i].Interior.ColorIndex = 40;
            }
            // Stochez fiecare valoare din tabel in cate o celula.
            for (int i = 0; i < dgvInfo.Rows.Count; i++)
            {
                for (int j = 0; j < dgvInfo.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = Convert.ToString(dgvInfo.Rows[i].Cells[j].Value);
                    worksheet.Cells[i + 2, j + 1].Interior.ColorIndex = 36;
                }
            }
            // Salvez aplicatia fara sa ma intrebe daca vreau sa suprascriu vre-un fisier existent.
            application.DisplayAlerts = false;
            string filename = dataSource + "EXCEL";
            workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Inchid aplicatia excel.
            application.Quit();
        }

        public void ExportToPDF(DataGridView dgvInfo, string dataSource)
        {
            try
            {
                string FirstLine = dataSource + " table exported to PDF";


                Document document = new Document(PageSize.A4.Rotate(), 30, 30, 20, 20);

                // Deschid fisierul pdf al meu.
                PdfWriter.GetInstance(document, new FileStream(dataSource + "PDF.pdf", FileMode.Append, FileAccess.Write));

                // Stabilesc fonturile.
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14.0F, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font tableFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12.0F, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Font headerfont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12.0F, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                // Creez un table pdf nou.
                PdfPTable PdfTable = new PdfPTable(dgvInfo.Columns.Count);
                PdfTable.TotalWidth = dgvInfo.Width;

                float[] widths = null;
                // Retin latimea coloanelor.
                if (dataSource == "Clienti")
                {
                    widths = new float[] { dgvInfo.Columns[0].Width, dgvInfo.Columns[1].Width, dgvInfo.Columns[2].Width, 
                                           dgvInfo.Columns[3].Width,dgvInfo.Columns[4].Width,dgvInfo.Columns[5].Width,dgvInfo.Columns[6].Width,dgvInfo.Columns[7].Width };
                }
                else if (dataSource == "ApartamenteLibere" || dataSource == "ApartamenteOcupate")
                {
                    widths = new float[] { dgvInfo.Columns[0].Width, dgvInfo.Columns[1].Width, dgvInfo.Columns[2].Width, 
                                           dgvInfo.Columns[3].Width,dgvInfo.Columns[4].Width,dgvInfo.Columns[5].Width,dgvInfo.Columns[6].Width };
                }
                else if (dataSource == "Adrese")
                {
                    widths = new float[] { dgvInfo.Columns[0].Width, dgvInfo.Columns[1].Width, dgvInfo.Columns[2].Width, 
                                           dgvInfo.Columns[3].Width,dgvInfo.Columns[4].Width,dgvInfo.Columns[5].Width,dgvInfo.Columns[6].Width,dgvInfo.Columns[7].Width,dgvInfo.Columns[8].Width };
                }
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1; // 0 - left, 1 - center, 2 - right;
                PdfTable.SpacingBefore = 2.0F;


                PdfPCell CurrentCell = null;

                document.Open();
                Phrase phrase = new Phrase(new Chunk(FirstLine, titleFont));
                document.Add(phrase);

                // Adaug numele coloanelor
                foreach (DataGridViewColumn c in dgvInfo.Columns)
                {
                    CurrentCell = new PdfPCell(new Phrase(new Chunk(c.HeaderText, headerfont)));
                    CurrentCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CurrentCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    CurrentCell.BackgroundColor = BaseColor.GREEN;
                    PdfTable.AddCell(CurrentCell);
                }

                // Adaug valorile de pe randurile tabelului.
                if (dgvInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvInfo.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvInfo.Columns.Count; j++)
                        {
                            CurrentCell = new PdfPCell(new Phrase(Convert.ToString(dgvInfo.Rows[i].Cells[j].Value), tableFont));
                            CurrentCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CurrentCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            PdfTable.AddCell(CurrentCell);
                        }
                    }
                }
                // Adaug tabelul pdf creat in fisier si apoi il inchid.
                document.Add(PdfTable);
                document.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "Error Generating PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable GetTablesWithRelationships(string spName, SqlParameter[] spc)
        {
            DataTable dt = new DataTable();
            dt = dl.ExecuteReader_StoredProcedure(spName, spc);
            return dt;
        }
        public int GetApartmentIdByAddressId(int id_adresa)
        {
            DataTable dt = null;
            dt = dl.ExecuteReader_GetTable("Apartamente");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["ID_ADRESA"]) == id_adresa)
                {
                    return Convert.ToInt32(dr["ID_APARTAMENT"]);
                }
            }
            return -1;
        }
        public int GetLastAddress()
        {
            DataTable dt = null;
            dt = dl.ExecuteReader_GetTable("Adresa");
            return Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ID_ADRESA"]);
        }
    }
}