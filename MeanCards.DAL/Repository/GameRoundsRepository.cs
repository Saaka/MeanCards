using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.DTO.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Games;

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
                Number = 1,
                QuestionCardId = model.QuestionCardId,
                OwnerId = model.RoundOwnerId,
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
                            Number = round.Number,
                            QuestionCardId = round.QuestionCardId,
                            OwnerId = round.OwnerId,
                            WinnerId = round.WinnerId
                        };

            return await query.FirstOrDefaultAsync();
        }
    }
}
