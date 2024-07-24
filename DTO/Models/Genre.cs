using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class Genre
    {
        public Genre()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
