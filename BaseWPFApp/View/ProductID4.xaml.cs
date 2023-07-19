using System;
using System.Windows;
using System.Windows.Controls;


namespace BaseWPFApp.View
{
    public partial class ProductID4 : Page
    {
        private string userMode;

        public ProductID4(string userMode)
        {
            InitializeComponent();
            this.userMode = userMode;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainPage(userMode));
        }
    }
}