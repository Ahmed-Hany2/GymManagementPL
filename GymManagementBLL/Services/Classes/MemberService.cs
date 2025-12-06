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
        private readonly IGenericRepositories<Membership> _membershipRepository;
        private readonly IGenericRepositories<Plan> _planRepository;
        private readonly IGenericRepositories<HealthRecord> _healthRecordRepository;

        public MemberService(
            IGenericRepositories<Member> memberRepository,
            IGenericRepositories<Membership> membershipRepository,
            IGenericRepositories<Plan> planRepository,
            IGenericRepositories<HealthRecord> healthRecordRepository
            )
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _planRepository= planRepository;
            _healthRecordRepository = healthRecordRepository;
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

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _memberRepository.GetById(memberId);
            if (member == null) return null;
            var memberViewModel = new MemberViewModel
            {
                Name = member.Name,
                Id = member.Id,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Gender = member.Gender.ToString(),
                Address = FormatAddress(member.Address),
            };
            var activeMembership = _membershipRepository
                .GetAll(m => m.MemberId == memberId && m.Status=="Active")
                .FirstOrDefault();

            if (activeMembership != null)
            {
                var activePlan = _planRepository.GetById(activeMembership.PlanId);
                memberViewModel.PlanName = activePlan?.Name;
                memberViewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }
            return memberViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _healthRecordRepository.GetById(memberId);
            if (memberHealthRecord == null) return null;

            return new HealthRecordViewModel
            {
                Height = memberHealthRecord.Height ,
                Weight = memberHealthRecord.Weight ,
                BloodType = memberHealthRecord.BloodType ,
                Note = memberHealthRecord.Note
            };
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
