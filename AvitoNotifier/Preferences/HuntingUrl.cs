namespace AvitoNotifier.Preferences
{
    public class HuntingUrl
    {
        public string Name { get; set; }
        public string Site { get; set; }
        public string Url { get; set; }

        public HuntingUrl(string name,string site, string url)
        {
            Site = site;
            Url = url;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} : {Site} - {Url}";
        }
    }
}