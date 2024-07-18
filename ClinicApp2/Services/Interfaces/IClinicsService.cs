namespace ClinicApp2.Services.Interfaces
{
    public interface IClinicsService
    {
        Task<Dictionary<string, object>> GetClinic(int idClinic, string[] columns);
        object GetClinics(int page, string[] columns);
    }
}
