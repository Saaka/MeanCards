using System.Threading.Tasks;

namespace MeanCards.Validators
{
    public interface IRequestValidator<T>
    {
        Task<ValidatorResult> Validate(T request);
    }
}
