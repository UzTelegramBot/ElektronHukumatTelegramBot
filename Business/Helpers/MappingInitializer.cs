using AutoMapper;
using Business.ModelDTO;
using Domains;

namespace Business.Helpers
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<Manager, ManagerForCreationDTO>().ReverseMap();
            CreateMap<Manager, ManagerDTO>().ReverseMap();

            CreateMap<Region, RegionForCreationDTO>().ReverseMap();
            CreateMap<Region, RegionDTO>().ReverseMap();

            CreateMap<Organization, OrganizationForCreationDTO>().ReverseMap();
            CreateMap<Organization, OrganizationDTO>().ReverseMap();

            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<Message, MessageForCreationDTO>().ReverseMap();
        }
    }
}
