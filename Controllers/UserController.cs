using Microsoft.AspNetCore.Mvc;
using Pre_Aceleracion_Julieta_Sosa.Interfaces;
using Pre_Aceleracion_Julieta_Sosa.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pre_Aceleracion_Julieta_Sosa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        ChallengeContext _context;
        private readonly IMailService _mailService;

        public UserController(ChallengeContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users.AsEnumerable();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            User dbUser = _context.Users.Find(id);

            if (dbUser == null)
            {
                return NotFound("User not found");
            }

            dbUser.Email = user.Email;
            dbUser.Password = user.Password;
            dbUser.Name = user.Name;
            _context.Users.Update(dbUser);
            _context.SaveChanges();
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                User dbUser = _context.Users.Find(id);

                if (dbUser == null)
                {
                    return NotFound($"User {id} not found");
                }

                _context.Users.Remove(dbUser);
                _context.SaveChanges();

                return Ok("Deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("sendmail")]
        public async Task<IActionResult> SendMail()
        {

            await _mailService.SendMail("julisoad15@gmail.com", "Pre-Aceleracion", "<h1>Este es el cuerpo del correo</h1>");
            return Ok();
        }

    }
}
