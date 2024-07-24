using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieGenres = new HashSet<MovieGenre>();
            Showtimes = new HashSet<Showtime>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string? StoryLine { get; set; }
        public string? Poster { get; set; }
        public string? DirectorName { get; set; }
        public string Rating { get; set; } = null!;
        public string? Time { get; set; }
        public string? Trailer { get; set; }
        [JsonIgnore]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        [JsonIgnore]
        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
