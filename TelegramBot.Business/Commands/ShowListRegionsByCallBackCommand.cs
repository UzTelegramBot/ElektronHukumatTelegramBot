using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Business.Interface;
using TelegramBot.Business.Model;
using TelegramBot.Business.ReplyMarkUps;
using TelegramBot.Business.StaticService;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.Commands
{
    public  class ShowListRegionsByCallBackCommand : ICommand
    {
        private readonly UserRepositoryAsync userRepository;
        private readonly string RegionId;

        public int MessageId { get; }
        public Chat Chat { get; }

        private readonly RegionRepositoryAsync regionRepository;
        private readonly BotTextDataRepositoryAsync botTextDataRepository;
        private readonly ApplicationDbContext _context;

        public ShowListRegionsByCallBackCommand(ApplicationDbContext context,
            string regionId, 
            int messageId,
            Chat chat)
        {
            this.userRepository = new UserRepositoryAsync(context);
            this.RegionId = regionId;
            this.MessageId = messageId;
            this.Chat = chat;
            this.regionRepository = new RegionRepositoryAsync(context);
            this.botTextDataRepository = new BotTextDataRepositoryAsync(context);
            this._context = context;
        }

        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var regions = await regionRepository
                .GetRegionLayerAsync(Guid.Parse(RegionId));

            var user = await userRepository.GetById(userId);
                user.RegionId = Guid.Parse(RegionId);
                await userRepository.Update(user);

            if(regions.Count > 0)
            {
                await client.EditMessageReplyMarkupAsync(
                Chat,
                MessageId,
                replyMarkup: ReplyMarkUp.OptionsRegions(regions)
                ); 
            }
            else
            {
                var data = await botTextDataRepository
                    .FindByCondition(b => b.TypeData == TypeData.Aware);
                var text = GetTextFromLanguage.GetText(user.Language, data);

                await client.DeleteMessageAsync(
                Chat,
                MessageId
                );
                await new SendKeyBoardButtonCommand(_context, user.Language, text)
                    .Execute(client, userId);

            }
        }
    }
}
