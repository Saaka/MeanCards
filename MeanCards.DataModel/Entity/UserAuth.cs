using System;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DataModel.Entity
{
    public class UserAuth
    {
        [Key]
        public int UserAuthId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(64)]
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
    }
}
