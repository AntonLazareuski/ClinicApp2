using ClickHouse.Client;
using ClickHouse.Client.ADO;
using ClinicApp2.Services.Interfaces;
using System.Data.SqlClient;

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

                var selectColumns = string.Join(",", columns);

                if (string.IsNullOrEmpty(selectColumns))
                    selectColumns = "*";

                var commandText = $"SELECT {selectColumns} FROM Clinics WHERE Clinic_id = {idClinic}";
                SqlCommand command = new SqlCommand();
                command.CommandText = commandText;
 
                var parameter = command.CreateParameter();
                parameter.Value = idClinic;
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
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

       /* public object GetClinics(int page, string[] columns)
        {
            using (var connection = new ClickHouseConnection(_connectionString))
            {
                var selectColumns = string.Join(",", columns);

                if (string.IsNullOrEmpty(selectColumns))
                    selectColumns = "*";

                var commandText = $"SELECT {selectColumns} FROM Clinics";
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

                    var pageCount = Math.Ceiling((double)clinics.Count / 10);
                    {
                        Page = page,
                        Pages = pageCount,
                        Clinics = clinics.Skip((page - 1) * 10).Take(10)
                    };

                    return result;
                }
            }
        } */     
    }
}
