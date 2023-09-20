using MuniBarva.DAO;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;
using Mapster;
using MuniBarva.MODELS;

namespace MuniBarva.SERVICES
{
    public class MenusService : IMenusService
    {
        private readonly IMenusDao _menusDao;

        public MenusService(IMenusDao menusDao)
        {
            _menusDao = menusDao;
        }

        public async Task<ApiResponse<List<MenusDTO>>> GetMenus(int _idRol)
        {
            var menus = await _menusDao.GetMenus(_idRol);

            var result = menus.Adapt<List<MenusDTO>>();

            return new ApiResponse<List<MenusDTO>> { Data = result };

        }
    }
}
