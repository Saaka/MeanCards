using MeanCards.Common.Constants;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Games;
using System;
using System.Threading.Tasks;

namespace MeanCards.GameManagement.CoreServices
{
    public interface IGameCheckpointUpdater
    {
        Task<string> Update(int gameId, string operationType);
    }

    public class GameCheckpointUpdater : IGameCheckpointUpdater
    {
        private readonly ICodeGenerator codeGenerator;
        private readonly IGameCheckpointRepository checkpointRepository;

        public GameCheckpointUpdater(
            ICodeGenerator codeGenerator,
            IGameCheckpointRepository checkpointRepository)
        {
            this.codeGenerator = codeGenerator;
            this.checkpointRepository = checkpointRepository;
        }

        public async Task<string> Update(int gameId, string operationType)
        {
            var checkpointCode = codeGenerator.Generate();

            var result = await checkpointRepository.CreateGameCheckpoint(new CreateGameCheckpointModel
            {
                Code = checkpointCode,
                GameId = gameId,
                OperationType = operationType
            });
            if (result == 0)
                throw new InvalidOperationException(GameErrors.CheckpointUpdateFailed);

            return checkpointCode;
        }
    }
}
