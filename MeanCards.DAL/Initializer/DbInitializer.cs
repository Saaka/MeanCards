using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeanCards.DAL.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext context;
        private readonly ILanguageInitializer languageInitializer;

        public DbInitializer(AppDbContext context, 
            ILanguageInitializer languageInitializer)
        {
            this.context = context;
            this.languageInitializer = languageInitializer;
        }

        public async Task Execute()
        {
            await context.Database.MigrateAsync();
            await context.SaveChangesAsync();

            await languageInitializer.Seed();
        }
    }
}
