using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServicoDeEmail.Application.Dtos.ServicesUsers
{
    public class ServiceUsersDto
    {
        public ServiceUsersDto()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Guid Id { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        [JsonIgnore]
        public string AccessKey { get; set; } = createSecret(20);
        [JsonIgnore]
        public bool Active { get; set; }
        public string? NameUser { get; set; }
        public IEnumerable<ConsumerUsersDto> ConsumerUsersDto { get; set; }

        public static string createSecret(int length)
        {
            using (var crypto = new RNGCryptoServiceProvider())
            {
                var bits = (length * 6);
                var byte_size = ((bits + 7) / 8);
                var bytesarray = new byte[byte_size];
                crypto.GetBytes(bytesarray);
                return Convert.ToBase64String(bytesarray);
            }
        }
    }
}
