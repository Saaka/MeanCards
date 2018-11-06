using MeanCards.DAL.Storage;

namespace MeanCards.DAL.Repository
{
    public class QuestionCardsRepository
    {
        private readonly AppDbContext context;

        public QuestionCardsRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
