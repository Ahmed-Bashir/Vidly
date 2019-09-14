using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        public IEnumerable<Movie> GetMovies()
        {

            return _context.Movies.ToList();
        }

        // GET /api/Movies/1

        public Movie GetMovies(int id)
        {

            var Movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (Movie == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Movie;
        }

        // POST /api/Movies
        [HttpPost]
        public Movie CreateMovie(Movie Movie)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);


            _context.Movies.Add(Movie);

            _context.SaveChanges();

            return Movie;
        }

        // PUT - /api/Movies 
        [HttpPut]

        public void UpdateMovie(int id, Movie Movie)
        {
            if (!ModelState.IsValid)

                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var MovieDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (MovieDb == null)

                throw new HttpResponseException(HttpStatusCode.NotFound);

            MovieDb.Name = Movie.Name;
            MovieDb.ReleaseDate = Movie.ReleaseDate;
            MovieDb.Genre = Movie.Genre;
            MovieDb.Stock = Movie.Stock;

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
