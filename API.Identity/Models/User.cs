using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Identity.Models
{
    public class User : IdentityUser
    {

        [Column("us_firstname")]
        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Column("us_lastname")]
        [MaxLength(255)]
        public string? LastName { get; set; }

        public Company Company { get; set; } = null!;
    }
}
