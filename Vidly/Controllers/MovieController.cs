using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie

        private MyDbContext _context;

        public MovieController()
        {
            _context = new MyDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    

        public ActionResult Index()
        {
            var movie = _context.Movies.Include(m => m.Genre).ToList();

            return View(movie);
        }

        public ActionResult Details(int? id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(x => x.Id == id);

            return View(movie);
        }

        public ActionResult Edit(int? id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(x => x.Id == id);

            var movieViewModel = new MovieFormViewModel(movie) {Genres = _context.Genres.ToList() };

            return View("MovieForm", movieViewModel);
        }

        public ActionResult New()
        {

          
            var movieViewModel = new MovieFormViewModel() {Genres = _context.Genres.ToList() };

            return View("MovieForm", movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {


            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie) {  Genres = _context.Genres.ToList() };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);

            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = (DateTime)movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.Stock = movie.Stock;


            }

                _context.SaveChanges();



            return RedirectToAction("Index", "Movie");
        }
    }
}