using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        Plan? GetById(int id);
        IEnumerable<Plan> GetAll();
        int Add(Plan plan);
        int Update(Plan plan);
        int Delete(int id);
    }
}
