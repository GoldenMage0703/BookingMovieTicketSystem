using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class MovieGenre
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Movie { get; set; } = null!;
        public virtual Movie MovieNavigation { get; set; } = null!;
    }
}
