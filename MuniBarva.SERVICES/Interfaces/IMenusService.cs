using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface IMenusService
    {
        Task<ApiResponse<List<MenusDTO>>> GetMenus(int _idRol);
    }
}
