using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetMenus")]
        public async Task<ActionResult<ApiResponse<List<MenuDTO>>>> GetMenus(int IdRol)
        {
            try
            {
                var result = await _menuService.GetMenus(IdRol);

                return Ok(new ApiResponse<List<MenuDTO>>
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
