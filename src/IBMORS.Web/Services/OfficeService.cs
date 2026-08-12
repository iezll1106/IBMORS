using IBMORS.Web.Models;
using Npgsql;

namespace IBMORS.Web.Services;

public class OfficeService
{
    private readonly string _connectionString;

    public OfficeService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("IBMORS")!;
    }

    public List<Office> GetOffices()
    {
        var offices = new List<Office>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            SELECT
                o.office_id,
                o.office_code,
                o.office_name,
                o.office_type,
                p.office_name AS parent_office,
                o.status
            FROM offices o
            LEFT JOIN offices p
                ON o.parent_office_id = p.office_id
            ORDER BY o.office_code;
        ";

        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            offices.Add(new Office
            {
                OfficeId = reader.GetInt32(0),
                OfficeCode = reader.GetString(1),
                OfficeName = reader.GetString(2),
                OfficeType = reader.IsDBNull(3) ? "" : reader.GetString(3),
                ParentOfficeName = reader.IsDBNull(4) ? null : reader.GetString(4),
                Status = reader.GetString(5)
            });
        }

        return offices;
    }
}