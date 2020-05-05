using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;
using ClearingFramework.dbBind;
using System.Linq;
using WinForms = System.Windows.Forms;
using System.Data;

namespace ClearingFramework
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class connect : Window
    {
        SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["Model"].ConnectionString);
        Configuration config;
        public connect()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
        }
        #region Backup & Restore
            #region BAckup
            private void browse_Click(object sender, EventArgs e)
            {
            WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog();
                if (dlg.ShowDialog() == WinForms.DialogResult.OK)
                {
                    ssd1.Text = dlg.SelectedPath;
                    Backup.IsEnabled = true;
                }
            }

            private void Backup_Click(object sender, EventArgs e)
            {
                string database = con.Database.ToString();
                try
                {
                    if (ssd1.Text == string.Empty)
                    {
                        MessageBox.Show("please enter backup file location");
                    }
                    else
                    {
                        string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + ssd1.Text +
                            "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                        using (SqlCommand command = new SqlCommand(cmd, con))
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("database backup done successefully");
                            Backup.IsEnabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            #endregion
            #region restore
        private void Browse2_Click(object sender, EventArgs e)
        {
            WinForms.OpenFileDialog dlg = new WinForms.OpenFileDialog();
            dlg.Filter = "SQL Server Database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == WinForms.DialogResult.OK)
            {
                ssd2.Text = dlg.FileName;
                Restore.IsEnabled = true;
            }
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, con);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK ='" + ssd2.Text + "' WITH REPLACE";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, con);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, con);
                bu4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #endregion
        #region Change connection
        private void Change(object sender, RoutedEventArgs e)
        {
            string serverName = server.Text;
            string userName = username.Text;
            string DBpassword = ssd.Text;
            string databaseName = databases.Text;
            string entityBuilder= "Data source="+serverName+";" +
                "initial catalog="+databaseName+";" +
                "User Id="+userName+";" +
                "Password="+DBpassword+";"+
                "App=EntityFramework;Pooling=false;";
            config.ConnectionStrings.ConnectionStrings["Model1"].ConnectionString = entityBuilder;
            config.ConnectionStrings.ConnectionStrings["Model1"].ProviderName = "System.Data.SqlClient";
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("done");
        }
        #endregion
        #region Exit & Check
        private void Exit(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
        private void check_Click(object sender, RoutedEventArgs e)
        {
            var connection = ConfigurationManager.ConnectionStrings["Model1"].ConnectionString;
            MessageBox.Show(connection.ToString());
        }
        #endregion
        #region USE LOCAL DATABASE
        private void uselocal_Click(object sender, RoutedEventArgs e)
        {      
            try
            {
                string cons = "data source=(LocalDB)\\v11.0;Initial catalog=Model1;Integrated security=True;" +
                    "MultipleActiveResultSets=True;App=EntityFramework;Pooling=false;";
                config.ConnectionStrings.ConnectionStrings["Model1"].ConnectionString = cons;
                config.ConnectionStrings.ConnectionStrings["Model1"].ProviderName = "System.Data.SqlClient";
                config.Save(ConfigurationSaveMode.Modified);
                using(var db = new Model1())
                {              
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
                        linkAcc="1",
                    };
                    db.Accounts.Add(acct);
                    AccountDetail accountDetail = new AccountDetail()
                    {
                        freezeValue = 12,
                        totalNumber = 55,
                        assetId = 1,
                        accountId = 1,
                        linkAcc = "1",
                        modified = DateTime.Now,
                    };
                    db.AccountDetails.Add(accountDetail);
                    AdminUser au = new AdminUser()
                    {
                        uname = "baganaakh",
                        password = "baganaakh",
                        modified = DateTime.Now,
                        role = "Done",
                        memId = 1
                    };
                    db.AdminUsers.Add(au);
                    AdminAccount adac = new AdminAccount()
                    {
                        memberid = 1,
                        accNumber = "1",
                        accountType = 3,
                        LinkAccount = 1,
                        modified = DateTime.Now,
                        mask = "mask",
                        startdate = DateTime.Today,
                        enddate = (DateTime.Now).AddDays(2),
                        state = 1,
                    };
                    db.AdminAccounts.Add(adac);
                    AdminAccountDetail adacd = new AdminAccountDetail()
                    {
                        freezeValue = 193,
                        amount = 911,
                        assetId = 1,
                        accountId = adac.id,
                    };
                    db.AdminAccountDetails.Add(adacd);
                    AdminActiveSession aacs = new AdminActiveSession()
                    {
                        sessionid = 5,
                        isactive = "1",
                        starttime = new TimeSpan(3,3,3),
                        endtime = new TimeSpan(2, 2, 2),
                        tduration = (new TimeSpan(1, 1, 1)),
                        matched = 6,
                        state = 1,
                    };
                    db.AdminActiveSessions.Add(aacs);
                    AdminAsset ast = new AdminAsset()
                    {
                        code = "amd",
                        name = "advanced micro devices",
                        volume = 1000,
                        note = "advanced micro devices",
                        expireDate = DateTime.Now.AddYears(1),
                        state = 1,
                        modified = DateTime.Now,
                        ratio = Convert.ToDecimal(18.5),
                        price = 45,
                    };
                    db.AdminAssets.Add(ast);
                    AdminBoard bo = new AdminBoard()
                    {
                        name = "board1",
                        dayType = 1,
                        tdays = "1;2;3;4;",
                        state = 1,
                        modified = DateTime.Now,
                        description = "dfgsdfgsdfg",
                        dealType = 1,
                        expTime = (new TimeSpan(2, 4, 5)),
                        expDate = 1,
                    };
                    db.AdminBoards.Add(bo);
                    AdminCalendar ac = new AdminCalendar()
                    {
                        tdate = DateTime.Today,
                        type = 1,
                        note = "sgsdfgsdfgsdfg",
                        state = 1,
                        modified = DateTime.Now,
                    };
                    db.AdminCalendars.Add(ac);
                    AdminContract aco = new AdminContract()
                    {
                        securityId = 1,
                        type = 1,
                        code = "sdsasd",
                        name = "ads",
                        lot = Convert.ToDecimal(12.42),
                        tickTable = 1,
                        sdate = DateTime.Now,
                        edate = DateTime.Now.AddDays(1),
                        groupId = 1,
                        state = 1,
                        modified = DateTime.Now,
                        mmorderLimit = 1,
                        orderLimit = 1,
                        refpriceParam = 66,
                        bid = 1,
                    };
                    db.AdminContracts.Add(aco);
                    AdminDeal adeal = new AdminDeal()
                    {

                    };
                    db.AdminDeals.Add(adeal);
                    AdminInterest aint = new AdminInterest()
                    {

                    };
                    AdminMember adme = new AdminMember()
                    {
                        type = 1,
                        code = "fg",
                        state = 1,
                        modified = DateTime.Now,
                        partid = 1,
                        mask = "dsa",
                        startdate = DateTime.Now,
                        enddate = DateTime.Now,
                        broker = true,
                        dealer = true,
                        ander = true,
                        nominal = true,
                        linkMember = 1,
                        name = "dasda",
                    };
                    db.AdminMembers.Add(adme);
                    AdminMember adme2 = new AdminMember()
                    {
                        type = 2,
                        code = "hj",
                        state = 2,
                        modified = DateTime.Now,
                        partid = 2,
                        mask = "dsa",
                        startdate = DateTime.Now,
                        enddate = DateTime.Now,
                        broker = true,
                        dealer = true,
                        ander = true,
                        nominal = true,
                        linkMember = 2,
                        name = "dasda2",
                    };
                    db.AdminMembers.Add(adme2);
                    Adminmtype adminmtype = new Adminmtype()
                    {
                        mtype="Арилжаа",
                        minValue=50,
                    };
                    db.Adminmtypes.Add(adminmtype);
                    AdminInterest adin = new AdminInterest()
                    {
                        interest=13,
                        assetid=1,
                        repoInterset=4,
                        loanInterset=5,
                        maxValue=6,
                        minValue=2,
                    };
                    db.AdminInterests.Add(adin);
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
                MessageBox.Show(ex.Message.ToString());
                return;
                }
        }
        #endregion
        #region DELETE Database
        private void DeleteDB_Click(object sender, RoutedEventArgs e)
        {
            if (server.Text == "accept" && username.Text == "delete" && ssd.Text == "localdb" && databases.Text == "confirm")
            {
                try
                {
                    using (var dbconx = new DbContext("Model1"))
                    {
                        dbconx.Database.Delete();
                    };
                    MessageBox.Show("Done 😀");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("Secret text wrong !!!! try again");
            }
        }
        #endregion
    }

}
