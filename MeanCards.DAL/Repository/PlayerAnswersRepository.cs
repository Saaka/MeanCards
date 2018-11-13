using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MeanCards.DAL.Interfaces.Repository;
using System.Collections.Generic;

namespace MeanCards.DAL.Repository
{
    public class PlayerAnswersRepository : IPlayerAnswersRepository
    {
        private readonly AppDbContext context;

        public PlayerAnswersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreatePlayerAnswer(CreatePlayerAnswerModel model)
        {
            var newAnswer = new PlayerAnswer
            {
                AnswerCardId = model.AnswerCardId,
                SecondaryAnswerCardId = model.SecondaryAnswerCardId,
                GameRoundId = model.GameRoundId,
                PlayerId = model.PlayerId,
                IsSelectedAnswer = false
            };

            context.PlayerAnswers.Add(newAnswer);
            await context.SaveChangesAsync();

            return newAnswer.PlayerAnswerId;
        }

        public async Task<List<PlayerAnswer>> GetAllPlayerAnswers(int gameRoundId)
        {
            var query = from answer in context.PlayerAnswers
                        where answer.GameRoundId == gameRoundId
                        select answer;

            return await query.ToListAsync();
        }


        public async Task MarkAnswerAsSelected(int playerAnswerId)
        {
            var answer = await context.PlayerAnswers
                .FirstOrDefaultAsync(x => x.PlayerAnswerId == playerAnswerId);
            if(answer != null)
            {
                answer.IsSelectedAnswer = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
