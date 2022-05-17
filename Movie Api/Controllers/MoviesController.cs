using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Api.Models;
using Movie_Api.Dtos;
using System.IO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Movie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContxet _context;
        public MoviesController(ApplicationDbContxet context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// //////////////////////////////////////Getall/////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> Getall() 
        {
            var movie = await _context.Movies.OrderByDescending(m=>m.Rate).Include(m =>m.Genre).ToListAsync();
            return Ok(movie);
        }

        /// /////////////////////////////////////////Getbyid//////////////////////////////////////

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id) 
        {
            var movies = await _context.Movies.Include(m=>m.Genre).SingleOrDefaultAsync(m=>m.Id==id);
            return Ok(movies);
        }

        /// //////////////////////////////////////GetbyGenreId/////////////////////////////////////////

        [HttpGet("GetbyGenreId")]
        public async Task<IActionResult> GetbyGenreId(Byte genreId) 
        {
            var movie = await _context.Movies.Where(m=>m.GenreId== genreId).Include(m => m.Genre).ToListAsync();
            return Ok(movie);
        }

        /// //////////////////////////////////////Create/////////////////////////////////////////

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MoviesDto dto ) 
        {
            if (dto.Poster == null)
            {
                return BadRequest("Poster is requried");
            }
               
            var isValedGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isValedGenre) 
            {
                return BadRequest("GenreId not found");
            }

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);

            
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return Ok(movie);
                
        }

        /// /////////////////////////////////////////Update//////////////////////////////////////
        /// 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , [FromBody] MoviesDto dto) 
        {
            var movies = await _context.Movies.FindAsync(id);

            if (movies == null)
                return NotFound();

            var isValedGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isValedGenre)
            {
                return BadRequest("GenreId not found");
            }

            if (dto.Poster!=null) 
            {
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                //movies.Poster = dataStream.ToArray();
            }

            var movie = _mapper.Map<Movie>(dto);

            _context.SaveChanges();
            return Ok(movie);
        
        }

        /// /////////////////////////////////////////Delete//////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
                return NotFound();
            _context.Movies.Remove(movies);
            _context.SaveChanges();
            return Ok(movies);
        }
    }
}
