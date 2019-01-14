using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.ValidationRules
{
    public interface IGameOwnerRule
    {
        Task<ValidatorResult> Validate<T>(T request)
            where T : IGameRequest, IUserRequest;
    }

    public class GameOwnerRule : IGameOwnerRule
    {
        private readonly IGamesRepository gamesRepository;

        public GameOwnerRule(
            IGamesRepository gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public async Task<ValidatorResult> Validate<T>(T request) 
            where T : IGameRequest, IUserRequest
        {
            var game = await gamesRepository.GetGameById(request.GameId);
            if (game.OwnerId != request.UserId)
                return new ValidatorResult(ValidatorErrors.Games.InvalidUserAction);

            return new ValidatorResult();
        }
    }
}
