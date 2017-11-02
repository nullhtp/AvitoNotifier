using System;
using System.Collections.Generic;
using AvitoNotifier.Interfaces;
using AvitoNotifier.Preferences;
using NLog;

namespace AvitoNotifier
{
    public class NotificationService : INotification
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ITelegramNotifier _telegramService;
        private readonly IEmailNotifier _emailService;
        private readonly List<NotificationRecord> _records;

        public NotificationService(ITelegramNotifier telegramService, IEmailNotifier emailService,
            List<NotificationRecord> records)
        {
            _telegramService = telegramService;
            _emailService = emailService;
            _records = records;
        }

        public void Notificate(IEnumerable<Item> items)
        {
            try
            {
                ConsoleNotify(items);
                foreach (var record in _records)
                {
                    try
                    {
                        switch (record.Type)
                        {
                            case "Telegram":
                                _telegramService.Notify(record, items);
                                break;
                            case "Email":
                                _emailService.Notify(record, items);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e, $"Notificate {record.Type} - {record.Value} FAILED");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "Send notification FAILED");
            }
        }

        private static void ConsoleNotify(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                Logger.Info(item);
            }
        }
    }
}