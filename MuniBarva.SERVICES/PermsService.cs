using MuniBarva.DAO;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using MuniBarva.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MuniBarva.MODELS;

namespace MuniBarva.SERVICES
{
    public class PermsService : IPermsService
    {
        private readonly IPermsDao _permsDao;

        public PermsService(IPermsDao permsDao)
        {
            _permsDao = permsDao;
        }

        public async Task<ApiResponse<List<PermsDTO>>> GetPerms(int _idRol)
        {
            var perms = await _permsDao.GetPerms(_idRol);

            var result = perms.Adapt<List<PermsDTO>>();

            return new ApiResponse<List<PermsDTO>> { Data = result };
        }
    }
}
