using FluentValidation.Results;
using ServicoDeEmail.Domain.Core;
using ServicoDeEmail.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Entities
{
    public class ServiceUsers : IEntity<Guid>
    {
        public ServiceUsers()
        {
            Id = Guid.NewGuid();
            Active = true;
            ConsumerUsers = new List<ConsumerUsers>();
        }

        public Guid Id { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string AccessKey { get; set; }
        public bool Active { get; set; }
        public string? NameUser { get; set; }
        public virtual IList<ConsumerUsers> ConsumerUsers { get; set; }

        public ValidationResult Validate => new ServiceUsersValidation().Validate(this);

        public string createSecret(int length)
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
