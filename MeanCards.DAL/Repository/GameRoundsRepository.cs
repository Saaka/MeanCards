using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.DTO.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Common.Enums;

namespace MeanCards.DAL.Repository
{
    public class GameRoundsRepository : IGameRoundsRepository
    {
        private readonly AppDbContext context;

        public GameRoundsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<GameRoundModel> CreateGameRound(CreateGameRoundModel model)
        {
            var newRound = new GameRound
            {
                GameId = model.GameId,
                Number = model.RoundNumber,
                QuestionCardId = model.QuestionCardId,
                OwnerId = model.RoundOwnerId,
                CreateDate = DateTime.UtcNow,
                Status = (byte)GameRoundStatusEnum.Created,
                IsActive = true
            };

            context.GameRounds.Add(newRound);
            await context.SaveChangesAsync();

            return MapToModel(newRound);
        }

        private GameRoundModel MapToModel(GameRound round)
        {
            return new GameRoundModel
            {
                GameId = round.GameId,
                GameRoundId = round.GameRoundId,
                Number = round.Number,
                OwnerId = round.OwnerId,
                QuestionCardId = round.QuestionCardId,
                WinnerId = round.WinnerId,
                Status = (GameRoundStatusEnum)round.Status
            };
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
                            WinnerId = round.WinnerId,
                            Status = (GameRoundStatusEnum)round.Status
                        };

            return await query.FirstOrDefaultAsync();
        }
    }
}
