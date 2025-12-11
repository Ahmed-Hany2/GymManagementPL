using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IplanService _planService;

        public PlanController(IplanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanById(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, UpdatePlanViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Missed", "Check missing data");
                return View(nameof(Edit), input);
            }
            var result = _planService.UpdatePlan(id, input);
            if (!result)
                TempData["ErrorMessage"] = "Failed to update plan. Please try again.";
            
            else
                TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            var result = _planService.ActivatePlan(id);
            if (!result)
                TempData["ErrorMessage"] = "Failed to activate plan. Please try again.";
            else
                TempData["SuccessMessage"] = "Plan activated successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
