using ClearingFramework.dbBind.AdminDatabase;
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
    /// Interaction logic for Collateral.xaml
    /// </summary>
    public partial class Collateral : Page
    {
        public Collateral()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(demoEntities1 contx=new demoEntities1())
            {
                ColReq req = new ColReq()
                {
                    accId = 1
                };
            }
        }
    }
}
