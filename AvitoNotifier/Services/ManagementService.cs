using System;
using System.Collections.Generic;
using System.Linq;
using AvitoNotifier.Interfaces;
using NLog;

namespace AvitoNotifier
{
    public class ManagementService : IManagement
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public event NewItems AddedNewItems;

        public void Manage(IEnumerable<Item> items)
        {
            FilterAndAddItems(items);
        }
        
        private void FilterAndAddItems(IEnumerable<Item> items)
        {
            if (!items.Any())
            {
                _logger.Error("Get item FAILED");
            }
            var filteredItems = FilterItems(items);
            if (!filteredItems.Any()) return;
            DbUtils.AddItems(filteredItems);
            AddedNewItems(filteredItems);
        }

        private IEnumerable<Item> FilterItems(IEnumerable<Item> items)
        {
            var listItems = new List<Item>();
            using (var db = new AvitoContext())
            {
                try
                {
                    listItems.AddRange(items.Where(item => db.Items.FirstOrDefault(i => i.Link == item.Link) == null));
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error add range");
                }
            }
            return listItems;
        }

    }
}