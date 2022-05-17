using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Api.Models
{
    public class ApplicationDbContxet:DbContext
    {
        public ApplicationDbContxet(DbContextOptions<ApplicationDbContxet>options):base(options)
        {
        }
        public DbSet <Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
