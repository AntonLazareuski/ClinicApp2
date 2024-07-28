using ClinicApp2.Entities;

namespace ClinicApp2.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
