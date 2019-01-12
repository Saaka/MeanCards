using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.DTO.Players;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Players;

namespace MeanCards.DAL.Repository
{
    public class PlayerCardsRepository : IPlayerCardsRepository
    {
        private readonly AppDbContext context;

        public PlayerCardsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PlayerCardModel>> GetUnusedPlayerCards(int playerId)
        {
            var query = from pc in context.PlayersCards
                        where pc.IsUsed == false
                              && pc.PlayerId == playerId
                        select pc;

            var cards = await query.ToListAsync();

            return cards.Select(MapToModel).ToList();
        }

        private PlayerCardModel MapToModel(PlayerCard pc)
        {
            if (pc == null)
                return null;

            return new PlayerCardModel
            {
                PlayerCardId = pc.PlayerCardId,
                PlayerId = pc.PlayerId,
                AnswerCardId = pc.AnswerCardId,
                IsUsed = pc.IsUsed
            };
        }

        public async Task<int> CreatePlayerCards(IEnumerable<CreatePlayerCardModel> models)
        {
            foreach (var model in models)
            {
                var card = CreateEntity(model);
                context.PlayersCards.Add(card);
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> CreatePlayerCard(CreatePlayerCardModel model)
        {
            var card = CreateEntity(model);

            context.PlayersCards.Add(card);
            await context.SaveChangesAsync();

            return card.PlayerCardId;
        }

        private PlayerCard CreateEntity(CreatePlayerCardModel model)
        {
            return new PlayerCard
            {
                PlayerId = model.PlayerId,
                AnswerCardId = model.AnswerCardId,
                IsUsed = false
            };
        }

        public async Task<int> GetAnswerCardIdForPlayerCard(int playerCardId)
        {
            var query = from pc in context.PlayersCards
                        where pc.PlayerCardId == playerCardId
                        select pc.AnswerCardId;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PlayerCardModel> GetPlayerCard(int playerCardId)
        {
            var query = from pc in context.PlayersCards
                        where pc.PlayerCardId == playerCardId
                        select pc;

            var card = await query.FirstOrDefaultAsync();

            return MapToModel(card);
        }

        public async Task<int> GetCardsCountForPlayer(int playerId)
        {
            var query = from pc in context.PlayersCards
                        where pc.PlayerId == playerId
                            && pc.IsUsed == false
                        select pc.PlayerCardId;

            return await query.CountAsync();
        }
    }
}
