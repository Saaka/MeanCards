using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Initializer
{
    public class LanguageInitializer : ILanguageInitializer
    {
        private readonly ILanguagesRepository languagesRepository;

        public LanguageInitializer(ILanguagesRepository languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }

        public async Task Seed()
        {
            if(!await languagesRepository.HasLanguages())
            {
                await languagesRepository.CreateLanguages(Languages);
            }
        }

        private List<CreateLanguageModel> Languages => new List<CreateLanguageModel>
        {
            new CreateLanguageModel { Code = "PL", Name = "Polski" },
            new CreateLanguageModel { Code = "EN", Name = "English" },
        };
    }
}
