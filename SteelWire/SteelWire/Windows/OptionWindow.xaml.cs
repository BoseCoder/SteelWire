using System;
using System.Configuration;
using System.Data.Entity;
using System.Windows;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.CustomMessage;
using SteelWire.Business.Database;

namespace SteelWire.Windows
{
    public partial class OptionWindow
    {
        public OptionWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.TxtSerialNumber.Text = IntelliLock.Licensing.HardwareID.GetHardwareID(true, true, true, true, true, false);
                ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["SteelWireContext"];
                if (connSettings != null && !string.IsNullOrWhiteSpace(connSettings.ConnectionString))
                {
                    SteelWireContext dbContext = new SteelWireContext();
                    this.TxtServer.Text = dbContext.Database.Connection.DataSource;
                    this.TxtDatabase.Text = dbContext.Database.Connection.Database;
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
                string connString;
                if (CheckInput(out connString))
                {
                    Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    appConfig.ConnectionStrings.ConnectionStrings.Clear();
                    ConnectionStringSettings connSettings = new ConnectionStringSettings("SteelWireContext", connString)
                    {
                        ProviderName = "System.Data.EntityClient"
                    };
                    appConfig.ConnectionStrings.ConnectionStrings.Add(connSettings);
                    appConfig.Save();
                    MessageManager.Information("OptionSave");
                    this.Close();
                }
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
            connString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=False;User ID={2};Password={3};",
                this.TxtServer.Text.Trim(), this.TxtDatabase.Text.Trim(), this.TxtDbUser.Text.Trim(), this.PassBoxDbUser.Password.Trim());
            DbContext dbContext = null;
            try
            {
                dbContext = new DbContext(connString);
                dbContext.Database.Connection.Open();
                connString = string.Format("metadata=res://*/Database.SteelWireModel.csdl|res://*/Database.SteelWireModel.ssdl|res://*/Database.SteelWireModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={0};initial catalog={1};persist security info=True;user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework\"",
                    this.TxtServer.Text.Trim(), this.TxtDatabase.Text.Trim(), this.TxtDbUser.Text.Trim(), this.PassBoxDbUser.Password.Trim());
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
    }
}
