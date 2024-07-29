
namespace ClinicApp2.Services.Interfaces
{
    public interface IClinicsService
    {
        Task<Dictionary<string, object>> GetClinic(int idClinic, string[] columns);
        Task<List<Dictionary<string, object>>> GetClinics(int page, int pageSize, string[] columns);
    }
}
