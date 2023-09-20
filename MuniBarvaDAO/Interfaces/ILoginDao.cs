using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO.Interfaces
{
    public interface ILoginDao
    {
        Task<Employees> SignIn(string _email, string _password); 
        Task SaveToken(string _token, string _email);
        Task RecoverPassword(Employees _employees);
        Task<VerifyTokenDTO> VerifyToken(string _token);
        Task<List<Rols>> GetRols(int _idEmployee);
    }
}
