using AutoMapper;
using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Application.Dtos.Emails;
using ServicoDeEmail.Application.Dtos.ServicesUsers;
using ServicoDeEmail.Application.Dtos.User;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Application.Mappings
{
    public class DtoToModelMap : Profile
    {
        public DtoToModelMap()
        {
            CreateMap<CreateEmailDto, Email>()
                .AfterMap((dto, entity) =>
                {
                    entity.Id = Guid.NewGuid();
                });

            CreateMap<CreateConsumerDto, ServiceUsers>();
            CreateMap<GetUserDto, ServiceUsers>();    
            CreateMap<IEnumerable<GetUserDto>, IEnumerable<ServiceUsers>>();
            CreateMap<ServiceUsersDto, ServiceUsers>();
            CreateMap<ConsumerUsersDto, ConsumerUsers>();
        }
    }
}
