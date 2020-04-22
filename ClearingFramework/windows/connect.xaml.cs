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
            //string providerName = "System.Data.SqlClient";
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
            try
            {
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
                        accNum = "1",
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
                        state = "1",
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
                        type = 1,
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
    }

}
