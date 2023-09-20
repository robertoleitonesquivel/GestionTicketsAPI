using Dapper;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO
{
    public class LoginDao : ILoginDao
    {

        private readonly MasterDao _masterDao;

        public LoginDao(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

        public async Task<Employees> SignIn(string _email, string _password)
        {
            using (var connection = this._masterDao.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Employees>("MUNI_PA_SIGNIN", new
                {
                    Email = _email,
                    Password = _password
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SaveToken(string _token, string _email)
        {
            using (var connection = this._masterDao.GetConnection())
            {
                await connection.ExecuteAsync("MUNI_PA_SAVETOKEN", new
                {
                    Email = _email,
                    Token = _token
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task RecoverPassword(Employees _employees)
        {
            using (var connection = _masterDao.GetConnection())
            {
                await connection.ExecuteAsync("MUNI_PA_UPDATEPASSWORD", new
                {
                    Id = _employees.Id, 
                    Password = _employees.Password,
                    Token = _employees.Token
                }, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<VerifyTokenDTO> VerifyToken(string _token)
        {
            using (var connection = _masterDao.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<VerifyTokenDTO>("MUNI_PA_VERIFYTOKEN", new
                {
                    Token = _token
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Rols>> GetRols(int _idEmployee)
        {
            using (var connection = _masterDao.GetConnection())
            {
                var result = await connection.QueryAsync<Rols>("MUNI_PA_GETROLS", new
                {
                    IdEmployee = _idEmployee              
                }, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
