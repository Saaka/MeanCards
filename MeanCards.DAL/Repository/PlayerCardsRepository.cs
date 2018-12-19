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
                        select new PlayerCardModel
                        {
                            PlayerCardId = pc.PlayerCardId,
                            PlayerId = pc.PlayerId,
                            AnswerCardId = pc.AnswerCardId,
                            IsUsed = pc.IsUsed
                        };

            return await query.ToListAsync();
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

        public async Task<bool> IsCardLinkedWithUser(int userId, int playerCardId)
        {
            var query = from pc in context.PlayersCards
                        join p in context.Players on pc.PlayerId equals p.PlayerId
                        where p.UserId == userId
                            && pc.PlayerCardId == playerCardId
                        select pc.PlayerCardId;

            return await query.AnyAsync();
        }
    }
}
