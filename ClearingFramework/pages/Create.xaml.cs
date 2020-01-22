using System;
using System.Collections.Generic;
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
        }
        #region insert
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string lname = lName.Text;
            string fname = fName.Text;
            string IdNumber = idNumber.Text;
            string Phone = phone.Text;
            string Account = account.Text;
            string SecAcc = secAcc.Text;
            string email = mail.Text;
        }
        #endregion
    }
}
