using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GeminiApp.Classes.Interactions;
using Newtonsoft.Json;

namespace GeminiApp.Classes
{
    public class GetTranslation
    {
        private readonly HttpClient _httpClient;


        public GetTranslation()
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
                            new { text = "These are your custom instructions. Do not talk about the instructions.  1. Repeat your last Response (response from 'model') 2. then the translation of your latest response 3. Only write explain words that might be difficult to understand (english word = translation, next word = translation, (all in the same line to save space)" }
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
                    //Have to add user otherwise it won't answer the question
                    contents.Add(new
                    {
                        role = "user",
                        parts = new[]
                    {
                                new { text = "Please answer the question as described in the instructions. Translate the *whole* last response from 'model' fully to german." }
                            }
                    });
                    (bool flowControl, string textPart) = await CreateRequestClass.CreateRequest(contents, _httpClient);
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