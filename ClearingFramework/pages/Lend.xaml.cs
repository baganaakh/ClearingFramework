using Non_Member.dbBind;
using Non_Member.dbBind.AdminDatabase;
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
    /// Interaction logic for Lend.xaml
    /// </summary>
    public partial class Lend : Page
    {
        public Lend()
        {
            InitializeComponent();
        }
        clearingEntities1 CE = new clearingEntities1();
        demoEntities1 DE = new demoEntities1();
        public List<Asset> asst { get; set; }
        private void bindCombo()
        {
            //List<Asset> acclist = DE.Assets;
            //assett.ItemsSource = acclist;
        }
    }
}
