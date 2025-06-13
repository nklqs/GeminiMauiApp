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
            inputText.Focus();
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
            inputText.Focus();
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
            inputText.Focus();
        }
    }
}
