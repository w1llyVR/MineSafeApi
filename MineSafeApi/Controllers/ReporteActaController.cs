using Application.Dto.ReporteActa;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Swashbuckle.AspNetCore.Annotations;

namespace MineSafeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteActaController : Controller
    {
        private readonly IReporteActaService _reporteActaService;

        public ReporteActaController(IReporteActaService reporteActaService)
        {
            _reporteActaService = reporteActaService;
        }



        /// <summary>
        /// Lista todos los reportes de actas registrados.
        /// </summary>
        //[Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los reportes de actas", Description = "Retorna la lista completa de reportes de actas.")]
        [ProducesResponseType(typeof(Response<IEnumerable<ReporteActaResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _reporteActaService.GetAllAsync();
            return StatusCode((int)result.CodeError, result);
        }

        /// <summary>
        /// Obtiene un reporte de acta específico por su ID.
        /// </summary>
        //[Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener un reporte de acta por ID", Description = "Retorna un reporte específico por su identificador.")]
        [ProducesResponseType(typeof(Response<ReporteActaResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ReporteActaResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _reporteActaService.GetByIdAsync(id);
            return StatusCode((int)result.CodeError, result);
        }

        /// <summary>
        /// Registra un nuevo reporte de acta.
        /// </summary>
        //[Authorize]
        [HttpPost]
        [SwaggerOperation(Summary = "Crear un nuevo reporte de acta", Description = "Registra un nuevo reporte de acta en el sistema.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ReporteActaRequestDto_Create request)
        {
            var result = await _reporteActaService.CreateAsync(request);
            return StatusCode((int)result.CodeError, result);
        }

        /// <summary>
        /// Actualiza un reporte de acta existente.
        /// </summary>
        //[Authorize]
        [HttpPost("update")]
        [SwaggerOperation(Summary = "Actualizar un reporte de acta", Description = "Modifica los datos de un reporte ya existente.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] ReporteActaRequestDto_Update request)
        {
            var result = await _reporteActaService.UpdateAsync(request);
            return StatusCode((int)result.CodeError, result);
        }

        /// <summary>
        /// Elimina un reporte de acta por ID.
        /// </summary>
        //[Authorize]
        [HttpPost("{id}")]
        [SwaggerOperation(Summary = "Eliminar un reporte de acta", Description = "Elimina un reporte de acta específico por su ID.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reporteActaService.DeleteAsync(id);
            return StatusCode((int)result.CodeError, result);
        }
    }
}
