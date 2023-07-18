using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BaseWPFApp.View
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Save user mode as "Customer"
            string user_mode = "Customer";
            // Redirect to MainWindow
            NavigationService?.Navigate(new MainPage(user_mode));
        }

        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {
            // Save user mode as "Staff"
            string user_mode = "Staff";
            // Redirect to MainWindow
            NavigationService?.Navigate(new MainPage(user_mode));
        }
    }
}