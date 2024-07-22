using ClinicApp2.Models;

namespace ClinicApp2.Services.Interfaces
{
    public interface IClinicsService
    {
        Task<Dictionary<string, object>> GetClinic(int idClinic, string[] columns);
        Task<List<ClinicModel>> GetClinics(int page, int pageSize, string[] columns);
    }
}
