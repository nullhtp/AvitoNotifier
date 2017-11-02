using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Autofac;
using AvitoNotifier.Interfaces;
using AvitoNotifier.IoC;
using AvitoNotifier.Preferences;
using NLog;
using NLog.Fluent;

namespace AvitoNotifier
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetLogger("Main");

        private static readonly IContainer Container = ConfigurationModule.Configure();
        private static readonly IParser Parser = Container.Resolve<IParser>();
        private static readonly IManagement Management = Container.Resolve<IManagement>();
        private static readonly INotification Notifier = Container.Resolve<INotification>();


        private static void Main()
        {
            //SetupData();
            Configuration();
            while (true)
            {
                try
                {
                    foreach (var url in ConfigurationModule.HuntingUrls)
                    {
                        var rule = ConfigurationModule.ParserRules.Find(r => r.Name == url.Site);
                        if (rule != null)
                        {
                            Parser.Parse(rule, url);
                            Thread.Sleep(new Random().Next(ConfigurationModule.ApplicationPref.DelayMin,
                                ConfigurationModule.ApplicationPref.DelayMax));
                        }
                    }
                    Logger.Info("Succes");
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                Thread.Sleep(ConfigurationModule.ApplicationPref.UpdateTime);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void Configuration()
        {
            Parser.ParsedSuccess += Management.Manage;
            Management.AddedNewItems += Notifier.Notificate;
        }

        // ReSharper disable once UnusedMember.Local
        private static void SetupData()
        {
            var listUrls = new List<HuntingUrl>
            {
                new HuntingUrl("AvitoFlat", "Avito",
                    "https://www.avito.ru/kaliningradskaya_oblast/kvartiry/sdam/na_dlitelnyy_srok?pmax=15000&s=104"),
                new HuntingUrl("AvitoHouse", "Avito",
                    "https://www.avito.ru/kaliningradskaya_oblast/doma_dachi_kottedzhi/sdam/dom/na_dlitelnyy_srok?s=104"),
                new HuntingUrl("Ya39Flat", "Ya39",
                    "https://ya39.ru/realty/?action_id=4&params_btw%5B36%5D%5B%5D=&params_btw%5B36%5D%5B%5D=16000&text=&params_btw_sqr_m%5B8%5D%5B%5D=&params_btw_sqr_m%5B8%5D%5B%5D=&params_btw_sqr_m%5B9%5D%5B%5D=&params_btw_sqr_m%5B9%5D%5B%5D=&params_btw_sqr_m%5B10%5D%5B%5D=&params_btw_sqr_m%5B10%5D%5B%5D=&search_adv=N"),
                new HuntingUrl("IrrFlat", "Irr",
                    "http://kaliningrad.irr.ru/real-estate/rent/search/price=%20%D0%B4%D0%BE%2016%20000/"),
                new HuntingUrl("2RentFlat", "2Rent",
                    "https://2rent.pro/offer-live/index?OfferLiveSearch%5Bcity_id%5D=111236&OfferLiveSearch%5Bobj_type%5D=&OfferLiveSearch%5Bprice1%5D=&OfferLiveSearch%5Bprice2%5D=16000&OfferLiveSearch%5Bdistrict_id%5D=&OfferLiveSearch%5Bmetro_id%5D=&OfferLiveSearch%5Bstreet_id%5D=&OfferLiveSearch%5Bhouse%5D=&OfferLiveSearch%5Bsquare1%5D=&OfferLiveSearch%5Bsquare2%5D=&OfferLiveSearch%5Bstage1%5D=&OfferLiveSearch%5Bstage2%5D=&OfferLiveSearch%5Bstage_count1%5D=&OfferLiveSearch%5Bstage_count2%5D=&OfferLiveSearch%5Bmaterial%5D=&OfferLiveSearch%5Bcurrency%5D=&OfferLiveSearch%5Bwarning%5D=&OfferLiveSearch%5Badmin_check%5D=&OfferLiveSearch%5Bid%5D=&OfferLiveSearch%5Bis_sell%5D=&OfferLiveSearch%5Bsearch_period%5D=30&OfferLiveSearch%5Buser_id%5D=")
            };

            var listNotification = new List<NotificationRecord>
            {
                new NotificationRecord("Telegram", ""),
                new NotificationRecord("Telegram", ""),
                //new NotificationRecord("Email", ""),
                //new NotificationRecord("Viber", "")
            };

            var listRules = new List<Rule>
            {
                new Rule("Avito", "div.item", "h3.title a", "h3.title a", "div.about", "div.c-2", "avito.ru"),
                new Rule("Ya39", "div.specBox", "a.preTit", "a.preTit", "div.preTxt", "div.date", "ya39.ru"),
                new Rule("Irr", "div.listing__item", "a.listing__itemTitle", "a.listing__itemTitle",
                    "div.listing__itemPrice", "div.updateProduct"),
                new Rule("2Rent", "div.test-result-row", "a.c-font-16", "a.c-font-16", "div.offer-add-info-div",
                    "span.hidden-md", "2rent.pro")
            };
            var appPref = new ApplicationPref();
            PrefUtils<List<Rule>>.Set(listRules, ConstPaths.RulesPath);
            PrefUtils<ApplicationPref>.Set(appPref, ConstPaths.ApplicationPrefPath);
            PrefUtils<List<HuntingUrl>>.Set(listUrls, ConstPaths.UrlListPath);
            PrefUtils<List<NotificationRecord>>.Set(listNotification, ConstPaths.NotificationPrefPath);
        }
    }
}