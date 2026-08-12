using IBMORS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace IBMORS.Web.Controllers;

[Route("Offices")]
public class OfficesController : Controller
{
    private readonly OfficeService _officeService;

    public OfficesController(OfficeService officeService)
    {
        _officeService = officeService;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public IActionResult Index()
    {
        var offices = _officeService.GetOffices();
        return View(offices);
    }
}