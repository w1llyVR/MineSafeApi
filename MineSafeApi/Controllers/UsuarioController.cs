using Application.Dto.Usuario;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Swashbuckle.AspNetCore.Annotations;

namespace MineSafeApi.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Permite iniciar sesión con credenciales válidas.
        /// </summary>
        /// <param name="request">Credenciales del usuario</param>
        /// <returns>Datos del usuario y token JWT</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Iniciar sesión", Description = "Permite al usuario autenticarse y obtener un token JWT.")]
        [ProducesResponseType(typeof(Response<UsuarioLoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _usuarioService.Login( email, password);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="request">Datos del nuevo usuario</param>
        /// <returns>Resultado del registro</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registrar usuario", Description = "Permite registrar un nuevo usuario.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UsuarioRequestDto request)
        {
            var result = await _usuarioService.CreateAsync(request);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Cambia la contraseña de un usuario.
        /// </summary>
        /// <param name="request">Datos para la verificación y nueva contraseña</param>
        /// <returns>Resultado de la operación</returns>
        [AllowAnonymous]
        [HttpPost("change-password")]
        [SwaggerOperation(Summary = "Cambiar contraseña", Description = "Permite cambiar la contraseña de un usuario usando código de verificación.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(int codigoVerificacion, string email, string newPassword)
        {
            var result = await _usuarioService.ChangePasswordAsync(codigoVerificacion, email, newPassword);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result);
            return Ok(result);
        }

    
    }
}
