using MeanCards.Commands.Games;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeanCards.GameManager
{
    public class GameManager
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly ICodeGenerator codeGenerator;

        public GameManager(IGamesRepository gamesRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository,
            ICodeGenerator codeGenerator)
        {
            this.gamesRepository = gamesRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
            this.codeGenerator = codeGenerator;
        }

        public async Task<CreateGameResult> CreateGame(CreateGame command)
        {
            var gameCode = codeGenerator.Generate();

            int gameId = await CreateGameModel(command, gameCode);
            var playerId = await CreatePlayerModel(gameId, command.OwnerId);

            return new CreateGameResult
            {
                GameId = gameId,
                GameCode = gameCode
            };
        }

        private async Task<int> CreateGameModel(CreateGame command, string gameCode)
        {
            var gameId = await gamesRepository.CreateGame(new CreateGameModel
            {
                GameCode = gameCode,
                LanguageId = command.LanguageId,
                Name = command.Name,
                OwnerId = command.OwnerId,
                ShowAdultContent = command.ShowAdultContent
            });
            return gameId;
        }

        private async Task<int> CreatePlayerModel(int gameId, int userId)
        {
            var playerId = await playersRepository.CreatePlayer(new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId
            });

            return playerId;
        }
    }
}
