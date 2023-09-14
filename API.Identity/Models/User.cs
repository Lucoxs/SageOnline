using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API.Identity.DTO.User;

namespace API.Identity.Models
{
    public class User : IdentityUser
    {
        [Column("us_firstname")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Column("us_lastname")]
        [MaxLength(255)]
        public string LastName { get; set; }

        public Company Company { get; set; } = null!;

        public User() { }

        public User(UserNewDTO user, Company company)
        {
            var userName = $"{user.LastName} {user.FirstName}";

            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = userName;
            NormalizedEmail = userName.Replace(" ", "_").ToUpper();
            Email = user?.Email;
            NormalizedEmail = user?.Email?.ToLower();
            PhoneNumber = user?.PhoneNumber;
            Company = company;
        }
    }
}
