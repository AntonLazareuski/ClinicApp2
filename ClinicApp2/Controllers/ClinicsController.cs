using ClinicApp2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("{idClinic}")]
        public async Task<IActionResult> GetClinic(int idClinic, [FromQuery] string[] columns)
        {
            var clinicData = await _clinicsService.GetClinic(idClinic, columns);
            if (clinicData == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(clinicData));
        }

        [HttpGet]
        public async Task<IActionResult> GetClinics([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string[] columns)
        {
            var clinicListData = await _clinicsService.GetClinics(page, pageSize, columns);
            if (clinicListData == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(clinicListData)); ;
        }
    }
}
