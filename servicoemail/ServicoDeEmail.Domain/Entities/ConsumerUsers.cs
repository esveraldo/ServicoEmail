using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Entities
{
    public class ConsumerUsers
    {
        public ConsumerUsers()
        {
            Id = Guid.NewGuid();    
        }


        public Guid Id { get; set; }
        public string? Consumer { get; set; }
        public string? ConsumerPassword { get; set; }
        [JsonIgnore]
        public int Counter { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public DateTime CurrentDate { get; set; }
        public Guid ServiceUsersId { get; set; }
        public ServiceUsers ServiceUsers { get; set; }
    }
}
