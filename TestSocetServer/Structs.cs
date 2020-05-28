using System;
using System.Collections.Generic;
using System.Text;

namespace TestSocetServer
{
    struct UserData {
        public int userID;
        public string login;
        public string hPassword;

        public UserData(int userID, string login, string hPassword)
        {
            this.userID = userID;
            this.login = login;
            this.hPassword = hPassword;
        }

    }

    struct AccountData {
        public int userID;
        public int accountID;

        public AccountData(int userID, int accountID)
        {
            this.userID = userID;
            this.accountID = accountID;
        }
    }
}
