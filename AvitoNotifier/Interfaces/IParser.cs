using AvitoNotifier.Preferences;

namespace AvitoNotifier.Interfaces
{
    public interface IParser
    {
        event ParsedItems ParsedSuccess;
        void Parse(Rule rule,HuntingUrl url);
    }
}