using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayerCardsRepository
    {
        Task<int> CreatePlayerCard(CreatePlayerCardModel model);
        Task CreatePlayerCards(IEnumerable<CreatePlayerCardModel> models);
        Task<List<PlayerCard>> GetUnusedPlayerCards(int playerId);
    }
}
