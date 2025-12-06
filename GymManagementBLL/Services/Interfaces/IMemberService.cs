using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService 
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel model);

        MemberViewModel? GetMemberDetails(int memberId);
    }
}
