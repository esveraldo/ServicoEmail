using AutoMapper;
using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Application.Dtos.Emails;
using ServicoDeEmail.Application.Dtos.ServicesUsers;
using ServicoDeEmail.Application.Dtos.User;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Application.Mappings
{
    public class ModelToDtoMap : Profile
    {
        public ModelToDtoMap()
        {
            CreateMap<Email, GetEmailDto>();
            CreateMap<ServiceUsers, GetUserDto>();
            CreateMap<ServiceUsers, CreateConsumerDto>();
            CreateMap<IEnumerable<ServiceUsers>, IEnumerable<CreateConsumerDto>>();
            CreateMap<ServiceUsers, ServiceUsersDto>();
            CreateMap<ConsumerUsers, ConsumerUsersDto>();
        }
    }
}
