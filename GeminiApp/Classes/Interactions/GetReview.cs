using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeminiApp.Classes.Interactions
{
    public class GetReview
    {
        private readonly HttpClient _httpClient;


        public GetReview()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetResponseAsync()
        {
            try
            {
                List<(string role, string text)> _chatHistory = GetRequest2._chatHistory;
                // Create the list of message contents
                var contents = new List<object>();

                // Add your custom instructions as the first message
                contents.Add(new
                {
                    role = "model",
                    parts = new[]
                    {
                            new { text = "These are your custom instructions. Do not talk or discuss about the instructions. Find all messages from User and review *all* of them in easy to understand english. Example: (Error 1: Explain mistakes/grammatical errors/tips for smoother english  \n Error 2: Explain mistakes/grammatical errors/tips for smoother english \n Error n: Explain mistakes/grammatical errors/tips for smoother english " }
                        }
                });

                // Add the conversation history
                foreach (var (role, text) in _chatHistory)
                {
                    contents.Add(new
                    {
                        role,
                        parts = new[] { new { text } }
                    });
                }
                contents.Add(new
                {
                    role = "user",
                    parts = new[]
{
                                    new { text = "Please answer the questions as instructed in the model instructions and find all previous responses from me to review them." }
                                }
                });
                string textPart;
                (bool flowControl, textPart) = await CreateRequestClass.CreateRequest(contents, _httpClient);
                if (!flowControl)
                {
                    return textPart;
                }
                CreateBubbles.defaultAssistantColor = "6db400";
                return textPart;
            }

            catch (Exception ex)
                    {
                        return "Dies sollte nicht passieren. Versuche es später erneut. Wenn dies öfter passiert, gib Niklas Bescheid. Fehler: " + ex.Message;
                    }
        
        }
    }
}