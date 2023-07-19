using System.Windows;
using System.Windows.Controls;

namespace BaseWPFApp.View
{
    public partial class MainPage : Page
    {
        private string userMode;
        private Chatbot chatbotWindow;

        public MainPage(string userMode)
        {
            InitializeComponent();
            this.userMode = userMode;
            WelcomeTextBlock.Text = "Welcome " + userMode + "!";
        }

        private void btnChatbot_Click(object sender, RoutedEventArgs e)
        {
            if (chatbotWindow == null || !chatbotWindow.IsVisible)
            {
                chatbotWindow = new Chatbot(mainPageFrame, userMode); // Pass the MainPage's Frame instance
                chatbotWindow.Show();
            }
        }

        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.Navigate(new ProductID1(userMode));
        }

        private void Page2Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.Navigate(new ProductID2(userMode));
        }

        private void Page3Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.Navigate(new ProductID3(userMode));
        }

        private void Page4Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.Navigate(new ProductID4(userMode));
        }

        private void Page5Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.Navigate(new ProductID5(userMode));
        }

        private void ChangeUserModeButton_Click(object sender, RoutedEventArgs e)
        {
            CloseChatbotWindow();
            mainPageFrame.Navigate(new LoginPage());
        }
        private void CloseChatbotWindow()
        {
            // Check if Chatbot window is open and close it if needed
            if (chatbotWindow != null && chatbotWindow.IsVisible)
            {
                chatbotWindow.Close();
                chatbotWindow = null;
            }
        }
    }
}
