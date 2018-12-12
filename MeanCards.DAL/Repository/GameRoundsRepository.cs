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
            return new GameRoundModel
            {
                GameId = round.GameId,
                GameRoundId = round.GameRoundId,
                Number = round.Number,
                OwnerPlayerId = round.OwnerPlayerId,
                QuestionCardId = round.QuestionCardId,
                WinnerPlayerId = round.WinnerPlayerId,
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
                            OwnerPlayerId = round.OwnerPlayerId,
                            WinnerPlayerId = round.WinnerPlayerId,
                            Status = (GameRoundStatusEnum)round.Status
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> IsGameRoundPending(int gameRoundId)
        {
            var status = (byte)GameRoundStatusEnum.Pending;

            var query = from round in context.GameRounds
                        where round.IsActive
                            && round.Status == status
                            && round.GameRoundId == gameRoundId
                        select round.GameRoundId;

            return await query.AnyAsync();
        }

        public async Task<bool> IsGameRoundOwner(int gameRoundId, int playerId)
        {
            var query = from round in context.GameRounds
                        where round.GameRoundId == gameRoundId
                            && round.OwnerPlayerId == playerId
                        select round.GameRoundId;

            return await query.AnyAsync();
        }

        public async Task<bool> StartGameRound(int gameRoundId)
        {
            var query = from r in context.GameRounds
                        where r.GameRoundId == gameRoundId
                        select r;

            var round = await query.FirstOrDefaultAsync();
            if (round == null)
                return false;

            round.Status = (byte)GameRoundStatusEnum.InProgress;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsRoundInGame(int gameId, int gameRoundId)
        {
            var query = from r in context.GameRounds
                        where r.GameRoundId == gameRoundId
                            && r.GameId == gameId
                        select r;

            return await query.AnyAsync();
        }
    }
}
