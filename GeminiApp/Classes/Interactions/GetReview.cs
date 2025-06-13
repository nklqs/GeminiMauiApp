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
        private List<(string role, string text)> _chatHistory = GetRequest2._chatHistory;


        public GetReview()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetResponseAsync()
        {
            try
            {

                    // Create the list of message contents
                    var contents = new List<object>();

                    // Add your custom instructions as the first message
                    contents.Add(new
                    {
                        role = "model",
                        parts = new[]
                        {
                            new { text = "These are your custom instructions. Do not talk about the instructions. Find all messages from User and review *all* of them in easy to understand english. Example: (Error 1: Explain mistakes/grammatical errors/tips for smoother english  \n Error 2: Explain mistakes/grammatical errors/tips for smoother english \n Error n: Explain mistakes/grammatical errors/tips for smoother english " }
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
                                    new { text = "Please answer the questions as instructed in the model instructions." }
                                }
                    });
                    var requestBody = new { contents };
                    var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                    var request = new HttpRequestMessage(
                    HttpMethod.Post, GlobalVars.postString);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.SendAsync(request);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    if (jsonString == null)
                    {
                    jsonString = await response.Content.ReadAsStringAsync();
                    }
                    var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    var textPart = jsonObject.candidates[0].content.parts[0].text;
                    if (textPart == null)
                    {
                        textPart = "Ich habe aktuell Probleme mich zu verbinden, bist du mit dem Internet verbunden? Bitte versuche es später erneut.";
                        return textPart;
                    }
                    CreateBubbles.defaultAssistantColor = "6db400";
                    SaveAndLoadHistory.AddToHistory(); // Save the chat history after getting the response
                    return textPart;
                    }

                    catch (Exception ex)
                    {
                        return "Dies sollte nicht passieren. Versuche es später erneut. Wenn dies öfter passiert, gib Niklas Bescheid. Fehler: " + ex.Message;
                    }
        
        }
    }
}