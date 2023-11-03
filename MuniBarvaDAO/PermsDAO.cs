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
    public class PermsDAO : IPermsDAO
    {
        private readonly MasterDao _masterDao;

        public PermsDAO(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

        public async Task<List<Perms>> GetPerms(int _idRol)
        {
            using (var con = _masterDao.GetConnection())
            {
                var result = await con.QueryAsync<Perms>(
                    "MUNI_PA_GETPERMS", new
                    {
                        IdRol = _idRol
                    },
                    commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
