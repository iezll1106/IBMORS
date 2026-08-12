namespace IBMORS.Web.Models;
using System.ComponentModel.DataAnnotations;

public class Office
{
    public int OfficeId { get; set; }

    [Required]
    [Display(Name = "Office Code")]
    public string OfficeCode { get; set; } = "";

    [Required]
    [Display(Name = "Office Name")]
    public string OfficeName { get; set; } = "";

    [Required]
    [Display(Name = "Office Type")]
    public string OfficeType { get; set; } = "";

    public string? ParentOfficeName { get; set; }

    [Required]
    public string Status { get; set; } = "";
}