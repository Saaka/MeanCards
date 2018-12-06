﻿namespace MeanCards.Common.Constants
{
    public static class ValidatorErrors
    {
        public static class Users
        {
            public const string DuplicatedUserEmail = nameof(DuplicatedUserEmail);
            public const string DuplicatedUserName = nameof(DuplicatedUserName);
            public const string UserNameRequired = nameof(UserNameRequired);
            public const string UserEmailRequired = nameof(UserEmailRequired);
            public const string UserPasswordRequired = nameof(UserPasswordRequired);
            public const string UserPasswordTooShort = nameof(UserPasswordTooShort);
            public const string GoogleIdRequired = nameof(GoogleIdRequired);
            public const string UserIdNotFound= nameof(UserIdNotFound);
        }
        public static class Games
        {
            public const string GameNameRequired = nameof(GameNameRequired);
            public const string GameLanguageRequired = nameof(GameLanguageRequired);
            public const string GameOwnerRequired = nameof(GameOwnerRequired);
            public const string GameIdRequired = nameof(GameIdRequired);
            public const string UserIdRequired = nameof(UserIdRequired);
            public const string GameNotFoundOrInactive = nameof(GameNotFoundOrInactive);
            public const string UserAlreadyJoined = nameof(UserAlreadyJoined);
        }
    }
}

