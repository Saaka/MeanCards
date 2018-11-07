using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Initializer
{
    public class LanguageInitializer
    {
        private readonly AppDbContext context;

        public LanguageInitializer(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!await context.Languages.AnyAsync())
            {
                context.Languages.AddRange(Languages);
                await context.SaveChangesAsync();
            }
        }

        private List<Language> Languages => new List<Language>
        {
            new Language { Code = "PL", Name = "Polski", IsActive = true },
            new Language { Code = "EN", Name = "English", IsActive = true },
        };
    }
}
