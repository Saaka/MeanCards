﻿using MediatR;

namespace MeanCards.Model.Core.Users
{
    public class AuthenticateGoogleUser : IRequest<CreateUserResult>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string GoogleId { get; set; }
    }
}
