using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DataModel.Entity
{
    public class User
    {
        public User()
        {
            Games = new List<Game>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(64)]
        [Required]
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public List<Game> Games { get; set; }
    }
}
