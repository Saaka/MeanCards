using MeanCards.Common.Enums;

namespace MeanCards.Model.DTO.Games
{
    public class GameRoundModel
    {
        public int GameRoundId { get; set; }
        public int Number { get; set; }
        public int GameId { get; set; }
        public GameRoundStatusEnum Status { get; set; }
        public int QuestionCardId { get; set; }
        public int OwnerId { get; set; }
        public int? WinnerId { get; set; }
    }
}
