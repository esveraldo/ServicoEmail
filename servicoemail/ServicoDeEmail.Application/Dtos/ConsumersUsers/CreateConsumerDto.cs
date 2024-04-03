using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Application.Dtos.ConsumersUsers
{
    public class CreateConsumerDto
    {
        public IEnumerable<ConsumerUsers> ConsumerUsers { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
    }
}
