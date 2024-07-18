using ClinicApp2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class ClinicsController: ControllerBase  
    {
        private readonly IClinicsService _clinicsService;

        public ClinicsController(IClinicsService clinicsService)
        {
            _clinicsService = clinicsService;
        }

        [HttpGet]
        public IActionResult GetClinic([FromQuery] int idClinic, [FromQuery] string[] columns)
        {
            var clinicData = _clinicsService.GetClinic(idClinic, columns);
            if (clinicData == null)
            {
                return NotFound();
            }

            return Ok(clinicData);
        }

       /* [HttpGet]
        public IActionResult GetClinics([FromQuery] int page, [FromQuery] string[] columns)
        {
            var clinicListData = _clinicsService.GetClinics(page, columns);
            if (clinicListData == null)
            {
                return NotFound();
            }

            return Ok(clinicListData);
        }*/
    }
}
