using System;
using System.Collections.Generic;
using System.Text;

namespace TestSocetServer
{
    class User
    {
        string login;
        string hashPassword;

        public User(string login, string hashPassword) {
            this.login = login;
            this.hashPassword = hashPassword;
        }

        public void addBalance() { 
        
        }

        public void getBalance() { 
        
        }

        public void transfer(long fromUserID, long toUserID, long amount) { 
            
        }


    }
}
