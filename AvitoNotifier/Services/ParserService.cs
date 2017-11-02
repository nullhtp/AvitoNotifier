using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using AvitoNotifier.Extensions;
using AvitoNotifier.Interfaces;
using AvitoNotifier.Preferences;
using NLog;

namespace AvitoNotifier
{
    public class ParserService : IParser
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public event ParsedItems ParsedSuccess;

        public async void Parse(Rule rule, HuntingUrl url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            try
            {
                var document = await BrowsingContext.New(config).OpenAsync(url.Url);
                var itemCells = document.QuerySelectorAll(rule.ItemSelector);
                var items = itemCells.Select(c => new Item
                {
                    Title = c.QuerySelector(rule.TitleSelector)?.TextContent?.CleanResult() ?? "",
                    About = c.QuerySelector(rule.AboutSelector)?.TextContent?.CleanResult() ?? "",
                    Link = rule.LinkPrefix + c.QuerySelector(rule.TitleSelector)?.Attributes.GetNamedItem("href")?.Value
                               ?.CleanResult(),
                    Date = c.QuerySelector(rule.DateSelector)?.TextContent?.CleanResult() ?? ""
                }).ToList();
                ParsedSuccess(items);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
    }
}