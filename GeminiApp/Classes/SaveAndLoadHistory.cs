using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GeminiApp.Classes.Interactions;

namespace GeminiApp.Classes
{
    public static class SaveAndLoadHistory
    {
        private static readonly string fileName = "chatHistory.json"; 
        public class ChatEntry
        {
            public string Role { get; set; }
            public string Text { get; set; }
        }
        private static readonly string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
        public static async void AddToHistory(List<(string role, string text)> _chatHistory)
        {
            List<ChatEntry> entries = _chatHistory.Select(x => new ChatEntry { Role = x.role, Text = x.text }).ToList();
            string json = JsonSerializer.Serialize(entries);
            await File.WriteAllTextAsync(filePath, json);
        }
        public static void LoadData()
        {
            List<(string role, string text)> _chatHistory = new();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<ChatEntry>? entries = JsonSerializer.Deserialize<List<ChatEntry>>(json); // Use nullable type
                if (entries != null) // Add null check
                {
                    GetRequest2._chatHistory = entries.Select(x => (x.Role, x.Text)).ToList();
                }
                
            }  
        }
    }
}
