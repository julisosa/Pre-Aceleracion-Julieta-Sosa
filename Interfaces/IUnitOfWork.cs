using Pre_Aceleracion_Julieta_Sosa.Models;
using System;
using System.Threading.Tasks;

namespace Pre_Aceleracion_Julieta_Sosa.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Character> CharacterRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }
        IGenericRepository<Movie> MovieRepository { get; }
        IGenericRepository<User> UserRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
