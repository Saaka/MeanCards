using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Languages;

namespace MeanCards.DAL.Repository
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly AppDbContext context;

        public LanguagesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateLanguage(CreateLanguageModel model)
        {
            var newLanguage = new Language
            {
                Code = model.Code,
                Name = model.Name,
                IsActive = true
            };

            context.Languages.Add(newLanguage);
            await context.SaveChangesAsync();

            return newLanguage.LanguageId;
        }


        public async Task CreateLanguages(IEnumerable<CreateLanguageModel> models)
        {
            var newLanguages = models.Select(x => new Language
            {
                Code = x.Code,
                Name = x.Name,
                IsActive = true
            });

            context.Languages.AddRange(newLanguages);
            await context.SaveChangesAsync();
        }

        public async Task<List<LanguageModel>> GetAllActiveLanguages()
        {
            var query = from language in context.Languages
                        where language.IsActive == true
                        select new LanguageModel
                        {
                            LanguageId = language.LanguageId,
                            Code = language.Code,
                            Name = language.Name
                        };

            return await query.ToListAsync();
        }

        public async Task<bool> HasLanguages()
        {
            var query = from language in context.Languages
                        where language.IsActive == true
                        select language.LanguageId;

            return await query.AnyAsync();
        }
    }
}
