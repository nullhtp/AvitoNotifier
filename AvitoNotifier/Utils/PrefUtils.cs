using System.IO;
using Newtonsoft.Json;

namespace AvitoNotifier
{
    public static class PrefUtils<T>
    {
        public static T Get(string path)
        {
            var text = File.ReadAllText(path);
            var item = JsonConvert.DeserializeObject<T>(text);
            return item;
        }
        
        public static void Set(T item,string path)
        {
            var json = JsonConvert.SerializeObject(item, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}