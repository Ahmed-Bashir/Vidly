using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Controllers.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private MyDbContext _context;

        public MoviesController()
        {
            _context = new MyDbContext();
        }

        // GET /api/Movies 

        public IEnumerable<MovieDto> GetMovies()
        {

            return _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDto >);
        }

        // GET /api/Movies/1

        public IHttpActionResult GetMovies(int id)
        {

            var Movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (Movie == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Movie, MovieDto>(Movie));
        }

        // POST /api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)

                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(MovieDto);
            _context.Movies.Add(movie);

            MovieDto.Id = movie.Id;

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/"+ movie.Id), MovieDto);
        }

        // PUT - /api/Movies 
        [HttpPut]

        public void UpdateMovie(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var MovieDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (MovieDb == null)

                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(MovieDto, MovieDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void RemoveMovie(int id)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var MovieDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (MovieDb == null)

                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(MovieDb);

            _context.SaveChanges();
        }

    }
}
