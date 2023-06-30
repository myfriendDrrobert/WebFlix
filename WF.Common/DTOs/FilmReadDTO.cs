using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs;

public class FilmReadDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    //public int DirectorId { get; set; }

    //public bool Free { get; set; }
    [DataType(DataType.Date)]
    public DateTime Released { get; set; }
    
    public string? Description { get; set; }

    public string? FilmUrl { get; set; }

    //public string? MarqueeUrl { get; set; }

    public string? ThumbUrl { get; set; }

    //public List<SimilarFilmsDTO> SimilarFilms { get; set; } = null!;
    //public List<GenreDTO> Genres { get; set; }
    public string Director { get; set; }
}
