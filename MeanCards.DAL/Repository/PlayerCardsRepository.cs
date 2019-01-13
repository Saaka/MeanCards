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

        public async Task<List<PlayerCardsInfo>> GetPlayersCardsInfo(int gameId)
        {
            var query = from p in context.Players
                        join pc in context.PlayersCards on p.PlayerId equals pc.PlayerId
                        where p.IsActive
                            && p.GameId == gameId
                            && pc.IsUsed == false
                        group pc by pc.PlayerId into gpc
                        select new PlayerCardsInfo
                        {
                             PlayerId  = gpc.Key,
                             PlayerCardsCount = gpc.Count()
                        };

            return await query.ToListAsync();
        }

        public async Task MarkCardAsUsed(int playerCardId)
        {
            var query = from pc in context.PlayersCards
                        where pc.PlayerCardId == playerCardId
                        select pc;

            var card = await query.FirstOrDefaultAsync();
            if (card == null)
                return;

            card.IsUsed = true;
            await context.SaveChangesAsync();
        }
    }
}
