using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Api.Dtos
{
    public class CreateGenresDto
    {
        [Required]
        public string Name { get; set; }
    }
}
