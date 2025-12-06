using GymManagementDAL.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Member : GymUser
    {
        public string? Photo { get; set; }
        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<Membership> MemberPlans { get; set; } = null!;
        public ICollection<Booking> MemberSessions { get; set; } = null!;
    }
}
