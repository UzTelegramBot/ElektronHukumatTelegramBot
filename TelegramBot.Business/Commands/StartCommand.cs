using System;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Business.Interface;
using TelegramBot.Business.Model;
using TelegramBot.Business.StaticService;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.Commands
{
    public class StartCommand : ICommand
    {
        public string UserName { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public User User { get; private set; }

        private readonly ApplicationDbContext _context;
        private readonly UserRepositoryAsync userRepositoryAsync;
        private readonly BotTextDataRepositoryAsync buttonRepositoryAsync;

        public StartCommand(ApplicationDbContext context, string firstName,
            string lastName,
            string userName)
        {
            this.UserName = userName;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.User = new User();
            _context = context;
            userRepositoryAsync = new UserRepositoryAsync(context);
            buttonRepositoryAsync = new BotTextDataRepositoryAsync(context);
        }
        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await userRepositoryAsync.GetById(userId);
            
            var Botdata = await buttonRepositoryAsync
                .FindByCondition(b => b.TypeData == TypeData.Hello);
            if (user is null)
            {
                User.UserId = userId;
                User.Language = Language.Uz;
                User.FirstName = FirstName;
                User.LastName = LastName;
                User.UserName = UserName;
                User.RegionId = Guid.Parse(DefaultValues.DEFAULTREGIONID);
                await userRepositoryAsync.Create(User);

                await client.SendTextMessageAsync(
                    chatId : userId,
                    text : Botdata.Uz
                    );
                await new ShowLanguagesCommand(_context).Execute(client,userId);
            }
            else
            {


                var text = GetTextFromLanguage
                    .GetText(user.Language,Botdata);

                await new SendKeyBoardButtonCommand(_context,user.Language,text)
                    .Execute(client, userId);
            }
             
        }
    }
}
