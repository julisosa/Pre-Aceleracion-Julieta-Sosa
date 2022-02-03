using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pre_Aceleracion_Julieta_Sosa.DTOs;
using Pre_Aceleracion_Julieta_Sosa.Interfaces;
using Pre_Aceleracion_Julieta_Sosa.Models;
using System;

namespace Pre_Aceleracion_Julieta_Sosa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = _unitOfWork.MovieRepository.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Movie movie = _unitOfWork.MovieRepository.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Post(MovieDto movieDto)
        {
            Genre dbGenre = _unitOfWork.GenreRepository.GetById(movieDto.GenreId);

            if (dbGenre == null)
            {
                return NotFound("Genre not found");
            }

            Movie movie = new Movie
            {
                Genre = dbGenre,
                Image = movieDto.Image,
                Title = movieDto.Title,
                CreationDate = movieDto.CreationDate,
                Qualification = movieDto.Qualification
            };

            _unitOfWork.MovieRepository.Add(movie);
            _unitOfWork.SaveChanges();
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, MovieDto movieDto)
        {
            Movie dbMovie = _unitOfWork.MovieRepository.GetById(id);

            if (dbMovie == null)
            {
                return NotFound("Movie not found");
            }

            Genre dbGenre = _unitOfWork.GenreRepository.GetById(movieDto.GenreId);

            if (dbGenre == null)
            {
                return NotFound("Genre not found");
            }

            dbMovie.Genre = dbGenre;
            dbMovie.Image = movieDto.Image;
            dbMovie.Title = movieDto.Title;
            dbMovie.CreationDate = movieDto.CreationDate;
            dbMovie.Qualification = movieDto.Qualification;

            _unitOfWork.MovieRepository.Update(dbMovie);
            _unitOfWork.SaveChanges();
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Movie dbMovie = _unitOfWork.MovieRepository.GetById(id);

                if (dbMovie == null)
                {
                    return NotFound($"Movie {id} not found");
                }

                //var characters = _unitOfWork.CharacterRepository.GetAll();

                //var hasMovie = characters.Where(x => x.MovieId == id).Any();

                _unitOfWork.MovieRepository.Delete(id);
                _unitOfWork.SaveChanges();

                return Ok("Deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
