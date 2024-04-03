using AutoMapper;
using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Application.Dtos.Emails;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Application.Mappings
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CreateEmailDto, Email>();
                cfg.CreateMap<AttachmentsDto, Attachments>();
                cfg.CreateMap<CreateConsumerDto, ServiceUsers>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
