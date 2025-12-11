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

        public bool CreateSession(CreateSessionViewModel input)
        {
            if(!IsTrainerExists(input.TrainerId) || !IsCategoryExists(input.CategoryId) || !IsValidDateRange( input.StartDate, input.EndDate))
                return false;

            var session = _mapper.Map<CreateSessionViewModel, Session>(input);
            _unitOfWork.GetRepository<Session>().Add(session);
            _unitOfWork.SaveChanges();
            return true;
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

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = _unitOfWork.sessionRepository.GetSessionWithTrainerAndCategory(sessionId);

            if (session == null)
                return null;
            var mapedSession = _mapper.Map <Session, SessionViewModel> (session);


            mapedSession.AvailableSlots = session.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            
            return mapedSession;
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel input)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (!IsSessionAvailableForUpdate(session) || 
                !IsTrainerExists(input.TrainerId) || 
                !IsValidDateRange(input.StartDate, input.EndDate)
                )
                return false;

            session.TrainerId = input.TrainerId;
            session.StartDate = input.StartDate;
            session.EndDate = input.EndDate;
            session.Description = input.Description;
            session.UpdatedAt = DateTime.Now;
            _unitOfWork.GetRepository<Session>().Update(session);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool RemoveSession(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (!IsSessionAvailableForRemove(session))
                return false;

           
            _unitOfWork.GetRepository<Session>().Delete(session);
            _unitOfWork.SaveChanges();
            return true;

        }

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (session == null)
                return null;
           return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoriesDropDown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map< IEnumerable<CategorySelectViewModel> >(categories);
        }

        public IEnumerable<TrainerSelectViewModel> GetTrainersDropDown()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        #region Helper Methods

        private bool IsTrainerExists(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            return trainer != null;
        }
        private bool IsCategoryExists(int categoryId)
        {
            var category = _unitOfWork.GetRepository<Category>().GetById(categoryId);
            return category != null;
        }
        private bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate && startDate >= DateTime.Now;
        }

        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session == null || session.EndDate < DateTime.Now || session.StartDate <= DateTime.Now)
                return false;

            var hasActiveBookings = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBookings)
                return false;

            return true;
        }

        private bool IsSessionAvailableForRemove(Session session)
        {
            if (session == null || 
                session.StartDate > DateTime.Now ||
                (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) 
                )
                return false;

            var hasActiveBookings = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBookings)
                return false;

            return true;
        }

        

        #endregion
    }
}
