using MeanCards.DAL.Interfaces.Queries;
using MeanCards.Model.Core.Queries;
using MeanCards.Validators.Games;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MeanCards.Queries.GameQueries
{
    public interface IGetGameQueryHandler : IRequestHandler<GetGame, GetGameResult>
    {
    }

    public class GetGameQueryHandler : IGetGameQueryHandler
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IBaseGameRequestsValidator requestsValidator;

        public GetGameQueryHandler(IQueryExecutor queryExecutor,
            IBaseGameRequestsValidator requestsValidator)
        {
            this.queryExecutor = queryExecutor;
            this.requestsValidator = requestsValidator;
        }

        public async Task<GetGameResult> Handle(GetGame request, CancellationToken cancellationToken)
        {
            var validationResult = await requestsValidator.Validate(request);
            if (!validationResult.IsSuccessful)
                return new GetGameResult(validationResult.Error);

            return await queryExecutor
                .QueryFirstOrDefault<GetGameResult>(QueryString, new { request.GameId });
        }

        private const string QueryString = @"SELECT Name, Code FROM meancards.Games WHERE GameId = @GameId";
    }
}
