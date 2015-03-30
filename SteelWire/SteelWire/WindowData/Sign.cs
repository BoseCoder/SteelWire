using System;
using System.Collections.ObjectModel;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;

namespace SteelWire.WindowData
{
    public class Sign
    {
        public static readonly Sign Data;

        public int UserID { get; private set; }
        public string Account { get; private set; }
        public string UserName { get; private set; }
        public DependencyItem<string> UserDisplay { get; private set; }
        public DependencyItem<bool> IsSign { get; private set; }
        public DependencyItem<bool> IsRegist { get; private set; }

        static Sign()
        {
            Data = new Sign();
        }

        private Sign()
        {
            this.UserDisplay = new DependencyItem<string>();
            this.IsSign = new DependencyItem<bool>(true);
            this.IsSign.ItemValueChangedHandler += ChangeSignToRegist;
            this.IsRegist = new DependencyItem<bool>();
            this.IsRegist.ItemValueChangedHandler += ChangeRegistToSign;
        }

        private void ChangeSignToRegist(object sender, EventArgs e)
        {
            this.IsRegist.ItemValue = !this.IsSign.ItemValue;
        }

        private void ChangeRegistToSign(object sender, EventArgs e)
        {
            this.IsSign.ItemValue = !this.IsRegist.ItemValue;
        }

        public bool IsSignIn()
        {
            return !string.IsNullOrEmpty(this.Account);
        }

        public void SignIn(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ErrorException("AccountEmpty");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ErrorException("PasswordEmpty");
            }
            SecurityUser user = UserOperator.SignIn(account, password);
            if (user == null)
            {
                throw new ErrorException("UserNotExist");
            }
            if (user.Password != password)
            {
                throw new ErrorException("PasswordNotEqual");
            }
            SetUserInfo(user);
        }

        public void Regist(string account, string password, string userName)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ErrorException("AccountEmpty");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ErrorException("PasswordEmpty");
            }
            DateTime now = DateTime.Now;
            SecurityUser user = new SecurityUser
            {
                Account = account,
                Password = password,
                Name = userName ?? account,
                Checked = true,
                Enabled = true,
                RegistTime = now,
                UpdateTime = now,
                Machine = new Collection<Machine>
                {
                    new Machine
                    {
                        MachineCode = Environment.Machine.GetMachineNumber(),
                        RegistTime = now
                    }
                }
            };
            user = UserOperator.Regist(user);
            SetUserInfo(user);
        }

        private void SetUserInfo(SecurityUser user)
        {
            this.UserID = user.ID;
            this.Account = user.Account;
            this.UserName = user.Name;
            this.UserDisplay.ItemValue = string.IsNullOrEmpty(this.UserName) ? this.Account : this.UserName;
        }
    }
}
