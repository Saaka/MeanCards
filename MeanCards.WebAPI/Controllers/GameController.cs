using MeanCards.GameManagement;
using MeanCards.ViewModel.Game;
using MeanCards.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerAuthBase
    {
        private readonly ICreateGameHandler createGameHandler;

        public GameController(
            IUserContextDataProvider userContextDataProvider,
            ICreateGameHandler createGameHandler)
            : base(userContextDataProvider)
        {
            this.createGameHandler = createGameHandler;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateGameResult>> Post([FromBody] CreateGame model)
        {
            var user = await GetUserData();
            var result = await createGameHandler.Handle(new Model.Core.Games.CreateGame
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
    }
}
