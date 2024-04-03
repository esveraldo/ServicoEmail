using FluentValidation.Results;
using ServicoDeEmail.Domain.Core;
using ServicoDeEmail.Domain.Validations;

namespace ServicoDeEmail.Domain.Entities
{
    public class Email : IEntity<Guid>
    {
        public Email()
        {
            Created = DateTime.Now;
            Status = "Em processamento";
            Obs = "";
            InvalidEmails = "";
            Id = Guid.NewGuid();
        }


        public Guid Id { get; set; }
        public string From { get; set; }
        public string? InvalidEmails { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string AccessKey { get; set; }
        public string? Obs { get; set; }
        public string AttachmentsDocs { get; set; }
        public List<Attachments> Attachments { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ScheduleDate { get; set; }

        public ValidationResult Validate => new EmailValidation().Validate(this);
    }
}
