using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Api.Dtos
{
    public class MoviesDto
    {
        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storyline { get; set; }

        
        public IFormFile Poster { get; set; }

        public int GenreId { get; set; }
    }
}
