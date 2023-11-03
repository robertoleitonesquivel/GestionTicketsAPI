using MuniBarva.MODELS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface IPermsService
    {
        Task<List<PermsDTO>> GetPerms(int _IdRol);
    }
}
