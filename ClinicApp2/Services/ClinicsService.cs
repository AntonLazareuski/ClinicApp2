using ClickHouse.Client;
using ClickHouse.Client.ADO;
using ClinicApp2.Models;
using ClinicApp2.Services.Interfaces;
using System.Data;

namespace ClinicApp2.Services
{
    public class ClinicsService: IClinicsService
    {
        private readonly string _connectionString;

        public ClinicsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ClickHouseConnection");
        }

        public async Task<Dictionary<string, object>> GetClinic(int idClinic, string[] columns)
        {
            using (var connection = new ClickHouseConnection(_connectionString))
            {
                await connection.OpenAsync();

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

        public async Task<List<ClinicModel>> GetClinics(int page, int pageSize, string[] columns)
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
                    var clinics = new List<ClinicModel>();

                    while (reader.Read())
                    {
                        var clinic = new ClinicModel
                        {
                            Clinic_id = reader.GetInt32("Clinic_id"),
                            Clinic_name = reader.GetString("Clinic_name")
                        };

                        clinics.Add(clinic);
                    }

                    return clinics;
                }
            }
        }
    }
}
