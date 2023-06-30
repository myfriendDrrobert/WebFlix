using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs;

public class FilmCreateDTO
{
    
    public string Title { get; set; } = null!;
    public int DirectorId { get; set; }

    public bool Free { get; set; }
    [DataType(DataType.Date)]
    public DateTime Released { get; set; }
    
    public string? Description { get; set; }

    public string? FilmUrl { get; set; }

    public string? ThumbUrl { get; set; }
    public string? MarqueeUrl { get; set; }
    public List<GenreDTO>? Genres { get; set; }

    public int[]? GenreIds { get; set; }

    public int[]? SimilarIds { get; set; }




}
