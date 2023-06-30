using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs;

public class FilmEditDTO  : FilmCreateDTO
{
    public int Id { get; set; }
   
}
