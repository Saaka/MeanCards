﻿using MeanCards.DAL.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class LanguagesRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateLanguage()
        {
            var repository = Fixture.GetService<ILanguagesRepository>();

            await repository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = "PL", Name = "Polski" });
            var languages = await repository.GetAllActiveLanguages();

            Assert.Single(languages);

            var language = languages.First();
            Assert.Equal("PL", language.Code);
            Assert.Equal("Polski", language.Name);
            Assert.NotEqual(0, language.LanguageId);
        }
    }
}
