using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Range(0.1, 300.0, ErrorMessage = "Height must be between 0.1 and 300 cm.")]
        public decimal Height { get; set; }
        [Range(0.1, 500.0, ErrorMessage = "Weight must be between 0.1 and 300 cm.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood Type is required")]
        [StringLength(3, ErrorMessage = "Blood Type cannot exceed 3 characters.")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }
    }
}
