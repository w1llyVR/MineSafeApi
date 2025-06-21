using Application.Dto;
using Application.Dto.TipoCondicion;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Swashbuckle.AspNetCore.Annotations;

namespace MineSafeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [SwaggerTag("Gestión de Tipos de Condición")]
    [Tags("TipoCondicion")] 
    public class TipoCondicionController : Controller
    {
        private readonly ITipoCondicionService _tipoCondicionService;
        
        public TipoCondicionController(ITipoCondicionService tipoCondicionService)
        {
            this._tipoCondicionService = tipoCondicionService;
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(
        Summary = "Listar todos los tipos de condición",
        Description = "Retorna todos los registros disponibles en el catálogo de tipos de condición"
    )]
        [SwaggerResponse(200, "Lista obtenida correctamente", typeof(ResultDto<List<TipoCondicionResponseDto>>))]
        [SwaggerResponse(500, "Error interno del servidor")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _tipoCondicionService.GetAllAsync();
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result.Msj);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Obtener un tipo de condición por ID",
        Description = "Devuelve el detalle del tipo de condición correspondiente al ID proporcionado"
    )]
        [SwaggerResponse(200, "Tipo de condición encontrado", typeof(ResultDto<TipoCondicionResponseDto>))]
        [SwaggerResponse(404, "No se encontró el tipo de condición con ese ID")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _tipoCondicionService.GetByIdAsync(id);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result.Msj);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
        Summary = "Registrar un nuevo tipo de condición",
        Description = "Permite registrar un nuevo tipo de condición en el sistema"
    )]
        [SwaggerResponse(200, "Registro exitoso")]
        [SwaggerResponse(400, "Error en la solicitud o datos inválidos")]
        public async Task<IActionResult> Create([FromBody] TipoCondicionRequestDto_Create request)
        {
            var result = await _tipoCondicionService.CreateAsync(request);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result.Msj);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("update")]
        [SwaggerOperation(
        Summary = "Actualizar tipo de condición",
        Description = "Permite modificar un tipo de condición existente"
    )]
        [SwaggerResponse(200, "Actualización exitosa")]
        [SwaggerResponse(400, "Error en los datos o validación fallida")]
        public async Task<IActionResult> Update([FromBody] TipoCondicionRequestDto_Update request)
        {
            var result = await _tipoCondicionService.UpdateAsync(request);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result.Msj);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("{id}")]
        [SwaggerOperation(
        Summary = "Eliminar un tipo de condición",
        Description = "Elimina el tipo de condición con el ID especificado"
    )]
        [SwaggerResponse(200, "Eliminado exitosamente")]
        [SwaggerResponse(404, "No se encontró el tipo de condición para eliminar")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoCondicionService.DeleteAsync(id);
            if (result.CodeError != HttpErrorCode.Success)
                return BadRequest(result.Msj);

            return Ok(result);
        }

    }
}
