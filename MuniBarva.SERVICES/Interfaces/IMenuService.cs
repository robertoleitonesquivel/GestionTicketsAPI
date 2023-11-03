using MuniBarva.MODELS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetMenus(int _IdRol);
    }
}
