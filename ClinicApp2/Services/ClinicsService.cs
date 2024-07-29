using ClickHouse.Client.ADO;
using ClinicApp2.Services.Interfaces;
using System.Data;

namespace ClinicApp2.Services
{
    public class ClinicsService: IClinicsService
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public ClinicsService(ILogger<ClinicsService> logger, IConfiguration configuration)
        {
            _logger = logger;   
            _connectionString = configuration.GetConnectionString("ClickHouseConnection");
        }

        public async Task<Dictionary<string, object>> GetClinic(int idClinic, string[] columns)
        {
            using (var connection = new ClickHouseConnection(_connectionString))
            {
                await connection.OpenAsync();

                _logger.LogInformation("Getting clinic with ID: {Id}", idClinic);

                var selectColumns = columns?.Length > 0 ? string.Join(",", columns) : "*";
                var commandText = $"SELECT {selectColumns} FROM Clinics WHERE Clinic_id = {idClinic}";

                var command = connection.CreateCommand();
                command.CommandText = commandText;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new Dictionary<string, object>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            result[columnName] = columnValue;
                        }

                        return result;
                    }
                }
            }

            return null;
        }

        public async Task<List<Dictionary<string, object>>> GetClinics(int page, int pageSize, string[] columns)
        {
            using (var connection = new ClickHouseConnection(_connectionString))
            {
                await connection.OpenAsync();

                var selectColumns = columns?.Length > 0 ? string.Join(",", columns) : "*";
                var offset = (page - 1) * pageSize;
                var commandText = $"SELECT {selectColumns} FROM Clinics LIMIT {pageSize} OFFSET {offset}";

                var command = connection.CreateCommand();
                command.CommandText = commandText;

                using (var reader = command.ExecuteReader())
                {
                    var clinics = new List<Dictionary<string, object>>();

                    while (reader.Read())
                    {
                        var clinic = new Dictionary<string, object>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            clinic[columnName] = columnValue;
                        }

                        clinics.Add(clinic);
                    }

                    return clinics;
                }
            }
        }
    }
}
