using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MeanCards.DAL.Repository
{
    public class LanguagesRepository
    {
        private readonly AppDbContext context;

        public LanguagesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateLanguage(CreateLanguageModel model)
        {
            var newLanguage = new Language
            {
                Code = model.Code,
                Name = model.Name,
                IsActive = true
            };

            context.Languages.Add(newLanguage);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<Language>> GetAllActiveLanguages()
        {
            var query = from language in context.Languages
                        where language.IsActive == true
                        select language;

            return await query.ToListAsync();
        }
    }
}
