using System;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Business.Interface;
using TelegramBot.Business.Model;
using TelegramBot.Business.ReplyMarkUps;
using TelegramBot.Business.StaticService;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.Commands
{
    public class ShowListRegionsByTextCommand : ICommand
    {
        private readonly UserRepositoryAsync userRepository;
        private readonly string regionId;
        private readonly BotTextDataRepositoryAsync botTextDataRepository;
        private readonly RegionRepositoryAsync regionRepository;

        public ShowListRegionsByTextCommand(ApplicationDbContext context)
        {
            this.userRepository = new UserRepositoryAsync(context);
            this.regionId = DefaultValues.DEFAULTREGIONID;
            this.botTextDataRepository = new BotTextDataRepositoryAsync(context);
            regionRepository = new RegionRepositoryAsync(context);
        }

        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await userRepository.GetById(userId);

            var data = await botTextDataRepository
                .FindByCondition(b => b.TypeData == Domains.TypeData.Area);

            var text = GetTextFromLanguage.GetText(user.Language, data);

            var regions = await regionRepository.GetRegionLayerAsync(Guid.Parse(regionId));
            if(regions.Count != 0)
            {
            await client.SendTextMessageAsync(
                chatId: userId,
                text: text,
                replyMarkup: ReplyMarkUp.OptionsRegions(regions) 
                );
            }
        }
    }
}
