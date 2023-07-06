using System;
using System.Windows;
using System.Windows.Controls;


namespace BaseWPFApp.View
{
    public partial class ProductID5 : Page
    {
        public ProductID5()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Redirect to MainWindow.xaml
            NavigationService?.Navigate(new Uri("/View/MainWindow.xaml", UriKind.Relative));
        }
    }
}
