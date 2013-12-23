using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProiectBD_InchiriereApartamente
{
    class DataLayer
    {
        public DataTable ExecuteReader_GetTable(string TableName)
        {
            try
            {
                DataTable dt = new DataTable();
                var connectionString = ConfigurationManager.ConnectionStrings["Apartamente"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction tran = connection.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {

                            using (SqlCommand command = connection.CreateCommand())
                            {
                                command.Transaction = tran;
                                command.CommandText = "SELECT * FROM " + TableName;
                                SqlDataReader dataReader = command.ExecuteReader();
                                dt.Load(dataReader);
                                return dt;
                            }

                        }
                        catch (Exception xcp)
                        {
                            tran.Rollback();
                            MessageBox.Show("Something went wrong!Operation aborted");
                            throw new Exception();
                            return null;
                        }
                    }

                }
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("No data source specified!");
                return null;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Connection Error!");
                return null;
            }
        }

        public DataTable ExecuteReader_StoredProcedure(string spName, SqlParameter[] spc)
        {
            try
            {
                DataTable dt = new DataTable();
                var connectionString = ConfigurationManager.ConnectionStrings["Apartamente"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;
                        //atasez parametrii la comanda
                        if (spc != null)
                        {
                            command.Parameters.AddRange(spc);
                        }
                        SqlDataReader dataReader = command.ExecuteReader();
                        dt.Load(dataReader);
                        return dt;
                    }

                }
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("No data source specified!");
                return null;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Connection Error!");
                return null;
            }
        }
        public int ExecuteNonQuery_StoredProcedure_CSharpTransaction(string spName, SqlParameter[] spc)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["Apartamente"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction tran = connection.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {

                            using (SqlCommand command = connection.CreateCommand())
                            {
                                int affectedRows;
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = tran;
                                command.CommandText = spName;
                                if (spc != null)
                                    command.Parameters.AddRange(spc);
                                affectedRows = command.ExecuteNonQuery();
                                tran.Commit();
                                return affectedRows;
                            }

                        }
                        catch (Exception xcp)
                        {
                            tran.Rollback();
                            MessageBox.Show("Something went wrong!Operation aborted");
                            throw new Exception();
                            return -1;
                        }
                    }

                }
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("No data source specified!");
                throw new Exception();
                return -1;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Connection Error!");
                throw new Exception();
                return -1;
            }
        }
        public int ExecuteNonQuery_StoredProcedure_SqlTransaction(string spName, SqlParameter[] spc)
        {
            try
            {
                DataTable dt = new DataTable();
                var connectionString = ConfigurationManager.ConnectionStrings["Apartamente"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;
                        //atasez parametrii la comanda
                        if (spc != null)
                        {
                            command.Parameters.AddRange(spc);
                        }
                        int rows_affected = command.ExecuteNonQuery();
                        return rows_affected;
                    }

                }
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("No data source specified!");
                throw new Exception();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Connection Error!");
                throw new Exception();
            }
        }


    }
}
