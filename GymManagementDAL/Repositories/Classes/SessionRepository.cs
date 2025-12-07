using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _context;

        public SessionRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(Session session)
        {
            _context.Sessions.Add(session);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = _context.Sessions.Find(id);
            if (session != null)
            {
                _context.Remove(session);
                return _context.SaveChanges();
            }
            return 0;
        }

        

        public IEnumerable<Session> GetAll() => _context.Sessions.ToList();

        

        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory(int memberId)
        {
            return _context.Sessions
                .Include(s=> s.Trainer)
                .Include(s=> s.Category)
                .ToList();
        }

        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            throw new NotImplementedException();
        }

        public Session? GetById(int id) => _context.Sessions.Find(id);

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _context.Bookings.Where(x => x.SessionId == sessionId).Count();
        }

        public Session GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _context.Sessions
               .Include(s => s.Trainer)
               .Include(s => s.Category)
               .FirstOrDefault(x=> x.Id == sessionId);
        }

        public int Update(Session session)
        {
            _context.Update(session);
            return _context.SaveChanges();
        }

        
    }
}
