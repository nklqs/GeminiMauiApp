using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeminiApp.Classes;

public static class NetworkStatus
{
    public static string CheckNetworkConnection()
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            return null;
        }
        if (accessType == NetworkAccess.Local)
        {
            CreateBubbles.defaultAssistantColor = "f24214";
            return "Um diese Anwendung nutzen zu können ist Internet notwendig. Bitte verbinde dich mit dem Internet!";
        }
        if (accessType == NetworkAccess.None)
        {
            CreateBubbles.defaultAssistantColor = "f24214";
            return "Um diese Anwendung nutzen zu können ist Internet notwendig. Bitte verbinde dich mit dem Internet!";
        }

        return null;
    }
}