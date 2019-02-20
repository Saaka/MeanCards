namespace MeanCards.Common.Constants
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
            public const string GameRoundIdRequired = nameof(GameRoundIdRequired);
            public const string PlayerIdRequired = nameof(PlayerIdRequired);
            public const string InvalidGameRoundStatus = nameof(InvalidGameRoundStatus);
            public const string RoundNotLinkedWithGame = nameof(RoundNotLinkedWithGame);
            public const string PlayerCardIdRequired = nameof(PlayerCardIdRequired);
            public const string SecondPlayerCardIdRequired = nameof(SecondPlayerCardIdRequired);
            public const string CardNotLinkedWithPlayer = nameof(CardNotLinkedWithPlayer);
            public const string PlayerNotFound = nameof(PlayerNotFound);
            public const string PlayerNotActive = nameof(PlayerNotActive);
            public const string NotEnoughPlayers = nameof(NotEnoughPlayers);
            public const string InvalidUserAction = nameof(InvalidUserAction);
            public const string NotEnoughSubmittedAnswers = nameof(NotEnoughSubmittedAnswers);
            public const string PlayerAlreadySubmittedAnswer = nameof(PlayerAlreadySubmittedAnswer);
            public const string PlayerAnswerDoesNotExists = nameof(PlayerAnswerDoesNotExists);
            public const string SelectedAnswerPlayerIsNotActive = nameof(SelectedAnswerPlayerIsNotActive);
            public const string GameNameInUse = nameof(GameNameInUse);
        }
        public static class Players
        {
            public const string UserNotLinkedWithPlayer = nameof(UserNotLinkedWithPlayer);
        }
    }
}

