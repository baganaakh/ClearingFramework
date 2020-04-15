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
            string entityBuilder= "Data source="+serverName+";initial catalog="+databaseName+";;MultipleActiveResultSets=True;App=EntityFramework";
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

        private void uselocal_Click(object sender, RoutedEventArgs e)
        {
            //string localstring = "Server = (LocalDB)\\MSSQLLocalDB; Integrated Security = true";
            //config.ConnectionStrings.ConnectionStrings["Model1"].ConnectionString = localstring;
            //config.Save(ConfigurationSaveMode.Modified);            
            try
            {
                using(var db = new Model1())
                {                    
                    db.Database.CreateIfNotExists();
                    Account acct = new Account
                    {
                        accNum = "1",
                        idNum = "sd5669889",
                        lname = "last",
                        fname = "First",
                        phone = "7879879",
                        mail = "sdadas",
                        state = Convert.ToInt16(1),
                        modified = DateTime.Now,
                        secAcc = "s123123",
                        fee = Convert.ToDecimal(0.56),
                        denchinPercent = Convert.ToDecimal(0.35),
                        contractFee = Convert.ToDecimal(0.94),
                        pozFee = Convert.ToDecimal(0.68),
                        memId = 1,
                        bank = 1,
                    };
                    AdminUser au = new AdminUser()
                    {
                        uname = "baganaakh",
                        password = "baganaakh",
                        modified = DateTime.Now,
                        role = "user",
                        memId = 1
                    };
                    db.AdminUsers.Add(au);
                    db.Accounts.Add(acct);
                    db.SaveChanges();
                    var query = db.AdminUsers.Where(s => s.uname == "baganaakh").FirstOrDefault<AdminUser>();
                    MessageBox.Show(query.role);
                    Login log = new Login();
                    log.Show();
                    this.Close();
                }
            }

            catch (Exception ex)
                {
                MessageBox.Show(ex.ToString());
                return;
                }

        }
    }

}
