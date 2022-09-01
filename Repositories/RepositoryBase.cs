using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace MoreniApp.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly static string DATABASE = "MoreniDB.accdb";
        private readonly string strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=w:\\db\\" + DATABASE + ";Persist Security Info=False";
        
        private static OleDbConnection Conn = new OleDbConnection();
        private static string _error;
        public RepositoryBase()
        {
        }

        public string Error 
        { 
            get => _error; 
            set => _error = value; 
        }

        protected OleDbConnection GetConnection()
        {
            try
            {
                if (Conn == null) // si la coneccion no existe, se crea y se abre.
                {
                    Conn = new OleDbConnection(strconn);
                    Conn.Open();
                }
                else if (Conn.State == ConnectionState.Closed) // Si la coneccion esta cerrada, se abre.
                {
                    Conn.ConnectionString = strconn;
                    Conn.Open();
                }
                return Conn;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
    }
}
