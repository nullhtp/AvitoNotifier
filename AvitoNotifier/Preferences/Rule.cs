namespace AvitoNotifier
{
    public class Rule
    {
        public string Name { get; set; }
        public string ItemSelector { get; set; }
        public string TitleSelector { get; set; }
        public string LinkSelector { get; set; }
        public string LinkPrefix { get; set; }
        public string AboutSelector { get; set; }
        public string DateSelector { get; set; }

        public Rule(string name, string itemSelector, string titleSelector, string linkSelector,
            string aboutSelector, string dateSelector,string linkPrefix="")
        {
            Name = name;
            ItemSelector = itemSelector;
            TitleSelector = titleSelector;
            LinkSelector = linkSelector;
            LinkPrefix = linkPrefix;
            AboutSelector = aboutSelector;
            DateSelector = dateSelector;
        }

        public override string ToString()
        {
            return $"{Name}\n" +
                   $"{ItemSelector}\n" +
                   $"{TitleSelector}\n" +
                   $"{LinkSelector}\n" +
                   $"{AboutSelector}\n" +
                   $"{DateSelector}\n";
        }
    }
}