using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.AnswerCards;
using MeanCards.Model.DAL.Creation.AnswerCards;

namespace MeanCards.DAL.Repository
{
    public class AnswerCardsRepository : IAnswerCardsRepository
    {
        private readonly AppDbContext context;

        public AnswerCardsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAnswerCards(List<CreateAnswerCardModel> models)
        {
            foreach(var model in models)
            {
                var entity = MapToEntity(model);
                context.AnswerCards.Add(entity);
            }
            await context.SaveChangesAsync();
        }

        public async Task<int> CreateAnswerCard(CreateAnswerCardModel model)
        {
            AnswerCard newCard = MapToEntity(model);

            context.AnswerCards.Add(newCard);
            await context.SaveChangesAsync();
            return newCard.AnswerCardId;
        }

        private AnswerCard MapToEntity(CreateAnswerCardModel model)
        {
            return new AnswerCard
            {
                LanguageId = model.LanguageId,
                Text = model.Text,
                IsAdultContent = model.IsAdultContent,
                CreateDate = DateTime.UtcNow,
                IsActive = true
            };
        }

        public async Task<List<AnswerCardModel>> GetAllActiveAnswerCards()
        {
            var query = from ac in context.AnswerCards
                        where ac.IsActive == true
                        select new AnswerCardModel
                        {
                            AnswerCardId = ac.AnswerCardId,
                            IsAdultContent = ac.IsAdultContent,
                            LanguageId = ac.LanguageId,
                            Text  = ac.Text
                        };

            return await query.ToListAsync();
        }

        public async Task<List<AnswerCardModel>> GetAnswerCardsWithoutMatureContent()
        {
            var query = from ac in context.AnswerCards
                        where ac.IsActive == true
                                && ac.IsAdultContent == false
                        select new AnswerCardModel
                        {
                            AnswerCardId = ac.AnswerCardId,
                            IsAdultContent = ac.IsAdultContent,
                            LanguageId = ac.LanguageId,
                            Text = ac.Text
                        };

            return await query.ToListAsync();
        }

        public async Task<List<AnswerCardModel>> GetRandomAnswerCardsForGame(int gameId, int cardCount)
        {
            var query = from ac in context.AnswerCards
                        join game in context.Games on ac.LanguageId equals game.LanguageId
                        where ac.IsActive && game.GameId == gameId
                            && (!ac.IsAdultContent || ac.IsAdultContent == game.ShowAdultContent)
                            && !(from pc in context.PlayersCards
                                 join player in context.Players on pc.PlayerId equals player.PlayerId
                                 where player.GameId == gameId
                                 select pc.AnswerCardId).Contains(ac.AnswerCardId)
                        select new AnswerCardModel
                        {
                            AnswerCardId = ac.AnswerCardId,
                            IsAdultContent = ac.IsAdultContent,
                            LanguageId = ac.LanguageId,
                            Text = ac.Text
                        };

            return await query
                .OrderBy(o => Guid.NewGuid())
                .Take(cardCount)
                .ToListAsync();
        }
    }
}
