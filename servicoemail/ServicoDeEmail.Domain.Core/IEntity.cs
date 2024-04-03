using FluentValidation.Results;

namespace ServicoDeEmail.Domain.Core
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }

        ValidationResult Validate { get; }
    }
}
