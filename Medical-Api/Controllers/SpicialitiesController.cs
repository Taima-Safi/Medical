using M_Core.Dtos.DoctorDto;
using M_Core.Dtos.ServiceDto;
using M_Core.Dtos.SpecialityDto;
using M_Core.UserStatics;
using M_Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class SpicialitiesController : ControllerBase
    {
        private readonly ISpecialitiesService _specialitiesService;
        private readonly IServicesService _servicesService;
        private readonly IDoctorsService _doctorsService;

        public SpicialitiesController(ISpecialitiesService specialitiesService, IServicesService servicesService, IDoctorsService doctorsService)
        {
            _specialitiesService = specialitiesService;
            _servicesService = servicesService;
            _doctorsService = doctorsService;
        }


        //Speciality EndPoints

        [HttpPost("AddSpeciality")]
        public async Task<IActionResult> AddSpeciality([FromForm] SpecialityDto dto)
        {
            return Ok(await _specialitiesService.AddSpeciality(dto));
        }

        [HttpDelete("DeleteSpeciality")]
        public async Task<IActionResult> DeleteSpeciality(int specialityId)
        {
            return Ok(await _specialitiesService.DeleteSpeciality(specialityId));
        }

        [HttpPut("EditSpeciality")]
        public async Task<IActionResult> EditSpeciality([FromForm] EditSpecialityDto dto)
        {
            return Ok(await _specialitiesService.EditSpeciality(dto));
        }

        //Service EndPoints

        [HttpPost("AddService")]
        public async Task<IActionResult> AddService([FromForm] ServiceDto dto)
        {
            return Ok(await _servicesService.AddService(dto));
        }

        [HttpDelete("DeleteService")]
        public async Task<IActionResult> DeleteService(int ServiceId)
        {
            return Ok(await _servicesService.DeleteService(ServiceId));
        }

        [HttpPut("EditService")]
        public async Task<IActionResult> EditService([FromForm] EditServiceDto dto)
        {
            return Ok(await _servicesService.EditService(dto));
        }

        //Doctor Endpoints

        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorDto dto)
        {
            return Ok(await _doctorsService.AddDoctor(dto));
        }
        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor(int DoctorId)
        {
            return Ok(await _doctorsService.DeleteDoctor(DoctorId));
        }

        [HttpPut("EditDoctor")]
        public async Task<IActionResult> EditDoctor([FromForm] EditDoctorDto dto)
        {
            return Ok(await _doctorsService.EditDoctor(dto));
        }
    }
}
