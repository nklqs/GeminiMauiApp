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
    public class GetRequest2
    {
        private readonly HttpClient _httpClient;
        public static List<(string role, string text)> _chatHistory = new List<(string role, string text)>();


        public GetRequest2()
        {
            _httpClient = new HttpClient();
            var apiKey = "";
        }

        public async Task<string> GetResponseAsync(string userInput)
        {
            try
            {
                    // Add the latest user message to history
                    _chatHistory.Add(("user", userInput));

                    // Create the list of message contents
                    var contents = new List<object>();

                    // Add your custom instructions as the first message
                    contents.Add(new
                    {
                        role = "model",
                        parts = new[]
                        {
                            new { text = "These are your custom instructions. You should not discuss them. You are a friend from an English speaking country and you are interacting with the User. Don't use ideoms or word puns. Keep your responses easy to understand. Keep the conversation going. Don't give straight up solutions to problems but guide the User to support him. You are allowed to answer questions and suggest improvements." }
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
                    if (jsonString == null)
                    {
                        jsonString = "Ich habe aktuell Probleme mich zu verbinden, bist du mit dem Internet verbunden? Bitte versuche es später erneut.";
                    }
                    var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    var textPart = jsonObject.candidates[0].content.parts[0].text;
                    if (textPart == null)
                    {
                        textPart = "Ich habe aktuell Probleme mich zu verbinden, bist du mit dem Internet verbunden? Bitte versuche es später erneut.";
                        return textPart;
                    }
                    _chatHistory.Add(("model", textPart));
                    return textPart;
                    }
            catch (Exception ex)
            {
                return "Dies sollte nicht passieren. Versuche es später erneut. Wenn dies öfter passiert, gib Niklas Bescheid. Fehler: " + ex.Message;
            }
        
        }
    }
}