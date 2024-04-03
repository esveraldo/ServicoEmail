using Quartz;
using ServicoDeEmail.Infraestructure.Data.Contexts;
using ServicoDeEmail.Infraestructure.Data.Repositories;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Quartz
{
    public class QuartzJob : IJob
    {
        private readonly SendMail _sendMail;
        private readonly DataContext _dataContext;
        private readonly AuxEmailRepository _auxEmailRepository;

        public QuartzJob(SendMail sendMail, DataContext dataContext, AuxEmailRepository auxEmailRepository)
        {
            _sendMail = sendMail;
            _dataContext = dataContext;
            _auxEmailRepository = auxEmailRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {

            try
            {

                SendEmail();

            }
            catch (Exception)
            {

                throw;
            }

            return Task.FromResult(true);
        }

        private async Task SendEmail()
        {
            var emails = _dataContext.Emails.Where(x => x.SendDate == null && (x.ScheduleDate == null || x.ScheduleDate <= DateTime.Now));
            
            foreach (var email in emails)
            {
                _sendMail.Send(email.From, email.Subject, email.Message, email.AttachmentsDocs, email.Id, email.AccessKey);
                Console.WriteLine("Mensagem processada!!!");
                _auxEmailRepository.Update(email.Id, "Mensagem processada.");

            }
        }
    }
}
