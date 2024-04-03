using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServicoDeEmail.Domain.Interfaces.Services;
using ServicoDeEmail.Infraestructure.Data.Repositories;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Settings;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.ValueObjects;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Mail
{
    public class SendMail
    {
        private readonly EmailMessageSettings _emailMessageSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly ILogger<SendMail> _logger;
        private readonly AuxEmailRepository _auxEmailRepository;
        private readonly ISecurityDomainService _securityDomainService;
        private readonly AuxSendMailRepository _auxSendMailRepository;
        private string user;
        private string password;
        private int counter;
        private DateTime currentDate;
        private Guid identity;
        private string smtp;
        private Guid idService;
        private Guid idConsumer;
        private int port;
        private string access;
        private string name;
        private string msg;

        private dynamic smtpClient;

        public SendMail(IOptions<EmailMessageSettings> emailMessageSettings, IOptions<SecuritySettings> securitySettings, ILogger<SendMail> logger, AuxEmailRepository auxEmailRepository, ISecurityDomainService securityDomainService, AuxSendMailRepository auxSendMailRepository)
        {
            _emailMessageSettings = emailMessageSettings.Value;
            _logger = logger;
            _auxEmailRepository = auxEmailRepository;
            _securitySettings = securitySettings.Value;
            _securityDomainService = securityDomainService;
            _auxSendMailRepository = auxSendMailRepository;
        }

        private void ConfigSendMail(Guid id, string code)
        {
            try
            {
                var configSend = _auxSendMailRepository.GetUserConsumerByCode(code);

                if (configSend == null)
                {
                    _auxEmailRepository.UpdateEmailObs(id, "Ocorreu um erro ao recuperar as informações, verifique a chave de acesso.");
                    _logger.LogInformation("Ocorreu um erro ao recuperar as informações, verifique a sua chave.");
                }

                for (int i = 0; i < configSend.Result.Count; i++)
                {
                    idService = configSend.Result[i].Id;
                    smtp = configSend.Result[i].Smtp;
                    port = configSend.Result[i].Port;
                    access = configSend.Result[i].AccessKey;
                    name = configSend.Result[i].NameUser;
                    for (int c = 0; c < configSend.Result[i].ConsumerUsers.Count; c++)
                    {
                        if (configSend.Result[i].ConsumerUsers[c].Counter <= _emailMessageSettings.QuantidadeDeEmailsPermitidos)
                        {
                            _logger.LogInformation("Usando o cliente para envio de emails...");
                            idConsumer = configSend.Result[i].ConsumerUsers[c].Id;
                            user = configSend.Result[i].ConsumerUsers[c].Consumer;
                            password = _securityDomainService.DecryptString(_securitySettings.SecretKey, configSend.Result[i].ConsumerUsers[c].ConsumerPassword);
                            counter = configSend.Result[i].ConsumerUsers[c].Counter;
                            currentDate = configSend.Result[i].ConsumerUsers[c].CurrentDate;
                            break;
                        }
                    }
                }
                SendConfiguration(smtp, port, user, password);
            }
            catch (NullReferenceException e)
            {

                _logger.LogInformation($"Erro: {e}");
            }
        }

        private void SendConfiguration(string smtp, int port, string user, string password)
        {
            _logger.LogInformation("Capturando informações de configuração de envio...");
            smtpClient = new SmtpClient(smtp, port);
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 180000;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(user.Trim(), password.Trim());
        }

        public void Send(string mailTo, string subject, string body, string attachments, Guid id, string accessKey)
        {
            if (!string.IsNullOrEmpty(accessKey) || !string.IsNullOrWhiteSpace(accessKey))
            {

                if (accessKey == null)
                {
                    _logger.LogInformation("A chave informada não é valida.");
                    msg = "A chave informada não é valida.";
                }

                if (accessKey != null)
                {
                    List<AttachmentsVO> attachList = (List<AttachmentsVO>)JsonConvert.DeserializeObject(attachments, typeof(List<AttachmentsVO>));
                    var attachListSize = attachList.Count();

                    ConfigSendMail(id, accessKey);

                    foreach (var to in mailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!this.IsValidEmail(to))
                        {
                            continue;
                        }

                        try
                        {
                            MailMessage mail = new MailMessage(user, to.Trim());

                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.Priority = MailPriority.High;

                            foreach (var a in attachList)
                            {
                                if (attachListSize > 0)
                                {
                                    string fileName;
                                    Byte[] bytes;

                                    if (a.Code.Contains("data:image/"))
                                    {
                                        fileName = a.FileNameSend;
                                        bytes = Convert.FromBase64String(Cleaning(a.Code));
                                    }
                                    else
                                    {
                                        fileName = $"{a.FileNameSend}.{a.Extension}";
                                        bytes = Convert.FromBase64String(a.Code);
                                    }

                                    try
                                    {
                                        MemoryStream ms = new MemoryStream(bytes);
                                        Attachment data = new Attachment(ms, fileName);
                                        data.ContentId = fileName;
                                        data.ContentDisposition.Inline = true;
                                        mail.Attachments.Add(data);
                                    }
                                    catch (Exception)
                                    {
                                        _auxEmailRepository.UpdateEmailObs(id, "Falha no carregamento de um anexo, formato inválido.");
                                        mail.Body += $"\n\rUm anexo não foi enviado, formato inválido.\n\rFavor informar ao remetente.";
                                    }
                                }
                                else
                                {
                                    _auxEmailRepository.UpdateEmailObs(id, "Falha no carregamento de um anexo, arquivo inexistente.");
                                    mail.Body += $"\n\rHouve uma tentativa de envio de um anexo nessa mensagem que não foi encontrada na sua origem.\n\rFavor informar ao remetente.";
                                }
                            }


                            try
                            {
                                _logger.LogInformation("Enviando email...");
                                smtpClient.Send(mail);
                                mail.Dispose();
                                msg = "Email enviado com sucesso.";
                                _auxEmailRepository.UpdateSendDateEmails(id, DateTime.Now);
                                counter = counter + 1;

                                _auxEmailRepository.UpdateCounterConsumerUser(idConsumer, counter);
                                _logger.LogInformation("Email enviado com sucesso.");
                            }
                            catch (System.Net.Mail.SmtpException)
                            {
                                msg = @"A mensagem foi processada mas as informações 
                                                                        continuam na fila, houve um problema no envio.";
                            }

                        }
                        catch (System.FormatException)
                        {
                            msg = @"As credenciais não foram reconhecidas pelo sistema.";
                        }
                    }
                }
                _auxEmailRepository.UpdateEmailObs(id, msg);
                UpdateCounter(idConsumer, currentDate);
            }
        }

        private void UpdateCounter(Guid id, DateTime currentDate)
        {
            int resetCounter = 1;
            var _today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var _currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            if (_currentDate < _today)
            {
                _auxEmailRepository.UpdateDataCounter(id, resetCounter, _today);
            }
        }


        private bool IsValidEmail(String Email)
        {
            if (Email != null && Email != "")
                return Regex.IsMatch(Email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            else
                return false;
        }


        private string Cleaning(string img)
        {
            var Base64 = img.Split(',')[1];
            StringBuilder sb = new StringBuilder(Base64, Base64.Length);
            sb.Replace("\r\n", string.Empty);
            sb.Replace(" ", string.Empty);
            return sb.ToString();
        }

    }

}


