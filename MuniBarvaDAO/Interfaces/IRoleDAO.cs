using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO.Interfaces
{
    public interface IRoleDAO
    {
        Task<List<Rols>> GetRols(int _idEmpleado);
    }
}
