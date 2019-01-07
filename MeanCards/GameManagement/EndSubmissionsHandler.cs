using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface IEndSubmissionsHandler
    {
        Task<EndSubmissionsResult> Handle(EndSubmissions request);
    }

    public class EndSubmissionsHandler : IEndSubmissionsHandler
    {
        private readonly IRequestValidator<EndSubmissions> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;

        public EndSubmissionsHandler(
            IRequestValidator<EndSubmissions> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
        }
        public async Task<EndSubmissionsResult> Handle(EndSubmissions request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validationResult = await requestValidator.Validate(request);
                if (!validationResult.IsSuccessful)
                    return new EndSubmissionsResult(validationResult.Error);

                transaction.CommitTransaction();

                throw new System.NotImplementedException();
            }
        }
    }
}
