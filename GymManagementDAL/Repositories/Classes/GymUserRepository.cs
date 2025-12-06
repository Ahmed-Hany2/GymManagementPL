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
    public class GymUserRepository : IGymUserRepository
    {
        private readonly GymDbContext _context;

        public GymUserRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(GymUser gymUser)
        {
            _context.GymUsers.Add(gymUser);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var gymUser = _context.GymUsers.Find(id);
            if (gymUser != null)
            {
                _context.Remove(gymUser);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<GymUser> GetAll() => _context.GymUsers.ToList();

        public GymUser? GetById(int id) => _context.GymUsers.Find(id);
        public int Update(GymUser gymUser)
        {
            _context.Update(gymUser);
            return _context.SaveChanges();
        }
    }
}
