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
        private List<(string role, string text)> _chatHistory = GetRequest2._chatHistory;


        public GetTranslation()
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
                            new { text = "These are your custom instructions. Translate the last response fully to german. 1. Write down your last response you must translate again the last response 2. then the translation 3. Only write down words in vocabulary style tht might be difficult to understand (english word = translation, next word = translation, (all in the same line to save space)" }
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
                                new { text = "Please answer the question as described in the instructions" }
                            }
                    });
                    var requestBody = new { contents = contents };
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