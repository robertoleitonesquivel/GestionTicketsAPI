using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface IPermsService
    {
        Task<ApiResponse<List<PermsDTO>>> GetPerms(int _idRol);
    }
}
