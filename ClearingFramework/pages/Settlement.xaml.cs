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
    /// Interaction logic for Settlement.xaml
    /// </summary>
    public partial class Settlement : Page
    {
        public Settlement()
        {
            InitializeComponent();
            bindCombo();
        }
        string accountID;
        #region combos
        public List<Account> acct { get; set; }
        private void bindCombo()
        {
        ClearingEntities ce = new ClearingEntities();
            var acid = ce.Accounts.ToList();
            acct = acid;
            accid.ItemsSource = acct;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = accid.SelectedItem as Account;
            try
            {
                accountID= item.id.ToString();
                sname.Text = item.fname.ToString();
                idnum.Text = item.idNum.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
        #region бүртгэх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(var context=new ClearingEntities())
            {
                var tran = new transaction()
                {
                    accid = accountID,
                    transType = transType.Text,
                    value = Decimal.Parse(trvalue.Text),
                    note = trnote.Text
                };
                context.transactions.Add(tran);
                context.SaveChanges();
            }
        }
        #endregion

        private void trvalue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
    }
}
