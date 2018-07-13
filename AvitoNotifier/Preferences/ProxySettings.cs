namespace AvitoNotifier.Preferences
{
    public class ProxySettings
    {
        public string Url { get; set; } = "";
        public int Port { get; set; } = 8080;
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        
        public ProxySettings()
        {
        }
    }
}