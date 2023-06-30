using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Membership.Database.Entities
{
    public class Film : IEntity
    {
        //public Film()
        //{
        //    SimilarFilms = new HashSet<SimilarFilm>();
        //    Genres = new HashSet<Genre>();
        //}
        public int Id { get; set; }
        public string? Title { get; set; } 
        public int DirectorId { get; set; }

        public bool? Free { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Released { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(1024)]
        public string? FilmUrl{ get; set; }

        public string? MarqueeUrl { get; set; }

        public string? ThumbUrl { get; set; }

        public virtual ICollection<SimilarFilm>? SimilarFilms { get; set; }
        public virtual ICollection<Genre>? Genres { get; set; }
        public virtual Director Director { get; set; } = null!;
    }
}
