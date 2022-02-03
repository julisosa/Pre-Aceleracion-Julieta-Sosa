using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Pre_Aceleracion_Julieta_Sosa.Models
{
    public class ChallengeContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ChallengeContext(IConfiguration configuration )
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Challenge"]);
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
