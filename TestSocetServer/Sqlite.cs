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

        //Задает изначальные параметры базы данных и соединяет с ней
        public Sqlite(string name) {

            dbConnection = new SQLiteConnection();
            dbCommand = new SQLiteCommand();
            dbFileName = name;
            status = false;
            connection();
            
        }

        //создание соединения с базой данных
        private void connection() {
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            try {
                dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Vertion=1");
                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                status = true;
            }catch(SQLiteException ex){
                Console.WriteLine("Error" + ex);
            }
        }

        //Добавление пользователя и выдача ему счета
        public void addUser(string login, string hPassword) {
            if (dbConnection.State != ConnectionState.Open) {
                Console.WriteLine("LogI (addUser): Database closed!");
                return;
            }

            try {

                dbCommand.CommandText = string.Format("INSERT INTO Users ('Login', 'hPassword') values ('{0}', '{1}')",login, hPassword);
                dbCommand.ExecuteNonQuery();

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }

        //Добавление счета
        public void addAccount(long userID) {
            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return;
            }

            try
            {

                dbCommand.CommandText = string.Format("INSERT INTO Accounts ('UserID') values ('{0}')", userID);
                dbCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Получение списка пользователей
        public List<UserData> getUsers() {
            List<UserData> result = new List<UserData>();

            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return result;
            }

            try
            {
                dbCommand.CommandText = "SELECT * FROM Users";
                SQLiteDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new UserData(reader.GetInt32(0),reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


            return result;
        }

        //Получение списка счетов в банке
        public List<AccountData> getAccounts() {
            List<AccountData> result = new List<AccountData>();

            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return result;
            }

            try
            {
                dbCommand.CommandText = "SELECT * FROM Accounts";
                SQLiteDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new AccountData(reader.GetInt32(0), reader.GetInt32(2)));
                }
                reader.Close();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return result;
        }

        public int getUserIDByLogin(string login) {
            int result = -1;

            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return result;
            }

            try
            {
                dbCommand.CommandText = String.Format("SELECT ID FROM Users WHERE Login='{0}'", login);
                SQLiteDataReader reader = dbCommand.ExecuteReader();
                reader.Read();
                result = reader.GetInt32(0);
                reader.Close();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return result;
        }

        public void addBalance(int userID, int amount) {
            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return;
            }

            try
            {
                int balance = getBalance(userID);
                if (balance != -1)
                {
                    dbCommand.CommandText = string.Format("UPDATE Accounts SET Amount={0} WHERE UserID={1}", balance + amount, userID);
                    dbCommand.ExecuteNonQuery();
                }
                else {
                    Console.WriteLine("LogI (addBalance): Something go wrong!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int getBalance(int userID) {
            int result = -1;

            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return result;
            }

            try
            {
                dbCommand.CommandText = String.Format("SELECT Amount FROM Accounts WHERE UserID={0}", userID);
                SQLiteDataReader reader = dbCommand.ExecuteReader();
                reader.Read();
                result = reader.GetInt32(0);
                reader.Close();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return result;
        }

        public void transfer(int fromUserID, int toUserID, int amount) {
            if (dbConnection.State != ConnectionState.Open)
            {
                Console.WriteLine("LogI (addUser): Database closed!");
                return;
            }

            try
            {
                int fromBalance = getBalance(fromUserID);
                int toBalance = getBalance(toUserID);
                if (toBalance != -1 && fromBalance != -1)
                {
                    dbCommand.CommandText = string.Format("UPDATE Accounts SET Amount={0} WHERE UserID={1}", toBalance + amount, toUserID);
                    dbCommand.ExecuteNonQuery();
                    dbCommand.CommandText = string.Format("UPDATE Accounts SET Amount={0} WHERE UserID={1}", fromBalance + amount, fromUserID);
                    dbCommand.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("LogI (addBalance): Something go wrong!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}
