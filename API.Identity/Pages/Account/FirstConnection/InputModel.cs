using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Identity.Pages.Account.FirstConnection
{
    public class InputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CheckPassword { get; set; }

        public string ReturnUrl { get; set; }

        public string Button { get; set; }
    }
}
