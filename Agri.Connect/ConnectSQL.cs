using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agri.Connect
{
    public class ConnectSQL
    {
        static public (string, string, string) LoadCredentials()
        {
            string filepath = "config.ini";
            string _user = File.ReadAllLines(filepath)[0].Split('=')[1].Trim();
            string _pwd = File.ReadAllLines(filepath)[1].Split('=')[1].Trim();
            string _db = File.ReadAllLines(filepath)[2].Split('=')[1].Trim();


            return (_user, _pwd, _db);
        }

        static string user = LoadCredentials().Item1;
        static string pwd = LoadCredentials().Item2;
        static string db = LoadCredentials().Item3;

        static SqlConnection sqlConnection = new("User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";");
        static SqlCommand sqlCommand = sqlConnection.CreateCommand();

        public SqlConnection SqlConnection { get => sqlConnection; set => sqlConnection = value; }
        public SqlCommand SqlCommand { get => sqlCommand; set => sqlCommand = value; }


        public bool CheckDatabase()
        {
            if (SqlConnection.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                try
                {
                    SqlConnection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public DataTable SqltoDataTable(string sqlcommand)
        {
            DataTable dt = new();

            if (CheckDatabase())
            {
                SqlDataReader sdr = RunSqlExecuteReader(sqlcommand);
                dt.Load(sdr);
                return dt;
            }

            return dt;
        }

        public object? RunSqlExecuteScalar(string sqlcommand)
        {
            object? newobj = null;

            if (CheckDatabase())
            {
                try
                {
                    SqlCommand.CommandText = sqlcommand;
                    newobj = SqlCommand.ExecuteScalar();
                }
                catch 
                {
                    return null;
                   
                }
            }

            return newobj;  
        }

        private SqlDataReader RunSqlExecuteReader(string sqlcommand)
        {
            if (CheckDatabase())
            {
                SqlCommand.CommandText = sqlcommand;
                SqlDataReader sdr = SqlCommand.ExecuteReader();
                return sdr;
            }

            return null;
        }

        public void RunSqlNonQuery(string sqlcommand)
        {
            if (CheckDatabase())
            {
                SqlCommand.CommandText = sqlcommand;
                SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
