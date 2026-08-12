namespace IBMORS.Web.Models;

public class Office
{
    public int OfficeId { get; set; }

    public string OfficeCode { get; set; } = "";

    public string OfficeName { get; set; } = "";

    public string OfficeType { get; set; } = "";

    public string? ParentOfficeName { get; set; }

    public string Status { get; set; } = "";
}