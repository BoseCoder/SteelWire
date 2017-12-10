using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.CustomMessage;
using SteelWire.AppCode.Data;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.Config;
using SteelWire.Business.Database;
using SteelWire.Language;
using SteelWire.WindowData;

namespace SteelWire.Windows
{
    public partial class OptionWindow
    {
        public OptionWindow()
        {
            InitializeComponent();

            InitializeEnumComboBox();
        }

        private void InitializeEnumComboBox()
        {
            Type languageEnumType = typeof(LanguageEnum);
            this.CboLanguage.ItemsSource = Enum.GetValues(languageEnumType).Cast<LanguageEnum>().Select(value =>
                new KeyValuePair<LanguageEnum, DependencyLanguage>(value, DependencyLanguage.Generate(() =>
                    LanguageManager.GetLocalResourceStringLeft(languageEnumType.Name, value.ToString()))));

            Type unitSystemEnumType = typeof(UnitSystemEnum);
            this.CboUnitSystem.ItemsSource = Enum.GetValues(unitSystemEnumType).Cast<UnitSystemEnum>().Select(value =>
                new KeyValuePair<UnitSystemEnum, DependencyLanguage>(value, DependencyLanguage.Generate(() =>
                    LanguageManager.GetLocalResourceStringLeft(unitSystemEnumType.Name, value.ToString()))));
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 去除License限制
                // this.TxtSerialNumber.Text = IntelliLock.Licensing.HardwareID.GetHardwareID(true, true, true, true, true, false);

                if (!GlobalData.IsSignIn && DatabaseConfigManager.OnceInstance.DatabaseType == DatabaseType.SqlServer)
                {
                    this.TabSqlServerOption.Visibility = Visibility.Visible;
                    ConnectionStringSettings connSettings =
                        ConfigurationManager.ConnectionStrings[SteelWireSqlServerContext.ConnectionName];
                    if (!string.IsNullOrWhiteSpace(connSettings?.ConnectionString))
                    {
                        using (SteelWireSqlServerContext dbContext = new SteelWireSqlServerContext())
                        {
                            this.TxtServer.Text = dbContext.Database.Connection.DataSource;
                            this.TxtDatabase.Text = dbContext.Database.Connection.Database;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void OptionOk(object sender, RoutedEventArgs e)
        {
            try
            {
                Option.SaveConfig();
                if (!GlobalData.IsSignIn && DatabaseConfigManager.OnceInstance.DatabaseType == DatabaseType.SqlServer)
                {
                    string connString;
                    if (CheckInput(out connString))
                    {
                        Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        ConnectionStringSettings settings = appConfig.ConnectionStrings.ConnectionStrings[SteelWireSqlServerContext.ConnectionName];
                        if (settings.ConnectionString != connString)
                        {
                            settings.ConnectionString = connString;
                            appConfig.Save();
                            MessageManager.Information("OptionSave");
                        }
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void OptionCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckInput(out string connString)
        {
            if (string.IsNullOrWhiteSpace(this.TxtServer.Text))
            {
                throw new ErrorException("OptionServerEmpty");
            }
            if (string.IsNullOrWhiteSpace(this.TxtDatabase.Text))
            {
                throw new ErrorException("OptionDatabaseEmpty");
            }
            if (string.IsNullOrWhiteSpace(this.TxtDbUser.Text))
            {
                throw new ErrorException("OptionDbUserEmpty");
            }
            if (string.IsNullOrWhiteSpace(this.PassBoxDbUser.Password))
            {
                throw new ErrorException("OptionDbPassEmpty");
            }
            connString =
                $"Data Source={this.TxtServer.Text.Trim()};Initial Catalog={this.TxtDatabase.Text.Trim()};Integrated Security=False;User ID={this.TxtDbUser.Text.Trim()};Password={this.PassBoxDbUser.Password.Trim()};";
            DbContext dbContext = null;
            try
            {
                dbContext = new DbContext(connString);
                dbContext.Database.Connection.Open();
                connString =
                    $"metadata=res://*/Database.SteelWireSqlServerModel.csdl|res://*/Database.SteelWireSqlServerModel.ssdl|res://*/Database.SteelWireSqlServerModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={this.TxtServer.Text.Trim()};initial catalog={this.TxtDatabase.Text.Trim()};persist security info=True;user id={this.TxtDbUser.Text.Trim()};password={this.PassBoxDbUser.Password.Trim()};MultipleActiveResultSets=True;App=EntityFramework\"";
                return true;
            }
            catch (Exception ex)
            {
                throw new ErrorException("OptionDatabaseInvalid", ex);
            }
            finally
            {
                if (dbContext != null && dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbContext.Database.Connection.Close();
                }
            }
        }

        private void OptionWindowClosing(object sender, CancelEventArgs e)
        {
            Option.ReadConfig();
        }
    }
}
