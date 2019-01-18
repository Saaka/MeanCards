using MeanCards.Model.DTO.Players;

namespace MeanCards.Model.DAL.Modification.Players
{
    public class AddPlayerPointResult
    {
        public bool IsSuccessful { get; set; }
        public PlayerPointsInfo PlayerPoints { get; set; }
    }
}
