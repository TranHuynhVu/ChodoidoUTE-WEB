using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } 
        public DateTime DateChecked { get; set; }

        public virtual AppUser User { get; set; }
    }
}
