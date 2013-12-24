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

        public void CheckIfValidLogin(string username, string password,ref int userType)
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

        public void filter_datagridview(DataGridView m,string f,string g,string h,string i,bool oc)
        {
            DataTable y = new DataTable();
            DataTable u = new DataTable();
            DataTable t=new DataTable();
            t=read_table("dbo.Apartamente");
            u = read_table("dbo.Clienti");
            y = read_table("dbo.Adresa");
            int j = y.Rows.Count;
            int w = u.Rows.Count;
            int r=t.Rows.Count;
           
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
                        if(ok==false)
                            y.Rows[q].Delete();
                    }
                }
          
            
            DataView filter = new DataView(y);
            filter.RowFilter = string.Format("ORAS LIKE '{0}*' AND STRADA LIKE '{1}*' AND SECTOR LIKE '{2}*' AND ETAJ LIKE '{3}*'", f, g, h, i);
            m.DataSource = filter;
            
        }
    }
}
