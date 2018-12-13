using MeanCards.DAL.Entity;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.DAL.Repository
{
    public class GameCheckpointRepository : IGameCheckpointRepository
    {
        private readonly AppDbContext context;

        public GameCheckpointRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<string> GetCurrentCheckpoint(int gameId)
        {
            var query = from gc in context.GameCheckpoints
                        where gc.GameId == gameId
                        orderby gc.CreateDate descending
                        select gc.Code;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<GameCheckpointModel>> GetCheckpointsForGame(int gameId)
        {
            var query = from gc in context.GameCheckpoints
                        where gc.GameId == gameId
                        orderby gc.CreateDate descending
                        select gc;

            return (await query.ToListAsync())
                .Select(MapToModel)
                .ToList();
        }

        public GameCheckpointModel MapToModel(GameCheckpoint entity)
        {
            return new GameCheckpointModel
            {
                Code = entity.Code,
                CreateDate = entity.CreateDate,
                GameCheckpointId = entity.GameCheckpointId,
                GameId = entity.GameId,
                OperationType = entity.OperationType
            };
        }

        public async Task<int> CreateGameCheckpoint(CreateGameCheckpointModel model)
        {
            var checkpoint = new GameCheckpoint
            {
                Code = model.Code,
                GameId = model.GameId,
                OperationType = model.OperationType,
                CreateDate = DateTime.UtcNow
            };

            context.GameCheckpoints.Add(checkpoint);
            await context.SaveChangesAsync();

            return checkpoint.GameCheckpointId;
        }
    }
}
