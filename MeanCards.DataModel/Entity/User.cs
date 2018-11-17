using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DataModel.Entity
{
    public class User
    {
        public User()
        {
            Games = new List<Game>();
            UserAuths = new List<UserAuth>();
            UserGoogleAuths = new List<UserGoogleAuth>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(256)]
        [Required]
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public virtual List<Game> Games { get; set; }
        public virtual List<UserAuth> UserAuths { get; set; }
        public virtual List<UserGoogleAuth> UserGoogleAuths { get; set; }
    }
}
