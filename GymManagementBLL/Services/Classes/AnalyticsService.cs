using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel? GetAnalyticsData()
        {
            var sessionRepository = _unitOfWork.GetRepository<Session>();
            return new AnalyticsViewModel
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpComingSessions = sessionRepository.GetAll(s => s.StartDate > DateTime.Now).Count(),
                OngoingSessions =  sessionRepository.GetAll(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now).Count(),
                CompletedSessions = sessionRepository.GetAll(s => s.EndDate < DateTime.Now).Count(),
            };
        }
    }
}
