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
                throw new ArgumentNullException(nameof(account));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return dbContext.SecurityUser.Any(u => u.Account == account);
            }
        }

        public static SecurityUser Regist(SecurityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrEmpty(user.Account))
            {
                throw new ArgumentException("Account property of user is null.", nameof(user));
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Password property of user is null.", nameof(user));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
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
                throw new ArgumentNullException(nameof(account));
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                return dbContext.SecurityUser.FirstOrDefault(u => u.Account == account);
            }
        }
    }
}
