using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _context;
        private readonly Dictionary<string, object> repositories = [];

        public UnitOfWork(GymDbContext context)
        {
            
            _context = context;
        }
        public IGenericRepositories<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var entityName = typeof(TEntity).Name;
            if(repositories.TryGetValue(entityName, out object? value))
                return (IGenericRepositories<TEntity>)value;

            var repository = new GenericRepository<TEntity>(_context);
            repositories.Add(entityName, repository);
            return repository;

        }

        public int SaveChanges() => _context.SaveChanges();
    }
}
