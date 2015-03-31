using SteelWire.Business.Database;
using System;
using System.Linq;

namespace SteelWire.Business.DbOperator
{
    public static class UserOperator
    {
        public static bool Exist(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException("account");
            }
            SteelWireContext dbContext = new SteelWireContext();
            return dbContext.SecurityUser.Any(u => u.Account == account);
        }

        public static SecurityUser Regist(SecurityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrEmpty(user.Account))
            {
                throw new ArgumentNullException("user.Account");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException("user.Password");
            }
            using (SteelWireContext dbContext = new SteelWireContext())
            {
                dbContext.SecurityUser.Add(user);
                dbContext.SaveChanges();
            }
            return user;
        }

        public static SecurityUser SignIn(string account, string password)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException("account");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            SteelWireContext dbContext = new SteelWireContext();
            SecurityUser user = dbContext.SecurityUser.FirstOrDefault(u => u.Account == account);
            return user;
        }
    }
}
