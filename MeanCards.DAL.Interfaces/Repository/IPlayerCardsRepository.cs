using MeanCards.Model.Creation;
using MeanCards.Model.DTO.Players;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayerCardsRepository
    {
        Task<int> CreatePlayerCard(CreatePlayerCardModel model);
        Task CreatePlayerCards(IEnumerable<CreatePlayerCardModel> models);
        Task<List<PlayerCardModel>> GetUnusedPlayerCards(int playerId);
    }
}
