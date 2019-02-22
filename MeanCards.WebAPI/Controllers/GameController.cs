using MeanCards.GameManagement;
using MeanCards.Model.DTO.Games;
using MeanCards.Queries.GameQueries;
using MeanCards.ViewModel.Game;
using MeanCards.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerAuthBase
    {
        private readonly IGameDataProvider gameDataProvider;
        private readonly ICreateGameHandler createGameHandler;
        private readonly IJoinGameHandler joinGameHandler;
        private readonly IGetGameListQueryHandler gameListQueryHandler;

        public GameController(
            IUserContextDataProvider userContextDataProvider,
            IGameDataProvider gameDataProvider,
            ICreateGameHandler createGameHandler,
            IJoinGameHandler joinGameHandler,
            IGetGameListQueryHandler gameListQueryHandler)
            : base(userContextDataProvider)
        {
            this.gameDataProvider = gameDataProvider;
            this.createGameHandler = createGameHandler;
            this.joinGameHandler = joinGameHandler;
            this.gameListQueryHandler = gameListQueryHandler;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateGameResult>> Post([FromBody] CreateGame model)
        {
            var user = await GetUserData();
            var result = await createGameHandler
                .Handle(new Model.Core.Games.CreateGame
                {
                    LanguageId = model.LanguageId,
                    Name = model.Name,
                    PointsLimit = model.PointsLimit,
                    ShowAdultContent = model.AdultContent,
                    UserId = user.UserId
                });

            return CreateResult(result, () => new CreateGameResult
            {
                GameCode = result.Code
            });
        }

        [HttpGet("join/{gameCode}")]
        public async Task<ActionResult<JoinGameResult>> JoinGame(string gameCode)
        {
            var user = await GetUserData();
            var game = await GetGame(gameCode);

            var result = await joinGameHandler
                .Handle(new Model.Core.Games.JoinGame
                {
                    GameId = game.GameId,
                    UserId = user.UserId
                });

            return CreateResult<JoinGameResult>(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<GetGameListResult>> GetGameList()
        {
            var user = await GetUserData();
            var result = await gameListQueryHandler
                .Handle(new GetGameList
                {
                    UserId = user.UserId
                });

            return Ok(result);
        }

        private async Task<GameSimpleModel> GetGame(string code)
        {
            return await gameDataProvider.GetGame(code);
        }

        private async Task<GameRoundSimpleModel> GetRound(string code)
        {
            return await gameDataProvider.GetGameRound(code);
        }
    }
}
