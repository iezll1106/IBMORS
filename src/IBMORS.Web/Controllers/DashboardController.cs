using Microsoft.AspNetCore.Mvc;

namespace IBMORS.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}