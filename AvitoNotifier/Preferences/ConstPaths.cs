using System;
using System.IO;
using System.Reflection;

namespace AvitoNotifier.Preferences
{
    public class ConstPaths
    {
        private static readonly string ApplicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string ApplicationPrefPath = ApplicationPath + "/Configs/Application.json";
        public static string UrlListPath = ApplicationPath + "/Configs/Urls.json";
        public static string NotificationPrefPath = ApplicationPath + "/Configs/Notification.json";
        public static string RulesPath = ApplicationPath + "/Configs/Rules.json";
    }
}