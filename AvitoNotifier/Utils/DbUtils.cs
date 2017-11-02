using System;
using System.Collections.Generic;
using NLog;

namespace AvitoNotifier
{
    public class DbUtils
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static void AddItems(IEnumerable<Item> cars)
        {
            try
            {
                using (var db = new AvitoContext())
                {
                    db.Items.AddRange(cars);
                    var count = db.SaveChanges();
                    Logger.Info($"{count} records saved to database");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "Add Items to DB FAILD");
            }
        }
    }
}