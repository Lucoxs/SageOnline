using System.ComponentModel.DataAnnotations;

namespace UI.Pages.Create;

public class InputModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}