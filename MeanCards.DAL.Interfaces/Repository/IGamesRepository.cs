using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGamesRepository
    {
        Task<int> CreateGame(CreateGameModel model);
        Task<Game> GetGameById(int gameId);
    }
}
