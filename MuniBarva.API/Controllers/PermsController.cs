using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS;
using MuniBarva.SERVICES;
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
        public async Task<ActionResult<ApiResponse<List<PermsDTO>>>> GetPerms(int IdRol)
        {
            try
            {
                var result = await _permsService.GetPerms(IdRol);

                return Ok(new ApiResponse<List<PermsDTO>>
                {
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
