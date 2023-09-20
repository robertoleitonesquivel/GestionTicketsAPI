using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermsController : ControllerBase
    {
        private readonly IPermsService _permsService;

        public PermsController(IPermsService permsService)
        {
            _permsService = permsService;
        }

        [HttpGet("GetPerms")]
        public async Task<IActionResult> GetPerms(int IdRol)
        {
            try
            {
                var response = await _permsService.GetPerms(IdRol);
              
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
