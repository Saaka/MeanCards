﻿using MeanCards.Model.Core.Games.Base;
using MediatR;

namespace MeanCards.Model.Core.Games
{
    public class EndSubmissions : IRequest<EndSubmissionsResult>, IGameRequest, IGameRoundRequest, IUserRequest, IPlayerRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
    }
}
