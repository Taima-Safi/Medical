using M_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ISpecialitiesService _specialitiesService;
        private readonly IServicesService _servicesService;
        private readonly IDoctorsService _doctorsService;

        public ClientsController(ISpecialitiesService specialitiesService, IServicesService servicesService, IDoctorsService doctorsService)
        {
            _specialitiesService = specialitiesService;
            _servicesService = servicesService;
            _doctorsService = doctorsService;
        }

        //speciality Endpoints

        [HttpGet("GetSpecialities")]
        public async Task<IActionResult> GetSpecialities()
        {
            return Ok(await _specialitiesService.GetAllSpecialities());
        }
        [HttpGet("GetSpecialitiesCount")]
        public async Task<IActionResult> GetSpecialitiesCount()
        {
            return Ok(await _specialitiesService.GetSpecialitiesCount());
        }

        //Service Endpoints




        //Doctor Endpoints
        [HttpGet("DoctorsNumberForSpeciality")]
        public async Task<IActionResult> DoctorsForSpeciality(int specialityId)
        {
            return Ok(await _doctorsService.DoctorsForSpeciality(specialityId));
        }
    }
}
