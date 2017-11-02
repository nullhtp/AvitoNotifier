namespace AvitoNotifier
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string About { get; set; }
        public string Date { get; set; }

        public override string ToString()
        {
            return $"{Title} \n {Link} \n {About} \n {Date} \n";
        }

    }
    
}