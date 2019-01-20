using MeanCards.Common.Enums;

namespace MeanCards.Model.DTO.Games
{
    public class GameModel
    {
        public int GameId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public GameStatusEnum Status { get; set; }
        public int LanguageId { get; set; }
        public int OwnerId { get; set; }
        public int? WinnerId { get; set; }
        public bool ShowAdultContent { get; set; }
        public int PointsLimit { get; set; }
        public bool IsActive { get; set; }
    }
}
