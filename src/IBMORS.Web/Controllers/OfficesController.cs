using IBMORS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using IBMORS.Web.Models;

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
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public IActionResult Create(Office office)
    {
        if (!ModelState.IsValid)
        {
            return View(office);
        }

        if (_officeService.OfficeCodeExists(office.OfficeCode))
        {
            ModelState.AddModelError("OfficeCode", "Office code already exists.");
            return View(office);
        }

        _officeService.AddOffice(office);

        TempData["SuccessMessage"] = "Office added successfully.";

        return RedirectToAction(nameof(Index));
    }
}