using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Api.Models;
using Movie_Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Movie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContxet _contxet;
        public GenresController(ApplicationDbContxet contxet)
        {
            _contxet = contxet;
        }
        [HttpGet]
        public async Task<IActionResult> Getall() 
        {
            var genres = await _contxet.Genres.OrderBy(g=>g.Name).ToListAsync();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGenresDto dto) 
        {
            Genre genre = new()
            {
                Name = dto.Name
            };
           await _contxet.Genres.AddAsync(genre);
            _contxet.SaveChanges();
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody] CreateGenresDto dto)
        {
            var genre = await _contxet.Genres.SingleOrDefaultAsync(g => g.Id==id);

            if (genre == null)
                return NotFound();

            genre.Name = dto.Name;
            _contxet.SaveChanges();

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _contxet.Genres.FindAsync(id);

            if (genre == null)
                return NotFound();

            _contxet.Genres.Remove(genre);
            _contxet.SaveChanges();

            return Ok(genre);
        }

    }
}
