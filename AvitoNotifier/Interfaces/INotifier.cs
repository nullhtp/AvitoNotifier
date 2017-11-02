using System.Collections.Generic;
using AvitoNotifier.Preferences;

namespace AvitoNotifier.Interfaces
{
    public interface INotifier
    {
        void Notify(NotificationRecord target, IEnumerable<Item> item);
    }
}