using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            //ViewBag.Message = "Hello";
            //ViewData["Welcome"] = "Hello Members!";
            //TempData.Keep("ErrorMessage");
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult MemberDetails(int id)
        {
            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public IActionResult HealthRecordDetails(int id)
        {
            var healthRecord = _memberService.GetMemberHealthRecord(id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Missed", "check missing data");
                return View(nameof(Create), input);
            }
            bool result = _memberService.CreateMember(input);
            if (result)
                TempData["SuccessMessage"] = "Member created successfully.";
            else
                TempData["ErrorMessage"] = "Failed to create member! Phone number or email already exist!";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult MemberEdit(int id)
        {
            var member = _memberService.GetMemberToUpdate(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult MemberEdit([FromRoute] int id, MemberToUpdateViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }
            bool result = _memberService.UpdateMemberDetails(id, input);
            if (result)
                TempData["SuccessMessage"] = "Member Updted Successfully.";
            else
                TempData["ErrorMessage"] = "Failed to update member!";

            return RedirectToAction(nameof(Index));

        }

      
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            var result = _memberService.RemoveMember(id);
            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully.";
            else
                TempData["ErrorMessage"] = "Failed to delete member!";
            return RedirectToAction(nameof(Index));
        }
    }
}
