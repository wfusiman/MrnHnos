using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace MoreniApp.Model
{
    class DB
    {
        readonly private static string DATABASE = "MoreniDB.accdb";
        readonly private static string strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=w:\\db\\" + DATABASE + ";Persist Security Info=False";
        //private static readonly string strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\\data\\db\\" + DATABASE + ";Persist Security Info=False";
        private static OleDbConnection conn;

        private static string error;

        // Cierra la coneccion a la base de datos
        public static void Close()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn.Close();
            }
        }

        public static OleDbConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new OleDbConnection(strconn);
                try
                {
                    conn.Open();
                    return conn;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
            else
            {
                return conn;
            }
        }

        public static string GetError()
        {
            return error;
        }

        public static void SetError( string err )
        {
            error = err;
        }
    }
}
