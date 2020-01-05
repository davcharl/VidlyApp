using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie
            {
                Name = "Shrek"
            };
            return View(movie);
        }

        // GET: Movies/Index
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int? id=0)
        {
            if (id == 0)
                return HttpNotFound();

            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = genres
            };
            ViewData["Message"] = "New Movie";
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            ViewData["Message"] = "Edit Movie";
            return View("MovieForm", viewModel);
        }

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var genres = _context.Genres.ToList();
                    var viewModel = new MovieFormViewModel
                    {
                        Movie = movie,
                        Genres = genres
                    };
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
                    //movieInDb.DateAdded = DateTime.Now;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                    movieInDb.GenreId = movie.GenreId;
                    movieInDb.NumberInStock = movie.NumberInStock;
                }

                _context.SaveChanges();


                return RedirectToAction("Index", "Movies");
            }
            catch(HttpAntiForgeryException afe)
            {
                ViewBag.message = afe.Message;
                return RedirectToAction("GenericError", "Home");
            }

            
        }

    }
}