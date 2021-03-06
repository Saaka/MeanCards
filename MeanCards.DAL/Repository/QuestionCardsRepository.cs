﻿using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.QuestionCards;
using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Common.Helpers;

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
            QuestionCard newCard = MapToEntity(model);

            context.QuestionCards.Add(newCard);
            await context.SaveChangesAsync();
            return newCard.QuestionCardId;
        }

        public async Task CreateQuestionCards(List<CreateQuestionCardModel> models)
        {
            foreach (var model in models)
            {
                var entity = MapToEntity(model);
                context.QuestionCards.Add(entity);
            }
            await context.SaveChangesAsync();
        }

        private QuestionCard MapToEntity(CreateQuestionCardModel model)
        {
            return new QuestionCard
            {
                LanguageId = model.LanguageId,
                Text = model.Text,
                IsAdultContent = model.IsAdultContent,
                NumberOfAnswers = model.NumberOfAnswers,
                CreateDate = DateTime.UtcNow,
                IsActive = true
            };
        }

        public async Task<List<QuestionCardModel>> GetAllActiveQuestionCards()
        {
            var query = from qc in context.QuestionCards
                        where qc.IsActive == true
                        select qc;

            var cards = await query.ToListAsync();

            return cards.Select(MapToModel).ToList();
        }

        public async Task<List<QuestionCardModel>> GetQuestionCardsWithoutMatureContent()
        {
            var query = from qc in context.QuestionCards
                        where qc.IsActive == true
                                && qc.IsAdultContent == false
                        select qc;

            var cards = await query.ToListAsync();

            return cards.Select(MapToModel).ToList();
        }

        private QuestionCardModel MapToModel(QuestionCard card)
        {
            if (card == null)
                return null;
            return new QuestionCardModel
            {
                QuestionCardId = card.QuestionCardId,
                LanguageId = card.LanguageId,
                Text = card.Text,
                NumberOfAnswers = card.NumberOfAnswers,
                IsAdultContent = card.IsAdultContent
            };
        }

        public async Task<QuestionCardModel> GetRandomQuestionCardForGame(int gameId)
        {
            var query = from qc in context.QuestionCards
                        join game in context.Games on qc.LanguageId equals game.LanguageId
                        where qc.IsActive && game.GameId == gameId
                            && (!qc.IsAdultContent || qc.IsAdultContent == game.ShowAdultContent)
                            && !(from gr in context.GameRounds
                                 where gr.GameId == game.GameId
                                 select gr.QuestionCardId).Contains(qc.QuestionCardId)
                        select new QuestionCardModel
                        {
                            IsAdultContent = qc.IsAdultContent,
                            LanguageId = qc.LanguageId,
                            NumberOfAnswers = qc.NumberOfAnswers,
                            QuestionCardId = qc.QuestionCardId,
                            Text = qc.Text
                        };

            var count = await query.CountAsync();
            int index = RandomFactory.Create().Next(count);

            return await query.Skip(index).FirstOrDefaultAsync();
        }
        
        public async Task<QuestionCardModel> GetActiveQuestionCardForRound(int gameRoundId)
        {
            var query = from gr in context.GameRounds
                        join qc in context.QuestionCards on gr.QuestionCardId equals qc.QuestionCardId
                        where gr.GameRoundId == gameRoundId
                        select qc;

            var card = await query.FirstOrDefaultAsync();

            return MapToModel(card);
        }
    }
}
