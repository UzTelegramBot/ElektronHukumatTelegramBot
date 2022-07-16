using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Domains;
using Infrastructure.Interface;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Business.Services
{
    public class MessageServiceAsync :  IMessageServiceAsync
    {
        private readonly ITelegramBotClient client;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageServiceAsync(
            ITelegramBotClient client,
            IUnitOfWork unitOfWork,
            IMapper mapper) 
        {
            this.client = client;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MessageDTO> CreateAsync(MessageForCreationDTO messageForCreationDTO, string language)
        {
            var message = await _unitOfWork.MessageRepository
                 .CreateAsync(_mapper.Map<Message>(messageForCreationDTO));
            await _unitOfWork.SaveChangesAsync();
            Language languageOfUser = language switch
            {
                "uz" => Language.Uz,
                "ru" => Language.Ru,
                "eng" => Language.Eng,
                _ => Language.Uz
            };
            await SendMessageAsync(message, languageOfUser);
            return _mapper.Map<MessageDTO>(message);
        }
        private async Task SendMessageAsync(Message message, Language language)
        {
            int countOfRegion = await _unitOfWork
                .RegionRepository
                .GetCount(message.RegionId);
            int pageSize = 10;  
            int pageRegion = countOfRegion / pageSize + 1;
            for(int i = 1; i <= pageRegion; i++)
            {
                var regions = await _unitOfWork
                    .RegionRepository
                    .GetListById(message.RegionId,i, pageSize);
                foreach(var region in regions)
                {
                    int CountOfUser = _unitOfWork
                        .UserRepository.GetCount(region.Id);
                    int pageUser = CountOfUser / pageSize +1;
                    for(int j = 1; j <= pageUser; j++)
                    {
                     var users = await _unitOfWork.UserRepository
                        .GetListById(region.Id,language, j, pageSize);
                        foreach (User user in users)
                        {
                            await client.SendTextMessageAsync(
                                chatId: user.UserId,
                                text: $"<b>{message.Title}</b>\n\n" +
                                      $"{message.Content}",
                                parseMode: ParseMode.Html
                                );
                        }
                    }
                }
            }
        }

        public async Task<MessageDTO> GetByIdAsync(Guid Id)
        {
            var message = await _unitOfWork.MessageRepository
                .FindByCondition(m => m.Id == Id);
            return _mapper.Map<MessageDTO>(message);
        }

        
    }
}
