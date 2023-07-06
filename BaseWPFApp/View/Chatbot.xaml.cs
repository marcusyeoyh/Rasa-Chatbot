using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BaseWPFApp.View
{
    public partial class Chatbot : Window
    {
        private bool isUserMessage = true; // Flag to track if the message is from the user or the bot
        private Frame mainPageFrame;

        public Chatbot(Frame mainPageFrame)
        {
            InitializeComponent();
            this.mainPageFrame = mainPageFrame; // Store the MainPage instance
        }

        private async void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            var userInput = txtInput.Text;

            if (userInput.ToLower() == "clear all")
            {
                ClearResults();
            }
            else
            {
                // Display user message on the right side
                DisplayMessage(userInput, isUserMessage);
                isUserMessage = !isUserMessage; // Toggle the flag

                var response = await QueryRasaChatbot(userInput);

                // Display bot response on the left side
                DisplayMessage(response, isUserMessage);
                isUserMessage = !isUserMessage; // Toggle the flag
            }

            // Clear the search box and restore the default message
            txtInput.Text = "Enter query here ...";
            txtInput.SelectAll();
        }


        private void ClearResults()
        {
            ResultsPanel.Children.Clear();
            txtInput.Clear();
        }

        private void TxtInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnGo_Click(sender, e);
                e.Handled = true;
            }
        }


        private async Task<string> QueryRasaChatbot(string userInput)
        {
            using (var httpClient = new HttpClient())
            {
                var rasaServerUrl = "http://localhost:5005/webhooks/rest/webhook";

                var requestBody = new
                {
                    sender = "user",
                    message = userInput
                };

                var response = await httpClient.PostAsJsonAsync(rasaServerUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }

                return "No response from the chatbot.";
            }
        }

        private void DisplayMessage(string message, bool isUserMessage)
        {
            if (isUserMessage)
            {
                DisplayTextMessage(message, isUserMessage);
            }
            else
            {
                try
                {
                    JArray responseData = JArray.Parse(message);
                    foreach (JObject obj in responseData)
                    {
                        string text = obj.Value<string>("text");

                        if (!string.IsNullOrEmpty(text))
                        {
                            if (text.StartsWith("Redirect"))
                            {
                                // Extract the page number from the response
                                string pageNumber = text.Replace("Redirect", "").Trim();
                                pageNumber = pageNumber.Replace("(", "").Replace(")", "").Trim(); // Remove brackets

                                // Construct the page name based on the number
                                string pageName = "ProductID" + pageNumber;

                                NavigateToPageOnMainPage(pageName);
                            }
                            else if (text.StartsWith("Please confirm if you would like to close the chatbot:"))
                            {
                                DisplayTextMessage("Please confirm if you would like to close the chatbot:", false);

                                // Add a button to close the chatbot window
                                Button closeButton = new Button();
                                closeButton.Content = "Close Chatbot";
                                closeButton.Style = FindResource("ProductButtonStyle") as Style;
                                closeButton.Click += (sender, e) =>
                                {
                                    Close(); // Close the chatbot window
                                };

                                ResultsPanel.Children.Add(closeButton);
                            }
                            else if (text.StartsWith("Please confirm if you would like to close the APP:"))
                            {
                                DisplayTextMessage("Please confirm if you would like to close the APP:", false);

                                // Add a button to close the entire app
                                Button closeAppButton = new Button();
                                closeAppButton.Content = "Close App";
                                closeAppButton.Style = FindResource("ProductButtonStyle") as Style;
                                closeAppButton.Click += (sender, e) =>
                                {
                                    Application.Current.Shutdown(); // Close the entire WPF app
                                };

                                ResultsPanel.Children.Add(closeAppButton);
                            }
                            else
                            {
                                DisplayTextMessage(text, isUserMessage);
                            }
                        }
                    }


                }
                catch (JsonReaderException)
                {
                    DisplayTextMessage("I can't seem to find a result, could you try again?", false);
                }
            }
        }


        private void DisplayTableMessageJson(string tableJson, bool isUserMessage)
        {
            // Display an additional message from the chatbot
            DisplayTextMessage("Here's what I found:", false);

            // Parse the table JSON into a DataTable
            DataTable table = ParseTableJson(tableJson);

            // Create a DataGrid control to display the table
            DataGrid dataGrid = new DataGrid();
            dataGrid.ItemsSource = table.DefaultView;
            dataGrid.IsReadOnly = true;
            dataGrid.AutoGenerateColumns = true;

            // Create a FlowDocument containing the DataGrid
            FlowDocument flowDocument = new FlowDocument();
            flowDocument.Blocks.Add(new BlockUIContainer(dataGrid));

            // Create a RichTextBox to display the FlowDocument
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Document = flowDocument;
            richTextBox.IsReadOnly = false; // Enable text selection
            richTextBox.IsDocumentEnabled = true;

            // Add the RichTextBox to the message bubble
            Border messageBubble = new Border();
            messageBubble.Style = isUserMessage ? FindResource("UserMessageBubbleStyle") as Style : FindResource("BotMessageBubbleStyle") as Style;
            messageBubble.Child = richTextBox;

            // Add the message bubble to the ResultsPanel
            ResultsPanel.Children.Add(messageBubble);
            if (table.Columns.Contains("ProductId"))
            {
                DisplayProductButtons(table);
            }
            // Display buttons for each unique ProductId
        }

        private void DisplayProductButtons(DataTable table)
        {
            HashSet<string> productIds = new HashSet<string>();

            // Get unique ProductIds from the table
            foreach (DataRow row in table.Rows)
            {
                string productId = row["ProductId"].ToString();
                if (!string.IsNullOrEmpty(productId) && !productIds.Contains(productId))
                {
                    productIds.Add(productId);
                }
            }

            // Create and display buttons for each unique ProductId
            foreach (string productId in productIds)
            {
                Button button = new Button();
                button.Content = "Product ID " + productId;
                button.Style = FindResource("ProductButtonStyle") as Style;
                button.Click += (sender, e) =>
                {
                    string pageName = "ProductID" + productId;
                    NavigateToPageOnMainPage(pageName);
                };

                ResultsPanel.Children.Add(button);
            }
        }


        private void DisplayTextMessage(string text, bool isUserMessage)
        {
            if (IsJsonString(text))
            {
                DisplayTableMessageJson(text, isUserMessage);
            }
            else
            {
                // Create a border to represent the message bubble
                Border messageBubble = new Border();
                messageBubble.Style = isUserMessage ? FindResource("UserMessageBubbleStyle") as Style : FindResource("BotMessageBubbleStyle") as Style;

                // Create a TextBlock to display the message content
                TextBlock messageText = new TextBlock();
                messageText.Text = text;
                messageText.Foreground = Brushes.White;

                // Add the TextBlock to the message bubble
                messageBubble.Child = messageText;

                // Add the message bubble to the ResultsPanel
                ResultsPanel.Children.Add(messageBubble);
            }
        }

        private bool IsJsonString(string input)
        {
            try
            {
                JToken.Parse(input);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        private DataTable ParseTableJson(string json)
        {
            JObject tableObject = JObject.Parse(json);
            JArray tableArray = (JArray)tableObject["table"];

            DataTable table = new DataTable();

            // Populate the DataTable schema from the first row of the table JSON
            foreach (JProperty property in tableArray[0])
            {
                table.Columns.Add(property.Name, typeof(string));
            }

            // Populate the DataTable rows from the table JSON
            foreach (JObject rowObject in tableArray)
            {
                DataRow row = table.NewRow();
                foreach (JProperty property in rowObject.Properties())
                {
                    row[property.Name] = property.Value.ToString();
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private void NavigateToPageOnMainPage(string pageName)
        {
            // Assuming your page types are in the same namespace as the MainPage
            string pageNamespace = typeof(MainPage).Namespace;

            // Construct the fully qualified type name of the page
            string pageTypeName = $"{pageNamespace}.{pageName}";

            // Get the Type object representing the page
            Type pageType = Type.GetType(pageTypeName);

            // Create an instance of the page using reflection
            object pageInstance = Activator.CreateInstance(pageType);

            // Navigate to the page on the MainPage's Frame
            mainPageFrame.Navigate(pageInstance);
        }

        private void TxtInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.SelectAll();
        }

        private void TxtInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }
    }
}