using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Api.Models;
using Movie_Api.Dtos;


namespace Movie_Api
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesDto>();
            CreateMap<MoviesDto, Movie>()
                .ForMember(src=>src.Poster,opt=>opt.Ignore());
        }
    }
}
