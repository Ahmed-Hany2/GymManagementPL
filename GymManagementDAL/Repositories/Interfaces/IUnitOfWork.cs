using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepositories<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
