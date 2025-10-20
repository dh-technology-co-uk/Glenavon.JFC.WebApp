namespace Glenavon.JFC.WebApp.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telephone number is required")]
    [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$",
        ErrorMessage = "Invalid UK telephone number")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [MinLength(150, ErrorMessage = "Your message must be at least 150 characters long.")]
    public string Message { get; set; } = string.Empty;
}