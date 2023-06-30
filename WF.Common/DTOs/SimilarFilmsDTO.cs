using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs;

public class SimilarFilmsDTO 
{
    public int FilmId { get; set; }
    public int SimilarFilmId { get; set; }

    public string Film { get; set; } = null!;

    //public string Similar { get; set; } = null!;

    public FilmReadDTO? SimilarFilm { get; set; } = null!;
}
