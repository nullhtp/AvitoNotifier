using System.Collections.Generic;

namespace AvitoNotifier.Interfaces
{
    public delegate void ParsedItems(ICollection<Item> item);
    public delegate void NewItems(IEnumerable<Item> items);
}