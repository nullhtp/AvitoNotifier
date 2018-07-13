using System;
using System.Collections.Generic;
using System.Net;
using AvitoNotifier.Extensions;
using AvitoNotifier.Interfaces;
using AvitoNotifier.Preferences;
using NLog;
using Telegram.Bot;

namespace AvitoNotifier
{
    public class TelegramService : ITelegramNotifier
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private static TelegramBotClient _bot;

        public TelegramService(string apiKey, ProxySettings proxySettings)
        {
            try
            {
                var webProxy = new WebProxy(proxySettings.Url, proxySettings.Port);
                if (!string.IsNullOrEmpty(proxySettings.UserName))
                {
                    webProxy.Credentials = new NetworkCredential
                    {
                        UserName = proxySettings.UserName,
                        Password = proxySettings.Password
                    };
                }
                _bot = new TelegramBotClient(apiKey, webProxy);
            }
            catch (Exception e)
            {
                _logger.Error($"Connection error: {e.Message}");
                throw;
            }
        }

        private async void SendMessage(string message, int id)
        {
            try
            {
                await _bot.SendTextMessageAsync(id, message);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Telegram notification FAILED for id {id}");
            }
        }

        public void Notify(NotificationRecord target, IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                var id = int.Parse(target.Value);
                var message = target.Template.TemplateReplace(item);
                SendMessage(message, id);
            }
        }
    }
}