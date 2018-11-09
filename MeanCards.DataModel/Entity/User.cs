using System.Collections.Generic;

namespace MeanCards.DataModel.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public List<Game> Games { get; set; }
    }
}
