namespace Glenavon.JFC.WebApp.Controllers;

public class ContactController(IRecaptchaService recaptcha, IConfiguration configuration) : Controller
{
    private readonly IRecaptchaService _recaptcha = recaptcha;

    // GET: /Contact/Index
    public IActionResult Index()
    {
        ViewBag.RecaptchaSiteKey = configuration["RecaptchaSettings:SiteKey"];
        var vm = new ContactViewModel();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.RecaptchaSiteKey = configuration["RecaptchaSettings:SiteKey"];
            ViewBag.Error = "Invalid reCAPTCHA. Please try again.";
            return View("Index", model);
        }

        // Validate the reCAPTCHA
        //var recaptchaResult = await _recaptcha.Validate(model.RecaptchaResponse);
        //if (!recaptchaResult.success)
        //{
        //    ModelState.AddModelError(string.Empty, "Captcha validation failed. Please try again.");
        //    ViewBag.RecaptchaSiteKey = _configuration["RecaptchaSettings:SiteKey"];
        //    return View("Index");
        //}

        // After successful submission (e.g., email sent)
        ViewBag.Message = "Your message has been sent successfully!";

        // Return to the same view
        ViewBag.RecaptchaSiteKey = configuration["RecaptchaSettings:SiteKey"];
        return View("Index", new ContactViewModel());
    }
}