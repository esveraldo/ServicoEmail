namespace ServicoDeEmail.Application.Dtos.User
{
    public class GetUserDto
    {
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string AccessKey { get; set; }
        public bool Active { get; set; }
    }
}