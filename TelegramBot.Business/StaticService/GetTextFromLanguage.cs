using System.Collections.Generic;
using TelegramBot.Domains;

namespace TelegramBot.Business.StaticService
{
    public static class GetTextFromLanguage
    {
        public static string GetText(Language language, BotTextData data)
        {
            return language switch
            {
                Language.Uz => data.Uz,
                Language.Ru => data.Ru,
                Language.Eng => data.Eng,
                _ => data.Uz
            };
        }
        public static List<string> GetKeyBoardText(Language language, List<BotTextData> KeyBoards)
        {
            List<string> textKeyboards = new List<string>();
            foreach (var keyBoard in KeyBoards)
            {
                textKeyboards.Add(GetTextFromLanguage.GetText(language, keyBoard));
            }
            return textKeyboards;
        }
    }
}
