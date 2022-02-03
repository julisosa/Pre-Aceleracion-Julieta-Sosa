using Microsoft.EntityFrameworkCore;
using Pre_Aceleracion_Julieta_Sosa.Interfaces;
using Pre_Aceleracion_Julieta_Sosa.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pre_Aceleracion_Julieta_Sosa.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ChallengeContext _context;
        protected readonly DbSet<T> _entities;

        public GenericRepository(ChallengeContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public void Add(T entity)
        {
            _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(int id)
        {
            T entity = GetById(id);
            _entities.Remove(entity);
        }
    }
}
