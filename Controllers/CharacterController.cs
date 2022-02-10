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
    public class CharacterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CharacterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create character
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var characters = _unitOfWork.CharacterRepository.GetAll();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Character character = _unitOfWork.CharacterRepository.GetById(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpPost]
        public IActionResult Post(CharacterDto characterDto)
        {
            Movie dbMovie = _unitOfWork.MovieRepository.GetById(characterDto.MovieId);

            if (dbMovie == null)
            {
                return NotFound("Movie not found");
            }

            Character character = new Character
            {
                Movie = dbMovie,
                Image = characterDto.Image,
                Name = characterDto.Name,
                Age = characterDto.Age,
                Weight = characterDto.Weight,
                Story = characterDto.Story,
            };

            _unitOfWork.CharacterRepository.Add(character);
            _unitOfWork.SaveChanges();
            return Ok(character);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CharacterDto characterDto)
        {
            Character character = _unitOfWork.CharacterRepository.GetById(id);

            if (character == null)
            {
                return NotFound("Character not found");
            }

            character.Image = characterDto.Image;
            character.Name = characterDto.Name;
            character.Age = characterDto.Age;
            character.Weight = characterDto.Weight;
            character.Story = characterDto.Story;
            _unitOfWork.CharacterRepository.Update(character);
            _unitOfWork.SaveChanges();
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Character dbCharacter = _unitOfWork.CharacterRepository.GetById(id);

                if (dbCharacter == null)
                {
                    return NotFound($"Character {id} not found");
                }

                _unitOfWork.CharacterRepository.Delete(id);
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
