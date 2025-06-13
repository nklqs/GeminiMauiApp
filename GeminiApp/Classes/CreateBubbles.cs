using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiApp.Classes
{
    public static class CreateBubbles
    {
        public static string defaultAssistantColor = "ad26ff";
        public static Frame CreateAssistantBubble(string response)
        {
            Frame bubble = new()
            {
                BackgroundColor = Color.FromArgb(defaultAssistantColor),
                CornerRadius = 15,
                Padding = 10,
                HasShadow = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0, 10, 20, 10)
            };
            Label label = new Label
            {
                Text = response,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromArgb("ffffff")
            };
            bubble.Content = label;
            defaultAssistantColor = "ad26ff";
            return bubble;
        }
        public static Frame CreateUserBubble(string response)
        {
            Frame bubble = new()
            {
                BackgroundColor = Color.FromArgb("6b26ff"),
                CornerRadius = 15,
                Padding = 10,
                HasShadow = true,
                Margin = new Thickness(40, 0, 10, 0)
            };
            Label label = new Label
            {
                Text = response,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Color.FromArgb("ffffff")
            };
            bubble.Content = label;
            return bubble;
        }
    }
}
