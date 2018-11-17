using System;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DataModel.Entity
{
    public class UserGoogleAuth
    {
        [Key]
        public int UserGoogleAuthId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        [StringLength(256)]
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
    }
}
