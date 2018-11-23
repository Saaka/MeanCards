using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using MeanCards.Model.DTO.Players;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task CreatePlayerCards(IEnumerable<CreatePlayerCardModel> models)
        {
            foreach (var model in models)
            {
                var card = CreateEntity(model);
                context.PlayersCards.Add(card);
            }
            await context.SaveChangesAsync();
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
    }
}
