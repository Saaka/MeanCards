using System;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class GameCheckpoint
    {
        [Key]
        public int GameCheckpointId { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        [StringLength(32)]
        public string Code { get; set; }
        [Required]
        [StringLength(64)]
        public string OperationType { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        public virtual Game Game { get; set; }
    }
}
