using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenditaInventario
{
    public class DatabaseManager
    {
        private static SQLiteConnection sqlite_conn = null;

        public DatabaseManager()
        {
        }

        public static SQLiteConnection getConnection()
        {
            if (sqlite_conn != null)
            {
                if (sqlite_conn.State == ConnectionState.Open)
                {
                    sqlite_conn.Close();
                }
            }
            else
            {
                sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;foreign keys=true;auto_vacuum=FULL;secure_delete=true;Version= 3;");
            }

            sqlite_conn.Open();
            return sqlite_conn;
        }
    }
}
