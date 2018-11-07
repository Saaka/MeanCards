using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.ViewModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;

namespace MeanCards.DAL.Repository
{
    public class QuestionCardsRepository : IQuestionCardsRepository
    {
        private readonly AppDbContext context;

        public QuestionCardsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateQuestionCard(CreateQuestionCardModel model)
        {
            var newCard = new QuestionCard
            {
                LanguageId = model.LanguageId,
                Text = model.Text,
                IsAdultContent = model.IsAdultContent,
                NumberOfAnswers = model.NumberOfAnswers,
                CreateDate = DateTime.UtcNow,
                IsActive = true
            };

            context.QuestionCards.Add(newCard);
            await context.SaveChangesAsync();
            return newCard.QuestionCardId;
        }

        public async Task<List<QuestionCard>> GetAllActiveQuestionCards()
        {
            var query = from    qc in context.QuestionCards
                        where   qc.IsActive == true
                        select  qc;

            return await query.ToListAsync();
        }

        public async Task<List<QuestionCard>> GetQuestionCardsWithoutMatureContent()
        {
            var query = from    qc in context.QuestionCards
                        where   qc.IsActive == true 
                                && qc.IsAdultContent == false
                        select  qc;

            return await query.ToListAsync();
        }
    }
}
