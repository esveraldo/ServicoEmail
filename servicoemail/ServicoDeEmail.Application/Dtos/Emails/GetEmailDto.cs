namespace ServicoDeEmail.Application.Dtos.Emails
{
    public class GetEmailDto
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Obs { get; set; }
        public DateTime Created { get; set; }
    }
}
