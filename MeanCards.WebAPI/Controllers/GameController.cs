using MeanCards.GameManagement;
using MeanCards.Model.DTO.Games;
using MeanCards.Queries.GameQueries;
using MeanCards.ViewModel.Game;
using MeanCards.WebAPI.Controllers.Base;
using MediatR;
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
        private readonly IMediator mediator;

        public GameController(
            IUserContextDataProvider userContextDataProvider,
            IGameDataProvider gameDataProvider,
            IMediator mediator)
            : base(userContextDataProvider)
        {
            this.gameDataProvider = gameDataProvider;
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateGameResponse>> Create([FromBody] CreateGame model)
        {
            var user = await GetUserData();
            var result = await mediator
                .Send(new Model.Core.Games.CreateGame
                {
                    LanguageId = model.LanguageId,
                    Name = model.Name,
                    PointsLimit = model.PointsLimit,
                    ShowAdultContent = model.AdultContent,
                    UserId = user.UserId
                });

            return CreateResult(result, () => new CreateGameResponse
            {
                GameCode = result.Code
            });
        }

        [HttpGet("join/{gameCode}")]
        public async Task<ActionResult<JoinGameResponse>> JoinGame(string gameCode)
        {
            var user = await GetUserData();
            var game = await GetGame(gameCode);

            var result = await mediator
                .Send(new Model.Core.Games.JoinGame
                {
                    GameId = game.GameId,
                    UserId = user.UserId
                });

            return CreateResult<JoinGameResponse>(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<GetGameListResponse>> GetGameList()
        {
            var user = await GetUserData();

            var result = await mediator
                .Send(new Model.Core.Queries.GetGameList
                {
                    UserId = user.UserId
                });

            return CreateResult(result, () => new GetGameListResponse
            {
                List = result.List
            });
        }

        [HttpGet("{gameCode}")]
        public async Task<ActionResult<GetGameListResponse>> Get(string gameCode)
        {
            var user = await GetUserData();
            var game = await GetGame(gameCode);

            var result = await mediator
                .Send(new Model.Core.Queries.GetGame
                {
                    GameId = game.GameId,
                    UserId = user.UserId
                });

            return GetResult(result);
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
