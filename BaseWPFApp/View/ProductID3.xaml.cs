using System;
using System.Windows;
using System.Windows.Controls;


namespace BaseWPFApp.View
{
    public partial class ProductID3 : Page
    {
        private string userMode;

        public ProductID3(string userMode)
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