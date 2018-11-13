﻿using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Tests.Integration.Config;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class AnswerCardRepositoryShould : IDisposable
    {
        private readonly DALServiceCollectionFixture Fixture;
        const string DefaultLanguageCode = "PL";

        public AnswerCardRepositoryShould()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        [Fact]
        public async Task InsertAnswerCards()
        {
            var languageId = await CreateDefaultLanguage();
            await PopulateAnswerCards(languageId);
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();

            var cards = await cardRepository.GetAllActiveQuestionCards();

            Assert.Equal(2, cards.Count);
        }

        [Fact]
        public async Task ReturnCardsWithoutMatureContent()
        {
            var languageId = await CreateDefaultLanguage();
            await PopulateAnswerCards(languageId);
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();

            var cards = await cardRepository.GetQuestionCardsWithoutMatureContent();

            Assert.Single(cards);

            var card = cards.First();
            Assert.Equal("Test2", card.Text);
            Assert.True(card.IsActive);
        }

        private async Task PopulateAnswerCards(int languageId)
        {
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();
            await cardRepository.CreateQuestionCard(new Model.Creation.CreateQuestionCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test1"
            });
            await cardRepository.CreateQuestionCard(new Model.Creation.CreateQuestionCardModel
            {
                IsAdultContent = false,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test2"
            });
        }

        private async Task<int> CreateDefaultLanguage()
        {
            var languageRepository = Fixture.GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = DefaultLanguageCode, Name = "Polski " });
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
