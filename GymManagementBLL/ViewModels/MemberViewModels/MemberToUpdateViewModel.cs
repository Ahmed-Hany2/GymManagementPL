using GymManagementDAL.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!;

        public string? Photo { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Phone number must start with '01' and be 11 digits long.")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Building Number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Building Number must be a positive integer.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City can only contain letters and single spaces between words.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Street is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Street can only contain letters and single spaces between words.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "City cannot exceed 150 characters.")]
        public string Street { get; set; } = null!;
 
    }
}
