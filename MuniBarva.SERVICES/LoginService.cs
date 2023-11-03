using Mapster;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using MuniBarva.COMMON.Interfaces;
using MuniBarva.MODELS;
using MuniBarva.COMMON;
using MuniBarva.MODELS.Models;

namespace MuniBarva.SERVICES
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDao _loginDAO;
        private readonly IConfiguration _config;
        private readonly IEncrypt _encrypt;
        private readonly ISendEmail _sendEmail;
        private readonly ISettingsService _settingsService;
        private readonly IConfigKey _configKey;
        private readonly IRolsService _rolsService;

        public LoginService
                (
                    ILoginDao loginDAO,
                    IConfiguration config,
                    IEncrypt encrypt,
                    ISendEmail sendEmail,
                    ISettingsService settingsService,
                    IConfigKey configKey,
                    IRolsService rolsService    
                )
        {
            _loginDAO = loginDAO;
            _config = config;
            _encrypt = encrypt;
            _sendEmail = sendEmail;
            _settingsService = settingsService;
            _configKey = configKey;
            _rolsService = rolsService;
        }

        public async Task<ApiResponse<string>> RecoverPassword(RecoverPasswordDTO _recoverPasswordDTO)
        {
           _recoverPasswordDTO.Password = await _encrypt.Sha256(_recoverPasswordDTO.Password);

            var result = _recoverPasswordDTO.Adapt<Employees>();

            await _loginDAO.RecoverPassword(result);

            return new ApiResponse<string>
            {
                Message = "Contraseña actualizada con éxito."
            };
        }

        public async Task<ApiResponse<string>> Send(SendEmailDTO _sendEmailDTO)
        {
            var token = await _encrypt.Sha256(Guid.NewGuid().ToString());

            await _loginDAO.SaveToken(token, _sendEmailDTO.Email);

            var settings = await _settingsService.Get(Constants.RecoverPassword);

            string message = settings.Description.Replace("@Email", _sendEmailDTO.Email);

            message = message.Replace("@Token", token);

            message = message.Replace("@Url", await _configKey.GetKeyValue(GeneralConfiguration.URL_UI));

            await _sendEmail.Send(_sendEmailDTO.Email, "Recuperación de contraseña", message);

            return new ApiResponse<string>
            {
                Message = "Ha sido enviado un mensaje a su email con los pasos para restablecer la contaseña, por favor revise su email."
            };

        }

        public async Task<ApiResponse<EmployeesDTO>> SignIn(string _email, string _password)
        {
            _password = await _encrypt.Sha256(_password);

            var oEmployee = await this._loginDAO.SignIn(_email, _password);

            if (oEmployee is null)
            {
                return null;
            }
            else
            {
                var oEmployeesDTO = oEmployee.Adapt<EmployeesDTO>();

                oEmployeesDTO.Rols = await _rolsService.GetRols(oEmployeesDTO.Id);

                oEmployeesDTO.Jwt = await GetJwT(oEmployeesDTO);

                return new ApiResponse<EmployeesDTO> { Data = oEmployeesDTO };
            }
        }

        public async Task<ApiResponse<VerifyTokenDTO>> VerifyToken(string _token)
        {
            var result = await _loginDAO.VerifyToken(_token);

            return new ApiResponse<VerifyTokenDTO> {  Data = result };
        }

        private async Task<string> GetJwT(EmployeesDTO _employeesDTO)
        {
            return await Task.Run(() =>
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Email, _employeesDTO.Email));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:KEY").Value.ToString()));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return token;
            });
        }
    }
}