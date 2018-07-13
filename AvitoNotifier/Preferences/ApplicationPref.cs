namespace AvitoNotifier.Preferences
{
    public class ApplicationPref
    {
        public string TelegramApi = "";
        public string Email = "";
        public string EmailSender = "Avito test";
        public string EmailPassword = "";
        public string EmailServer = "smtp.yandex.ru";
        public int EmailPort = 25;
        public int UpdateTime = 7 * 60 * 1000;
        public int DelayMax = 6 * 1000;
        public int DelayMin = 4 * 1000;
        public ProxySettings ProxySettings = null;

        public override string ToString()
        {
            return $"Telegram Api: {TelegramApi}\n" +
                   $"Email: {Email}\n" +
                   $"EmailSender: {EmailSender}\n" +
                   $"EmailServer: {EmailServer}\n" +
                   $"EmailPort: {EmailPort}\n" +
                   $"UpdateTime: {UpdateTime}\n" +
                   $"DelayMax: {DelayMax}\n" +
                   $"DelayMin: {DelayMin}\n"
                ;
        }
    }
}