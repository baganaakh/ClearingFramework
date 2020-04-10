using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;
using ClearingFramework.dbBind;
using System.Linq;

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
            string entityBuilder= "Data source="+serverName+";initial catalog="+databaseName+";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            config.ConnectionStrings.ConnectionStrings["Model1"].ConnectionString = entityBuilder;
            config.Save(ConfigurationSaveMode.Modified);
            using(Model1 conx=new Model1())
            {
                var query = conx.AdminUsers.Where(s => s.uname == "baganaakh").FirstOrDefault<AdminUser>();
                MessageBox.Show(query.role);
            }

        }
        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }

}
