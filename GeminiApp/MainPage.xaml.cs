using GeminiApp.Classes;
using static System.Net.Mime.MediaTypeNames;

namespace GeminiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private MainViewModel _viewModel = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        //private void OnCounterClicked(object? sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}




        private readonly GetRequest2 _getRequest = new();
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string input = inputText.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                commandTab.Text = "Bitte geben Sie einen Text ein.";
                return;
            }
            createUserBubble(input);
             if (_viewModel != null)
             {
              _viewModel.IsButtonEnabled = false; // disable the button
            }

            try
            {
                string response = await _getRequest.GetResponseAsync(input);
                createAssistantBubble(response);
            }
            catch (Exception ex)
            {
                createAssistantBubble("Fehler: " + ex.Message); // Display error message
            }

            // Re-enable the button if needed
             if (_viewModel != null)
             {
                 _viewModel.IsButtonEnabled = true;
             }

            inputText.Text = "";
            inputText.Focus();
        }
        public void createAssistantBubble(string response)
        {
            Frame bubble = new()
            {
                BackgroundColor = Color.FromArgb("2bf5fb"),
                CornerRadius = 15,
                Padding = 20,
                HasShadow = true,
                Margin = new Thickness(0, 0, 0, 100)
            };
            Label label = new Label
            {
                Text = response,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromArgb("000000")
            };
            bubble.Content = label;

            // Add BubbleFrame to BubbleContainer
            bubbleContainer.Children.Add(bubble);
            if (bubbleContainer.Children.Count > 0)
            {
                var lastBubble = bubbleContainer.Children.Last();
                ChatScroll.ScrollToAsync((Element)lastBubble, ScrollToPosition.End, true);
            }
        }
        public void createUserBubble(string response)
        {
            Frame bubble = new()
            {
                BackgroundColor = Color.FromArgb("3385ff"),
                CornerRadius = 15,
                Padding = 10,
                HasShadow = true,
                Margin = new Thickness(20, 0, 0, 0)
            };
            Label label = new Label
            {
                Text = response,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Color.FromArgb("000000")
            };
            bubble.Content = label;

            // Add BubbleFrame to BubbleContainer
            bubbleContainer.Children.Add(bubble);
        }

    }
}
