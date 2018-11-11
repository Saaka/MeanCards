using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;

namespace MeanCards.DAL.Repository
{
    public class AnswerCardsRepository : IAnswerCardsRepository
    {
        private readonly AppDbContext context;

        public AnswerCardsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAnswerCard(CreateAnswerCardModel model)
        {
            var newCard = new AnswerCard
            {
                LanguageId = model.LanguageId,
                Text = model.Text,
                IsAdultContent = model.IsAdultContent,
                CreateDate = DateTime.UtcNow,
                IsActive = true
            };

            context.AnswerCards.Add(newCard);
            await context.SaveChangesAsync();
            return newCard.AnswerCardId;
        }

        public async Task<List<AnswerCard>> GetAllActiveAnswerCards()
        {
            var query = from    ac in context.AnswerCards
                        where   ac.IsActive == true
                        select  ac;

            return await query.ToListAsync();
        }

        public async Task<List<AnswerCard>> GetAnswerCardsWithoutMatureContent()
        {
            var query = from    ac in context.AnswerCards
                        where   ac.IsActive == true 
                                && ac.IsAdultContent == false
                        select  ac;

            return await query.ToListAsync();
        }
    }
}
