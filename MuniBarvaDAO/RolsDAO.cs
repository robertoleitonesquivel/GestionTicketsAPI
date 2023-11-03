using Dapper;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO
{
    public class RolsDAO : IRoleDAO
    {
        private readonly MasterDao _masterDao;

        public RolsDAO(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

        public async Task<List<Rols>> GetRols(int _idEmpleado)
        {
            using (var con = _masterDao.GetConnection())
            {
                var result = await con.QueryAsync<Rols>(
                    "MUNI_PA_GETROLS", new
                    {
                        IdEmployee = _idEmpleado
                    },
                    commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
