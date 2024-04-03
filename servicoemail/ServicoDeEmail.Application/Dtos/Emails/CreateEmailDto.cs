using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicoDeEmail.Application.Dtos.Emails
{
    public class CreateEmailDto
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        [NotMapped]
        public string AccessKey { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public IEnumerable<AttachmentsDto>? Attachments { get; set; }
    }
}
