using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategory()
                .OrderByDescending(x=> x.StartDate);

            if (sessions == null || !sessions.Any())
                return Enumerable.Empty<SessionViewModel>();
            
            var mapedSessions = _mapper.Map<IEnumerable<Session>,  IEnumerable<SessionViewModel> >  (sessions);

            foreach(var session in mapedSessions)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            }
            return mapedSessions;
        }
    }
}
