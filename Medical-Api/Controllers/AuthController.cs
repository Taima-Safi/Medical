﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using M_Services.IServices;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
    }
}
