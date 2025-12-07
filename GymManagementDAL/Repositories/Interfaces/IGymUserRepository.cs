using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGymUserRepository
    {
        GymUser? GetById(int id);
        IEnumerable<GymUser> GetAll();
        int Add(GymUser gymUser);
        int Update(GymUser gymUser);
        int Delete(int id);
    }
}
