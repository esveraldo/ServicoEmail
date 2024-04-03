using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Application.Dtos.ServicesUsers;
using ServicoDeEmail.Application.Dtos.User;
using ServicoDeEmail.Application.Interfaces;
using ServicoDeEmail.Application.Mappings;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace ServicoDeEmail.Application.Services
{
    public class ServiceUsersAppService : IServiceUsersAppService
    {
        private readonly IMapper _mapper;
        private readonly IServiceUsersDomainService _serviceDomainService;
        private readonly IConsumerUsersDomainService _consumerUsersDomainService;
        private readonly ISecurityDomainService _securityDomainService;
        private readonly IConfiguration _configuration;

        public ServiceUsersAppService(IMapper mapper, IServiceUsersDomainService serviceDomainService, IConsumerUsersDomainService consumerUsersDomainService, IConfiguration configuration, ISecurityDomainService securityDomainService)
        {
            _mapper = mapper;
            _serviceDomainService = serviceDomainService;
            _consumerUsersDomainService = consumerUsersDomainService;
            _configuration = configuration;
            _securityDomainService = securityDomainService;
        }

        public async Task<string> Add(ServiceUsersDto serviceUsersDto)
        {
            string msg;

            var serviceUser = new ServiceUsers
            {
                Id = serviceUsersDto.Id,
                Smtp = serviceUsersDto.Smtp,
                Port = serviceUsersDto.Port,
                AccessKey = serviceUsersDto.AccessKey,
                Active = serviceUsersDto.Active,
                NameUser = serviceUsersDto.NameUser,    
            };

            var validate = serviceUser.Validate;
            if (!validate.IsValid)
                throw new ValidationException(validate.Errors);

            await _serviceDomainService.NewUser(serviceUser);

            foreach (var item in serviceUsersDto.ConsumerUsersDto)
            {
                var consumerUser = new ConsumerUsers { 
                    Id = item.Id,
                    Consumer = item.Consumer,
                    ConsumerPassword = _securityDomainService.EncryptString(_configuration.GetSection("SecuritySettings").GetSection("SecretKey").Value, item.ConsumerPassword),
                    Counter = item.Counter,
                    CreateDate = item.CreateDate,
                    CurrentDate = item.CurrentDate,
                    ServiceUsersId = serviceUsersDto.Id
                };

                await _consumerUsersDomainService.NewConsumer(consumerUser);
            }

            

            msg = "Usuário cadastrado com sucesso!";

            return msg;
        }

        public async Task<List<GetUserDto>> GetAll()
        {
            return _mapper.Map<List<GetUserDto>>(await _serviceDomainService.GetAllUsers());
        }

        public async Task ActivatingUserOfSystem(Guid id)
        {
            await _serviceDomainService.ActivatingUser(id);
        }

        public async Task InactivatingUserOfSystem(Guid id)
        {
            await _serviceDomainService.InactivatingUser(id);
        }

        public Task<ServiceUsers> GetUserByCodeOfSystem(string code)
        {
            var result = _serviceDomainService.GetUserByCode(code);
            return result;
        }
    }
}
