using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.Creation;
using MeanCards.Model.DTO.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.DAL.Repository
{
    public class GameRoundsRepository : IGameRoundsRepository
    {
        private readonly AppDbContext context;

        public GameRoundsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateGameRound(CreateGameRoundModel model)
        {
            var newRound = new GameRound
            {
                GameId = model.GameId,
                QuestionCardId = model.QuestionCardId,
                RoundOwnerId = model.RoundOwnerId,
                CreateDate = DateTime.UtcNow,
                IsActive = true
            };

            context.GameRounds.Add(newRound);
            await context.SaveChangesAsync();

            return newRound.GameRoundId;
        }

        public async Task<GameRoundModel> GetCurrentGameRound(int gameId)
        {
            var query = from round in context.GameRounds
                        where round.GameId == gameId && round.IsActive
                        select new GameRoundModel
                        {
                            GameId = round.GameId,
                            GameRoundId = round.GameRoundId,
                            QuestionCardId = round.QuestionCardId,
                            RoundOwnerId = round.RoundOwnerId,
                            RoundWinnerId = round.RoundWinnerId
                        };

            return await query.FirstOrDefaultAsync();
        }
    }
}
