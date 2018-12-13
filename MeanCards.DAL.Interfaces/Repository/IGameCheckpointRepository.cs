using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGameCheckpointRepository
    {
        Task<int> CreateGameCheckpoint(CreateGameCheckpointModel model);
        Task<string> GetCurrentCheckpoint(int gameId);
        Task<List<GameCheckpointModel>> GetCheckpointsForGame(int gameId);
    }
}
