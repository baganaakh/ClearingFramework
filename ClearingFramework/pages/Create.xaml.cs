using ClearingFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Page
    {
        public Create()
        {
            InitializeComponent();
            FillGrid();
        }
        #region fill
        private void FillGrid()
        {
            ClearingEntities CE = new ClearingEntities();

            //var data = from d in CE.Accounts select d;
            //vwOmniAccBalance.ItemsSource = data.ToList();
            //or
            var Accountss = CE.Accounts;
            vwOmniAccBalance.ItemsSource = Accountss.ToList();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
        #endregion
        string connectionString = ClearingFramework.Properties.Settings.Default.ConnectionString;
        #region insert
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (brokCode.SelectedValue == null && stat.SelectedValue == null && linkAc.SelectedValue == null)
            {
                MessageBox.Show("Combos are empty Please fill them");
            }
            using (ClearingEntities context=new ClearingEntities())
            {
                Account acct = new Account
                {
                    lname = lName.Text,
                    fname = fName.Text,
                    idNum = idNumber.Text,
                    phone = phonee.Text,
                    accNum = accountn.Text,
                    secAcc = secAc.Text,
                    mail = email.Text,
                    brokerCode = brokCode.Text,
                    //state = stat.SelectedValue.ToString(),
                    linkAcc = linkAc.Text
                };
                context.Accounts.Add(acct);
                context.SaveChanges();
                //context.Account acc=new 
            }
        }
        #endregion

    }
}
