using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ChodoidoUTE.Areas.Admin.Models
{
    public class LoginVM
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
