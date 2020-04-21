using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Deployment.Application;
using System.Reflection;

namespace ClearingFramework
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();            
            string vers="";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                vers = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            string version ="Login  Version: "+ ver.Major + "." + ver.Minor + "." + ver.Build + "." + ver.Revision;
            this.Title = vers;
            using(Model1 db=new Model1())
            {
                
            }
        }
            

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    var query = context.AdminUsers.Where(s => s.uname == txtLoginName.Text).FirstOrDefault<AdminUser>();
                    if (query == null)
                    {
                        MessageBox.Show("Username doesn't match");
                        return;
                    }
                    App.Current.Properties["User_id"] = query.id;
                    App.Current.Properties["member_id"] = query.memId;
                    if (query.password == txtLoginPass.Password)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.topPanel.Content = "MSX Clearing Workstation   " + query.uname + " ID : " + query.memId;
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password doesn't match");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connect connnect = new connect();
            connnect.Show();
            this.Close();
        }
    }
}
