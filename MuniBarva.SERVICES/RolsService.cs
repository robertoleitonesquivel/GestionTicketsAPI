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
    public class RolsService : IRolsService
    {
        private readonly IRoleDAO _roleDAO;

        public RolsService(IRoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }

        public async Task<List<RolsDTO>> GetRols(int _idEmpleado)
        {
            var result = await _roleDAO.GetRols(_idEmpleado);

            return result.Adapt<List<RolsDTO>>();
        }
    }
}
