using ServicoDeEmail.Application.Dtos.ServicesUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServicoDeEmail.Application.Dtos.ConsumersUsers
{
    public class ConsumerUsersDto
    {
        public ConsumerUsersDto()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Consumer { get; set; }
        public string? ConsumerPassword { get; set; }
        [JsonIgnore]
        public int Counter { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public Guid ServiceUsersId { get; set; }
    }
}
