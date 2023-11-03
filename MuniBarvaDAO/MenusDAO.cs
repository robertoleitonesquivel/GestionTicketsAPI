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
    public class MenusDAO : IMenuDao
    {
        private readonly MasterDao _masterDao;

        public MenusDAO(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

      
      
        public async Task<List<Menus>> GetMenus(int _idRol)
        {
            using (var con = _masterDao.GetConnection())
            {
                var result = await con.QueryAsync<Menus>(
                    "MUNI_PA_GETMENUS", new
                    {
                        IdRol = _idRol
                    },
                    commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
