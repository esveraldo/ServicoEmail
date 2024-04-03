using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Interfaces.Services
{
    public interface ISecurityDomainService
    {
        string EncryptString(string key, string plainText);
        string DecryptString(string key, string cipherText);
    }
}
