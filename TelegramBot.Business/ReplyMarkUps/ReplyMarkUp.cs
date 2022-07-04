using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.Model;
using TelegramBot.Domains;

namespace TelegramBot.Business.ReplyMarkUps
{
    public static class  ReplyMarkUp
    {
        public static IReplyMarkup OptionsOfLanguage()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text:"Uz",callbackData: "Uz"),
                    InlineKeyboardButton.WithCallbackData(text:"Ru",callbackData: "Ru"),
                    InlineKeyboardButton.WithCallbackData(text:"Eng",callbackData: "Eng")
                }
            });
        }
        public static InlineKeyboardMarkup OptionsRegions(
            IReadOnlyList<Region> regions            
            )
        {
            var rows = new List<InlineKeyboardButton[]>();
           
                foreach (var region in regions)
                {
                    rows.Add(new InlineKeyboardButton[]
                    {
                    InlineKeyboardButton.WithCallbackData(
                        text:region.UzName,
                        callbackData: region.Id.ToString())
                    });
                }
            rows.Add(new InlineKeyboardButton[]
            {
                    InlineKeyboardButton.WithCallbackData(
                         text : "⬅️",
                         callbackData: DefaultValues.DEFAULTREGIONID
                        )
            });


            return new InlineKeyboardMarkup(rows);

        }
        public static IReplyMarkup SendContact(string text)
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[]
            {
                KeyboardButton.WithRequestContact(text)
            });
        }
        public static IReplyMarkup SendKeyBoardButton(List<string> texts)
        {
            var rows = new List<KeyboardButton[]>();
            foreach(var text in texts)
            {
                rows.Add(new KeyboardButton[]
                {
                    new KeyboardButton(text)
                });
            }
            return new ReplyKeyboardMarkup(rows);
        }
    }
}
