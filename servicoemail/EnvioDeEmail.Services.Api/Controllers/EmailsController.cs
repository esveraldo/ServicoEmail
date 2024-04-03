using Microsoft.AspNetCore.Mvc;
using ServicoDeEmail.Application.Dtos.Emails;
using ServicoDeEmail.Application.Interfaces;

namespace EnvioDeEmail.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailAppService _emailAppService;

        public EmailsController(IEmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var emails = await _emailAppService.GetAll();
        //    return StatusCode(200, emails);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateEmailDto emailDto)
        {
            var result = _emailAppService.Add(emailDto);
            return StatusCode(201, new
            {
                Message = result,
                emailDto
            });
        }
    }
}
