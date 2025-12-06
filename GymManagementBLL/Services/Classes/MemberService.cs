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

        public bool CreateMember(CreateMemberViewModel model)
        {
            try
            {
                if (IsEmailExists(model.Email) || IsPhoneExists(model.Phone))
                    return false;
                var member = new Member
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Address = new Address
                    {
                        BuildingNumber = model.BuildingNumber,
                        City = model.City,
                        Street = model.Street
                    },
                    HealthRecord = new HealthRecord
                    {
                        Height = model.HealthRecordViewModel?.Height ?? 0,
                        Weight = model.HealthRecordViewModel?.Weight ?? 0,
                        BloodType = model.HealthRecordViewModel?.BloodType ?? null,
                        Note = model.HealthRecordViewModel?.Note ?? null
                    }
                };
                _memberRepository.Add(member);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #region Helper Methods
        private string FormatAddress(Address address)
        {
            if (address is null)
                return "N/A";
            return $"{address.BuildingNumber}, {address.Street}, {address.City}";
        }

        private bool IsEmailExists(string email)
        {
            var existingMember = _memberRepository.GetAll(m => m.Email.ToLower() == email.ToLower());
            return existingMember is not null && existingMember.Any();
        }

        private bool IsPhoneExists(string phone)
        {
            var existingMember = _memberRepository.GetAll(m => m.Phone == phone);
            return existingMember is not null && existingMember.Any();
        }
        #endregion
    }
}
