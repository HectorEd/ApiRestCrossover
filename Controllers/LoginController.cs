using ApiRestCrossover.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestCrossover.Resources;
using Microsoft.AspNetCore.Authentication;
using ApiRestCrossover.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestCrossover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private IConfiguration _config;
        private readonly IApiAuthService _apiService;

        public LoginController(IApiAuthService apiService)
        {
            _apiService = apiService;
        }

        // GET: api/<LoginController>
        [HttpGet("GetAuthentication")]
        public async Task<IActionResult> Get()
        {
            try
            {
                UserModel user = await _apiService.User();
                return Ok(user);
            }
            catch(Exception ex)
            {
                return NotFound(App_GlobalResources.USER_PASS_WRONG + " | "+ ex.Message);
            }
        }
    }
}
