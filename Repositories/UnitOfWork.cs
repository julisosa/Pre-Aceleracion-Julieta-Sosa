using Pre_Aceleracion_Julieta_Sosa.Interfaces;
using Pre_Aceleracion_Julieta_Sosa.Models;
using System.Threading.Tasks;

namespace Pre_Aceleracion_Julieta_Sosa.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChallengeContext _context;
        private readonly IGenericRepository<Character> _characterRepository;
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IGenericRepository<Movie> _movieRepository;
        private readonly IGenericRepository<User> _userRepository;

        public UnitOfWork(ChallengeContext context)
        {
            _context = context;
        }

        public IGenericRepository<Character> CharacterRepository => _characterRepository ?? new GenericRepository<Character>(_context);

        public IGenericRepository<Genre> GenreRepository => _genreRepository ?? new GenericRepository<Genre>(_context);

        public IGenericRepository<Movie> MovieRepository => _movieRepository ?? new GenericRepository<Movie>(_context);

        public IGenericRepository<User> UserRepository => _userRepository ?? new GenericRepository<User>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
