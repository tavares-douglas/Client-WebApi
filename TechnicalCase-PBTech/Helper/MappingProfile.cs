using AutoMapper;
using TechnicalCase_PBTech.Dto;
using TechnicalCase_PBTech.Models;

namespace TechnicalCase_PBTech.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
        }
        
    }
}
