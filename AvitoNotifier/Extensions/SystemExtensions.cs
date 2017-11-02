
namespace AvitoNotifier.Extensions
{
    public static class SystemExtensions
    {
        public static string CleanResult(this string str)
        {
            return str.Replace("\n", " ").Trim();
        }
        
        public static string TemplateReplace(this string str,Item item)
        {
            return str
                .Replace("{id}", item.Id.ToString())
                .Replace("{title}", item.Title)
                .Replace("{about}",item.About)
                .Replace("{date}",item.Date)
                .Replace("{url}",item.Link);
        }
    }
}