using MeanCards.DAL.Interfaces.Queries;
using MeanCards.ViewModel.Game;
using MeanCards.ViewModel.Game.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.Queries.GameQueries
{
    public interface IGetGameListQueryHandler
    {
        Task<GetGameListResult> Handle(GetGameList request);
    }

    public class GetGameListQueryHandler : IGetGameListQueryHandler
    {
        private readonly IQueryExecutor queryExecutor;

        public GetGameListQueryHandler(IQueryExecutor queryExecutor)
        {
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetGameListResult> Handle(GetGameList request)
        {
            var gameList = await queryExecutor.Query<GameListItem>(queryString, new { request.UserId });

            return new GetGameListResult
            {
                List = gameList.ToList()
            };
        }

        protected const string queryString =
            @"SELECT	G.[Code] as [GameCode],
		                G.[Name] as [GameName],
		                U.Code as [OwnerCode],
		                U.DisplayName as [Owner],
		                U.Email as [OwnerEmail],
		                G.ShowAdultContent as [AdultContent],
		                L.Code as [LanguageCode],
		                G.CreateDate as [CreateDate],
	                    CASE WHEN P.PlayerId IS NULL THEN 0 ELSE 1 END as [AlreadyJoined]
                FROM	meancards.Games G
		                JOIN meancards.AspNetUsers U ON G.OwnerId = U.Id
		                JOIN meancards.Languages L ON G.LanguageId = L.LanguageId
	                    LEFT JOIN meancards.Players P ON P.UserId = @UserId AND P.IsActive = 1
                WHERE	G.IsActive = 1";
    }
}
