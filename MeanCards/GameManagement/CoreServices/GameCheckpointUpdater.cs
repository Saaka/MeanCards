using MeanCards.Common.Constants;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace MeanCards.GameManagement.CoreServices
{
    public interface IGameCheckpointUpdater
    {
        Task<string> Update(int gameId);
    }

    public class GameCheckpointUpdater : IGameCheckpointUpdater
    {
        private readonly ICodeGenerator codeGenerator;
        private readonly IGamesRepository gamesRepository;

        public GameCheckpointUpdater(
            ICodeGenerator codeGenerator,
            IGamesRepository gamesRepository)
        {
            this.codeGenerator = codeGenerator;
            this.gamesRepository = gamesRepository;
        }

        public async Task<string> Update(int gameId)
        {
            var checkpointCode = codeGenerator.Generate();
            var result = await gamesRepository.UpdateCheckpoint(gameId, checkpointCode);
            if (!result)
                throw new InvalidOperationException(GameErrors.CheckpointUpdateFailed);

            return checkpointCode;
        }
    }
}
