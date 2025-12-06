using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Session? GetById(int id);
        IEnumerable<Session> GetAll();
        int Add(Session session);
        int Update(Session session);
        int Delete(int id);
    }
}
