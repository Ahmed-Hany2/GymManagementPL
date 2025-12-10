using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult MemberDetails(int id) 
        {
            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            
                return RedirectToAction(nameof(Index));
           
            return View(member);
        }

        public IActionResult HealthRecordDetails(int id)
        {
            var healthRecord = _memberService.GetMemberHealthRecord(id);
            if (healthRecord == null)
                return RedirectToAction(nameof(Index));
            return View(healthRecord);
        }
    }
}
