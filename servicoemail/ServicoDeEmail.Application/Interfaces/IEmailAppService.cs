using ServicoDeEmail.Application.Dtos.Emails;

namespace ServicoDeEmail.Application.Interfaces
{
    public interface IEmailAppService
    {
        Task<string> Add(CreateEmailDto emailDto);
        Task<IEnumerable<GetEmailDto>> GetAll();
    }
}
