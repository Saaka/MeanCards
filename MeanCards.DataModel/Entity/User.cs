﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DataModel.Entity
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Games = new List<Game>();
        }
        
        [StringLength(64)]
        public string UserCode { get; set; }
        public long? GoogleId { get; set; }
        public string ImageUrl { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
