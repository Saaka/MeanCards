﻿using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
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

        public async Task<GameRound> GetCurrentGameRound(int gameId)
        {
            var query = from round in context.GameRounds
                        where round.GameId == gameId && round.IsActive
                        select round;

            return await query.FirstOrDefaultAsync();
        }
    }
}