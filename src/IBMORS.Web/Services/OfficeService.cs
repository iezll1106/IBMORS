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

    public void AddOffice(Office office)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            INSERT INTO offices
            (office_code, office_name, office_type, status)
            VALUES
            (@code, @name, @type, @status);
        ";

        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@code", office.OfficeCode);
        command.Parameters.AddWithValue("@name", office.OfficeName);
        command.Parameters.AddWithValue("@type", office.OfficeType);
        command.Parameters.AddWithValue("@status", office.Status);

        command.ExecuteNonQuery();
    }

    public bool OfficeCodeExists(string officeCode)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var sql = "SELECT COUNT(*) FROM offices WHERE office_code = @code";

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@code", officeCode);

        var count = Convert.ToInt32(command.ExecuteScalar());

        return count > 0;
    }

    public Office? GetOfficeById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            SELECT office_id, office_code, office_name,
                office_type, status
            FROM offices
            WHERE office_id = @id";

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Office
            {
                OfficeId = reader.GetInt32(0),
                OfficeCode = reader.GetString(1),
                OfficeName = reader.GetString(2),
                OfficeType = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Status = reader.GetString(4)
            };
        }

        return null;
    }

    public void UpdateOffice(Office office)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            UPDATE offices
            SET office_name = @name,
                office_type = @type,
                status = @status,
                updated_at = CURRENT_TIMESTAMP
            WHERE office_id = @id";

        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", office.OfficeId);
        command.Parameters.AddWithValue("@name", office.OfficeName);
        command.Parameters.AddWithValue("@type", office.OfficeType);
        command.Parameters.AddWithValue("@status", office.Status);

        command.ExecuteNonQuery();
    }
}