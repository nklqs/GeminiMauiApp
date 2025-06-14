using GeminiApp.Classes;
using GeminiApp.Classes.Interactions;

namespace GeminiApp
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
            LoadMessages();
        }
        private void LoadMessages()
        {
            SaveAndLoadHistory.LoadData();
            if (GetRequest2._chatHistory.Count != 0)
            {
                foreach (var item in GetRequest2._chatHistory)
                {
                    if(item.role == "user")
                    {
                        var bubble = CreateBubbles.CreateUserBubble(item.text);
                        bubbleContainer.Children.Add(bubble);
                    }
                    if(item.role == "model")
                    {
                        var bubble = CreateBubbles.CreateAssistantBubble(item.text);
                        bubbleContainer.Children.Add(bubble);
                    }
                }
            }
        }
        private readonly GetRequest2 _getRequest = new();
        private readonly GetReview _getReview = new();
        private readonly GetTranslation _getTranslation = new();
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string input = inputText.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                commandTab.Text = "Bitte geben Sie einen Text ein.";
                return;
            }
            var bubble = CreateBubbles.CreateUserBubble(input);
            bubbleContainer.Children.Add(bubble);
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = false; // disable the button
            }
            string response = await _getRequest.GetResponseAsync(input);
            var assistantBubble = CreateBubbles.CreateAssistantBubble(response);
            bubbleContainer.Children.Add(assistantBubble);
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = true;
            }

            await Task.Delay(100);
            if (ChatScroll.ContentSize.Height > ChatScroll.Height)
            {
                await ChatScroll.ScrollToAsync(0, ChatScroll.ContentSize.Height, true);
            }
            inputText.Text = "";
        }
        private async void commandTab_Clicked(object sender, EventArgs e)
        {
            inputText.Completed += async (sender, e) =>
            {
                OnSubmitClicked(sender, e);
            };
        }
        private async void OnSubmitReview(object sender, EventArgs e)
        {
            string input = inputText.Text;
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = false; // disable the button
            }
            string response = await _getReview.GetResponseAsync();
            var assistantBubble = CreateBubbles.CreateAssistantBubble(response);
            bubbleContainer.Children.Add(assistantBubble);
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = true;
            }

            await Task.Delay(100);
            if (ChatScroll.ContentSize.Height > ChatScroll.Height)
            {
                await ChatScroll.ScrollToAsync(0, ChatScroll.ContentSize.Height, true);
            }
            inputText.Text = "";
        }

        private async void OnSubmitHelp(object sender, EventArgs e)
        {
            string input = inputText.Text;
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = false; // disable the button
            }
            string response = await _getTranslation.GetResponseAsync();
            var assistantBubble = CreateBubbles.CreateAssistantBubble(response);
            bubbleContainer.Children.Add(assistantBubble);
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = true;
            }

            await Task.Delay(100);
            if (ChatScroll.ContentSize.Height > ChatScroll.Height)
            {
                await ChatScroll.ScrollToAsync(0, ChatScroll.ContentSize.Height, true);
            }
            inputText.Text = "";
        }

        private void OnSubmitClear(object sender, EventArgs e)
        {
            bubbleContainer.Children.Clear();
            GetRequest2._chatHistory.Clear();
            SaveAndLoadHistory.AddToHistory(GetRequest2._chatHistory);
            if (_viewModel != null)
            {
                _viewModel.IsButtonEnabled = true;
            }
        }
    }
}
