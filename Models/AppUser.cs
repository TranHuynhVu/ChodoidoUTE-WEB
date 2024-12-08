using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class AppUser : IdentityUser
    {
        public int? CountPost { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public bool? Gender { get; set; }
        public string? ImgUrl { get; set; }
        public string? Local { get; set; }
        public string? Name { get; set; }
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public int? Status {  get; set; }
        public int? Point { get; set; }
        public int? ProductLost { get; set; }
        public int? ProductSuccess { get; set; }
        public string? Role { get; set; } // ADMIN, USER
        public string? Zalo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
        public virtual ICollection<Follower> Followers { get; set; }
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
        public virtual ICollection<MissionDetail> MissionDetails { get; set; }
    }
}
