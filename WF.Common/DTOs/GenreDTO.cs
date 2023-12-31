﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs;

public class GenreDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

   
}

public class GenreEditDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class GenreCreateDTO : GenreEditDTO
{
    public string Name { get; set; }
    
}
