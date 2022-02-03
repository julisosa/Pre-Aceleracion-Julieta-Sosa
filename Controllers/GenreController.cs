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
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var genres = _unitOfWork.GenreRepository.GetAll();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Genre genre = _unitOfWork.GenreRepository.GetById(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);

        }

        [HttpPost]
        public IActionResult Post(GenreDto genreDto)
        {
            Genre genre = new Genre
            {
                Image = genreDto.Image,
                Name = genreDto.Name,
            };

            _unitOfWork.GenreRepository.Add(genre);
            _unitOfWork.SaveChanges();
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, GenreDto genreDto)
        {
            Genre genre = _unitOfWork.GenreRepository.GetById(id);

            if (genre == null)
            {
                return NotFound("Genre not found");
            }

            genre.Name = genreDto.Name;
            genre.Image = genreDto.Image;
            _unitOfWork.GenreRepository.Update(genre);
            _unitOfWork.SaveChanges();
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Genre dbGenre = _unitOfWork.GenreRepository.GetById(id);

                if (dbGenre == null)
                {
                    return NotFound($"User {id} not found");
                }

                _unitOfWork.GenreRepository.Delete(id);
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
