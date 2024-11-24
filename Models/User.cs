using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class User : IdentityUser
    {
        [Key]
        public long Id { get; set; }
        public long? CountPost { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public bool? Gender { get; set; }
        public string ImgUrl { get; set; }
        public string Local { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public long? Point { get; set; }
        public long? ProductLost { get; set; }
        public long? ProductSuccess { get; set; }
        public string Role { get; set; } // ADMIN, USER
        public string Zalo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
        public virtual ICollection<Follower> Followers { get; set; }
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
        public virtual ICollection<MissionDetail> MissionDetails { get; set; }
    }
}
