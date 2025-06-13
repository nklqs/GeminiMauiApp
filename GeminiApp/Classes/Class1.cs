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
        private readonly static List<(string role, string text)> _chatHistory = GetRequest2._chatHistory; // Static list to hold chat history
        public static async void AddToHistory()
        {
            List<ChatEntry> entries = _chatHistory.Select(x => new ChatEntry{Role = x.role, Text = x.text}).ToList();
            string json = System.Text.Json.JsonSerializer.Serialize(entries);
            await File.WriteAllTextAsync(filePath, json);
        }
        public static async void LoadData()
        {
            if(File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                List<ChatEntry> entries = JsonSerializer.Deserialize<List<ChatEntry>>(json);
                //_chatHistory = entries.Select<ChatEntry<>>
            }
        }
    }
}
