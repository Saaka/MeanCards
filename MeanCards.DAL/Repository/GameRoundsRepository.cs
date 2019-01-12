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
                OwnerPlayerId = model.OwnerPlayerId,
                CreateDate = DateTime.UtcNow,
                Status = (byte)GameRoundStatusEnum.Pending,
                IsActive = true
            };

            context.GameRounds.Add(newRound);
            await context.SaveChangesAsync();

            return MapToModel(newRound);
        }

        private GameRoundModel MapToModel(GameRound round)
        {
            if (round == null)
                return null;

            return new GameRoundModel
            {
                GameId = round.GameId,
                GameRoundId = round.GameRoundId,
                IsActive = round.IsActive,
                Number = round.Number,
                OwnerPlayerId = round.OwnerPlayerId,
                QuestionCardId = round.QuestionCardId,
                WinnerPlayerId = round.WinnerPlayerId,
                Status = (GameRoundStatusEnum)round.Status
            };
        }

        public async Task<GameRoundModel> GetCurrentGameRound(int gameId)
        {
            var query = from r in context.GameRounds
                        where r.GameId == gameId && r.IsActive
                        select r;

            var round = await query.FirstOrDefaultAsync();

            return MapToModel(round);
        }

        public async Task<GameRoundModel> GetGameRound(int gameId, int gameRoundId)
        {
            var query = from r in context.GameRounds
                        where r.GameId == gameId
                                && r.GameRoundId == gameRoundId
                        select r;

            var round = await query.FirstOrDefaultAsync();

            return MapToModel(round);
        }

        public async Task<bool> UpdateGameRoundStatus(int gameRoundId, GameRoundStatusEnum status)
        {
            var query = from r in context.GameRounds
                        where r.GameRoundId == gameRoundId
                        select r;

            var round = await query.FirstOrDefaultAsync();
            if (round == null)
                return false;

            round.Status = (byte)status;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SkipRound(int gameRoundId)
        {
            var query = from r in context.GameRounds
                        where r.GameRoundId == gameRoundId
                        select r;

            var round = await query.FirstOrDefaultAsync();
            if (round == null)
                return false;

            round.Status = (byte)GameRoundStatusEnum.Skipped;
            round.IsActive = false;

            return await context.SaveChangesAsync() > 0;
        }
    }
}
