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

    [HttpGet("Details/{id}")]
    public IActionResult Details(int id)
    {
        var office = _officeService.GetOfficeById(id);

        if (office == null)
            return NotFound();

        return View(office);
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var office = _officeService.GetOfficeById(id);

        if (office == null)
            return NotFound();

        return View(office);
    }

    [HttpPost("Edit/{id}")]
    public IActionResult Edit(int id, Office office)
    {
        if (!ModelState.IsValid)
        {
            return View(office);
        }

        office.OfficeId = id;

        _officeService.UpdateOffice(office);

        TempData["SuccessMessage"] = "Office updated successfully.";

        return RedirectToAction(nameof(Index));
    }
}