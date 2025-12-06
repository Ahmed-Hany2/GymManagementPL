using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category? GetById(int id);
        IEnumerable<Category> GetAll();
        int Add(Category category);
        int Update(Category category);
        int Delete(int id);
    }
}
