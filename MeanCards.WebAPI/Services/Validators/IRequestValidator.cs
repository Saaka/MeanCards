using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services.Validators
{
    public interface IRequestValidator<T>
    {
        Task<ValidatorResult> Validate(T request);
    }
}
