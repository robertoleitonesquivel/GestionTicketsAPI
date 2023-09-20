using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenusService _menusService;

        public MenusController(IMenusService menusService)
        {
            _menusService = menusService;
        }

        [HttpGet("GetMenus")]
        public async Task<ActionResult<ApiResponse<bool>>> GetMenus(int IdRol)
        {
            try
            {
                var response = await this._menusService.GetMenus(IdRol);

                if (response is null || response.Data.Count == 0)
                {
                    return NotFound("El rol seleccionado no posee ningún menu asociado, por favor contactar al administrador para que realize la debida asignación.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
