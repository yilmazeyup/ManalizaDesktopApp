using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManalizaDesktopApp.Services
{
    class dbSqliteConnection
    {
        public static string dbAddress = @"Data Source=" +"C:/Dr/MnKantar_Ertepe/Database/Ertepe_kantar_db;New=False;Compress=True;Read Only=False";        
        public static string ConnectionStatus;
        public static void ConnectionTest()
        {
            using (SQLiteConnection conn = new SQLiteConnection(dbAddress))
            {
                if(conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                        ConnectionStatus = "SQLite Connection successful";
                    }
                    catch(Exception)
                    {
                        ConnectionStatus = "SQLite Connection failed";
                    }
                }
                else
                {
                    ConnectionStatus = "SQLite Connection successful";
                }
            }

        }
    }
}
