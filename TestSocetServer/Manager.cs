using System;
using System.Collections.Generic;
using System.Text;

namespace TestSocetServer
{
    class Manager
    {
        private Sqlite sData;
        private List<UserData> uData;
        private List<AccountData> aData;

        public Manager() {
            sData = new Sqlite("bank.db");
            uData = sData.getUsers();
            aData = sData.getAccounts();
        }

        public void register(string login, string hPassword) {

            bool exist = false;

            foreach (UserData user in uData) {
                if (user.login == login) {
                    exist = true; 
                }
            }

            if (!exist) {
                sData.addUser(login, hPassword);
            }

            refresh();

        }

        public int login(string login, string hPassword) {

            foreach (UserData user in uData)
            {
                if (user.login == login && user.hPassword == hPassword)
                {
                    return user.userID;
                }
            }

            return -1;

        }

        public void addBalance(int userID, int amount) {
            if (amount < 0) {
                return;
            }

            sData.addBalance(userID, amount);
            refresh();
        }

        public int getBalance(int userID) {
            int result = -1;
            result = sData.getBalance(userID);
            return result;
        }

        public void transfer(int fromUserID, int toUserID, int amount) {
            if (amount < 0) {
                return;
            }

            sData.transfer(fromUserID, toUserID, amount);
        }

        public void refresh() {
            uData = sData.getUsers();
            aData = sData.getAccounts();
        }

    }
}
