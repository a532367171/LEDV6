using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LedControlSystem
{
    public static class DbUtils
    {
        private static string cs = "server=47.100.126.101;uid=sa;pwd=905E27CF6F8CCF0CF5259F9B3DCE1F;database=rsmonitor";

        public static int Insert(object o)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(DbUtils.cs))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    result = System.Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return result;
        }

        public static int ExecuteNonQuerySp(string sp)
        {
            int affectedRows = 0;
            using (SqlConnection conn = new SqlConnection(DbUtils.cs))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 5;
                    try
                    {
                        cmd.CommandText = sp;
                        conn.Open();
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            return affectedRows;
        }

        public static DataTable ExecuteDataTable(string sp)
        {
            DataTable result;
            using (SqlConnection conn = new SqlConnection(DbUtils.cs))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sp;
                cmd.CommandTimeout = 5;
                DataTable ds = new DataTable();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                result = ds;
            }
            return result;
        }
    }
}
