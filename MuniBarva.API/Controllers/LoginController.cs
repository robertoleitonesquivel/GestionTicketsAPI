using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("SignIn")]
        public async Task<ActionResult<ApiResponse<EmployeesDTO>>> SignIn(string email, string password)
        {
            try
            {
                var response = await this._loginService.SignIn(email, password);

                if (response is null)
                {
                    return NotFound("Credenciales incorrectos.");
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send(SendEmailDTO senEmailDTO)
        {
            try
            {
                var response = await this._loginService.Send(senEmailDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordDTO recoverPasswordDTO)
        {
            try
            {
                var response = await _loginService.RecoverPassword(recoverPasswordDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("VerifyToken")]
        public async Task<ActionResult<ApiResponse<VerifyTokenDTO>>> VerifyToken(string _token)
        {
            try
            {
                var response = await this._loginService.VerifyToken(_token);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
