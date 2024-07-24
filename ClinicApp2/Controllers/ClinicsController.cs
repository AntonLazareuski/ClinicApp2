using ClinicApp2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class ClinicsController: ControllerBase  
    {
        private readonly ILogger<ClinicsController> _logger;
        private readonly IClinicsService _clinicsService;

        public ClinicsController(ILogger<ClinicsController> logger,IClinicsService clinicsService)
        {
            _logger = logger;   
            _clinicsService = clinicsService;
        }

        [HttpGet("{idClinic}")]
        public async Task<IActionResult> GetClinic(int idClinic, [FromQuery] string[] columns)
        {
            _logger.LogInformation("Getting clinic");

            try
            {
                var clinicData = await _clinicsService.GetClinic(idClinic, columns);
                
                return Ok(JsonConvert.SerializeObject(clinicData));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the list of clinics");
                return StatusCode(500, "An error occurred");
            }

            
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
