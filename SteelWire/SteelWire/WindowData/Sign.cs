using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.Data;
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
        /// 窗口模式（登陆）
        /// </summary>
        public DependencyObject<bool> IsSign { get; } = new DependencyObject<bool>(true);
        /// <summary>
        /// 窗口模式（注册）
        /// </summary>
        public DependencyObject<bool> IsRegist { get; } = new DependencyObject<bool>();

        public Sign()
        {
            this.IsSign.ValueChangedHandler += ChangeWindowMode;
            this.IsRegist.ValueChangedHandler += ChangeWindowMode;
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

        private void ChangeWindowMode(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, this.IsSign))
            {
                this.IsRegist.ValueChangedHandler -= ChangeWindowMode;
                this.IsRegist.Value = !this.IsSign.Value;
                this.IsRegist.ValueChangedHandler += ChangeWindowMode;
            }
            else
            {
                this.IsSign.ValueChangedHandler -= ChangeWindowMode;
                this.IsSign.Value = !this.IsRegist.Value;
                this.IsSign.ValueChangedHandler += ChangeWindowMode;
            }
        }

        /// <summary>
        /// 判断是否已经登陆
        /// </summary>
        /// <returns></returns>
        public bool IsSignIn()
        {
            return !string.IsNullOrEmpty(GlobalData.Account);
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
                RegistrationTime = now,
                UpdateTime = now,
                Machine = new Collection<Machine>
                {
                    new Machine
                    {
                        MachineCode = Environment.Machine.GetMachineNumber(),
                        RegistrationTime = now
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
            GlobalData.UserId = user.ID;
            GlobalData.SearchUserId = SystemConfigManager.OnceInstance.DataIsolation ? GlobalData.UserId : 0;
            GlobalData.Account = user.Account;
            GlobalData.UserDisplay.Value = string.IsNullOrEmpty(user.Name) ? user.Account : user.Name;
        }
    }
}
