using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;

namespace ClearingFramework
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class connect : Window
    {
        Configuration config;
        public connect()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string serverName = server.Text;
            string userName = username.Text;
            string DBpassword = ssd.Text;
            string databaseName = databases.Text;
            string providerName = "System.Data.SqlClient";
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = serverName;
            sqlBuilder.InitialCatalog = databaseName;
            sqlBuilder.IntegratedSecurity = true;

            string providerString = sqlBuilder.ToString();
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = providerName;
            entityBuilder.ProviderConnectionString = providerString;
            entityBuilder.Metadata = @"res://*/dbBind.Model1.csdl|res://*/dbBind.Model1.ssdl|res://*/dbBind.Model1.msl";
            MessageBox.Show(entityBuilder.ToString());
            try
            {
                using (EntityConnection conn = new EntityConnection(entityBuilder.ToString()))
                {
                    conn.Open();
                    var connectString = ConfigurationManager.ConnectionStrings["clearingEntities"].ConnectionString;
                    MessageBox.Show("connected");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("fail " + ex.ToString());
                return;
            }
            config.ConnectionStrings.ConnectionStrings["clearingEntities"].ConnectionString = entityBuilder.ToString();
            config.Save(ConfigurationSaveMode.Modified);
        }
        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }
    public static class ConnectionTools
    {
        public static void ChangeDataBase(this DbContext source,
                                                string initialCatalog = "",
                                                string dataSource = "",
                                                string userId = "",
                                                string password = "",
                                                bool integratedSecurity = true,
                                                string configConnectionStingName = "")
        {
            try
            {
                var configNameEf = string.IsNullOrEmpty(configConnectionStingName)
                    ? source.GetType().Name
                    : configConnectionStingName;
                var entityCnxStringBuilder = new EntityConnectionStringBuilder
                    (System.Configuration.ConfigurationManager
                        .ConnectionStrings[configNameEf].ConnectionString);
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    (entityCnxStringBuilder.ProviderConnectionString);
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                sqlCnxStringBuilder.IntegratedSecurity = integratedSecurity;
                source.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

}
