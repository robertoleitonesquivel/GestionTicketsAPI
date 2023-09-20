﻿using Microsoft.AspNetCore.Authorization;
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

                if (response.Data.Rols is null || response.Data.Rols.Count == 0)
                {
                    return NotFound("No posee ningún rol asignado, por favor contactar al administradosr del sistema para que le asigne un rol.");
                }

                return Ok(response);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send(SendEmailRecoverPasswordDTO recoverPassword)
        {
            try
            {
                var response = await _loginService.Send(recoverPassword);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordDTO recoverPassword)
        {
            try
            {
                var response = await _loginService.RecoverPassword(recoverPassword);

                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("VerifyToken")]
        public async Task<ActionResult<ApiResponse<bool>>> VerifyToken(string token)
        {
            try
            {
                var response = await this._loginService.VerifyToken(token);

                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

    }
}
