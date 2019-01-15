namespace MeanCards.Common.Constants
{
    public static class GameErrors
    {
        public const string NoQuestionCardsAvailable = nameof(NoQuestionCardsAvailable);
        public const string NotEnoughAnswerCards = nameof(NotEnoughAnswerCards);
        public const string CheckpointUpdateFailed = nameof(CheckpointUpdateFailed);
        public const string GameRoundCouldNotBeStarted = nameof(GameRoundCouldNotBeStarted);
        public const string SubmitAnswerFailed = nameof(SubmitAnswerFailed);
        public const string CouldNotEndAnswersSubmissions = nameof(CouldNotEndAnswersSubmissions);
        public const string GameRoundCouldNotBeSkipped = nameof(GameRoundCouldNotBeSkipped);
        public const string CouldNotFindNextRoundOwner = nameof(CouldNotFindNextRoundOwner);
        public const string GameCouldNotBeCancelled = nameof(GameCouldNotBeCancelled);
    }
}
