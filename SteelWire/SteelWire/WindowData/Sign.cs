using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;

namespace SteelWire.WindowData
{
    /// <summary>
    /// SignWindow的绑定数据
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly Sign Data;

        /// <summary>
        /// 当前登陆用户ID
        /// </summary>
        public int UserID { get; private set; }
        /// <summary>
        /// 当前登陆用户账户
        /// </summary>
        public string Account { get; private set; }
        /// <summary>
        /// 当前登陆用户姓名
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// 当前登陆用户显示名
        /// </summary>
        public DependencyItem<string> UserDisplay { get; private set; }
        /// <summary>
        /// 窗口模式（登陆）
        /// </summary>
        public DependencyItem<bool> IsSign { get; private set; }
        /// <summary>
        /// 窗口模式（注册）
        /// </summary>
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

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        private string GetMd5(string pass)
        {
            byte[] result = Encoding.Default.GetBytes(pass);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return Convert.ToBase64String(output);
        }

        private void ChangeSignToRegist(object sender, EventArgs e)
        {
            this.IsRegist.ItemValue = !this.IsSign.ItemValue;
        }

        private void ChangeRegistToSign(object sender, EventArgs e)
        {
            this.IsSign.ItemValue = !this.IsRegist.ItemValue;
        }

        /// <summary>
        /// 判断是否已经登陆
        /// </summary>
        /// <returns></returns>
        public bool IsSignIn()
        {
            return !string.IsNullOrEmpty(this.Account);
        }

        /// <summary>
        /// 执行登陆
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
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
            password = GetMd5(password);
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

        /// <summary>
        /// 执行注册
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password1"></param>
        /// <param name="password2"></param>
        /// <param name="userName"></param>
        public void Regist(string account, string password1, string password2, string userName)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ErrorException("AccountEmpty");
            }
            if (string.IsNullOrWhiteSpace(password1))
            {
                throw new ErrorException("PasswordEmpty");
            }
            if (!Equals(password1, password2))
            {
                throw new ErrorException("ConfirmPasswordNotEqual");
            }
            if (password1.Length < 6)
            {
                throw new ErrorException("PasswordTooShort");
            }
            if (UserOperator.Exist(account))
            {
                throw new ErrorException("AccountNotUnique");
            }
            DateTime now = DateTime.Now;
            SecurityUser user = new SecurityUser
            {
                Account = account,
                Password = GetMd5(password1),
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

        /// <summary>
        /// 设置当前用户信息
        /// </summary>
        /// <param name="user"></param>
        private void SetUserInfo(SecurityUser user)
        {
            this.UserID = user.ID;
            this.Account = user.Account;
            this.UserName = user.Name;
            this.UserDisplay.ItemValue = string.IsNullOrEmpty(this.UserName) ? this.Account : this.UserName;
        }
    }
}
