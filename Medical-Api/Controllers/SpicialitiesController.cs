using M_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpicialitiesController : ControllerBase
    {
        private readonly ISpecialitiesService _servicesService;

        public SpicialitiesController(ISpecialitiesService servicesService)
        {
            _servicesService = servicesService;
        }

        //[HttpPost("AddService")]
        //public async Task<IActionResult> AddService()
        //{
        //    return Ok(await _servicesService.AddService(dto));
        //}
    }
}
