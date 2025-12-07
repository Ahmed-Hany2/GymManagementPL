using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IplanService
    {
        bool UpdatePlan(int id, UpdatePlanViewModel input);
        UpdatePlanViewModel? GetPlanToUpdate(int Planid);

        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanById(int planId);

        bool ActivatePlan(int planId);


    }
}
