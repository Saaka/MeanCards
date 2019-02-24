using MeanCards.DAL.Interfaces.Queries;
using MeanCards.Model.Core.Queries;
using MeanCards.Validators.Games;
using MeanCards.ViewModel.Game.Models.GameViewModels;
using MediatR;
using System.Linq;
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

            var game =  await queryExecutor
                .QueryFirstOrDefault<GameData>(GetGameDataQuery, new { request.GameId, request.UserId });

            var players = await queryExecutor
                .Query<PlayerData>(GetGamePlayersQuery, new { request.GameId, request.UserId });

            return new GetGameResult
            {
                Game = game,
                Players = players.ToList()
            };
        }

        private const string GetGameDataQuery = 
            @"SELECT G.Name, G.Code, U.DisplayName as [Owner], P.PlayerId 
                FROM meancards.Games G
                JOIN meancards.AspNetUsers U ON G.OwnerId = U.Id
                JOIN meancards.Players P ON G.GameId = P.GameId AND P.UserId = @UserId AND P.IsActive = 1
                WHERE G.GameId = @GameId";

        private const string GetGamePlayersQuery =
            @"SELECT P.PlayerId,
                    U.DisplayName, 
		            U.ImageUrl as [Avatar], 
		            CASE WHEN GR.OwnerPlayerId = P.PlayerId THEN 1 ELSE 0 END AS [IsRoundOwner],
		            CASE WHEN PA.PlayerAnswerId IS NULL THEN 0 ELSE 1 END AS [SubmittedAnswer],
		            P.Points
            FROM	meancards.Players P 
		            JOIN meancards.AspNetUsers U ON p.UserId = U.Id
		            JOIN meancards.GameRounds GR ON GR.GameId = @GameId AND GR.IsActive = 1
		            LEFT JOIN meancards.PlayerAnswers PA ON GR.GameRoundId = PA.GameRoundId AND PA.PlayerId = P.PlayerId
            WHERE	P.GameId = @GameId AND P.IsActive = 1";
    }
}
