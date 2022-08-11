using ApiRestCrossover.Interfaces;
using ApiRestCrossover.Models;
using ApiRestCrossover.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestCrossover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IApiGitService _apiGitService;

        public RepositoryController(IApiGitService apiGitService)
        {
            _apiGitService = apiGitService;
        }
        // GET: api/<UserController>
        [HttpGet("GetRepository")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userList = await _apiGitService.Repository();
                if(userList!=null && userList.Count>0)
                    return Ok(userList);
                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                return NotFound(App_GlobalResources.ERROR_API + " " + ex.Message);
            }
        }
    }
}
