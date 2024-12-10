using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ChodoidoUTE.Areas.Admin.Models
{
    public class UserAdminVM
    {
        [Required(ErrorMessage ="Vui lòng không để trống tên")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Email")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword {  get; set; }
        public string? Address {  get; set; }
        public IFormFile? ImgUrl { get; set; }
    }
}
