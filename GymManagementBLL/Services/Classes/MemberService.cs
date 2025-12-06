using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepositories<Member> _memberRepository;
        public MemberService(IGenericRepositories<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll().ToList() ?? [];
            if( members is null || !members.Any() )
                return [];
            var memberViewModels = members.Select(x => new MemberViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Photo = x.Photo,
                Email = x.Email,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth.ToShortDateString(),
                Gender= x.Gender.ToString(),
                //Address = FormatAddress(x.Address),
                //PlanName = x.MemberPlans?.FirstOrDefault()?.Plan?.Name,
                //MembershipStartDate = x.MemberPlans?.FirstOrDefault()?.CreatedAt.ToShortDateString(),
                //MembershipEndDate = x.MemberPlans?.FirstOrDefault()?.EndDate.ToShortDateString()
            });

            return memberViewModels;
        }
        #region Helper Methods
        private string FormatAddress(Address address)
        {
            if (address is null)
                return "N/A";
            return $"{address.BuildingNumber}, {address.Street}, {address.City}";
        }
        #endregion
    }
}
