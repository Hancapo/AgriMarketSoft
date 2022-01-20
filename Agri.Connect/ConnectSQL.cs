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
        static private (string, bool, string) LoadCredentials()
        {
            string filepath = "config.ini";
            string _server = File.ReadAllLines(filepath)[0].Split('=')[1].Trim();
            bool _TC = bool.Parse(File.ReadAllLines(filepath)[1].Split('=')[1].Trim());
            string _db = File.ReadAllLines(filepath)[2].Split('=')[1].Trim();   


            return (_server, _TC, _db);
        }

        private static string Server_ = LoadCredentials().Item1;
        private static bool TC_ = LoadCredentials().Item2;
        private static string DB_ = LoadCredentials().Item3;

        static private SqlConnection sqlConnection = new($"Data Source={Server_};Initial Catalog={DB_};Integrated Security={TC_.ToString().ToLowerInvariant()}");
        static private SqlCommand sqlCommand = sqlConnection.CreateCommand();

        private SqlConnection SqlConnection { get => sqlConnection; set => sqlConnection = value; }
        private SqlCommand SqlCommand { get => sqlCommand; set => sqlCommand = value; }


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

        public object RunSqlExecuteScalar(string sqlcommand)
        {
            object newobj = null;

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
