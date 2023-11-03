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
    public class PermsService : IPermsService
    {
        private readonly IPermsDAO _permsDAO;

        public PermsService(IPermsDAO permsDAO)
        {
            _permsDAO = permsDAO;
        }

        public async Task<List<PermsDTO>> GetPerms(int _IdRol)
        {
            var result = await _permsDAO.GetPerms(_IdRol);

            return result.Adapt<List<PermsDTO>>();
        }
    }
}
