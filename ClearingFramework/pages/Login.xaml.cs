using ClearingFramework;
using ClearingFramework.dbBind;
using ClearingFramework.dbBind.AdminDatabase;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using(demoEntities1 context=new demoEntities1())
            {
                var query = context.users.Where(s => s.uname == txtLoginName.Text).FirstOrDefault<user>();
                App.Current.Properties["User_id"] = query.id;
                if(query.password == txtLoginPass.Password)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    
                    //Window parentWindow = Window.GetWindow(this).Button;
                    
                    //((this.Parent) as Window).Content.Bu;
                    //parentWindow.
                    //dashboard.start.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
        }
    }
}
