using System;
using System.Collections.Generic;
using System.Text;

namespace TestSocetServer
{
    struct UserData {
        public string login;
        public string hPassword;

        public UserData(string login, string hPassword)
        {
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
