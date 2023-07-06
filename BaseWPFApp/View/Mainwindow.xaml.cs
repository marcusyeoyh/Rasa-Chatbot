using System;
using System.Windows;
using System.Windows.Controls;

namespace BaseWPFApp.View
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnChatbot_Click(object sender, RoutedEventArgs e)
        {
            Chatbot chatbotWindow = new Chatbot(mainPageFrame); // Pass the MainPage's Frame instance
            chatbotWindow.Show();
        }


        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Page1.xaml
            NavigationService?.Navigate(new ProductID1());
        }

        private void Page2Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Page2.xaml
            NavigationService?.Navigate(new ProductID2());
        }

        private void Page3Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Page3.xaml
            NavigationService?.Navigate(new ProductID3());
        }

        private void Page4Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Page3.xaml
            NavigationService?.Navigate(new ProductID4());
        }

        private void Page5Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Page3.xaml
            NavigationService?.Navigate(new ProductID5());
        }
    }
}