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
    public class PermsDao : IPermsDao
    {
        private readonly MasterDao _masterDao;

        public PermsDao(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

        public async Task<List<Perms>> GetPerms(int _idRol)
        {
            using (var connection = _masterDao.GetConnection())
            {
                var result = await connection.QueryAsync<Perms>("MUNI_PA_GETPERMS", new
                {
                    IdRol = _idRol
                }, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
