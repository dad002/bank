using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace TestSocetServer
{
    class Sqlite
    {
        private string dbFileName;
        private SQLiteConnection dbConnection;
        private SQLiteCommand dbCommand;
        private bool status;
        public Sqlite(string name) {

            dbConnection = new SQLiteConnection();
            dbCommand = new SQLiteCommand();
            dbFileName = name;
            status = false;
            connection();
            
        }

        private void connection() {
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            try {
                dbConnection = new SQLiteConnection("Data source=" + dbFileName + ";Version 1;");
                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                status = true;
            }catch(SQLiteException ex){
                Console.WriteLine("Error" + ex);
            }
        }

        public bool checkConnection() {
            return status;
        }
    }
}
