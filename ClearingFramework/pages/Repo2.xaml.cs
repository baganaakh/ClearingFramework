using Non_Member;
using Non_Member.dbBind;
using Non_Member.dbBind.AdminDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Account1 = Non_Member.dbBind.Account;
using Account2 = Non_Member.dbBind.AdminDatabase.Account;
using AccountDetail = Non_Member.dbBind.AccountDetail;
using MessageBox = System.Windows.MessageBox;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Repo.xaml
    /// interst, refPrice, account-totalsum
    /// </summary>
    public partial class Repo2 : Page
    {
        public Repo2()
        {
            InitializeComponent();

        }
        clearingEntities1 CE = new clearingEntities1();
        demoEntities1 DE = new demoEntities1();

    }
}
