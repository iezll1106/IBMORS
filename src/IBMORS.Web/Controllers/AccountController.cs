using Microsoft.AspNetCore.Mvc;

namespace IBMORS.Web.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Temporary login for prototype
        return RedirectToAction("Index", "Dashboard");
    }
}