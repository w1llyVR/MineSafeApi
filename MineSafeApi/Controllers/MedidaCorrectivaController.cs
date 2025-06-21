using Application.Dto.MedidaCorrectiva;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Swashbuckle.AspNetCore.Annotations;

namespace MineSafeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedidaCorrectivaController : Controller
    {
        private readonly IMedidaCorrectivaService _medidaCorrectivaService;

        public MedidaCorrectivaController(IMedidaCorrectivaService medidaCorrectivaService)
        {
            _medidaCorrectivaService = medidaCorrectivaService;
        }

        //[Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todas las medidas correctivas", Description = "Devuelve una lista completa de medidas correctivas registradas.")]
        [ProducesResponseType(typeof(Response<IEnumerable<MedidaCorrectivaResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _medidaCorrectivaService.GetAllAsync();
            return StatusCode((int)result.CodeError, result);
        }

        ////[Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener medida correctiva por ID", Description = "Devuelve una medida correctiva específica según su identificador.")]
        [ProducesResponseType(typeof(Response<MedidaCorrectivaResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<MedidaCorrectivaResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _medidaCorrectivaService.GetByIdAsync(id);
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpPost]
        [SwaggerOperation(Summary = "Crear medida correctiva", Description = "Registra una nueva medida correctiva en el sistema.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MedidaCorrectivaRequestDto_Create request)
        {
            var result = await _medidaCorrectivaService.CreateAsync(request);
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpPost("update")]
        [SwaggerOperation(Summary = "Actualizar medida correctiva", Description = "Modifica los datos de una medida correctiva registrada.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] MedidaCorrectivaRequestDto_Update request)
        {
            var result = await _medidaCorrectivaService.UpdateAsync(request);
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpPost("{id}")]
        [SwaggerOperation(Summary = "Eliminar medida correctiva", Description = "Elimina una medida correctiva específica del sistema.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medidaCorrectivaService.DeleteAsync(id);
            return StatusCode((int)result.CodeError, result);
        }
    }
}
