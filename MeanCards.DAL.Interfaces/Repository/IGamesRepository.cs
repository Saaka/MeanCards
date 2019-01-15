﻿using MeanCards.Common.Enums;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGamesRepository
    {
        Task<GameModel> CreateGame(CreateGameModel model);
        Task<GameModel> GetGameById(int gameId);
        Task<GameStatusEnum> GetGameStatus(int gameId);
        Task<GameModel> GetGameByCode(string code);
        Task<bool> CancelGame(int gameId);
    }
}
