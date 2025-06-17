using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeminiApp.Classes
{
    public static class GlobalVars
    {
        public static string postString = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-lite:generateContent?key=AIzaSyA0gYnwEtGBwxAHZ_OQf5Zi45ErhxuxMO4";
    }
    public static class CreateRequestClass
    {
        public static async Task<(bool flowControl, string textPart)> CreateRequest(List<object> contents, HttpClient _httpClient)
        {
            string network = NetworkStatus.CheckNetworkConnection();
            if (network != null)
            {
                return (flowControl: false, textPart: network);
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
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
            var textPart = jsonObject.candidates[0].content.parts[0].text;
            if (textPart == null)
            {
                textPart = "Ich habe aktuell Probleme mich zu verbinden, bist du mit dem Internet verbunden? Bitte versuche es später erneut.";
                return (flowControl: false, textPart);
            }
            return (flowControl: true, textPart);
        }
    }
}
