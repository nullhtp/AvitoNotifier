using System.Collections.Generic;

namespace AvitoNotifier.Interfaces
{
    public interface IManagement
    {
        event NewItems AddedNewItems;
        void Manage(IEnumerable<Item> items);
    }
}