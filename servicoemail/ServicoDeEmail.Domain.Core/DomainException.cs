namespace ServicoDeEmail.Domain.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        public static void When(bool hasError, string errorMessage) {
            if (hasError)
                throw new DomainException(errorMessage);        
        }
        
    }
}
