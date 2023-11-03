using Mapster;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES
{
    public class MenusService : IMenuService
    {

        private readonly IMenuDao _menuDAO;

        public MenusService(IMenuDao menuDao)
        {
            _menuDAO = menuDao;
        }

        public async Task<List<MenuDTO>> GetMenus(int _IdRol)
        {
            var result = await _menuDAO.GetMenus(_IdRol);

            return result.Adapt<List<MenuDTO>>();
        }
    }
}
