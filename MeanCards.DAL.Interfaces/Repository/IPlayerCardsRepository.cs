using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Players;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayerCardsRepository
    {
        Task<int> CreatePlayerCard(CreatePlayerCardModel model);
        Task<int> CreatePlayerCards(IEnumerable<CreatePlayerCardModel> models);
        Task<List<PlayerCardModel>> GetUnusedPlayerCards(int playerId);
        Task<int> GetAnswerCardIdForPlayerCard(int playerCardId);
        Task<PlayerCardModel> GetPlayerCard(int playerCardId);
    }
}
