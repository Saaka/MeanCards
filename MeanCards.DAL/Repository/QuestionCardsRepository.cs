using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.Creation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.QuestionCards;

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

        public async Task<List<QuestionCardModel>> GetAllActiveQuestionCards()
        {
            var query = from qc in context.QuestionCards
                        where qc.IsActive == true
                        select new QuestionCardModel
                        {
                            QuestionCardId = qc.QuestionCardId,
                            LanguageId = qc.LanguageId,
                            Text = qc.Text,
                            NumberOfAnswers = qc.NumberOfAnswers,
                            IsAdultContent = qc.IsAdultContent
                        };

            return await query.ToListAsync();
        }

        public async Task<List<QuestionCardModel>> GetQuestionCardsWithoutMatureContent()
        {
            var query = from qc in context.QuestionCards
                        where qc.IsActive == true
                                && qc.IsAdultContent == false
                        select new QuestionCardModel
                        {
                            QuestionCardId = qc.QuestionCardId,
                            LanguageId = qc.LanguageId,
                            Text = qc.Text,
                            NumberOfAnswers = qc.NumberOfAnswers,
                            IsAdultContent = qc.IsAdultContent
                        };

            return await query.ToListAsync();
        }
    }
}
