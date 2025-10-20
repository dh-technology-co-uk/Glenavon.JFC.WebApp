using Microsoft.AspNetCore.Mvc;
using Glenavon.JFC.WebApp.Services;

namespace Glenavon.JFC.WebApp.Controllers;

public class ContactController(EmailService emailService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        // Just return empty form
        return View(new ContactViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(ContactViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var htmlBody = $@"
<b>Name:</b> {model.Name}<br/>
<b>Phone:</b> {model.Phone}<br/>
<b>Email:</b> {model.Email}<br/>
<b>Message:</b> {model.Message}";

        try
        {
            await emailService.SendEmailAsync(
                "equipmentkitrequests@glenavonjfc.co.uk",
                "Contact Form Submission",
                htmlBody);

            // ✅ store success message in TempData
            TempData["SuccessMessage"] = "Your message has been sent successfully!";
        }
        catch
        {
            TempData["ErrorMessage"] = "Sorry, something went wrong while sending your message.";
        }

        // ✅ redirect to Home/Index so message shows on the homepage
        return RedirectToAction("Index", "Home");
    }
}
