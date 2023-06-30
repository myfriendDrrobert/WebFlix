using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs;

public class FilmGenreDTO
{
    public FilmGenreDTO(int filmId, int genreId)
    {
        this.FilmId = filmId;
        this.GenreId = genreId;
    }

    public int FilmId { get; set; }
    public int GenreId { get; set; }

    public string Film { get; set; } = null!;
    public string Genre { get; set; } = null!;
}
