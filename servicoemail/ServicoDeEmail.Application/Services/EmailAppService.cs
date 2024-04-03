using AutoMapper;
using FluentValidation;
using Newtonsoft.Json;
using ServicoDeEmail.Application.Dtos.Emails;
using ServicoDeEmail.Application.Interfaces;
using ServicoDeEmail.Application.Mappings;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace ServicoDeEmail.Application.Services
{
    public class EmailAppService : IEmailAppService
    {
        private readonly IMapper _mapper;
        private readonly IEmailDomainService _emailDomainService;
        private readonly IServiceUsersAppService _userAppService;
        private readonly IConsumerUsersDomainService _consumerUsersDomainService;

        public EmailAppService(IMapper mapper, IEmailDomainService emailDomainService, IServiceUsersAppService userAppService, IConsumerUsersDomainService consumerUsersDomainService)
        {
            _mapper = mapper;
            _emailDomainService = emailDomainService;
            _userAppService = userAppService;
            _consumerUsersDomainService = consumerUsersDomainService;
        }

        public async Task<string> Add(CreateEmailDto emailDto)
        {
            string msg;

            var mapper = MapperConfig.InitializeAutomapper();
            var email = mapper.Map<Email>(emailDto);

            var attach = JsonConvert.SerializeObject(email.Attachments);

            var validate = email.Validate;
            if (!validate.IsValid)
                throw new ValidationException(validate.Errors);

       
            if (string.IsNullOrEmpty(email.AccessKey) || string.IsNullOrWhiteSpace(email.AccessKey))
            {
                msg = "O código de acesso tem que ser informado, processo finalizado.";
            }

            var validUser = await _userAppService.GetUserByCodeOfSystem(email.AccessKey);

            if (!string.IsNullOrEmpty(VerifyEmails(email.From)))
            {
                msg = $"O(s) email(s) não estão em conformidade [ {this.VerifyEmails(email.From)} ], se houver outros email válidos, vão ser processados.";
            }
            else
            {
                msg = "Processando a mensagem";
            }

            email.InvalidEmails = this.VerifyEmails(email.From).ToString();
            email.AttachmentsDocs = attach;
            await _emailDomainService.NewEmail(email);


            return msg;
        }

        public async Task<IEnumerable<GetEmailDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetEmailDto>>(await _emailDomainService.GetAllEmails());
        }

        private string VerifyEmails(string? email)
        {
            string msg = "";
            foreach (var to in email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!string.IsNullOrEmpty(IsValidEmail(to)))
                {
                    msg += $"{IsValidEmail(to)},";
                }
            }
            return msg.TrimEnd(',');
        }

        private string IsValidEmail(string? email)
        {
            string result = "";
            if (!Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                result = email;

            return result;
        }
    }
}
