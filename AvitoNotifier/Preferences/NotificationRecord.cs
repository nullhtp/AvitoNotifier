namespace AvitoNotifier.Preferences
{
    public class NotificationRecord
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Template { get; set; }

        public NotificationRecord(string type, string value,string template = "{title}\n{url}")
        {
            Type = type;
            Value = value;
            Template = template;
        }
        
        public override string ToString()
        {
            return $"{Type} : {Value}";
        }
    }
}