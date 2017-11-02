using System.Collections.Generic;

namespace AvitoNotifier.Interfaces
{
    public interface INotification
    {
        void Notificate(IEnumerable<Item> items);
    }
}