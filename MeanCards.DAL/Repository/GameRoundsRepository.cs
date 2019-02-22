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
                Code = model.RoundCode,
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
                Code = round.Code,
                IsActive = round.IsActive,
                Number = round.Number,
                OwnerPlayerId = round.OwnerPlayerId,
                QuestionCardId = round.QuestionCardId,
                WinnerPlayerId = round.WinnerPlayerId,
                Status = (GameRoundStatusEnum)round.Status
            };
        }

        private GameRoundSimpleModel MapToSimpleModel(GameRound round)
        {
            if (round == null)
                return null;

            return new GameRoundSimpleModel
            {
                GameRoundId = round.GameRoundId,
                GameRoundCode = round.Code
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

        public async Task<GameRoundSimpleModel> GetGameRoundByCode(string code)
        {
            var query = from r in context.GameRounds
                        where r.Code == code
                        select r;

            var round = await query.FirstOrDefaultAsync();

            return MapToSimpleModel(round);
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
            return await UpdateGameRoundStatusAndDeactivate(gameRoundId, GameRoundStatusEnum.Skipped);
        }

        private async Task<bool> UpdateGameRoundStatusAndDeactivate(int gameRoundId, GameRoundStatusEnum status)
        {
            var query = from r in context.GameRounds
                        where r.GameRoundId == gameRoundId
                        select r;

            var round = await query.FirstOrDefaultAsync();
            if (round == null)
                return false;

            round.Status = (byte)status;
            round.IsActive = false;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SelectRoundWinner(int gameRoundId, int playerId)
        {
            var query = from r in context.GameRounds
                        where r.GameRoundId == gameRoundId
                        select r;

            var round = await query.FirstOrDefaultAsync();
            if (round == null)
                return false;

            round.WinnerPlayerId = playerId;
            round.Status = (byte)GameRoundStatusEnum.Finished;
            round.IsActive = false;

            return await context.SaveChangesAsync() > 0;
        }
    }
}
