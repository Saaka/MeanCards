namespace MeanCards.ViewModel.Game.Models.GameViewModels
{
    public class PlayerData
    {
        public int PlayerId { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public bool IsRoundOwner { get; set; }
        public bool SubmittedAnswer { get; set; }
        public int Points { get; set; }
    }
}
