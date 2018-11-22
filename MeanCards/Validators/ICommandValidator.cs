using System.Threading.Tasks;

namespace MeanCards.Validators
{
    public interface ICommandValidator<T>
    {
        Task<ValidatorResult> Validate(T request);
    }
}
