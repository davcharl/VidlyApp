using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/Movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            if (movieDto.Id == 0)
                movie.DateAdded = DateTime.Now;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            _context.SaveChanges();
        }

        //DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

        }

    }
}