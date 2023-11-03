using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface ILoginService
    {
        Task<ApiResponse<EmployeesDTO>> SignIn(string _email, string _password);
        Task<ApiResponse<string>> Send(SendEmailDTO _sendEmailDTO);
        Task<ApiResponse<VerifyTokenDTO>> VerifyToken(string _token);
        Task<ApiResponse<string>> RecoverPassword(RecoverPasswordDTO _recoverPasswordDTO);
    }
}
