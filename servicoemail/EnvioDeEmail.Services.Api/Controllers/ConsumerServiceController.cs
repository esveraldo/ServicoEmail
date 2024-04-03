using Microsoft.AspNetCore.Mvc;
using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Application.Dtos.ServicesUsers;
using ServicoDeEmail.Application.Interfaces;

namespace EnvioDeEmail.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ConsumerServiceController : ControllerBase
    {
        private readonly IServiceUsersAppService _userAppService;

        public ConsumerServiceController(IServiceUsersAppService userAppService)
        {
            _userAppService = userAppService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var emails = await _userAppService.GetAll();
        //    return StatusCode(200, emails);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(ServiceUsersDto serviceUsersDto)
        {
            var result = _userAppService.Add(serviceUsersDto);
            return StatusCode(201, new
            {
                Message = result,
                serviceUsersDto
            });
        }

        //[HttpDelete("/inactive/{id}")]
        //public async Task<IActionResult> Inactive(Guid id)
        //{
        //    await _userAppService.InactivatingUserOfSystem(id);

        //    return StatusCode(202, new
        //    {
        //        Message = $"Usuário identificador {id} foi inativado."
        //    });
        //}

        //[HttpDelete("/active/{id}")]
        //public async Task<IActionResult> Active(Guid id)
        //{
        //    await _userAppService.ActivatingUserOfSystem(id);

        //    return StatusCode(202, new
        //    {
        //        Message = $"Usuário identificador {id} foi ativado."
        //    });
        //}
    }
}
