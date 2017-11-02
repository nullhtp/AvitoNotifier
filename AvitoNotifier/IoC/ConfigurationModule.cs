using System;
using System.Collections.Generic;
using Autofac;
using AvitoNotifier.Interfaces;
using AvitoNotifier.Preferences;
using NLog;

namespace AvitoNotifier.IoC
{
    public static class ConfigurationModule
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static List<HuntingUrl> HuntingUrls;
        public static List<NotificationRecord> NotificationRecords;
        public static ApplicationPref ApplicationPref;
        public static List<Rule> ParserRules;

        public static IContainer Configure()
        {
            LoadPreferences();

            var builder = new ContainerBuilder();
            builder.RegisterType<TelegramService>()
                .As<ITelegramNotifier>()
                .WithParameter(
                    new NamedParameter("apiKey", ApplicationPref.TelegramApi)
                )
                .SingleInstance();

            builder.RegisterType<EmailService>()
                .As<IEmailNotifier>()
                .WithParameters(new[]
                {
                    new NamedParameter("server", ApplicationPref.TelegramApi),
                    new NamedParameter("fromEmail", ApplicationPref.Email),
                    new NamedParameter("fromName", ApplicationPref.EmailSender),
                    new NamedParameter("password", ApplicationPref.EmailPassword),
                    new NamedParameter("port", ApplicationPref.EmailPort)
                })
                .SingleInstance();

            builder.RegisterType<ManagementService>().As<IManagement>().SingleInstance();
            builder.RegisterType<ParserService>().As<IParser>().SingleInstance();
            builder.RegisterType<NotificationService>()
                .As<INotification>()
                .WithParameter(
                    new NamedParameter("records", NotificationRecords)
                )
                .SingleInstance();

            return builder.Build();
        }

        private static void LoadPreferences()
        {
            try
            {
                ApplicationPref = PrefUtils<ApplicationPref>.Get(ConstPaths.ApplicationPrefPath);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Application preferences cannot parse");
                throw;
            }
            try
            {
                HuntingUrls = PrefUtils<List<HuntingUrl>>.Get(ConstPaths.UrlListPath);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Hunting urls cannot parse");
                throw;
            }
            try
            {
                NotificationRecords = PrefUtils<List<NotificationRecord>>.Get(ConstPaths.NotificationPrefPath);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Notification records cannot parse");
                throw;
            }
            try
            {
                ParserRules = PrefUtils<List<Rule>>.Get(ConstPaths.RulesPath);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Rules cannot parse");
                throw;
            }
            Logger.Info("\n---------Preferences Loaded---------\n" +
                        $"{ApplicationPref} \n\n" +
                        $"Parser Rules Loaded: {ParserRules.Count}\n" +
                        $"Hunting Urls Loaded: {HuntingUrls.Count}\n" +
                        $"Notification Records Loaded: {NotificationRecords.Count}\n");
        }
    }
}